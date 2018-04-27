// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeRefactorings;
using Roslynator.CSharp.Refactorings;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCodeRefactoringVerifier;

namespace Roslynator.Refactorings.Tests
{
    public static class RR0159SwapBinaryOperandsTests
    {
        private const string RefactoringId = RefactoringIdentifiers.SwapBinaryOperands;

        private static CodeRefactoringProvider CodeRefactoringProvider { get; } = new RoslynatorCodeRefactoringProvider();

        [Theory]
        [InlineData("f && f2", "f2 && f")]
        [InlineData("f &<<<>>>& f2", "f2 && f")]
        [InlineData("f || f2", "f2 || f")]
        [InlineData("i == j", "j == i")]
        [InlineData("i != j", "j != i")]
        [InlineData("i > j", "j < i")]
        [InlineData("i >= j", "j <= i")]
        [InlineData("i < j", "j > i")]
        [InlineData("i <= j", "j >= i")]
        public static void TestRefactoring(string fixableCode, string fixedCode)
        {
            VerifyRefactoring(@"
class C
{
    void M()
    {
        bool f = false;
        bool f2 = false;
        int i = 0;
        int j = 0;
            
        if (<<<>>>) { }
    }
}
", fixableCode, fixedCode, CodeRefactoringProvider, RefactoringId);
        }

        [Theory]
        [InlineData("i + j", "j + i")]
        [InlineData("i * j", "j * i")]
        public static void TestRefactoring_AddMultiply(string fixableCode, string fixedCode)
        {
            VerifyRefactoring(@"
class C
{
    void M(int i, int j)
    {
        int k = <<<>>>;
    }
}
", fixableCode, fixedCode, CodeRefactoringProvider, RefactoringId);
        }

        [Fact]
        public static void TestNoRefactoring()
        {
            VerifyNoRefactoring(@"
class C
{
    void M(object x, bool f)
    {
        if (x =<<<>>>= null) { }
        if (x !<<<>>>= null) { }
        if (f =<<<>>>= false) { }
        if (f =<<<>>>= true) { }
    }
}
", CodeRefactoringProvider, RefactoringId);
        }
    }
}
