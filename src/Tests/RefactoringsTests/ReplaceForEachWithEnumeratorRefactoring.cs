// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.CSharp.Refactorings.Tests
{
    internal static class ReplaceForEachWithEnumeratorRefactoring
    {
        private class Foo
        {
            public static void Bar()
            {
                foreach (int item in Enumerable.Range(0, 1))
                {
                    int x = item;
                }

                foreach (int item in Enumerable.Range(0, 1))
                    Bar(item);

                SyntaxList<SyntaxNode> nodes;

                foreach (var node in nodes)
                {
                    SyntaxNode x = node;
                }
            }

            private static int Bar(int value)
            {
                return value;
            }
        }
    }
}
