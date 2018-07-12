// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class DocumentationGenerator
    {
        private SymbolDocumentationInfo _emptySymbolInfo;

        protected DocumentationGenerator(
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

        private DocumentationWriter CreateWriter(SymbolDocumentationInfo symbolInfo, SymbolDocumentationInfo directoryInfo)
        {
            DocumentationWriter writer = CreateWriterCore(symbolInfo, directoryInfo);

            writer.CanCreateMemberLocalUrl = Options.IsEnabled(DocumentationParts.Member);
            writer.CanCreateTypeLocalUrl = Options.IsEnabled(DocumentationParts.Type);

            return writer;
        }

        protected abstract DocumentationWriter CreateWriterCore(SymbolDocumentationInfo symbolInfo, SymbolDocumentationInfo directoryInfo);

        internal SymbolDocumentationInfo GetSymbolInfo(ISymbol symbol)
        {
            return CompilationInfo.GetSymbolInfo(symbol);
        }

        public IEnumerable<DocumentationFile> GenerateFiles(
            string heading = null,
            string objectModelHeading = null,
            string extendedTypesHeading = null)
        {
            DocumentationParts parts = Options.Parts;

            using (DocumentationWriter writer = CreateWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null))
            {
                DocumentationFile objectModelFile = default;
                DocumentationFile extendedTypesFile = default;

                if ((parts & DocumentationParts.ObjectModel) != 0)
                {
                    objectModelFile = GenerateObjectModel(objectModelHeading ?? Resources.ObjectModelTitle);

                    if (!objectModelFile.HasContent)
                        parts &= ~DocumentationParts.ObjectModel;
                }

                if ((parts & DocumentationParts.ExtendedExternalTypes) != 0)
                {
                    extendedTypesFile = GenerateExtendedExternalTypesFile(extendedTypesHeading ?? Resources.ExtendedExternalTypesTitle);

                    if (!extendedTypesFile.HasContent)
                        parts &= ~DocumentationParts.ExtendedExternalTypes;
                }

                yield return GenerateRootFile(writer, heading, parts);

                bool generateTypes = (parts & DocumentationParts.Type) != 0;

                if ((parts & DocumentationParts.Namespace) != 0)
                {
                    foreach (INamespaceSymbol namespaceSymbol in CompilationInfo.Namespaces)
                        yield return GenerateNamespaceFile(namespaceSymbol);
                }

                if (generateTypes)
                {
                    bool generateMembers = (parts & DocumentationParts.Member) != 0;

                    foreach (INamedTypeSymbol typeSymbol in CompilationInfo.Types)
                    {
                        yield return GenerateTypeFile(typeSymbol);

                        if (generateMembers)
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

                    foreach (ITypeSymbol typeSymbol in CompilationInfo.GetExtendedExternalTypes())
                        yield return GenerateExtendedTypeFile(typeSymbol);
                }
            }
        }

        private DocumentationFile GenerateRootFile(
            DocumentationWriter writer,
            string heading,
            DocumentationParts documentationParts)
        {
            if (heading != null)
                writer.WriteHeading1(heading);

            if ((documentationParts & DocumentationParts.ObjectModel) != 0
                && Options.IsEnabled(RootDocumentationParts.ObjectModelLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ObjectModelTitle, WellKnownNames.ObjectModelFileName);
                writer.WriteEndBulletItem();
            }

            if ((documentationParts & DocumentationParts.ExtendedExternalTypes) != 0
                && Options.IsEnabled(RootDocumentationParts.ExtendedTypesLink))
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(Resources.ExtendedExternalTypesTitle, WellKnownNames.ExtendedExternalTypesFileName);
                writer.WriteEndBulletItem();
            }

            if (Options.IsEnabled(RootDocumentationParts.NamespaceList))
            {
                writer.WriteList(CompilationInfo.Namespaces, Resources.NamespacesTitle, 2, FormatProvider.NamespaceFormat);
            }

            if (Options.IsEnabled(RootDocumentationParts.Namespaces))
            {
                foreach (IGrouping<INamespaceSymbol, INamedTypeSymbol> grouping in CompilationInfo.Types
                   .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                   .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                {
                    writer.WriteHeading(2, grouping.Key, FormatProvider.NamespaceFormat);

                    GenerateNamespaceContent(writer, grouping, 3, addLink: Options.IsEnabled(DocumentationParts.Type));
                }
            }

            return new DocumentationFile(writer.ToString(), path: FileName, DocumentationKind.Root);
        }

        private DocumentationFile GenerateNamespaceFile(INamespaceSymbol namespaceSymbol)
        {
            SymbolDocumentationInfo symbolInfo = GetSymbolInfo(namespaceSymbol);

            using (DocumentationWriter writer = CreateWriter(symbolInfo: EmptySymbolInfo, symbolInfo))
            {
                if (Options.IsEnabled(NamespaceDocumentationParts.Heading))
                {
                    writer.WriteHeading(1, namespaceSymbol, FormatProvider.NamespaceFormat, addLink: false);
                }

                GenerateNamespaceContent(writer, CompilationInfo.GetTypes(namespaceSymbol), 2, addLink: Options.IsEnabled(DocumentationParts.Type));

                return DocumentationFile.Create(writer, symbolInfo, FileName, DocumentationKind.Namespace);
            }
        }

        private void GenerateNamespaceContent(
            DocumentationWriter writer,
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel,
            bool addLink)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                .GroupBy(f => f.TypeKind)
                .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), Options.NamespacePartComparer))
            {
                TypeKind typeKind = grouping.Key;

                writer.WriteTable(
                    grouping,
                    Resources.GetPluralName(typeKind),
                    headingLevel,
                    Resources.GetName(typeKind),
                    Resources.SummaryTitle,
                    format,
                    addLink: addLink);
            }
        }

        private DocumentationFile GenerateExtendedExternalTypesFile(string heading = null)
        {
            using (DocumentationWriter writer = CreateWriter(symbolInfo: EmptySymbolInfo, directoryInfo: null))
            {
                IEnumerable<INamespaceSymbol> namespaces = CompilationInfo.GetExtendedExternalTypes()
                    .Select(f => f.ContainingNamespace)
                    .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);

                if (namespaces.Any())
                    writer.WriteHeading1(heading);

                writer.WriteList(namespaces, Resources.NamespacesTitle, 2, FormatProvider.NamespaceFormat);

                if (Options.IsEnabled(RootDocumentationParts.Namespaces))
                {
                    foreach (IGrouping<INamespaceSymbol, ITypeSymbol> grouping in CompilationInfo.GetExtendedExternalTypes()
                       .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                       .GroupBy(f => f.ContainingNamespace, MetadataNameEqualityComparer<INamespaceSymbol>.Instance)
                       .OrderBy(f => f.Key.ToDisplayString(FormatProvider.NamespaceFormat)))
                    {
                        INamespaceSymbol namespaceSymbol = grouping.Key;

                        writer.WriteHeading(2, namespaceSymbol, FormatProvider.NamespaceFormat);

                        foreach (IGrouping<TypeKind, ITypeSymbol> grouping2 in grouping
                            .Where(f => Options.IsNamespacePartEnabled(f.TypeKind))
                            .GroupBy(f => f.TypeKind)
                            .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), Options.NamespacePartComparer))
                        {
                            writer.WriteList(grouping2, Resources.GetPluralName(grouping2.Key), 3, FormatProvider.TypeFormat, canCreateExternalUrl: false);
                        }
                    }
                }

                return new DocumentationFile(writer.ToString(), WellKnownNames.ExtendedExternalTypesFileName, DocumentationKind.ExtendedExternalTypes);
            }
        }

        private DocumentationFile GenerateExtendedTypeFile(ITypeSymbol typeSymbol)
        {
            SymbolDocumentationInfo symbolInfo = GetSymbolInfo(typeSymbol);

            using (DocumentationWriter writer = CreateWriter(symbolInfo, symbolInfo))
            {
                writer.WriteStartHeading(1 + writer.BaseHeadingLevel);
                writer.WriteLink(symbolInfo, FormatProvider.TitleFormat);
                writer.WriteSpace();
                writer.WriteString(Resources.GetName(typeSymbol.TypeKind));
                writer.WriteSpace();
                writer.WriteString(Resources.ExtensionsTitle);
                writer.WriteEndHeading();

                IEnumerable<IMethodSymbol> extensionMethods = CompilationInfo.GetExtensionMethods(typeSymbol);

                writer.WriteTable(
                    extensionMethods,
                    heading: null,
                    headingLevel: -1,
                    Resources.ExtensionMethodTitle,
                    Resources.SummaryTitle,
                    FormatProvider.MethodFormat);

                return DocumentationFile.Create(writer, symbolInfo, FileName, DocumentationKind.Type);
            }
        }

        private DocumentationFile GenerateTypeFile(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;
            bool isDelegateOrEnum = typeKind.Is(TypeKind.Delegate, TypeKind.Enum);

            SymbolDocumentationInfo info = GetSymbolInfo(typeSymbol);

            using (DocumentationWriter writer = CreateWriter(info, info))
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
                        case TypeDocumentationParts.Definition:
                            {
                                if (typeSymbol.Kind != SymbolKind.Namespace)
                                    writer.WriteDefinition(typeSymbol);

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

        private IEnumerable<DocumentationFile> GenerateMemberFiles(ITypeSymbol typeSymbol)
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
                ImmutableArray<ISymbol> symbols = grouping.ToImmutableArray();

                ISymbol symbol = symbols[0];
                SymbolDocumentationInfo symbolInfo = GetSymbolInfo(symbol);

                using (DocumentationWriter writer = CreateWriter(symbolInfo, symbolInfo))
                {
                    MemberDocumentationWriter memberWriter = MemberDocumentationWriter.Create(symbol, writer);

                    memberWriter.WriteMember(symbol, symbols);

                    yield return DocumentationFile.Create(writer, symbolInfo, FileName, DocumentationKind.Member);
                }
            }
        }

        public DocumentationFile GenerateObjectModel(string heading)
        {
            INamedTypeSymbol objectType = CompilationInfo.Compilation.ObjectType;

            var nodes = new HashSet<ITypeSymbol>(CompilationInfo.Types.Where(f => !f.IsStatic))
            {
                objectType
            };

            foreach (INamedTypeSymbol type in CompilationInfo.Types)
            {
                INamedTypeSymbol baseType = type.BaseType;

                while (baseType != null)
                {
                    nodes.Add(baseType.OriginalDefinition);
                    baseType = baseType.BaseType;
                }
            }

            using (DocumentationWriter writer = CreateWriter(_emptySymbolInfo, null))
            {
                writer.WriteHeading1(heading);

                WriteBulletItem(objectType, writer);

                using (IEnumerator<INamedTypeSymbol> en = CompilationInfo.Types
                    .Where(f => f.TypeKind == TypeKind.Interface)
                    .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat)).GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        writer.WriteHeading2(Resources.InterfacesTitle);

                        do
                        {
                            writer.WriteBulletItemLink(en.Current, FormatProvider.TypeFormat);
                        }
                        while (en.MoveNext());
                    }
                }

                return new DocumentationFile(writer.ToString(), WellKnownNames.ObjectModelFileName, DocumentationKind.ObjectModel);
            }

            void WriteBulletItem(ITypeSymbol baseType, DocumentationWriter writer)
            {
                writer.WriteStartBulletItem();
                writer.WriteLink(baseType, FormatProvider.TypeFormat);

                nodes.Remove(baseType);

                foreach (ITypeSymbol derivedType in nodes
                    .Where(f => f.BaseType?.OriginalDefinition == baseType.OriginalDefinition)
                    .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                    .ToList())
                {
                    WriteBulletItem(derivedType, writer);
                }

                writer.WriteEndBulletItem();
            }
        }
    }
}
