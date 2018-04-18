// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharpCompilerCodeFixVerifier;

#pragma warning disable xUnit1008

namespace Roslynator.CodeFixes.Tests
{
    public static class CSTests
    {
        private const string SourceTemplate = @"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
    }
}
";

        public static void TestCodeFix()
        {
            VerifyCodeFix(
@"
",
@"
",
                diagnosticId: CompilerDiagnosticIdentifiers.Zero,
                codeFixProvider: default,
                equivalenceKey: EquivalenceKey.Create(CompilerDiagnosticIdentifiers.Zero, additionalKey1: null));
        }

        [InlineData("", "")]
        public static void TestCodeFix2(string fixableCode, string fixedCode)
        {
            VerifyCodeFix(
                SourceTemplate,
                fixableCode,
                fixedCode,
                diagnosticId: CompilerDiagnosticIdentifiers.Zero,
                codeFixProvider: default,
                equivalenceKey: EquivalenceKey.Create(CompilerDiagnosticIdentifiers.Zero, additionalKey1: null));
        }

        public static void TestNoCodeFix()
        {
            VerifyNoCodeFix(
@"
",
                codeFixProvider: default,
                equivalenceKey: EquivalenceKey.Create(CompilerDiagnosticIdentifiers.Zero, additionalKey1: null));
        }
    }
}
