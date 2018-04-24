// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Roslynator.CSharp.Refactorings;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCodeRefactoringVerifier;

namespace Roslynator.Refactorings.Tests
{
    public static class RR0204ReplaceForEachWithEnumeratorTests
    {
        private const string RefactoringId = RefactoringIdentifiers.ReplaceForEachWithEnumerator;

        private static CodeRefactoringProvider CodeRefactoringProvider { get; } = new RoslynatorCodeRefactoringProvider();

        [Fact]
        public static void TestCodeRefactoring_WithUsing()
        {
            VerifyRefactoring(
@"
using System.Linq;

class C
{
    void M()
    {
        <<<>>>foreach (int item in Enumerable.Range(0, 1))
        {
            int x = item;
        }

        for<<<>>>each (int item in Enumerable.Range(0, 1))
            M(item);
    }

    int M(int value)
    {
        return value;
    }
}
",
@"
using System.Linq;

class C
{
    void M()
    {
        using (var en2 = Enumerable.Range(0, 1).GetEnumerator())
        {
            while (en2.MoveNext())
            {
                int x = en2.Current;
            }
        }

        using (var en = Enumerable.Range(0, 1).GetEnumerator())
        {
            while (en.MoveNext())
                M(en.Current);
        }
    }

    int M(int value)
    {
        return value;
    }
}
",
                CodeRefactoringProvider,
                RefactoringId);
        }

        [Fact]
        public static void TestCodeRefactoring_WithoutUsing()
        {
            VerifyRefactoring(
@"
using Microsoft.CodeAnalysis;

class C
{
    void M()
    {
        SyntaxList<SyntaxNode> nodes;

        <<<>>>foreach (SyntaxNode node in nodes)
        {
            SyntaxNode x = node;
        }
    }
}
",
@"
using Microsoft.CodeAnalysis;

class C
{
    void M()
    {
        SyntaxList<SyntaxNode> nodes;
        var en = nodes.GetEnumerator();
        while (en.MoveNext())
        {
            SyntaxNode x = en.Current;
        }
    }
}
",
                CodeRefactoringProvider,
                RefactoringId);
        }

        [Fact]
        public static void TestNoCodeRefactoring_InvalidSpan()
        {
            VerifyNoRefactoring(
@"
using System.Linq;

class C
{
    void M()
    {
        <<<foreach>>> (int item in Enumerable.Range(0, 1))
        {
            int x = item;
        }

        <<<foreach (int item in Enumerable.Range(0, 1))
        {
            int x = item;
        }>>>

        <<<f>>>oreach (int item in Enumerable.Range(0, 1))
        {
            int x = item;
        }
        foreach (int item in Enumerable.Range(0, 1))
        {
            <<<>>>int x = item;
        }
    }
}
",
                CodeRefactoringProvider,
                RefactoringId);
        }
    }
}
