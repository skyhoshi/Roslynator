// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolExtensions
    {
        public static ImmutableArray<INamedTypeSymbol> GetTypes(this IAssemblySymbol assemblySymbol, Func<INamedTypeSymbol, bool> predicate = null)
        {
            ImmutableArray<INamedTypeSymbol>.Builder builder = ImmutableArray.CreateBuilder<INamedTypeSymbol>();

            GetTypes(assemblySymbol.GlobalNamespace);

            return builder.ToImmutableArray();

            void GetTypes(INamespaceOrTypeSymbol namespaceOrTypeSymbol)
            {
                if (namespaceOrTypeSymbol is INamedTypeSymbol namedTypeSymbol
                    && (predicate == null || predicate(namedTypeSymbol)))
                {
                    builder.Add(namedTypeSymbol);
                }

                foreach (ISymbol memberSymbol in namespaceOrTypeSymbol.GetMembers())
                {
                    if (memberSymbol is INamespaceOrTypeSymbol namespaceOrTypeSymbol2)
                    {
                        GetTypes(namespaceOrTypeSymbol2);
                    }
                }
            }
        }

        public static ImmutableArray<ISymbol> GetMembers(this ITypeSymbol typeSymbol, Func<ISymbol, bool> predicate, bool includeInherited = false)
        {
            if (includeInherited)
            {
                return GetMembersIncludingInherited(typeSymbol, predicate);
            }
            else if (predicate != null)
            {
                return typeSymbol
                    .GetMembers()
                    .Where(predicate)
                    .ToImmutableArray();
            }

            return typeSymbol.GetMembers();
        }

        private static ImmutableArray<ISymbol> GetMembersIncludingInherited(ITypeSymbol typeSymbol, Func<ISymbol, bool> predicate = null)
        {
            ImmutableArray<ISymbol>.Builder builder = ImmutableArray.CreateBuilder<ISymbol>();

            HashSet<ISymbol> overriddenSymbols = null;

            foreach (ISymbol symbol in GetMembers(typeSymbol, predicate: predicate))
            {
                ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                if (overriddenSymbol != null)
                {
                    (overriddenSymbols ?? (overriddenSymbols = new HashSet<ISymbol>())).Add(overriddenSymbol);
                }

                builder.Add(symbol);
            }

            INamedTypeSymbol baseType = typeSymbol.BaseType;

            while (baseType != null)
            {
                foreach (ISymbol symbol in baseType.GetMembers())
                {
                    if (!symbol.IsStatic
                        && (predicate == null || predicate(symbol)))
                    {
                        if (overriddenSymbols?.Remove(symbol) != true)
                            builder.Add(symbol);

                        ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                        if (overriddenSymbol != null)
                        {
                            (overriddenSymbols ?? (overriddenSymbols = new HashSet<ISymbol>())).Add(overriddenSymbol);
                        }
                    }
                }

                baseType = baseType.BaseType;
            }

            return builder.ToImmutableArray();
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
    }
}
