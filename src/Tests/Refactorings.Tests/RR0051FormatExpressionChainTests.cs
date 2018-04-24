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

        [Theory]
        [InlineData("x.M().<<<>>>M()", @"x
                .M()
                .M()")]
        [InlineData("this.F.M().<<<>>>M()", @"this.F
                .M()
                .M()")]
        [InlineData("A.B.Foo.SM().<<<>>>M()", @"A.B.Foo
                .SM()
                .M()")]
        [InlineData("x.M()?.<<<>>>P", @"x
                .M()?
                .P")]
        [InlineData("x.M()?.P.P[0]?.M().P?.M()[0].P.P.P?.<<<>>>P", @"x
                .M()?
                .P
                .P[0]?
                .M()
                .P?
                .M()[0]
                .P
                .P
                .P?
                .P")]
        public static void TestCodeRefactoring(string fixableCode, string fixedCode)
        {
            const string sourceTemplate = @"
namespace A.B
{
    class Foo
    {
        Foo M()
        {
            var x = new Foo();

            x = <<<>>>;
                
            return null;
        }

        public static Foo SM() => null;

        public Foo F;
        public static Foo SF;

        public Foo P { get; }
        public static Foo SP { get; }

        public Foo this[int index] => null;
    }
}
";

        VerifyRefactoring(
                sourceTemplate,
                fixableCode,
                fixedCode,
                refactoringProvider: CodeRefactoringProvider,
                equivalenceKey: RefactoringId);
        }

        [Fact]
        public static void TestNoCodeRefactoring()
        {
            VerifyNoRefactoring(
@"
namespace A.B
{
    class Foo
    {
        Foo M()
        {
            var x = new Foo();

            x = x.<<<>>>M();
            x = x.<<<>>>P;
            x = x.<<<>>>P[0];

            x = x?.<<<>>>M();
            x = x?.<<<>>>P;
            x = x?.<<<>>>P[0];

            x = x
                .M() //
                .<<<>>>M();
                
            return null;
        }

        public static Foo SM() => null;

        public Foo F;
        public static Foo SF;

        public Foo P { get; }
        public static Foo SP { get; }

        public Foo this[int index] => null;
    }
}
",
                refactoringProvider: CodeRefactoringProvider,
                equivalenceKey: RefactoringId);
        }
    }
}
