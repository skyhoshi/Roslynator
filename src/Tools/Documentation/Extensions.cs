// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class Extensions
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

        public static void WriteLink(
            this MarkdownWriter writer,
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormat format)
        {
            string url = null;
            if (symbolInfo.IsExternal)
            {
                switch (symbolInfo.Names.LastOrDefault())
                {
                    case "System":
                    case "Microsoft":
                        {
                            url = "https://docs.microsoft.com/en-us/dotnet/api/" + string.Join(".", symbolInfo.Names.Select(f => f.ToLowerInvariant()).Reverse());
                            break;
                        }
                }
            }
            else
            {
                url = symbolInfo.GetUrl(directoryInfo);
            }

            writer.WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(format), url);
        }
    }
}
