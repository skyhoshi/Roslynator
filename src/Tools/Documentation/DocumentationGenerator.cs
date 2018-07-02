// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationGenerator
    {
        private static readonly Regex _newLineWithWhitespaceRegex = new Regex(@"\r?\n\s*");

        private readonly ImmutableArray<DocumentationSource> _sources;
        private ImmutableArray<ITypeSymbol> _typeSymbols;

        private readonly Dictionary<ISymbol, SymbolDocumentationInfo> _symbolDocumentationCache;
        private readonly Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public DocumentationGenerator(ImmutableArray<DocumentationSource> sources, SymbolDisplayFormatProvider formatProvider = null)
        {
            _sources = sources;

            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;

            _xmlDocumentations = _sources.ToDictionary(f => f.AssemblySymbol, f => f.GetXmlDocumentation());

            _symbolDocumentationCache = new Dictionary<ISymbol, SymbolDocumentationInfo>();
        }

        public ImmutableArray<ITypeSymbol> TypeSymbols
        {
            get
            {
                if (_typeSymbols.IsDefault)
                {
                    _typeSymbols = _sources
                        .SelectMany(f => f.AssemblySymbol.GetPubliclyVisibleTypes())
                        .ToImmutableArray();
                }

                return _typeSymbols;
            }
        }

        public SymbolDisplayFormatProvider FormatProvider { get; }

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

            using (var writer = new MarkdownTypeDocumentationWriter(this, info))
            {
                writer.WriteTitle(typeSymbol);
                writer.WriteNamespace(typeSymbol);
                writer.WriteAssembly(typeSymbol);
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

                TypeMembersInfo members = TypeMembersInfo.Create(typeSymbol);

                if (typeSymbol.TypeKind == TypeKind.Enum)
                    writer.WriteEnumFields(members.GetFields());

                writer.WriteConstructors(members.GetConstructors());

                if (typeSymbol.TypeKind != TypeKind.Enum)
                    writer.WriteFields(members.GetFields());

                writer.WriteProperties(members.GetProperties());
                writer.WriteMethods(members.GetMethods());
                writer.WriteOperators(members.GetOperators());
                writer.WriteEvents(members.GetEvents());
                writer.WriteExplicitInterfaceImplementations(members.GetExplicitInterfaceImplementations());
                writer.WriteExtensionMethods(typeSymbol);
                writer.WriteSeeAlso(typeSymbol);

                return writer.ToString();
            }
        }

        internal SymbolDocumentationInfo GetDocumentationInfo(ISymbol symbol)
        {
            if (!TryGetDocumentationInfo(symbol, out SymbolDocumentationInfo info))
                throw new InvalidOperationException();

            return info;
        }

        internal bool TryGetDocumentationInfo(ISymbol symbol, out SymbolDocumentationInfo info)
        {
            if (_symbolDocumentationCache.TryGetValue(symbol, out SymbolDocumentationInfo value))
            {
                info = value;
                return true;
            }

            if (_sources.Any(f => f.AssemblySymbol == symbol.ContainingAssembly))
            {
                info = SymbolDocumentationInfo.Create(symbol);

                _symbolDocumentationCache[symbol] = info;
                return true;
            }

            info = default;
            return false;
        }

        internal XElement GetDocumentationElement(ISymbol symbol, string name)
        {
            return _xmlDocumentations[symbol.ContainingAssembly].GetElement(GetDocumentationInfo(symbol).CommentId, name);
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
            using (IEnumerator<ISymbol> en = symbols.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    writer.WriteHeading(headingLevel, heading);

                    WriteTableHeader(writer, header1, header2);

                    do
                    {
                        WriteTableRow(writer, en.Current, format, directoryInfo);
                    }
                    while (en.MoveNext());

                    writer.WriteEndTable();
                }
            }
        }

        private static void WriteTableHeader(
            MarkdownWriter writer,
            string header1,
            string header2)
        {
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
        }

        private void WriteTableRow(
            MarkdownWriter writer,
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDocumentationInfo directoryInfo)
        {
            writer.WriteStartTableRow();
            writer.WriteStartTableCell();

            SymbolDocumentationInfo info = GetDocumentationInfo(symbol);

            writer.WriteLink(info, directoryInfo, format);

            writer.WriteEndTableCell();
            writer.WriteStartTableCell();

            string s = _xmlDocumentations[symbol.ContainingAssembly]
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
