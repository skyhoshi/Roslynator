// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolExtensions
    {
        public static ImmutableArray<ITypeSymbol> GetPubliclyVisibleTypes(this IAssemblySymbol assemblySymbol)
        {
            ImmutableArray<ITypeSymbol>.Builder builder = ImmutableArray.CreateBuilder<ITypeSymbol>();

            GetPubliclyVisibleNamespacesAndTypes(assemblySymbol.GlobalNamespace);

            return builder.ToImmutableArray();

            void GetPubliclyVisibleNamespacesAndTypes(INamespaceOrTypeSymbol namespaceOrTypeSymbol)
            {
                if (namespaceOrTypeSymbol is ITypeSymbol typeSymbol
                    && typeSymbol.IsPubliclyVisible())
                {
                    builder.Add(typeSymbol);
                }

                foreach (ISymbol memberSymbol in namespaceOrTypeSymbol.GetMembers())
                {
                    if (memberSymbol is INamespaceOrTypeSymbol namespaceOrTypeSymbol2)
                    {
                        GetPubliclyVisibleNamespacesAndTypes(namespaceOrTypeSymbol2);
                    }
                }
            }
        }

        public static int GetArity(this ISymbol symbol)
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

        public static ImmutableArray<ITypeParameterSymbol> GetTypeParameters(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).TypeParameters;
                case SymbolKind.NamedType:
                    return ((INamedTypeSymbol)symbol).TypeParameters;
            }

            return ImmutableArray<ITypeParameterSymbol>.Empty;
        }

        public static ISymbol GetFirstExplicitInterfaceImplementation(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return ((IEventSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
                case SymbolKind.Property:
                    return ((IPropertySymbol)symbol).ExplicitInterfaceImplementations.FirstOrDefault();
            }

            return null;
        }

        public static ISymbol OverriddenSymbol(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    return ((IMethodSymbol)symbol).OverriddenMethod;
                case SymbolKind.Property:
                    return ((IPropertySymbol)symbol).OverriddenProperty;
                case SymbolKind.Event:
                    return ((IEventSymbol)symbol).OverriddenEvent;
            }

            return null;
        }

        internal static string GetName(this ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return "Event";
                case SymbolKind.Field:
                    return "Field";
                case SymbolKind.Method:
                    return "Method";
                case SymbolKind.Namespace:
                    return "Namespace";
                case SymbolKind.Property:
                    return "Property";
                case SymbolKind.NamedType:
                    {
                        switch (((ITypeSymbol)symbol).TypeKind)
                        {
                            case TypeKind.Class:
                                return "Class";
                            case TypeKind.Delegate:
                                return "Delegate";
                            case TypeKind.Enum:
                                return "Enum";
                            case TypeKind.Interface:
                                return "Interface";
                            case TypeKind.Struct:
                                return "Struct";
                        }

                        break;
                    }
            }

            throw new InvalidOperationException();
        }
    }
}
