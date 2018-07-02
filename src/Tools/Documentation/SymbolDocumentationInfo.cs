// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class SymbolDocumentationInfo
    {
        private SymbolDocumentationInfo(
            ISymbol symbol,
            string commentId,
            ImmutableArray<ISymbol> symbols,
            ImmutableArray<string> names)
        {
            Symbol = symbol;
            CommentId = commentId;
            Symbols = symbols;
            Names = names;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public ImmutableArray<ISymbol> Symbols { get; }

        public ImmutableArray<string> Names { get; }

        public static SymbolDocumentationInfo Create(ISymbol symbol)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            int arity = GetArity();

            names.Add((arity > 0) ? symbol.Name + "-" + arity.ToString(CultureInfo.InvariantCulture) : symbol.Name);

            symbols.Add(symbol);

            INamedTypeSymbol containingType = symbol.ContainingType;

            while (containingType != null)
            {
                arity = containingType.Arity;

                names.Add((arity > 0) ? containingType.Name + "-" + arity.ToString(CultureInfo.InvariantCulture) : containingType.Name);

                symbols.Add(containingType);

                containingType = containingType.ContainingType;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            while (containingNamespace?.IsGlobalNamespace == false)
            {
                names.Add(containingNamespace.Name);

                symbols.Add(containingNamespace);

                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return new SymbolDocumentationInfo(symbol, symbol.GetDocumentationCommentId(), symbols.ToImmutableArray(), names.ToImmutableArray());

            int GetArity()
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
        }
    }
}
