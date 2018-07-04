// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationGenerator
    {
        private static readonly Regex _newLineWithWhitespaceRegex = new Regex(@"\r?\n\s*");
        private ImmutableArray<ITypeSymbol> _typeSymbols;
        private ImmutableArray<IMethodSymbol> _extensionMethodSymbols;

        private readonly Dictionary<ISymbol, SymbolDocumentationInfo> _symbolDocumentationCache;
        private readonly Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public DocumentationGenerator(ImmutableArray<DocumentationSource> sources, SymbolDisplayFormatProvider formatProvider = null)
        {
            Sources = sources;

            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;

            _xmlDocumentations = Sources.ToDictionary(f => f.AssemblySymbol, f => f.GetXmlDocumentation());

            _symbolDocumentationCache = new Dictionary<ISymbol, SymbolDocumentationInfo>();
        }

        public ImmutableArray<ITypeSymbol> TypeSymbols
        {
            get
            {
                if (_typeSymbols.IsDefault)
                {
                    _typeSymbols = Sources
                        .SelectMany(f => f.AssemblySymbol.GetPubliclyVisibleTypes())
                        .ToImmutableArray();
                }

                return _typeSymbols;
            }
        }

        public ImmutableArray<IMethodSymbol> ExtensionMethodSymbols
        {
            get
            {
                if (_extensionMethodSymbols.IsDefault)
                {
                    _extensionMethodSymbols = TypeSymbols
                        .Where(f => f.TypeKind == TypeKind.Class
                            && f.IsStatic
                            && f.ContainingType == null)
                        .SelectMany(f => f.GetMembers())
                        .Where(f => f.Kind == SymbolKind.Method
                            && f.IsStatic
                            && f.IsPubliclyVisible())
                        .Cast<IMethodSymbol>()
                        .Where(f => f.IsExtensionMethod)
                        .ToImmutableArray();
                }

                return _extensionMethodSymbols;
            }
        }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        public ImmutableArray<DocumentationSource> Sources { get; }

        public string GenerateRootDocument(string heading)
        {
            using (MarkdownWriter writer = MarkdownWriter.Create(new StringBuilder()))
            {
                writer.WriteHeading1(heading);

                foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in TypeSymbols
                    .GroupBy(f => f.ContainingNamespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    INamespaceSymbol namespaceSymbol = grouping.Key;

                    writer.WriteStartHeading(2);

                    SymbolDocumentationInfo info = GetDocumentationInfo(namespaceSymbol);

                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), string.Join("/", info.Names.Reverse()) + "/README.md");
                    writer.WriteString(" Namespace");
                    writer.WriteEndHeading();

                    WriteNamespaceContent(writer, grouping, 3, null);
                }

                return writer.ToString();
            }
        }

        public string GenerateNamespaceDocument(INamespaceSymbol namespaceSymbol)
        {
            using (MarkdownWriter writer = MarkdownWriter.Create(new StringBuilder()))
            {
                writer.WriteStartHeading(1);
                writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
                writer.WriteString(" Namespace");
                writer.WriteEndHeading();

                SymbolDocumentationInfo info = GetDocumentationInfo(namespaceSymbol);

                WriteNamespaceContent(writer, TypeSymbols.Where(f => f.ContainingNamespace == namespaceSymbol), 2, info);

                return writer.ToString();
            }
        }

        public string GenerateTypeDocument(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = GetDocumentationInfo(typeSymbol);

            using (var writer = new TypeDocumentationMarkdownWriter(typeSymbol, info, this))
            {
                writer.WriteTitle(typeSymbol);
                writer.WriteNamespace(typeSymbol);
                writer.WriteAssembly(typeSymbol);

                if (typeSymbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    writer.WriteObsolete(typeSymbol);

                writer.WriteSummary(typeSymbol);
                writer.WriteTypeParameters(typeSymbol);
                writer.WriteParameters(typeSymbol);
                writer.WriteReturnValue(typeSymbol);
                writer.WriteInheritance(typeSymbol);
                writer.WriteAttributes(typeSymbol);
                writer.WriteDerived(typeSymbol);
                writer.WriteImplements(typeSymbol);
                writer.WriteExamples(typeSymbol);
                writer.WriteRemarks(typeSymbol);

                if (typeSymbol.TypeKind == TypeKind.Enum)
                    writer.WriteEnumFields(info.GetFields());

                writer.WriteConstructors(info.GetConstructors());

                if (typeSymbol.TypeKind != TypeKind.Enum)
                    writer.WriteFields(info.GetFields());

                writer.WriteProperties(info.GetProperties());
                writer.WriteMethods(info.GetMethods());
                writer.WriteOperators(info.GetOperators());
                writer.WriteEvents(info.GetEvents());
                writer.WriteExplicitInterfaceImplementations(info.GetExplicitInterfaceImplementations());
                writer.WriteExtensionMethods(typeSymbol);
                writer.WriteSeeAlso(typeSymbol);

                return writer.ToString();
            }
        }

        internal SymbolDocumentationInfo GetDocumentationInfo(ISymbol symbol)
        {
            if (_symbolDocumentationCache.TryGetValue(symbol, out SymbolDocumentationInfo info))
                return info;

            info = SymbolDocumentationInfo.Create(symbol, isExternal: !Sources.Any(f => f.AssemblySymbol == symbol.ContainingAssembly));

            _symbolDocumentationCache[symbol] = info;
            return info;
        }

        internal XmlDocumentation GetXmlDocumentation(IAssemblySymbol assemblySymbol)
        {
            if (!_xmlDocumentations.TryGetValue(assemblySymbol, out XmlDocumentation xmlDocumentation))
            {
                //TODO: find xml documentation file for an assembly

                string assemblyFileName = assemblySymbol.Name + ".dll";

                if (RuntimeMetadataReference.TrustedPlatformAssemblyPaths.TryGetValue(assemblyFileName, out string path))
                {
                    string xmlDocPath = Path.ChangeExtension(path, "xml");

                    if (File.Exists(xmlDocPath))
                    {
                        xmlDocumentation = XmlDocumentation.Load(path);
                        _xmlDocumentations[assemblySymbol] = xmlDocumentation;
                    }
                }
            }

            return xmlDocumentation;
        }

        internal XElement GetDocumentationElement(ISymbol symbol)
        {
            return GetXmlDocumentation(symbol.ContainingAssembly)?.GetElement(GetDocumentationInfo(symbol).CommentId);
        }

        internal XElement GetDocumentationElement(ISymbol symbol, string name)
        {
            return GetXmlDocumentation(symbol.ContainingAssembly)?.GetElement(GetDocumentationInfo(symbol).CommentId, name);
        }

        internal void WriteTable(
            MarkdownWriter writer,
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format,
            SymbolDocumentationInfo directoryInfo)
        {
            using (IEnumerator<(ISymbol symbol, string displayString)> en = symbols
                .Select(f => (symbol: f, displayString: f.ToDisplayString(format)))
                .OrderBy(f => f.displayString)
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    writer.WriteHeading(headingLevel, heading);

                    writer.WriteStartTable(2);
                    writer.WriteStartTableRow();
                    writer.WriteStartTableCell();
                    writer.WriteString(header1);
                    writer.WriteEndTableCell();
                    writer.WriteStartTableCell();
                    writer.WriteString(header2);
                    writer.WriteEndTableCell();
                    writer.WriteEndTableRow();
                    writer.WriteTableHeaderSeparator();

                    do
                    {
                        ISymbol symbol = en.Current.symbol;

                        writer.WriteStartTableRow();
                        writer.WriteStartTableCell();

                        SymbolDocumentationInfo info = GetDocumentationInfo(symbol);

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            writer.WriteString(en.Current.displayString);
                        }
                        else
                        {
                            writer.WriteLink(info, directoryInfo, format);
                        }

                        writer.WriteEndTableCell();
                        writer.WriteStartTableCell();

                        //TODO: write element content
                        string s = GetXmlDocumentation(symbol.ContainingAssembly)?
                            .GetElement(info.CommentId, "summary")?
                            .Value;

                        if (s != null)
                        {
                            s = s.Trim();
                            s = _newLineWithWhitespaceRegex.Replace(s, " ");
                        }

                        writer.WriteString(s);
                        writer.WriteEndTableCell();
                        writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    writer.WriteEndTable();
                }
            }
        }

        private void WriteNamespaceContent(
            MarkdownWriter writer,
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel,
            SymbolDocumentationInfo directoryInfo)
        {
            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                .GroupBy(f => f.TypeKind)
                .OrderBy(f => f.Key, TypeKindComparer.Instance))
            {
                TypeKind typeKind = grouping.Key;

                switch (typeKind)
                {
                    case TypeKind.Class:
                        {
                            WriteTable(writer, grouping, "Classes", headingLevel, "Class", "Summary", FormatProvider.TypeFormat, directoryInfo);
                            break;
                        }
                    case TypeKind.Struct:
                        {
                            WriteTable(writer, grouping, "Structs", headingLevel, "Struct", "Summary", FormatProvider.TypeFormat, directoryInfo);
                            break;
                        }
                    case TypeKind.Interface:
                        {
                            WriteTable(writer, grouping, "Interfaces", headingLevel, "Interface", "Summary", FormatProvider.TypeFormat, directoryInfo);
                            break;
                        }
                    case TypeKind.Enum:
                        {
                            WriteTable(writer, grouping, "Enums", headingLevel, "Enum", "Summary", FormatProvider.TypeFormat, directoryInfo);
                            break;
                        }
                    case TypeKind.Delegate:
                        {
                            WriteTable(writer, grouping, "Delegates", headingLevel, "Delegate", "Summary", FormatProvider.TypeFormat, directoryInfo);
                            break;
                        }
                    default:
                        {
                            Debug.Fail(typeKind.ToString());
                            break;
                        }
                }
            }
        }
    }
}
