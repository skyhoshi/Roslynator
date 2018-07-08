// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class SymbolDocumentationInfo
    {
        private ImmutableArray<ISymbol> _publiclyVisibleMembers;

        private ImmutableArray<ISymbol> _allPubliclyVisibleMembers;

        private SymbolDocumentationInfo(
            ISymbol symbol,
            string commentId,
            ImmutableArray<ISymbol> members,
            ImmutableArray<ISymbol> symbols,
            ImmutableArray<string> names,
            CompilationDocumentationInfo compilation)
        {
            Symbol = symbol;
            CommentId = commentId;
            Members = members;
            Symbols = symbols;
            Names = names;
            Compilation = compilation;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        private ImmutableArray<ISymbol> Symbols { get; }

        internal ImmutableArray<string> Names { get; }

        public CompilationDocumentationInfo Compilation { get; }

        public bool IsExternal
        {
            get
            {
                foreach (AssemblyDocumentationInfo assemblyInfo in Compilation.Assemblies)
                {
                    if (Symbol.ContainingAssembly == assemblyInfo.AssemblySymbol)
                        return false;
                }

                return true;
            }
        }

        public ImmutableArray<ISymbol> Members { get; }

        public ImmutableArray<ISymbol> PubliclyVisibleMembers
        {
            get
            {
                if (_publiclyVisibleMembers.IsDefault)
                {
                    _publiclyVisibleMembers = Members
                        .Where(f => f.IsPubliclyVisible())
                        .ToImmutableArray();
                }

                return _publiclyVisibleMembers;
            }
        }

        public ImmutableArray<ISymbol> AllPubliclyVisibleMembers
        {
            get
            {
                if (Symbol.IsStatic)
                    return PubliclyVisibleMembers;

                if (_allPubliclyVisibleMembers.IsDefault)
                {
                    ImmutableArray<ISymbol>.Builder builder = ImmutableArray.CreateBuilder<ISymbol>();

                    HashSet<ISymbol> overriddenSymbols = null;

                    foreach (ISymbol symbol in PubliclyVisibleMembers)
                    {
                        ISymbol overriddenSymbol = symbol.OverriddenSymbol();

                        if (overriddenSymbol != null)
                        {
                            (overriddenSymbols ?? (overriddenSymbols = new HashSet<ISymbol>())).Add(overriddenSymbol);
                        }

                        builder.Add(symbol);
                    }

                    INamedTypeSymbol baseType = (Symbol as ITypeSymbol)?.BaseType;

                    while (baseType != null)
                    {
                        foreach (ISymbol symbol in baseType.GetMembers())
                        {
                            if (!symbol.IsStatic
                                && symbol.IsPubliclyVisible())
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

                    _allPubliclyVisibleMembers = builder.ToImmutableArray();
                }

                return _allPubliclyVisibleMembers;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
        }

        internal static SymbolDocumentationInfo Create(CompilationDocumentationInfo compilation)
        {
            return new SymbolDocumentationInfo(
                symbol: null,
                commentId: null,
                ImmutableArray<ISymbol>.Empty,
                ImmutableArray<ISymbol>.Empty,
                ImmutableArray<string>.Empty,
                compilation);
        }

        public static SymbolDocumentationInfo Create(ISymbol symbol, CompilationDocumentationInfo compilation)
        {
            ImmutableArray<ISymbol> members = (symbol is ITypeSymbol typeSymbol)
                ? typeSymbol.GetMembers()
                : ImmutableArray<ISymbol>.Empty;

            return Create(symbol, members, compilation);
        }

        private static SymbolDocumentationInfo Create(ISymbol symbol, ImmutableArray<ISymbol> members, CompilationDocumentationInfo  compilation)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            if (symbol.Kind == SymbolKind.Namespace
                && ((INamespaceSymbol)symbol).IsGlobalNamespace)
            {
                names.Add("_Global");
            }
            else if (symbol.Kind == SymbolKind.Method
                && ((IMethodSymbol)symbol).MethodKind == MethodKind.Constructor)
            {
                names.Add("-ctor");
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
                        names.Add("_Global");
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
                members,
                symbols.ToImmutableArray(),
                names.ToImmutableArray(),
                compilation);
        }

        internal string GetUrl(SymbolDocumentationInfo directoryInfo = null, bool useExternalLink = true)
        {
            if (useExternalLink
                && IsExternal)
            {
                if (Symbols.LastOrDefault()?.Kind == SymbolKind.Namespace)
                {
                    switch (Names.Last())
                    {
                        case "System":
                        case "Microsoft":
                            return "https://docs.microsoft.com/en-us/dotnet/api/" + string.Join(".", Names.Select(f => f.ToLowerInvariant()).Reverse());
                    }
                }

                return null;
            }

            if (directoryInfo == null)
                return string.Join("/", Names.Reverse()) + "/README.md";

            if (this == directoryInfo)
                return "./README.md";

            int count = 0;

            int i = Symbols.Length - 1;
            int j = directoryInfo.Symbols.Length - 1;

            while (i >= 0
                && j >= 0
                && Symbols[i] == directoryInfo.Symbols[j])
            {
                count++;
                i--;
                j--;
            }

            int diff = directoryInfo.Symbols.Length - count;

            var sb = new StringBuilder();

            if (diff > 0)
            {
                sb.Append("..");
                diff--;

                while (diff > 0)
                {
                    sb.Append("/..");
                    diff--;
                }
            }

            i = Names.Length - 1 - count;

            if (i >= 0)
            {
                if (sb.Length > 0)
                    sb.Append("/");

                sb.Append(Names[i]);
                i--;

                while (i >= 0)
                {
                    sb.Append("/");
                    sb.Append(Names[i]);
                    i--;
                }
            }

            sb.Append("/README.md");

            return sb.ToString();
        }

        public IEnumerable<IFieldSymbol> GetFields(bool includeInherited = false)
        {
            foreach (ISymbol member in ((includeInherited) ? AllPubliclyVisibleMembers : PubliclyVisibleMembers))
            {
                if (member.Kind == SymbolKind.Field)
                {
                    yield return (IFieldSymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetConstructors()
        {
            foreach (ISymbol member in PubliclyVisibleMembers)
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
            foreach (ISymbol member in ((includeInherited) ? AllPubliclyVisibleMembers : PubliclyVisibleMembers))
            {
                if (member.Kind == SymbolKind.Property)
                {
                    yield return (IPropertySymbol)member;
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetMethods(bool includeInherited = false)
        {
            foreach (ISymbol member in ((includeInherited) ? AllPubliclyVisibleMembers : PubliclyVisibleMembers))
            {
                if (member.Kind == SymbolKind.Method)
                {
                    var methodSymbol = (IMethodSymbol)member;

                    if (methodSymbol.MethodKind == MethodKind.Ordinary)
                    {
                        yield return methodSymbol;
                    }
                }
            }
        }

        public IEnumerable<IMethodSymbol> GetOperators(bool includeInherited = false)
        {
            foreach (ISymbol member in ((includeInherited) ? AllPubliclyVisibleMembers : PubliclyVisibleMembers))
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
            foreach (ISymbol member in ((includeInherited) ? AllPubliclyVisibleMembers : PubliclyVisibleMembers))
            {
                if (member.Kind == SymbolKind.Event)
                {
                    yield return (IEventSymbol)member;
                }
            }
        }

        public IEnumerable<ISymbol> GetExplicitInterfaceImplementations()
        {
            foreach (ISymbol member in Members)
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

        public IEnumerable<IMethodSymbol> GetExtensionMethods()
        {
            if (Symbol.Kind == SymbolKind.NamedType
                && Symbol.IsStatic
                && Symbol.ContainingType == null)
            {
                foreach (ISymbol member in PubliclyVisibleMembers)
                {
                    if (member.Kind == SymbolKind.Method
                        && member.IsStatic)
                    {
                        var methodSymbol = (IMethodSymbol)member;

                        if (methodSymbol.IsExtensionMethod)
                        {
                            yield return methodSymbol;
                        }
                    }
                }
            }
        }
    }
}
