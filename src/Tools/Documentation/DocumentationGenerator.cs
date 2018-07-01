// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Diagnostics;
using System;
using System.Text.RegularExpressions;
using System.IO;

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

        public string GenerateDocument(ITypeSymbol typeSymbol)
        {
            using (var writer = new MarkdownDocumentationWriter(this))
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

                IEnumerable<ISymbol> members = typeSymbol.GetMembers().Where(f => f.IsPubliclyVisible());

                IEnumerable<IFieldSymbol> fields = members
                    .Where(f => f.Kind == SymbolKind.Field)
                    .Cast<IFieldSymbol>();

                if (typeSymbol.TypeKind == TypeKind.Enum)
                    writer.WriteEnumFields(fields);

                IEnumerable<IMethodSymbol> constructors = members
                    .Where(f => f.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(f => f.MethodKind == MethodKind.Constructor
                        && (f.ContainingType.TypeKind != TypeKind.Struct || f.Parameters.Any()));

                writer.WriteConstructors(constructors);

                writer.WriteFields(fields);

                IEnumerable<IPropertySymbol> properties = members
                    .Where(f => f.Kind == SymbolKind.Property)
                    .Cast<IPropertySymbol>();

                writer.WriteProperties(properties);

                IEnumerable<IMethodSymbol> methods = members
                    .Where(f => f.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(f => f.MethodKind == MethodKind.Ordinary);

                writer.WriteMethods(methods);

                IEnumerable<IMethodSymbol> operators = members
                    .Where(f => f.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(f => f.MethodKind.Is(MethodKind.UserDefinedOperator, MethodKind.Conversion));

                writer.WriteOperators(operators);

                IEnumerable<IEventSymbol> events = members
                    .Where(f => f.Kind == SymbolKind.Event)
                    .Cast<IEventSymbol>();

                writer.WriteEvents(events);

                IEnumerable<IMethodSymbol> explicitInterfaceImplementations = members
                    .Where(f => f.Kind == SymbolKind.Method)
                    .Cast<IMethodSymbol>()
                    .Where(f => f.MethodKind == MethodKind.ExplicitInterfaceImplementation);

                writer.WriteExplicitInterfaceImplementations(explicitInterfaceImplementations);

                writer.WriteExtensionMethods(typeSymbol);
                writer.WriteSeeAlso(typeSymbol);

                return writer.ToString();
            }
        }

        public string GenerateDocument(INamespaceSymbol namespaceSymbol)
        {
            using (MarkdownWriter writer = MarkdownWriter.Create(new StringBuilder()))
            {
                WriteNamespaceContent(writer, namespaceSymbol, TypeSymbols.Where(f => f.ContainingNamespace == namespaceSymbol), 1);

                return writer.ToString();
            }
        }

        public string GenerateIndex(string heading)
        {
            using (MarkdownWriter w = MarkdownWriter.Create(new StringBuilder()))
            {
                w.WriteHeading1(heading);

                foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in TypeSymbols
                    .GroupBy(f => f.ContainingNamespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    WriteNamespaceContent(w, grouping.Key, grouping, 2);
                }

                return w.ToString();
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
                string url = GetUrl(symbol);

                info = new SymbolDocumentationInfo(
                    symbol,
                    symbol.GetDocumentationCommentId(),
                    url,
                    url + "/README.md");

                _symbolDocumentationCache[symbol] = info;
                return true;
            }

            info = default;
            return false;
        }

        public virtual string GetUrl(ISymbol symbol)
        {
            string s = symbol.Name;

            int arity = GetArity();

            if (arity > 0)
            {
                s += "-";
                s += arity.ToString();
            }

            INamedTypeSymbol containingType = symbol.ContainingType;

            while (containingType != null)
            {
                string name = containingType.Name;

                arity = containingType.Arity;

                if (arity > 0)
                {
                    name += "-";
                    name += arity.ToString();
                }

                s = name + "/" + s;

                containingType = containingType.ContainingType;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            while (containingNamespace?.IsGlobalNamespace == false)
            {
                s = containingNamespace.Name + "/" + s;

                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return s;

            int GetArity()
            {
                switch (symbol.Kind)
                {
                    case SymbolKind.Method:
                        return ((IMethodSymbol)symbol).Arity;
                    case SymbolKind.NamedType:
                        return ((INamedTypeSymbol)symbol).Arity;
                }

                return 0;
            }
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
            SymbolDisplayFormat format)
        {
            using (IEnumerator<ISymbol> en = symbols.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    writer.WriteHeading(headingLevel, heading);

                    WriteTableHeader(writer, header1, header2);

                    do
                    {
                        WriteTableRow(writer, en.Current, format);
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
            SymbolDisplayFormat format)
        {
            writer.WriteStartTableRow();
            writer.WriteStartTableCell();

            SymbolDocumentationInfo info = GetDocumentationInfo(symbol);

            writer.WriteLinkOrText(symbol.ToDisplayString(format), info.FileUrl);

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
            INamespaceSymbol namespaceSymbol,
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel)
        {
            writer.WriteStartHeading(headingLevel);
            writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
            writer.WriteString(" Namespace");
            writer.WriteEndHeading();

            headingLevel++;

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
                            WriteTable(writer, grouping, "Classes", headingLevel, "Class", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Struct:
                        {
                            WriteTable(writer, grouping, "Structs", headingLevel, "Struct", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Interface:
                        {
                            WriteTable(writer, grouping, "Interfaces", headingLevel, "Interface", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Enum:
                        {
                            WriteTable(writer, grouping, "Enums", headingLevel, "Enum", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Delegate:
                        {
                            WriteTable(writer, grouping, "Delegates", headingLevel, "Delegate", "Summary", FormatProvider.TypeFormat);
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
