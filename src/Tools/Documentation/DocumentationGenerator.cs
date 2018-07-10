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
            DocumentationOptions options = null,
            DocumentationResources resources = null)
        {
            CompilationInfo = compilation;
            Options = options ?? DocumentationOptions.Default;
            Resources = resources ?? DocumentationResources.Default;
        }

        public CompilationDocumentationInfo CompilationInfo { get; }

        public DocumentationOptions Options { get; }

        public SymbolDisplayFormatProvider FormatProvider => Options.FormatProvider;

        public string FileName => Options.FileName;

        public DocumentationResources Resources { get; }

        private SymbolDocumentationInfo EmptySymbolInfo
        {
            get { return _emptySymbolInfo ?? (_emptySymbolInfo = SymbolDocumentationInfo.Create(CompilationInfo)); }
        }

        internal SymbolDocumentationInfo GetSymbolInfo(ISymbol symbol)
        {
            return CompilationInfo.GetSymbolInfo(symbol);
        }

        public IEnumerable<DocumentationFile> GenerateFiles(
            string heading = null,
            DocumentationParts documentationParts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member,
            string objectModelHeading = null,
            string extendedTypesHeading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, Options, Resources))
            {
                DocumentationFile objectModelFile = default;
                DocumentationFile extendedTypesFile = default;

                if ((documentationParts & DocumentationParts.ObjectModel) != 0)
                {
                    objectModelFile = GenerateObjectModel(objectModelHeading ?? Resources.ObjectModel);

                    if (!objectModelFile.HasContent)
                        documentationParts &= ~DocumentationParts.ObjectModel;
                }

                if ((documentationParts & DocumentationParts.ExtendedTypes) != 0)
                {
                    extendedTypesFile = GenerateExtendedTypesFile(extendedTypesHeading ?? Resources.ExtendedTypes);

                    if (!extendedTypesFile.HasContent)
                        documentationParts &= ~DocumentationParts.ExtendedTypes;
                }

                yield return GenerateRootFile(writer, heading, documentationParts);

                if ((documentationParts & DocumentationParts.Namespace) != 0)
                {
                    foreach (DocumentationFile namespaceFile in GenerateNamespaceFiles())
                        yield return namespaceFile;
                }

                if ((documentationParts & DocumentationParts.Type) != 0)
                {
                    bool generateTypes = (documentationParts & DocumentationParts.Member) != 0;

                    foreach (INamedTypeSymbol typeSymbol in CompilationInfo.Types)
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
            using (var writer = new DocumentationMarkdownWriter(
                symbolInfo: EmptySymbolInfo,
                directoryInfo: null,
                Options,
                Resources))
            {
                return GenerateRootFile(writer, heading);
            }
        }

        private DocumentationFile GenerateRootFile(
            DocumentationWriter writer,
            string heading,
            DocumentationParts documentationParts = DocumentationParts.None)
        {
            if (heading != null)
                writer.WriteHeading1(heading);

            if ((documentationParts & DocumentationParts.ObjectModel) != 0
                && Options.IsEnabled(RootDocumentationParts.ObjectModelLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ObjectModel, WellKnownNames.ObjectModelFileName);
                writer.WriteEndBulletItem();
            }

            if ((documentationParts & DocumentationParts.ExtendedTypes) != 0
                && Options.IsEnabled(RootDocumentationParts.ExtendedTypesLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ExtendedTypes, WellKnownNames.ExtendedTypesFileName);
                writer.WriteEndBulletItem();
            }

            if (Options.IsEnabled(RootDocumentationParts.NamespaceList))
            {
                writer.WriteHeading2(Resources.Namespaces);

                foreach (INamespaceSymbol namespaceSymbol in CompilationInfo.Namespaces
                    .OrderBy(f => f.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    writer.WriteStartBulletItem();

                    string url = GetSymbolInfo(namespaceSymbol).GetUrl(FileName, useExternalLink: false);

                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                    writer.WriteEndBulletItem();
                }
            }

            if (Options.IsEnabled(RootDocumentationParts.Namespaces))
            {
                foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in CompilationInfo.Types
               .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
               .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    INamespaceSymbol namespaceSymbol = grouping.Key;

                    writer.WriteStartHeading(2);

                    string url = GetSymbolInfo(namespaceSymbol).GetUrl(FileName);
                    writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                    writer.WriteSpace();
                    writer.WriteString(Resources.Namespace);
                    writer.WriteEndHeading();

                    writer.WriteNamespaceContentAsTable(grouping, 3);
                }
            }

            return new DocumentationFile(writer.ToString(), path: FileName, DocumentationKind.Root);
        }

        public IEnumerable<DocumentationFile> GenerateNamespaceFiles()
        {
            foreach (INamespaceSymbol namespaceSymbol in CompilationInfo.Namespaces)
            {
                yield return GenerateNamespaceFile(namespaceSymbol);
            }
        }

        private DocumentationFile GenerateNamespaceFile(INamespaceSymbol namespaceSymbol)
        {
            SymbolDocumentationInfo info = GetSymbolInfo(namespaceSymbol);

            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, info, Options, Resources))
            {
                if (Options.IsEnabled(NamespaceDocumentationParts.Heading))
                {
                    writer.WriteStartHeading(1);
                    writer.WriteString(namespaceSymbol, FormatProvider.NamespaceFormat);
                    writer.WriteSpace();
                    writer.WriteString(Resources.Namespace);
                    writer.WriteEndHeading();
                }

                writer.WriteNamespaceContentAsTable(CompilationInfo.GetTypes(namespaceSymbol), 2);

                return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Namespace);
            }
        }

        public DocumentationFile GenerateExtendedTypesFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null, Options, Resources))
            {
                using (IEnumerator<INamespaceSymbol> en = CompilationInfo.GetExtendedExternalTypes()
                    .Select(f => f.ContainingNamespace)
                    .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                    .OrderBy(f => f.ToDisplayString(FormatProvider.NamespaceFormat)).GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        if (heading != null)
                            writer.WriteHeading1(heading);

                        if (Options.IsEnabled(RootDocumentationParts.NamespaceList))
                        {
                            writer.WriteStartHeading(2);
                            writer.WriteString(Resources.Namespaces);
                            writer.WriteEndHeading();

                            do
                            {
                                writer.WriteStartBulletItem();

                                string url = GetSymbolInfo(en.Current).GetUrl(FileName);
                                writer.WriteLink(en.Current.ToDisplayString(FormatProvider.NamespaceFormat), url);

                                writer.WriteEndBulletItem();
                            }
                            while (en.MoveNext());
                        }
                    }

                    if (Options.IsEnabled(RootDocumentationParts.Namespaces))
                    {
                        foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in CompilationInfo.GetExtendedExternalTypes()
                       .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                       .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                       .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                        {
                            INamespaceSymbol namespaceSymbol = grouping.Key;

                            SymbolDocumentationInfo info = GetSymbolInfo(namespaceSymbol);

                            writer.WriteStartHeading(2);

                            string url = info.GetUrl(FileName);
                            writer.WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), url);

                            writer.WriteSpace();
                            writer.WriteString(Resources.Namespace);
                            writer.WriteEndHeading();

                            writer.CanCreateExternalLink = false;
                            writer.WriteNamespaceContentAsList(grouping, 3);
                            writer.CanCreateExternalLink = true;
                        }
                    }
                }

                return new DocumentationFile(writer.ToString(), WellKnownNames.ExtendedTypesFileName, DocumentationKind.ExtendedTypes);
            }
        }

        public IEnumerable<DocumentationFile> GenerateExtendedTypeFiles()
        {
            foreach (ITypeSymbol typeSymbol in CompilationInfo.GetExtendedExternalTypes())
                yield return GenerateExtendedTypeFile(typeSymbol);
        }

        private DocumentationFile GenerateExtendedTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo symbolInfo = GetSymbolInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(symbolInfo, symbolInfo, Options, Resources))
            {
                writer.WriteStartHeading(1 + writer.BaseHeadingLevel);
                writer.WriteLink(symbolInfo, FormatProvider.TitleFormat);
                writer.WriteSpace();
                writer.WriteString(Resources.GetName(typeSymbol.TypeKind));
                writer.WriteSpace();
                writer.WriteString(Resources.Extensions);
                writer.WriteEndHeading();

                IEnumerable<IMethodSymbol> extensionMethods = CompilationInfo.GetExtensionMethods(typeSymbol);

                writer.WriteTable(
                    extensionMethods,
                    heading: null,
                    2,
                    Resources.ExtensionMethod,
                    Resources.Summary,
                    FormatProvider.MethodFormat,
                    SymbolDisplayAdditionalOptions.None);

                return DocumentationFile.Create(writer, symbolInfo, FileName, DocumentationKind.Type);
            }
        }

        public DocumentationFile GenerateTypeFile(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;
            bool isDelegateOrEnum = typeKind.Is(TypeKind.Delegate, TypeKind.Enum);

            SymbolDocumentationInfo info = GetSymbolInfo(typeSymbol);

            using (var writer = new DocumentationMarkdownWriter(info, info, Options, Resources))
            {
                foreach (TypeDocumentationParts part in Options.EnabledAndSortedTypeParts)
                {
                    switch (part)
                    {
                        case TypeDocumentationParts.Title:
                            {
                                writer.WriteTitle(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Namespace:
                            {
                                writer.WriteNamespace(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Assembly:
                            {
                                writer.WriteAssembly(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Obsolete:
                            {
                                if (typeSymbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                                    writer.WriteObsolete(typeSymbol);

                                break;
                            }
                        case TypeDocumentationParts.Summary:
                            {
                                writer.WriteSummary(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Signature:
                            {
                                if (typeSymbol.Kind != SymbolKind.Namespace)
                                    writer.WriteSignature(typeSymbol);

                                break;
                            }
                        case TypeDocumentationParts.TypeParameters:
                            {
                                writer.WriteTypeParameters(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Parameters:
                            {
                                writer.WriteParameters(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.ReturnValue:
                            {
                                writer.WriteReturnValue(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Inheritance:
                            {
                                writer.WriteInheritance(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Attributes:
                            {
                                writer.WriteAttributes(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Derived:
                            {
                                writer.WriteDerived(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Implements:
                            {
                                writer.WriteImplements(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Examples:
                            {
                                writer.WriteExamples(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Remarks:
                            {
                                writer.WriteRemarks(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.Constructors:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteConstructors(info.GetConstructors());

                                break;
                            }
                        case TypeDocumentationParts.Fields:
                            {
                                if (typeKind != TypeKind.Delegate)
                                {
                                    if (typeKind == TypeKind.Enum)
                                    {
                                        writer.WriteEnumFields(info.GetFields());
                                    }
                                    else
                                    {
                                        writer.WriteFields(info.GetFields(includeInherited: true));
                                    }
                                }

                                break;
                            }
                        case TypeDocumentationParts.Properties:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteProperties(info.GetProperties(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Methods:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteMethods(info.GetMethods(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Operators:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteOperators(info.GetOperators(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.Events:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteEvents(info.GetEvents(includeInherited: true));

                                break;
                            }
                        case TypeDocumentationParts.ExplicitInterfaceImplementations:
                            {
                                if (!isDelegateOrEnum)
                                    writer.WriteExplicitInterfaceImplementations(info.GetExplicitInterfaceImplementations());

                                break;
                            }
                        case TypeDocumentationParts.ExtensionMethods:
                            {
                                writer.WriteExtensionMethods(typeSymbol);
                                break;
                            }
                        case TypeDocumentationParts.SeeAlso:
                            {
                                writer.WriteSeeAlso(typeSymbol);
                                break;
                            }
                    }
                }

                return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Type);
            }
        }

        public IEnumerable<DocumentationFile> GenerateMemberFiles(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
                yield break;

            SymbolDocumentationInfo info = GetSymbolInfo(typeSymbol);

            if (Options.IsEnabled(TypeDocumentationParts.Constructors))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetConstructors()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Fields))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetFields()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Properties))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetProperties()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Methods))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetMethods()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Operators))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetOperators()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.Events))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetEvents()))
                    yield return file;
            }

            if (Options.IsEnabled(TypeDocumentationParts.ExplicitInterfaceImplementations))
            {
                foreach (DocumentationFile file in GenerateMemberFile(info.GetExplicitInterfaceImplementations()))
                    yield return file;
            }
        }

        private IEnumerable<DocumentationFile> GenerateMemberFile(IEnumerable<ISymbol> members)
        {
            foreach (IGrouping<string, ISymbol> grouping in members.GroupBy(f => f.Name))
            {
                ImmutableArray<SymbolDocumentationInfo> symbols = grouping
                    .Select(f => SymbolDocumentationInfo.Create(f, CompilationInfo))
                    .ToImmutableArray();

                SymbolDocumentationInfo info = GetSymbolInfo(symbols[0].Symbol);

                using (MemberDocumentationMarkdownWriter writer = MemberDocumentationMarkdownWriter.Create(symbols, info, Options, Resources))
                {
                    writer.WriteMember();

                    yield return DocumentationFile.Create(writer, info, FileName, DocumentationKind.Member);
                }
            }
        }

        //TODO: replace linq to markdown with MarkdownWriter
        public DocumentationFile GenerateObjectModel(string heading)
        {
            var dic = new Dictionary<INamedTypeSymbol, MBulletItem>();

            List<(INamedTypeSymbol symbol, MBulletItem item)> items = CompilationInfo
                .Types
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
                dic[CompilationInfo.Compilation.ObjectType]);

            if (CompilationInfo
                .Types
                .Any(f => f.TypeKind == TypeKind.Interface))
            {
                doc.Add(
                    Heading2(Resources.GetPluralName(TypeKind.Interface)),
                    CompilationInfo
                        .Types
                        .Where(f => f.TypeKind == TypeKind.Interface)
                        .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                        .Select(f => BulletItem(GetBulletItemContent(f))));
            }

            return new DocumentationFile(doc.ToString(), WellKnownNames.ObjectModelFileName, DocumentationKind.ObjectModel);

            bool IsBottomType(ITypeSymbol s)
            {
                s = s.OriginalDefinition;

                foreach (INamedTypeSymbol symbol in CompilationInfo.Types)
                {
                    if (symbol.InheritsFrom(s))
                        return false;
                }

                return true;
            }

            object GetBulletItemContent(INamedTypeSymbol namedTypeSymbol)
            {
                SymbolDocumentationInfo symbolInfo = GetSymbolInfo(namedTypeSymbol);

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

                                    string url = GetSymbolInfo(symbol).GetUrl(FileName);

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
                    string url = symbolInfo.GetUrl(FileName);

                    return LinkOrText(symbolInfo.Symbol.ToDisplayString(FormatProvider.TypeFormat), url);
                }
            }
        }
    }
}
