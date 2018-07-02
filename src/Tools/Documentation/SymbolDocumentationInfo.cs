// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public sealed class SymbolDocumentationInfo
    {
        private SymbolDocumentationInfo(
            ISymbol symbol,
            string commentId,
            ImmutableArray<ISymbol> symbols,
            ImmutableArray<string> names,
            bool isExternal)
        {
            Symbol = symbol;
            CommentId = commentId;
            Symbols = symbols;
            Names = names;
            IsExternal = isExternal;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public ImmutableArray<ISymbol> Symbols { get; }

        public ImmutableArray<string> Names { get; }

        public bool IsExternal { get; }

        public static SymbolDocumentationInfo Create(ISymbol symbol, bool isExternal)
        {
            ImmutableArray<ISymbol>.Builder symbols = ImmutableArray.CreateBuilder<ISymbol>();
            ImmutableArray<string>.Builder names = ImmutableArray.CreateBuilder<string>();

            int arity = symbol.GetArity();

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

            return new SymbolDocumentationInfo(
                symbol,
                symbol.GetDocumentationCommentId(),
                symbols.ToImmutableArray(),
                names.ToImmutableArray(),
                isExternal);
        }

        internal string GetUrl(SymbolDocumentationInfo directoryInfo)
        {
            string s = null;

            if (directoryInfo == null)
            {
                return string.Join("/", Names.Reverse()) + "/README.md";
            }

            int count = Symbols.Length - directoryInfo.Symbols.Length;

            if (count > 0)
            {
                s = Names[0];

                int i = 1;

                while (i < count)
                {
                    s = Names[i] + "/" + s;
                    i++;
                }
            }
            else
            {
                count = 0;

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

                s = string.Join("/", Enumerable.Repeat("..", directoryInfo.Symbols.Length - count));

                i = Names.Length - 1 - count;

                while (i >= 0)
                {
                    s = s + "/" + Names[i];

                    i--;
                }
            }

            return s += "/README.md";
        }
    }
}
