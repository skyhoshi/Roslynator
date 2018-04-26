// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS1003SyntaxErrorCharExpectedTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.SyntaxErrorCharExpected;

        private static CodeFixProvider CodeFixProvider { get; } = new ExpressionCodeFixProvider();

        [Fact]
        public static void TestFix()
        {
            VerifyFix(@"
class C
{
    public void M()
    {
        string s = null;

        var items = new string[]
        {
            s,
            s
            s,
        };
    }
}
", @"
class C
{
    public void M()
    {
        string s = null;

        var items = new string[]
        {
            s,
            s,
            s,
        };
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }
    }
}
