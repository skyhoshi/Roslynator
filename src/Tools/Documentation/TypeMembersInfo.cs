// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal readonly struct TypeMembersInfo
    {
        private TypeMembersInfo(ITypeSymbol typeSymbol, ImmutableArray<ISymbol> members)
        {
            TypeSymbol = typeSymbol;
            Members = members;
        }

        public ITypeSymbol TypeSymbol { get; }

        public ImmutableArray<ISymbol> Members { get; }

        public static TypeMembersInfo Create(ITypeSymbol typeSymbol)
        {
            return new TypeMembersInfo(typeSymbol, typeSymbol.GetMembers());
        }

        public IEnumerable<IFieldSymbol> GetFields()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Field
                    && member.IsPubliclyVisible())
                {
                    yield return (IFieldSymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetConstructors()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Method
                    && member.IsPubliclyVisible())
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind == MethodKind.Constructor)
                    {
                        if (methodSymbol.ContainingType.TypeKind != TypeKind.Struct
                            || methodSymbol.Parameters.Any())
                        {
                            yield return methodSymbol;
                        }
                    }
                }
            }
        }

        public IEnumerable<IPropertySymbol> GetProperties()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Property
                    && member.IsPubliclyVisible())
                {
                    yield return (IPropertySymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetMethods()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Method
                    && member.IsPubliclyVisible())
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind == MethodKind.Ordinary)
                    {
                        yield return methodSymbol;
                    }
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetOperators()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Method
                    && member.IsPubliclyVisible())
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind.Is(
                        MethodKind.UserDefinedOperator,
                        MethodKind.Conversion))
                    {
                        yield return methodSymbol;
                    }
                }
            }
        }

        public IEnumerable<IEventSymbol> GetEvents()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Event
                    && member.IsPubliclyVisible())
                {
                    yield return (IEventSymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetExplicitInterfaceImplementations()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Method
                    && member.IsPubliclyVisible())
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind == MethodKind.ExplicitInterfaceImplementation)
                    {
                        yield return methodSymbol;
                    }
                }
            }
        }
    }
}
