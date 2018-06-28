// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using DotMarkdown.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslynator.CodeGeneration.CSharp;
using static DotMarkdown.Linq.MFactory;

namespace Roslynator.CodeGeneration.Markdown
{
    public static class SyntaxObjectModelGenerator
    {
        public static string GenerateCSharpSyntaxObjectModel()
        {
            List<INamedTypeSymbol> symbols = Symbols.SyntaxSymbols.ToList();

            var stack = new Stack<(INamedTypeSymbol symbol, MBulletItem item)>();

            INamedTypeSymbol nodeSymbol = Symbols.Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxNode");

            MBulletItem nodeItem = BulletItem(nodeSymbol.MetadataName);

            INamedTypeSymbol csharpNodeSymbol = Symbols.Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode");

            MBulletItem csharpNodeItem = BulletItem(csharpNodeSymbol.MetadataName);

            nodeItem.Add(csharpNodeItem);

            stack.Push((csharpNodeSymbol, csharpNodeItem));

            while (stack.Count > 0)
            {
                (INamedTypeSymbol symbol, MBulletItem item) = stack.Peek();

                INamedTypeSymbol derivedSymbol = symbols.Find(f => f.BaseType == symbol);

                if (derivedSymbol != null)
                {
                    MBulletItem item2 = BulletItem(Bold(derivedSymbol.MetadataName));

                    item2.Add(Symbols.GetPropertySymbols(derivedSymbol).Select(f => BulletItem(f.Name, " (", Italic(f.Type.ToDisplayString(SymbolDisplayFormats.Default)), ")")));

                    List<SyntaxKind> kinds = Symbols.GetKinds(derivedSymbol).ToList();

                    if (kinds.Count > 1)
                    {
                        item2.Add(BulletItem(
                            "SyntaxKinds:",
                            BulletList(kinds.Select(f => $"SyntaxKind.{f.ToString()}").OrderBy(f => f))));
                    }

                    item.Add(item2);

                    stack.Push((derivedSymbol, item2));

                    symbols.Remove(derivedSymbol);
                }
                else
                {
                    stack.Pop();
                }
            }

            var doc = new MDocument(Heading1("C# Syntax Object Model"), nodeItem);

            doc.AddFootnote();

            return doc.ToString();
        }
    }
}
