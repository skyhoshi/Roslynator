// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DotMarkdown.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.Documentation.Markdown;
using static DotMarkdown.Linq.MFactory;

namespace Roslynator.Documentation
{
    public class DocumentationGenerator
    {
        private SymbolDocumentationInfo _emptySymbolInfo;

        public DocumentationCompilation Compilation { get; }

        public DocumentationGenerator(
            DocumentationCompilation compilation,
            string fileName,
            SymbolDisplayFormatProvider formatProvider = null)
        {
            Compilation = compilation;
            FileName = fileName;
            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
        }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        public string FileName { get; }

        private SymbolDocumentationInfo EmptySymbolInfo
        {
            get { return _emptySymbolInfo ?? (_emptySymbolInfo = SymbolDocumentationInfo.Create(Compilation)); }
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
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, FormatProvider))
            {
                if (heading != null)
                    writer.WriteHeading1(heading);

                writer.WriteHeading2("Namespaces");

                foreach (INamespaceSymbol namespaceSymbol in Compilation.NamespaceSymbols
                    .OrderBy(f => f.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    writer.WriteStartBulletItem();

                    string url = string.Join("/", Compilation.GetDocumentationInfo(namespaceSymbol).Names.Reverse()) + "/" + FileName;
                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                    writer.WriteEndBulletItem();
                }

                foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in Compilation.TypeSymbols
                    .GroupBy(f => f.ContainingNamespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    INamespaceSymbol namespaceSymbol = grouping.Key;

                    writer.WriteStartHeading(2);

                    string url = string.Join("/", Compilation.GetDocumentationInfo(namespaceSymbol).Names.Reverse()) + "/" + FileName;
                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

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

            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, info, FormatProvider))
            {
                writer.WriteStartHeading(1);
                writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
                writer.WriteString(" Namespace");
                writer.WriteEndHeading();
                writer.WriteNamespaceContent(Compilation.GetTypeSymbols(namespaceSymbol), 2);

                return DocumentationFile.Create(writer.ToString(), info, DocumentationKind.Namespace);
            }
        }

        public DocumentationFile GenerateExtendedTypesFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, FormatProvider))
            {
                if (heading != null)
                    writer.WriteHeading1(heading);

                foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in Compilation.GetExtendedExternalTypes()
                    .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                    .GroupBy(f => f.ContainingNamespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    INamespaceSymbol namespaceSymbol = grouping.Key;

                    SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(namespaceSymbol);

                    writer.WriteStartHeading(2);

                    string url = info.GetUrl();
                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                    writer.WriteString(" Namespace");
                    writer.WriteEndHeading();

                    writer.CanCreateExternalLink = false;
                    writer.WriteNamespaceContent(grouping, 3);
                    writer.CanCreateExternalLink = true;
                }

                return new DocumentationFile(writer.ToString(), default(string), DocumentationKind.None);
            }
        }

        public IEnumerable<DocumentationFile> GenerateExtendedTypeFiles()
        {
            foreach (ITypeSymbol typeSymbol in Compilation.GetExtendedExternalTypes())
                yield return GenerateExtendedTypeFile(typeSymbol);
        }

        public DocumentationFile GenerateExtendedTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = Compilation.GetDocumentationInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(info, info, FormatProvider))
            {
                writer.WriteStartHeading(1 + writer.BaseHeadingLevel);

                writer.WriteString(typeSymbol.ToDisplayString(FormatProvider.TitleFormat));
                writer.WriteString(" ");
                writer.WriteString(typeSymbol.GetName());
                writer.WriteString(" Extensions");
                writer.WriteEndHeading();

                IEnumerable<IMethodSymbol> extensionMethods = Compilation.GetExtensionMethodSymbols(typeSymbol);

                writer.WriteTable(
                    extensionMethods,
                    heading: null,
                    2,
                    "Extension Method",
                    "Summary",
                    FormatProvider.MethodFormat,
                    SymbolDisplayAdditionalOptions.None);

                return DocumentationFile.Create(writer.ToString(), info, DocumentationKind.Type);
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

        public string GenerateObjectModel(string heading)
        {
            var dic = new Dictionary<INamedTypeSymbol, MBulletItem>();

            List<(INamedTypeSymbol symbol, MBulletItem item)> items = Compilation
                .TypeSymbols
                .Where(f => !f.IsStatic && IsBottomType(f))
                .Cast<INamedTypeSymbol>()
                .Select(f => (f, BulletItem(GetBulletItemContent(f))))
                .ToList();

            do
            {
                List<IGrouping<INamedTypeSymbol, (INamedTypeSymbol symbol, MBulletItem item)>> groupingList = items
                    .OrderBy(f => f.item.ToString())
                    .GroupBy(f => f.symbol.BaseType?.OriginalDefinition)
                    .ToList();

                items.Clear();

                foreach (IGrouping<INamedTypeSymbol, (INamedTypeSymbol symbol, MBulletItem item)> grouping in groupingList)
                {
                    INamedTypeSymbol baseType = grouping.Key;

                    bool success = dic.TryGetValue(baseType, out MBulletItem bulletItem);

                    if (!success)
                    {
                        bulletItem = BulletItem(GetBulletItemContent(baseType));
                        dic[baseType] = bulletItem;
                    }

                    bulletItem.Add(grouping.Select(f => f.item));

                    if (!success
                        && baseType.SpecialType != SpecialType.System_Object)
                    {
                        items.Add((baseType, bulletItem));
                    }
                }
            }
            while (items.Count > 0);

            var doc = new MDocument(Heading1(heading), dic[Compilation.Compilation.ObjectType]);

            return doc.ToString();

            bool IsBottomType(ITypeSymbol s)
            {
                s = s.OriginalDefinition;

                foreach (ITypeSymbol symbol in Compilation.TypeSymbols)
                {
                    if (symbol.InheritsFrom(s))
                        return false;
                }

                return true;
            }

            object GetBulletItemContent(INamedTypeSymbol namedTypeSymbol)
            {
                SymbolDocumentationInfo symbolInfo = Compilation.GetDocumentationInfo(namedTypeSymbol);

                if (namedTypeSymbol.TypeArguments.Any(f => f.Kind != SymbolKind.TypeParameter))
                {
                    var content = new List<object>();

                    foreach (SymbolDisplayPart part in symbolInfo
                        .Symbol
                        .ToDisplayParts(FormatProvider.TypeFormat))
                    {
                        switch (part.Kind)
                        {
                            case SymbolDisplayPartKind.ClassName:
                            case SymbolDisplayPartKind.DelegateName:
                            case SymbolDisplayPartKind.EnumName:
                            case SymbolDisplayPartKind.InterfaceName:
                            case SymbolDisplayPartKind.StructName:
                                {
                                    ISymbol symbol = part.Symbol;

                                    string url = Compilation.GetDocumentationInfo(symbol).GetUrl();

                                    content.Add(LinkOrText(symbol.Name, url));

                                    break;
                                }
                            default:
                                {
                                    content.Add(part);
                                    break;
                                }
                        }
                    }

                    return content;
                }
                else
                {
                    string url = symbolInfo.GetUrl();

                    return LinkOrText(symbolInfo.Symbol.ToDisplayString(FormatProvider.TypeFormat), url);
                }
            }
        }
    }
}
