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
    //TODO: struct?
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class SymbolDocumentationInfo
    {
        private ImmutableArray<ISymbol> _publiclyVisibleMembers;

        private ImmutableArray<ISymbol> _publiclyVisibleMembersIncludingInherited;

        private SymbolDocumentationInfo(
            ISymbol symbol,
            string commentId,
            ImmutableArray<ISymbol> symbols,
            ImmutableArray<string> names,
            CompilationDocumentationInfo compilation)
        {
            Symbol = symbol;
            CommentId = commentId;
            Symbols = symbols;
            Names = names;
            CompilationInfo = compilation;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        private ImmutableArray<ISymbol> Symbols { get; }

        private ImmutableArray<string> Names { get; }

        internal CompilationDocumentationInfo CompilationInfo { get; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Symbol.Kind} {Symbol.ToDisplayString(Roslynator.SymbolDisplayFormats.Test)}"; }
        }

        public ImmutableArray<ISymbol> GetPubliclyVisibleMembers(bool includeInherited = false)
        {
            if (includeInherited)
            {
                if (_publiclyVisibleMembersIncludingInherited.IsDefault)
                {
                    if (Symbol.IsStatic)
                    {
                        _publiclyVisibleMembersIncludingInherited = GetPubliclyVisibleMembers();
                    }
                    else
                    {
                        _publiclyVisibleMembersIncludingInherited = (Symbol is ITypeSymbol typeSymbol)
                            ? typeSymbol.GetPubliclyVisibleMembers(includeInherited: true)
                            : ImmutableArray<ISymbol>.Empty;
                    }
                }

                return _publiclyVisibleMembersIncludingInherited;
            }
            else
            {
                if (_publiclyVisibleMembers.IsDefault)
                {
                    _publiclyVisibleMembers = (Symbol is ITypeSymbol typeSymbol)
                        ? typeSymbol.GetPubliclyVisibleMembers()
                        : ImmutableArray<ISymbol>.Empty;
                }

                return _publiclyVisibleMembers;
            }
        }

        internal static SymbolDocumentationInfo Create(CompilationDocumentationInfo compilation)
        {
            return new SymbolDocumentationInfo(
                symbol: null,
                commentId: null,
                symbols: ImmutableArray<ISymbol>.Empty,
                names: ImmutableArray<string>.Empty,
                compilation: compilation);
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

        internal string GetPath(string fileName)
        {
            return string.Join(@"\", Names.Reverse()) + @"\" + fileName;
        }

        internal string GetUrl(string fileName, SymbolDocumentationInfo directoryInfo = null, bool useExternalLink = true)
        {
            if (useExternalLink
                && CompilationInfo.IsExternal(Symbol))
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
                return string.Join("/", Names.Reverse()) + "/" + fileName;

            if (this == directoryInfo)
                return "./" + fileName;

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

            sb.Append("/");
            sb.Append(fileName);

            return sb.ToString();
        }

        public IEnumerable<IFieldSymbol> GetFields(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetPubliclyVisibleMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Field)
                    yield return (IFieldSymbol)member;
            }
        }

        public IEnumerable<IMethodSymbol> GetConstructors()
        {
            foreach (ISymbol member in GetPubliclyVisibleMembers())
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
            foreach (ISymbol member in (GetPubliclyVisibleMembers(includeInherited)))
            {
                if (member.Kind == SymbolKind.Property)
                    yield return (IPropertySymbol)member;
            }
        }

        public IEnumerable<IMethodSymbol> GetMethods(bool includeInherited = false)
        {
            foreach (ISymbol member in (GetPubliclyVisibleMembers(includeInherited)))
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
            foreach (ISymbol member in (GetPubliclyVisibleMembers(includeInherited)))
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
            foreach (ISymbol member in (GetPubliclyVisibleMembers(includeInherited)))
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

        public IEnumerable<IMethodSymbol> GetExtensionMethods()
        {
            if (Symbol.Kind == SymbolKind.NamedType
                && Symbol.IsStatic
                && Symbol.ContainingType == null)
            {
                foreach (ISymbol member in GetPubliclyVisibleMembers())
                {
                    if (member.Kind == SymbolKind.Method
                        && member.IsStatic)
                    {
                        var methodSymbol = (IMethodSymbol)member;

                        if (methodSymbol.IsExtensionMethod)
                            yield return methodSymbol;
                    }
                }
            }
        }
    }
}
