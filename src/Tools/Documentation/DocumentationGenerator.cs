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

        public DocumentationGenerator(
            CompilationDocumentationInfo compilation,
            string fileName,
            DocumentationOptions options = null)
        {
            Compilation = compilation;
            FileName = fileName;
            Options = options ?? DocumentationOptions.Default;
        }

        public CompilationDocumentationInfo Compilation { get; }

        public SymbolDisplayFormatProvider FormatProvider
        {
            get { return Options.FormatProvider; }
        }

        public string FileName { get; }

        public DocumentationOptions Options { get; }

        private SymbolDocumentationInfo EmptySymbolInfo
        {
            get { return _emptySymbolInfo ?? (_emptySymbolInfo = SymbolDocumentationInfo.Create(Compilation)); }
        }

        public IEnumerable<DocumentationFile> GenerateFiles(string heading = null, DocumentationParts parts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, Options))
            {
                DocumentationFile objectModelFile = default;
                DocumentationFile extendedTypesFile = default;

                if ((parts & DocumentationParts.ObjectModel) != 0)
                {
                    objectModelFile = GenerateObjectModel(heading + " Object Model");

                    if (!objectModelFile.HasContent)
                        parts &= ~DocumentationParts.ObjectModel;
                }

                if ((parts & DocumentationParts.ExtendedTypes) != 0)
                {
                    extendedTypesFile = GenerateExtendedTypesFile("Types Extended by " + heading);

                    if (!extendedTypesFile.HasContent)
                        parts &= ~DocumentationParts.ExtendedTypes;
                }

                yield return GenerateRootFile(writer, heading, parts);

                if ((parts & DocumentationParts.Namespace) != 0)
                {
                    foreach (DocumentationFile namespaceFile in GenerateNamespaceFiles())
                        yield return namespaceFile;
                }

                if ((parts & DocumentationParts.Type) != 0)
                {
                    bool generateTypes = (parts & DocumentationParts.Member) != 0;

                    foreach (INamedTypeSymbol typeSymbol in Compilation.TypeSymbols)
                    {
                        yield return GenerateTypeFile(typeSymbol);

                        if (generateTypes)
                        {
                            foreach (DocumentationFile memberFile in GenerateMemberFiles(typeSymbol))
                                yield return memberFile;
                        }
                    }
                }

                if (objectModelFile.HasContent)
                    yield return objectModelFile;

                if (extendedTypesFile.HasContent)
                {
                    yield return extendedTypesFile;

                    foreach (DocumentationFile typeFile in GenerateExtendedTypeFiles())
                        yield return typeFile;
                }
            }
        }

        public DocumentationFile GenerateRootFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, Options))
            {
                return GenerateRootFile(writer, heading);
            }
        }

        private DocumentationFile GenerateRootFile(DocumentationWriter writer, string heading, DocumentationParts parts = DocumentationParts.None)
        {
            if (heading != null)
                writer.WriteHeading1(heading);

            if ((parts & DocumentationParts.ObjectModel) != 0)
            {
                writer.WriteStartBulletItem();
                writer.WriteLink("Object Model", "_ObjectModel.md");
                writer.WriteEndBulletItem();
            }

            if ((parts & DocumentationParts.ExtendedTypes) != 0)
            {
                writer.WriteStartBulletItem();
                writer.WriteLink("Extended Types", "_ExtendedTypes.md");
                writer.WriteEndBulletItem();
            }

            writer.WriteHeading2("Namespaces");

            foreach (INamespaceSymbol namespaceSymbol in Compilation.NamespaceSymbols
                .OrderBy(f => f.ToDisplayString(FormatProvider.NamespaceFormat)))
            {
                writer.WriteStartBulletItem();

                string url = string.Join("/", Compilation.GetSymbolInfo(namespaceSymbol).Names.Reverse()) + "/" + FileName;
                writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                writer.WriteEndBulletItem();
            }

            foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in Compilation.TypeSymbols
                .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer.Namespace)
                .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
            {
                INamespaceSymbol namespaceSymbol = grouping.Key;

                writer.WriteStartHeading(2);

                string url = string.Join("/", Compilation.GetSymbolInfo(namespaceSymbol).Names.Reverse()) + "/" + FileName;
                writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                writer.WriteString(" Namespace");
                writer.WriteEndHeading();

                writer.WriteNamespaceContentAsTable(grouping, 3);
            }

            return new DocumentationFile(writer.ToString(), path: FileName, DocumentationKind.Root);
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
            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(namespaceSymbol);

            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, info, Options))
            {
                writer.WriteStartHeading(1);
                writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
                writer.WriteString(" Namespace");
                writer.WriteEndHeading();
                writer.WriteNamespaceContentAsTable(Compilation.GetTypeSymbols(namespaceSymbol), 2);

                return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Namespace);
            }
        }

        public DocumentationFile GenerateExtendedTypesFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, Options))
            {
                using (IEnumerator<IGrouping<INamespaceSymbol, ITypeSymbol>> en = Compilation.GetExtendedExternalTypes()
                    .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                    .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer.Namespace)
                    .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)).GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        if (heading != null)
                            writer.WriteHeading1(heading);

                        do
                        {
                            INamespaceSymbol namespaceSymbol = en.Current.Key;

                            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(namespaceSymbol);

                            writer.WriteStartHeading(2);

                            string url = info.GetUrl();
                            writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                            writer.WriteString(" Namespace");
                            writer.WriteEndHeading();

                            writer.CanCreateExternalLink = false;
                            writer.WriteNamespaceContentAsList(en.Current, 3);
                            writer.CanCreateExternalLink = true;
                        }
                        while (en.MoveNext());
                    }
                }

                return new DocumentationFile(writer.ToString(), "_ExtendedTypes.md", DocumentationKind.ExtendedTypes);
            }
        }

        public IEnumerable<DocumentationFile> GenerateExtendedTypeFiles()
        {
            foreach (ITypeSymbol typeSymbol in Compilation.GetExtendedExternalTypes())
                yield return GenerateExtendedTypeFile(typeSymbol);
        }

        public DocumentationFile GenerateExtendedTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(info, info, Options))
            {
                writer.WriteStartHeading(1 + writer.BaseHeadingLevel);

                writer.WriteLink(info, FormatProvider.TitleFormat);
                writer.WriteString(" ");
                writer.WriteString(DocumentationFacts.GetName(typeSymbol.TypeKind));
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

                return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Type);
            }
        }

        public DocumentationFile GenerateTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(info, info, Options))
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

                if (typeSymbol.TypeKind != TypeKind.Delegate)
                {
                    if (typeSymbol.TypeKind == TypeKind.Enum)
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
                }

                writer.WriteExtensionMethods(typeSymbol);
                writer.WriteSeeAlso(typeSymbol);

                return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Type);
            }
        }

        public IEnumerable<DocumentationFile> GenerateMemberFiles(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
                return Enumerable.Empty<DocumentationFile>();

            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(typeSymbol);

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
                SymbolDocumentationInfo info = Compilation.GetSymbolInfo(symbols[0].Symbol);

                using (MemberDocumentationMarkdownWriter writer = MemberDocumentationMarkdownWriter.Create(symbols, info, Options))
                {
                    writer.WriteMember();

                    yield return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Member);
                }
            }
        }

        public DocumentationFile GenerateObjectModel(string heading)
        {
            var dic = new Dictionary<INamedTypeSymbol, MBulletItem>();

            List<(INamedTypeSymbol symbol, MBulletItem item)> items = Compilation
                .TypeSymbols
                .Where(f => !f.IsStatic && f.TypeKind != TypeKind.Interface && IsBottomType(f))
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

            var doc = new MDocument(
                Heading1(heading),
                dic[Compilation.Compilation.ObjectType]);

            if (Compilation
                .TypeSymbols
                .Any(f => f.TypeKind == TypeKind.Interface))
            {
                doc.Add(
                    Heading2(DocumentationFacts.GetPluralName(TypeKind.Interface)),
                    Compilation
                        .TypeSymbols
                        .Where(f => f.TypeKind == TypeKind.Interface)
                        .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                        .Select(f => BulletItem(GetBulletItemContent(f))));
            }

            return new DocumentationFile(doc.ToString(), "_ObjectModel.md", DocumentationKind.ObjectModel);

            bool IsBottomType(ITypeSymbol s)
            {
                s = s.OriginalDefinition;

                foreach (INamedTypeSymbol symbol in Compilation.TypeSymbols)
                {
                    if (symbol.InheritsFrom(s))
                        return false;
                }

                return true;
            }

            object GetBulletItemContent(INamedTypeSymbol namedTypeSymbol)
            {
                SymbolDocumentationInfo symbolInfo = Compilation.GetSymbolInfo(namedTypeSymbol);

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

                                    string url = Compilation.GetSymbolInfo(symbol).GetUrl();

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
