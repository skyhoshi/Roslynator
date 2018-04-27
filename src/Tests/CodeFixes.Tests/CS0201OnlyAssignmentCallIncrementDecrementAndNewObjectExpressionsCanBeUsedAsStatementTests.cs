// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS0201OnlyAssignmentCallIncrementDecrementAndNewObjectExpressionsCanBeUsedAsStatementTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.OnlyAssignmentCallIncrementDecrementAndNewObjectExpressionsCanBeUsedAsStatement;

        private static CodeFixProvider CodeFixProvider { get; } = new ExpressionCodeFixProvider();

        [Fact]
        public static void TestFix_RemoveParentheses()
        {
            VerifyFix(@"
class C
{
    void M()
    {
        (M());
    }
}
", @"
class C
{
    void M()
    {
        M();
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }

        [Fact]
        public static void TestFix_IntroduceLocal()
        {
            VerifyFix(@"
using System;

class C
{
    void M()
    {
        DateTime.Now;
    }
}
", @"
using System;

class C
{
    void M()
    {
        var dateTime = DateTime.Now;
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.IntroduceLocalVariable));
        }

        [Fact]
        public static void TestFix_IntroduceField()
        {
            VerifyFix(@"
using System;

class C
{
    void M()
    {
        DateTime.Now;
    }
}
", @"
using System;

class C
{
    private DateTime _dateTime;

    void M()
    {
        _dateTime = DateTime.Now;
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.IntroduceField));
        }

        [Fact]
        public static void TestFix_IntroduceStaticField()
        {
            VerifyFix(@"
using System;

class C
{
    static void M()
    {
        DateTime.Now;
    }
}
", @"
using System;

class C
{
    private static DateTime _dateTime;

    static void M()
    {
        _dateTime = DateTime.Now;
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.IntroduceField));
        }

        [Fact]
        public static void TestFix_AddArgumentList()
        {
            VerifyFix(@"
class C
{
    void M()
    {
        C x = null;

        M;
        x.M;
    }
}
", @"
class C
{
    void M()
    {
        C x = null;

        M();
        x.M();
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.AddArgumentList));
        }

        [Fact]
        public static void TestFix_ReplaceConditionalExpressionWithIfElse()
        {
            VerifyFix(@"
class C
{
    void M(bool f)
    {
        (f) ? M() : M2();
    }

    void M2()
    {
    }
}
", @"
class C
{
    void M(bool f)
    {
        if (f)
        {
            M();
        }
        else
        {
            M2();
        }
    }

    void M2()
    {
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.ReplaceConditionalExpressionWithIfElse));
        }

        [Fact]
        public static void TestFix_ReplaceComparisonWithAssignment()
        {
            VerifyFix(@"
class C
{
    void M(string s)
    {
        s == null;
        s == """";
    }
}
", @"
class C
{
    void M(string s)
    {
        s = null;
        s = """";
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, CodeFixIdentifiers.ReplaceComparisonWithAssignment));
        }
    }
}
