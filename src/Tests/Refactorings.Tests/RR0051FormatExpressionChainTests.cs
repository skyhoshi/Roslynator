// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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
        [InlineData("<<<>>>x.M().M()", @"x
                .M()
                .M()")]
        [InlineData("<<<>>>x?.M()?.M()", @"x?
                .M()?
                .M()")]
        [InlineData("<<<>>>x[0].M()[0].M()[0]", @"x[0]
                .M()[0]
                .M()[0]")]
        [InlineData("<<<>>>x?[0]?.M()[0]?.M()[0]", @"x?[0]?
                .M()[0]?
                .M()[0]")]
        [InlineData("<<<>>>x.P.P", @"x
                .P
                .P")]
        [InlineData("<<<>>>x?.P?.P", @"x?
                .P?
                .P")]
        [InlineData("<<<>>>x[0].P[0].P[0]", @"x[0]
                .P[0]
                .P[0]")]
        [InlineData("<<<>>>x?[0]?.P[0]?.P[0]", @"x?[0]?
                .P[0]?
                .P[0]")]
        [InlineData("<<<>>>x.M(x.M().M()).M(x.M().M())", @"x
                .M(x.M().M())
                .M(x.M().M())")]
        [InlineData("<<<>>>this.M().M().M()", @"this.M()
                .M()
                .M()")]
        [InlineData("<<<>>>A.B.Foo.SM().M()", @"A.B.Foo
                .SM()
                .M()")]
        public static void TestRefactoring(string fixableCode, string fixedCode)
        {
            VerifyRefactoring(@"
namespace A.B
{
    class Foo
    {
        Foo M(Foo foo = null)
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
", fixableCode, fixedCode, CodeRefactoringProvider, RefactoringId);
        }

        [Fact]
        public static void TestNoRefactoring()
        {
            VerifyNoRefactoring(@"
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
", CodeRefactoringProvider, RefactoringId);
        }
    }
}
