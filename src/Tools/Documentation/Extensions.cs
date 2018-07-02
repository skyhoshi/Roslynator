// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics;
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

        public static void WriteLink(
            this MarkdownWriter writer,
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormat format)
        {
            string s = null;

            if (directoryInfo == null)
            {
                s = string.Join("/", symbolInfo.Names.Reverse()) + "/README.md";
                writer.WriteLink(symbolInfo.Symbol.ToDisplayString(format), s);
                return;
            }

            int count = symbolInfo.Symbols.Length - directoryInfo.Symbols.Length;

            if (count > 0)
            {
                s = symbolInfo.Names[0];

                int i = 1;

                while (i < count)
                {
                    s = symbolInfo.Names[i] + "/" + s;
                    i++;
                }
            }
            else
            {
                count = 0;

                int i = symbolInfo.Symbols.Length - 1;
                int j = directoryInfo.Symbols.Length - 1;

                while (i >= 0
                    && j >= 0
                    && symbolInfo.Symbols[i] == directoryInfo.Symbols[j])
                {
                    count++;
                    i--;
                    j--;
                }

                s = string.Join("/", Enumerable.Repeat("..",  directoryInfo.Symbols.Length - count));

                i = count;

                while (i < symbolInfo.Names.Length)
                {
                    s = s + "/" + symbolInfo.Names[i];

                    i++;
                }
            }

            s += "/README.md";

            writer.WriteLink(symbolInfo.Symbol.ToDisplayString(format), s);
        }
    }
}
