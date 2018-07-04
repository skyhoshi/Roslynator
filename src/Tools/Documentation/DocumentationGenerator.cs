// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationGenerator
    {
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

                    using (var writer2 = new DocumentationMarkdownWriter(null, null, this, writer))
                    {
                        writer2.WriteNamespaceContent(grouping, 3);
                    }
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

                using (var writer2 = new DocumentationMarkdownWriter(null, info, this, writer))
                {
                    writer2.WriteNamespaceContent(TypeSymbols.Where(f => f.ContainingNamespace == namespaceSymbol), 2);
                }

                return writer.ToString();
            }
        }

        public string GenerateTypeDocument(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = GetDocumentationInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(typeSymbol, info, this))
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

                if (typeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
                {
                    writer.WriteEnumFields(info.GetFields());
                }
                else
                {
                    writer.WriteConstructors(info.GetConstructors());
                    writer.WriteFields(info.GetFields());
                    writer.WriteProperties(info.GetProperties());
                    writer.WriteMethods(info.GetMethods());
                    writer.WriteOperators(info.GetOperators());
                    writer.WriteEvents(info.GetEvents());
                    writer.WriteExplicitInterfaceImplementations(info.GetExplicitInterfaceImplementations());
                }

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
    }
}
