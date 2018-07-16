// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class CompilationDocumentationInfo
    {
        private ImmutableArray<INamedTypeSymbol> _typeSymbols;
        private ImmutableArray<IMethodSymbol> _extensionMethodSymbols;

        private readonly Dictionary<ISymbol, SymbolDocumentationInfo> _symbolDocumentationInfos;
        private Dictionary<IAssemblySymbol, XmlDocumentation> _xmlDocumentations;

        public CompilationDocumentationInfo(Compilation compilation, IEnumerable<AssemblyDocumentationInfo> assemblies)
        {
            Compilation = compilation;
            Assemblies = ImmutableArray.CreateRange(assemblies);

            _symbolDocumentationInfos = new Dictionary<ISymbol, SymbolDocumentationInfo>();
        }

        public Compilation Compilation { get; }

        public ImmutableArray<AssemblyDocumentationInfo> Assemblies { get; }

        public virtual bool IsVisible(ISymbol symbol)
        {
            return symbol.IsPubliclyVisible();
        }

        public IEnumerable<INamespaceSymbol> Namespaces
        {
            get
            {
                return Types
                    .Select(f => f.ContainingNamespace)
                    .Distinct(MetadataNameEqualityComparer<INamespaceSymbol>.Instance);
            }
        }

        public ImmutableArray<INamedTypeSymbol> Types
        {
            get
            {
                if (_typeSymbols.IsDefault)
                {
                    _typeSymbols = Assemblies
                        .SelectMany(f => f.AssemblySymbol.GetTypes(typeSymbol => IsVisible(typeSymbol)))
                        .ToImmutableArray();
                }

                return _typeSymbols;
            }
        }

        public ImmutableArray<IMethodSymbol> ExtensionMethods
        {
            get
            {
                if (_extensionMethodSymbols.IsDefault)
                {
                    _extensionMethodSymbols = Types
                        .Where(f => f.TypeKind == TypeKind.Class
                            && f.IsStatic
                            && f.ContainingType == null)
                        .SelectMany(f => f.GetMembers())
                        .Where(f => f.Kind == SymbolKind.Method
                            && f.IsStatic
                            && IsVisible(f))
                        .Cast<IMethodSymbol>()
                        .Where(f => f.IsExtensionMethod)
                        .ToImmutableArray();
                }

                return _extensionMethodSymbols;
            }
        }

        public IEnumerable<INamedTypeSymbol> GetTypes(INamespaceSymbol namespaceSymbol)
        {
            foreach (INamedTypeSymbol typeSymbol in Types)
            {
                if (typeSymbol.ContainingNamespace == namespaceSymbol)
                    yield return typeSymbol;
            }
        }

        public IEnumerable<IMethodSymbol> GetExtensionMethods(ITypeSymbol typeSymbol)
        {
            foreach (IMethodSymbol methodSymbol in ExtensionMethods)
            {
                ITypeSymbol typeSymbol2 = GetExtendedTypeSymbol(methodSymbol);

                if (typeSymbol == typeSymbol2)
                    yield return methodSymbol;
            }
        }

        public IEnumerable<ITypeSymbol> GetExtendedExternalTypes()
        {
            return Iterator().Distinct();

            IEnumerable<ITypeSymbol> Iterator()
            {
                foreach (IMethodSymbol methodSymbol in ExtensionMethods)
                {
                    ITypeSymbol typeSymbol = GetExternalSymbol(methodSymbol);

                    if (typeSymbol != null)
                        yield return typeSymbol;
                }
            }

            ITypeSymbol GetExternalSymbol(IMethodSymbol methodSymbol)
            {
                ITypeSymbol type = GetExtendedTypeSymbol(methodSymbol);

                if (type == null)
                    return null;

                foreach (AssemblyDocumentationInfo assemblyInfo in Assemblies)
                {
                    if (type.ContainingAssembly == assemblyInfo.AssemblySymbol)
                        return null;
                }

                return type;
            }
        }

        private static ITypeSymbol GetExtendedTypeSymbol(IMethodSymbol methodSymbol)
        {
            ITypeSymbol type = methodSymbol.Parameters[0].Type.OriginalDefinition;

            if (type.Kind == SymbolKind.TypeParameter)
            {
                return GetTypeParameterConstraintClass((ITypeParameterSymbol)type);
            }
            else
            {
                return type;
            }

            ITypeSymbol GetTypeParameterConstraintClass(ITypeParameterSymbol typeParameter)
            {
                foreach (ITypeSymbol constraintType in typeParameter.ConstraintTypes)
                {
                    if (constraintType.TypeKind == TypeKind.Class)
                    {
                        return constraintType;
                    }
                    else if (constraintType.TypeKind == TypeKind.TypeParameter)
                    {
                        return GetTypeParameterConstraintClass((ITypeParameterSymbol)constraintType);
                    }
                }

                return null;
            }
        }

        public IEnumerable<INamedTypeSymbol> GetDerivedTypes(ITypeSymbol typeSymbol, bool includeInterfaces = false)
        {
            foreach (INamedTypeSymbol type in Types)
            {
                if (type.InheritsFrom(typeSymbol, includeInterfaces: includeInterfaces))
                    yield return type;
            }
        }

        public bool IsExternalSymbol(ISymbol symbol)
        {
            foreach (AssemblyDocumentationInfo assemblyInfo in Assemblies)
            {
                if (symbol.ContainingAssembly == assemblyInfo.AssemblySymbol)
                    return false;
            }

            return true;
        }

        internal SymbolDocumentationInfo GetSymbolInfo(ISymbol symbol)
        {
            if (_symbolDocumentationInfos.TryGetValue(symbol, out SymbolDocumentationInfo info))
                return info;

            info = SymbolDocumentationInfo.Create(symbol, this);

            _symbolDocumentationInfos[symbol] = info;

            return info;
        }

        internal ISymbol GetFirstSymbolForDeclarationId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForDeclarationId(id, Compilation);
        }

        internal ISymbol GetFirstSymbolForReferenceId(string id)
        {
            return DocumentationCommentId.GetFirstSymbolForReferenceId(id, Compilation);
        }

        internal XmlDocumentation GetXmlDocumentation(IAssemblySymbol assemblySymbol)
        {
            if (_xmlDocumentations == null)
                _xmlDocumentations = Assemblies.ToDictionary(f => f.AssemblySymbol, f => f.GetXmlDocumentation());

            if (!_xmlDocumentations.TryGetValue(assemblySymbol, out XmlDocumentation xmlDocumentation))
            {
                //TODO: find xml documentation file for an assembly
            }

            return xmlDocumentation;
        }

        internal SymbolXmlDocumentation GetDocumentation(ISymbol symbol)
        {
            return GetXmlDocumentation(symbol.ContainingAssembly)?.GetDocumentation(GetSymbolInfo(symbol).CommentId);
        }
    }
}
