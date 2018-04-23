// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Roslynator.CSharp.Refactorings;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCodeRefactoringVerifier;

namespace Roslynator.Refactorings.Tests
{

    public static class RR0051FormatExpressionChainTests
    {
        private const string RefactoringId = RefactoringIdentifiers.FormatExpressionChain;

        private static CodeRefactoringProvider CodeRefactoringProvider { get; } = new RoslynatorCodeRefactoringProvider();

        private const string SourceTemplate = @"
namespace A.B
{
    class Foo
    {
        Foo M()
        {
            var x = new Foo();

            Foo s = <<<>>>;
                
            return null;
        }

        public Foo P { get; }

        public Foo this[int index] => null;
    }
}
";

        //[Fact]
        public static void TestCodeRefactoring()
        {
            VerifyCodeRefactoring(
@"
",
@"
",
                codeRefactoringProvider: CodeRefactoringProvider,
                equivalenceKey: RefactoringId);
        }

        [Theory]
        [InlineData("x.M().<<<>>>M()", @"x
                .M()
                .M()")]
        public static void TestCodeRefactoring2(string fixableCode, string fixedCode)
        {
            VerifyCodeRefactoring(
                SourceTemplate,
                fixableCode,
                fixedCode,
                codeRefactoringProvider: CodeRefactoringProvider,
                equivalenceKey: RefactoringId);
        }

        //[Fact]
        public static void TestNoCodeRefactoring()
        {
            VerifyNoCodeRefactoring(
@"
",
                codeRefactoringProvider: CodeRefactoringProvider,
                equivalenceKey: RefactoringId);
        }
    }
}
