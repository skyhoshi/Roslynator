// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;
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

        public IEnumerable<DocumentationFile> GenerateFiles(string heading = null)
        {
            yield return GenerateRootFile(heading);

            foreach (DocumentationFile namespaceFile in GenerateNamespaceFiles())
                yield return namespaceFile;

            foreach (ITypeSymbol typeSymbol in TypeSymbols)
            {
                yield return GenerateTypeFile(typeSymbol);

                foreach (DocumentationFile memberFile in GenerateMemberFiles(typeSymbol))
                    yield return memberFile;
            }
        }

        public DocumentationFile GenerateRootFile(string heading = null)
        {
            using (var writer = new DocumentationMarkdownWriter(null, null, this))
            {
                if (heading != null)
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
                    writer.WriteNamespaceContent(grouping, 3);
                }

                return new DocumentationFile(writer.ToString(), null, DocumentationKind.Root);
            }
        }

        public IEnumerable<DocumentationFile> GenerateNamespaceFiles()
        {
            foreach (INamespaceSymbol namespaceSymbol in TypeSymbols
                .Select(f => f.ContainingNamespace)
                .Distinct())
            {
                yield return GenerateNamespaceFile(namespaceSymbol);
            }
        }

        public DocumentationFile GenerateNamespaceFile(INamespaceSymbol namespaceSymbol)
        {
            SymbolDocumentationInfo info = GetDocumentationInfo(namespaceSymbol);

            using (var writer = new DocumentationMarkdownWriter(null, info, this))
            {
                writer.WriteStartHeading(1);
                writer.WriteString(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat));
                writer.WriteString(" Namespace");
                writer.WriteEndHeading();
                writer.WriteNamespaceContent(TypeSymbols.Where(f => f.ContainingNamespace == namespaceSymbol), 2);

                return DocumentationFile.Create(writer.ToString(), info, DocumentationKind.Namespace);
            }
        }

        public DocumentationFile GenerateTypeFile(ITypeSymbol typeSymbol)
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

            SymbolDocumentationInfo info = GetDocumentationInfo(typeSymbol);

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
                ImmutableArray<ISymbol> membersByName = grouping.ToImmutableArray();

                ISymbol symbol = membersByName[0];
                SymbolDocumentationInfo info = GetDocumentationInfo(symbol);

                using (MemberDocumentationWriter writer = MemberDocumentationWriter.Create(membersByName, info, this))
                {
                    writer.WriteMember();

                    yield return new DocumentationFile(writer.ToString(), string.Join(@"\", info.Names.Reverse()), DocumentationKind.Member);
                }
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
