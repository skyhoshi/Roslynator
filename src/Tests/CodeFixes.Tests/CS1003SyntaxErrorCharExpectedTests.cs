// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS1003SyntaxErrorCharExpectedTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.SyntaxErrorCharExpected;

        [Fact]
        public static void TestCodeFix()
        {
            VerifyFix(
@"
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
",
@"
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
",
                diagnosticId: DiagnosticId,
                fixProvider: new ExpressionCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId));
        }
    }
}
