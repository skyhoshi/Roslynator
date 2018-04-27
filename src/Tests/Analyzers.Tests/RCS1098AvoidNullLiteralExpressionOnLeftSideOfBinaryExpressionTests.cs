// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp;
using Roslynator.CSharp.Analysis;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpDiagnosticVerifier;

namespace Roslynator.Analyzers.Tests
{
    public static class RCS1098AvoidNullLiteralExpressionOnLeftSideOfBinaryExpressionTests
    {
        private static DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AvoidNullLiteralExpressionOnLeftSideOfBinaryExpression;

        private static DiagnosticAnalyzer Analyzer { get; } = new AvoidNullLiteralOnLeftOfBinaryExpressionAnalyzer();

        private static CodeFixProvider CodeFixProvider { get; } = new BinaryExpressionCodeFixProvider();

        [Fact]
        public static void TestDiagnosticWithCodeFix()
        {
            VerifyDiagnosticAndFix(@"
class C
{
    void M(string s)
    {
        if (<<<null>>> == s)
        {
        }
    }
}
", @"
class C
{
    void M(string s)
    {
        if (s == null)
        {
        }
    }
}
", Descriptor, Analyzer, CodeFixProvider);
        }

        [Fact]
        public static void TestNoDiagnostic()
        {
            VerifyNoDiagnostic(@"
class C
{
    void M(string s)
    {
        if (null
        #region
            == s)
        {
        }
        #endregion
    }
}
", Descriptor, Analyzer);
        }
    }
}
