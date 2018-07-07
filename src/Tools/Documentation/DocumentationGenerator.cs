// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationGenerator
    {
        private SymbolDocumentationInfo _emptySymbol;

        public DocumentationCompilation Compilation { get; }

        public DocumentationGenerator(
            DocumentationCompilation compilation,
            SymbolDisplayFormatProvider formatProvider = null)
        {
            Compilation = compilation;
            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
        }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        private SymbolDocumentationInfo EmptySymbol
        {
            get { return _emptySymbol ?? (_emptySymbol = SymbolDocumentationInfo.Create(Compilation)); }
        }

        public IEnumerable<DocumentationFile> GenerateFiles(string heading = null)
        {
            yield return GenerateRootFile(heading);

            foreach (DocumentationFile namespaceFile in GenerateNamespaceFiles())
                yield return namespaceFile;

            foreach (ITypeSymbol typeSymbol in Compilation.TypeSymbols)
            {
                yield return GenerateTypeFile(typeSymbol);

                foreach (DocumentationFile memberFile in GenerateMemberFiles(typeSymbol))
                    yield return memberFile;
            }
        }

        public DocumentationFile GenerateRootFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbol, directoryInfo: null, FormatProvider))
            {
                if (heading != null)
                    writer.WriteHeading1(heading);

                foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in Compilation.TypeSymbols
                    .GroupBy(f => f.ContainingNamespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    INamespaceSymbol namespaceSymbol = grouping.Key;

                    writer.WriteStartHeading(2);

                    SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(namespaceSymbol);

                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), string.Join("/", info.Names.Reverse()) + "/README.md");
                    writer.WriteString(" Namespace");
                    writer.WriteEndHeading();
                    writer.WriteNamespaceContent(grouping, 3);
                }

                return new DocumentationFile(writer.ToString(), directoryPath: null, DocumentationKind.Root);
            }
        }

        public IEnumerable<DocumentationFile> GenerateNamespaceFiles()
        {
            foreach (INamespaceSymbol namespaceSymbol in Compilation.NamespaceSymbols)
            {
                yield return GenerateNamespaceFile(namespaceSymbol);
            }
        }

        public DocumentationFile GenerateNamespaceFile(INamespaceSymbol namespaceSymbol)
        {
            SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(namespaceSymbol);

            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbol, info, FormatProvider))
            {
                writer.WriteStartHeading(1);
                writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
                writer.WriteString(" Namespace");
                writer.WriteEndHeading();
                writer.WriteNamespaceContent(Compilation.GetTypeSymbols(namespaceSymbol), 2);

                return DocumentationFile.Create(writer.ToString(), info, DocumentationKind.Namespace);
            }
        }

        public DocumentationFile GenerateTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(info, info, FormatProvider))
            {
                writer.WriteTitle(typeSymbol);
                writer.WriteNamespace(typeSymbol);
                writer.WriteAssembly(typeSymbol);

                if (typeSymbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    writer.WriteObsolete(typeSymbol);

                writer.WriteSummary(typeSymbol);

                if (typeSymbol.Kind != SymbolKind.Namespace)
                    writer.WriteSignature(typeSymbol);

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
                    writer.WriteFields(info.GetFields(includeInherited: true));
                    writer.WriteProperties(info.GetProperties(includeInherited: true));
                    writer.WriteMethods(info.GetMethods(includeInherited: true));
                    writer.WriteOperators(info.GetOperators(includeInherited: true));
                    writer.WriteEvents(info.GetEvents(includeInherited: true));
                    writer.WriteExplicitInterfaceImplementations(info.GetExplicitInterfaceImplementations());
                }

                writer.WriteExtensionMethods(typeSymbol);
                writer.WriteSeeAlso(typeSymbol);

                return DocumentationFile.Create(writer.ToString(), info, DocumentationKind.Type);
            }
        }

        public IEnumerable<DocumentationFile> GenerateMemberFiles(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
                return Enumerable.Empty<DocumentationFile>();

            SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(typeSymbol);

            IEnumerable<ISymbol> members = info.GetConstructors()
                .AsEnumerable<ISymbol>()
                .Concat(info.GetFields())
                .Concat(info.GetProperties())
                .Concat(info.GetMethods())
                .Concat(info.GetOperators())
                .Concat(info.GetEvents())
                .Concat(info.GetExplicitInterfaceImplementations());

            return GenerateMemberFile(members);
        }

        private IEnumerable<DocumentationFile> GenerateMemberFile(IEnumerable<ISymbol> members)
        {
            foreach (IGrouping<string, ISymbol> grouping in members.GroupBy(f => f.Name))
            {
                ImmutableArray<SymbolDocumentationInfo> symbols = grouping
                    .Select(f => SymbolDocumentationInfo.Create(f, Compilation))
                    .ToImmutableArray();

                //TODO: ?
                SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(symbols[0].Symbol);

                using (MemberDocumentationMarkdownWriter writer = MemberDocumentationMarkdownWriter.Create(symbols, info, FormatProvider))
                {
                    writer.WriteMember();

                    yield return new DocumentationFile(writer.ToString(), string.Join(@"\", info.Names.Reverse()), DocumentationKind.Member);
                }
            }
        }
    }
}
