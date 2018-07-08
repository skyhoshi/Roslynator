// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    //TODO: DocumentationMetadata
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

        public IEnumerable<IMethodSymbol> GetExtensionMethodSymbols(ITypeSymbol typeSymbol)
        {
            foreach (IMethodSymbol methodSymbol in ExtensionMethodSymbols)
            {
                ITypeSymbol typeSymbol2 = methodSymbol.Parameters[0].Type.OriginalDefinition;

                if (typeSymbol.Kind == SymbolKind.TypeParameter)
                {
                    var typeParameter = (ITypeParameterSymbol)typeSymbol;

                    typeSymbol = typeParameter.ConstraintTypes.First(f => f.TypeKind == TypeKind.Class);
                }

                if (typeSymbol == typeSymbol2)
                    yield return methodSymbol;
            }
        }

        public IEnumerable<ITypeSymbol> GetExtendedExternalTypes()
        {
            return Iterator().Distinct();

            IEnumerable<ITypeSymbol> Iterator()
            {
                foreach (IMethodSymbol methodSymbol in ExtensionMethodSymbols)
                {
                    if (IsExternalSymbol(methodSymbol))
                    {
                        ITypeSymbol typeSymbol = methodSymbol.Parameters[0].Type.OriginalDefinition;

                        if (typeSymbol is ITypeParameterSymbol typeParameterSymbol)
                        {
                            yield return typeParameterSymbol.ConstraintTypes.First(f => f.TypeKind == TypeKind.Class);
                        }
                        else
                        {
                            yield return typeSymbol;
                        }
                    }
                }
            }

            bool IsExternalSymbol(IMethodSymbol methodSymbol)
            {
                foreach (AssemblyDocumentationInfo assemblyInfo in Assemblies)
                {
                    ITypeSymbol type = methodSymbol.Parameters[0].Type.OriginalDefinition;

                    if (type.Kind == SymbolKind.TypeParameter)
                    {
                        var typeParameter = (ITypeParameterSymbol)type;

                        //TODO: T > T2 where T : Foo
                        type = typeParameter.ConstraintTypes.FirstOrDefault(f => f.TypeKind == TypeKind.Class);

                        if (type == null)
                            return false;
                    }

                    if (assemblyInfo.AssemblySymbol == type.ContainingAssembly)
                        return false;
                }

                return true;
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
                        xmlDocumentation = XmlDocumentation.Load(xmlDocPath);
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
