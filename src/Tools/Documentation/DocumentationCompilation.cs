// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationCompilation
    {
        private ImmutableArray<ITypeSymbol> _typeSymbols;
        private ImmutableArray<IMethodSymbol> _extensionMethodSymbols;

        private readonly Dictionary<ISymbol, SymbolDocumentationInfo> _symbolDocumentationCache;
        private readonly Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public DocumentationCompilation(Compilation compilation, ImmutableArray<AssemblyDocumentationInfo> assemblies)
        {
            Compilation = compilation;
            Assemblies = assemblies;

            _xmlDocumentations = Assemblies.ToDictionary(f => f.AssemblySymbol, f => f.GetXmlDocumentation());
            _symbolDocumentationCache = new Dictionary<ISymbol, SymbolDocumentationInfo>();
        }

        public Compilation Compilation { get; }

        public ImmutableArray<AssemblyDocumentationInfo> Assemblies { get; }

        public IEnumerable<INamespaceSymbol> NamespaceSymbols
        {
            get
            {
                return TypeSymbols
                    .Select(f => f.ContainingNamespace)
                    .Distinct();
            }
        }

        //TODO: INamedTypeSymbol
        public ImmutableArray<ITypeSymbol> TypeSymbols
        {
            get
            {
                if (_typeSymbols.IsDefault)
                {
                    _typeSymbols = Assemblies
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

        public IEnumerable<ITypeSymbol> GetTypeSymbols(INamespaceSymbol namespaceSymbol)
        {
            foreach (ITypeSymbol typeSymbol in TypeSymbols)
            {
                if (typeSymbol.ContainingNamespace == namespaceSymbol)
                    yield return typeSymbol;
            }
        }

        internal SymbolDocumentationInfo GetDocumentationInfo(ISymbol symbol)
        {
            if (_symbolDocumentationCache.TryGetValue(symbol, out SymbolDocumentationInfo info))
                return info;

            info = SymbolDocumentationInfo.Create(symbol, this);

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
