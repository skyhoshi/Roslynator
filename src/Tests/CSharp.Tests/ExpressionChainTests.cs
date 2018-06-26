// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Tests.Text;
using Xunit;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp.Tests
{
    public static class ExpressionChainTests
    {
        [Fact]
        public static void TestExpressionChainEnumerator()
        {
            var be = (BinaryExpressionSyntax)ParseExpression("a + b + c");
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain.Enumerator en = ExpressionChain.Create(be).GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be2.Left);
            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainEnumerator_WithSpan()
        {
            const string s = @"
class C
{
    void M(string a, string b, string c, string d)
    {
        string s = a + b + [|c + d|];
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain chain = ExpressionChain.Create(be, analysis.Spans[0].Span);

            Assert.Equal(chain.Span, TextSpan.FromBounds(be2.Right.SpanStart, be.Right.Span.End));
            Assert.Equal(chain.FullSpan, TextSpan.FromBounds(be2.Right.FullSpan.Start, be.Right.FullSpan.End));

            ExpressionChain.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainEnumerator_WithSpan2()
        {
            const string s = @"
class C
{
    void M(string a, string b, string c, string d)
    {
        string s = a + [|b + c|] + d;
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();
            be = (BinaryExpressionSyntax)be.Left;
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain chain = ExpressionChain.Create(be, analysis.Spans[0].Span);

            Assert.Equal(chain.Span, TextSpan.FromBounds(be2.Right.SpanStart, be.Right.Span.End));
            Assert.Equal(chain.FullSpan, TextSpan.FromBounds(be2.Right.FullSpan.Start, be.Right.FullSpan.End));

            ExpressionChain.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainEnumerator_WithSpan3()
        {
            const string s = @"
class C
{
    void M(string a, string b)
    {
        string s = [|a|] + b;
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();

            ExpressionChain chain = ExpressionChain.Create(be, analysis.Spans[0].Span);

            Assert.Equal(chain.Span, be.Left.Span);
            Assert.Equal(chain.FullSpan, be.Left.FullSpan);

            ExpressionChain.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be.Left);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainReversedEnumerator_WithSpan()
        {
            const string s = @"
class C
{
    void M(string a, string b, string c, string d)
    {
        string s = a + b + [|c + d|];
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain.Reversed chain = ExpressionChain.Create(be, analysis.Spans[0].Span).Reverse();

            ExpressionChain.Reversed.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainReversedEnumerator_WithSpan2()
        {
            const string s = @"
class C
{
    void M(string a, string b, string c, string d)
    {
        string s = a + [|b + c|] + d;
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();
            be = (BinaryExpressionSyntax)be.Left;
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain.Reversed chain = ExpressionChain.Create(be, analysis.Spans[0].Span).Reverse();

            ExpressionChain.Reversed.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainReversedEnumerator_WithSpan3()
        {
            const string s = @"
class C
{
    void M(string a, string b)
    {
        string s = [|a|] + b;
    }
}";
            TestSourceTextAnalysis analysis = TestSourceText.GetSpans(s);

            BinaryExpressionSyntax be = CSharpSyntaxTree.ParseText(analysis.Source).GetRoot().FirstDescendant<BinaryExpressionSyntax>();

            ExpressionChain.Reversed chain = ExpressionChain.Create(be, analysis.Spans[0].Span).Reverse();

            ExpressionChain.Reversed.Enumerator en = chain.GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be.Left);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestExpressionChainReversedEnumerator()
        {
            var be = (BinaryExpressionSyntax)ParseExpression("a + b + c");
            var be2 = (BinaryExpressionSyntax)be.Left;

            ExpressionChain.Reversed.Enumerator en = new ExpressionChain.Reversed(ExpressionChain.Create(be)).GetEnumerator();

            Assert.True(en.MoveNext() && en.Current == be.Right);
            Assert.True(en.MoveNext() && en.Current == be2.Right);
            Assert.True(en.MoveNext() && en.Current == be2.Left);
            Assert.True(!en.MoveNext());
        }

        [Fact]
        public static void TestBinaryExpressionAsChainEnumerate()
        {
            var be = (BinaryExpressionSyntax)ParseExpression("a + b + c");
            var be2 = (BinaryExpressionSyntax)be.Left;

            Assert.Equal(
                be.AsChain(),
                new ExpressionSyntax[] { be2.Left, be2.Right, be.Right });
        }

        [Fact]
        public static void TestBinaryExpressionAsChainReversedEnumerate()
        {
            var be = (BinaryExpressionSyntax)ParseExpression("a + b + c");
            var be2 = (BinaryExpressionSyntax)be.Left;

            Assert.Equal(
                be.AsChain().Reverse(),
                new ExpressionSyntax[] { be.Right, be2.Right, be2.Left });
        }
    }
}
