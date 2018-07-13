// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class SymbolDocumentationInfo
    {
        private ImmutableArray<ISymbol> _members;
        private ImmutableArray<ISymbol> _membersIncludingInherited;

        private SymbolDocumentationInfo(
            ISymbol symbol,
            string commentId,
            ImmutableArray<ISymbol> symbolAndBaseTypesAndNamespaces,
            ImmutableArray<string> nameAndBaseNamesAndNamespaceNames,
            CompilationDocumentationInfo compilationInfo)
        {
            Symbol = symbol;
            CommentId = commentId;
            SymbolAndBaseTypesAndNamespaces = symbolAndBaseTypesAndNamespaces;
            NameAndBaseNamesAndNamespaceNames = nameAndBaseNamesAndNamespaceNames;
            CompilationInfo = compilationInfo;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        internal ImmutableArray<ISymbol> SymbolAndBaseTypesAndNamespaces { get; }

        internal ImmutableArray<string> NameAndBaseNamesAndNamespaceNames { get; }

        internal CompilationDocumentationInfo CompilationInfo { get; }

        public bool IsExternal
        {
            get { return CompilationInfo.IsExternalSymbol(Symbol); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
        }

        public ImmutableArray<ISymbol> Members
        {
            get
            {
                if (_members.IsDefault)
                {
                    _members = (Symbol is ITypeSymbol typeSymbol)
                        ? typeSymbol.GetMembers(CompilationInfo.Predicate)
                        : ImmutableArray<ISymbol>.Empty;
                }

                return _members;
            }
        }

        public ImmutableArray<ISymbol> MembersIncludingInherited
        {
            get
            {
                if (_membersIncludingInherited.IsDefault)
                {
                    if (Symbol.IsStatic)
                    {
                        _membersIncludingInherited = Members;
                    }
                    else
                    {
                        _membersIncludingInherited = (Symbol is ITypeSymbol typeSymbol)
                            ? typeSymbol.GetMembers(CompilationInfo.Predicate, includeInherited: true)
                            : ImmutableArray<ISymbol>.Empty;
                    }
                }

                return _membersIncludingInherited;
            }
        }

        public ImmutableArray<ISymbol> GetMembers(bool includeInherited = false)
        {
            return (includeInherited) ? MembersIncludingInherited : Members;
        }

        internal static SymbolDocumentationInfo Create(CompilationDocumentationInfo compilation)
        {
            return new SymbolDocumentationInfo(
                symbol: null,
                commentId: null,
                symbolAndBaseTypesAndNamespaces: ImmutableArray<ISymbol>.Empty,
                nameAndBaseNamesAndNamespaceNames: ImmutableArray<string>.Empty,
                compilationInfo: compilation);
        }

        public static SymbolDocumentationInfo Create(ISymbol symbol, CompilationDocumentationInfo compilation)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            if (symbol.Kind == SymbolKind.Namespace
                && ((INamespaceSymbol)symbol).IsGlobalNamespace)
            {
                names.Add(WellKnownNames.GlobalNamespaceName);
            }
            else if (symbol.Kind == SymbolKind.Method
                && ((IMethodSymbol)symbol).MethodKind == MethodKind.Constructor)
            {
                names.Add(WellKnownNames.ConstructorName);
            }
            else if (symbol.Kind == SymbolKind.Property
                && ((IPropertySymbol)symbol).IsIndexer)
            {
                names.Add("Item");
            }
            else
            {
                ISymbol explicitImplementation = symbol.GetFirstExplicitInterfaceImplementation();

                if (explicitImplementation != null)
                {
                    string name = explicitImplementation
                        .ToDisplayParts(SymbolDisplayFormats.ExplicitImplementationFullName, SymbolDisplayAdditionalOptions.UseItemProperty)
                        .Where(part => part.Kind != SymbolDisplayPartKind.Space)
                        .Select(part => (part.IsPunctuation()) ? part.WithText("-") : part)
                        .ToImmutableArray()
                        .ToDisplayString();

                    names.Add(name);
                }
                else
                {
                    int arity = symbol.GetArity();

                    if (arity > 0)
                    {
                        names.Add(symbol.Name + "-" + arity.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        names.Add(symbol.Name);
                    }
                }
            }

            symbols.Add(symbol);

            INamedTypeSymbol containingType = symbol.ContainingType;

            while (containingType != null)
            {
                int arity = containingType.Arity;

                names.Add((arity > 0) ? containingType.Name + "-" + arity.ToString(CultureInfo.InvariantCulture) : containingType.Name);

                symbols.Add(containingType);

                containingType = containingType.ContainingType;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            if (containingNamespace != null)
            {
                if (containingNamespace.IsGlobalNamespace)
                {
                    if (symbol.Kind != SymbolKind.Namespace)
                    {
                        names.Add(WellKnownNames.GlobalNamespaceName);
                        symbols.Add(containingNamespace);
                    }
                }
                else
                {
                    do
                    {
                        names.Add(containingNamespace.Name);

                        symbols.Add(containingNamespace);

                        containingNamespace = containingNamespace.ContainingNamespace;
                    }
                    while (containingNamespace?.IsGlobalNamespace == false);
                }
            }

            return new SymbolDocumentationInfo(
                symbol,
                symbol.GetDocumentationCommentId(),
                symbols.ToImmutableArray(),
                names.ToImmutableArray(),
                compilation);
        }

        public IEnumerable<IFieldSymbol> GetFields(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Field)
                    yield return (IFieldSymbol)member;
            }
        }

        public IEnumerable<IMethodSymbol> GetConstructors()
        {
            foreach (ISymbol member in Members)
            {
                if (member.Kind == SymbolKind.Method)
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

        public IEnumerable<IPropertySymbol> GetProperties(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Property)
                    yield return (IPropertySymbol)member;
            }
        }

        public IEnumerable<IMethodSymbol> GetMethods(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Method)
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind == MethodKind.Ordinary)
                        yield return methodSymbol;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetOperators(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Method)
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

        public IEnumerable<IEventSymbol> GetEvents(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Event)
                    yield return (IEventSymbol)member;
            }
        }

        public IEnumerable<ISymbol> GetExplicitInterfaceImplementations()
        {
            if (!(Symbol is ITypeSymbol typeSymbol))
                yield break;

            foreach (ISymbol member in typeSymbol.GetMembers())
            {
                switch (member.Kind)
                {
                    case SymbolKind.Event:
                        {
                            var eventSymbol = (IEventSymbol)member;

                            if (!eventSymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                                yield return eventSymbol;

                            break;
                        }
                    case SymbolKind.Method:
                        {
                            var methodSymbol = (IMethodSymbol)member;

                            if (methodSymbol.MethodKind != MethodKind.ExplicitInterfaceImplementation)
                                break;

                            ImmutableArray<IMethodSymbol> explicitInterfaceImplementations = methodSymbol.ExplicitInterfaceImplementations;

                            if (explicitInterfaceImplementations.IsDefaultOrEmpty)
                                break;

                            if (methodSymbol.MetadataName.EndsWith(".get_Item", StringComparison.Ordinal))
                            {
                                if (explicitInterfaceImplementations[0].MethodKind == MethodKind.PropertyGet)
                                    break;
                            }
                            else if (methodSymbol.MetadataName.EndsWith(".set_Item", StringComparison.Ordinal))
                            {
                                if (explicitInterfaceImplementations[0].MethodKind == MethodKind.PropertySet)
                                    break;
                            }

                            yield return methodSymbol;
                            break;
                        }
                    case SymbolKind.Property:
                        {
                            var propertySymbol = (IPropertySymbol)member;

                            if (!propertySymbol.ExplicitInterfaceImplementations.IsDefaultOrEmpty)
                                yield return propertySymbol;

                            break;
                        }
                }
            }
        }
    }
}
