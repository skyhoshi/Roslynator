// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CSTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.OperatorCannotBeAppliedToOperands;

        private static CodeFixProvider CodeFixProvider { get; }

        //[Fact]
        public static void TestFix()
        {
            VerifyFix(@"
using System;
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
    }
}
", @"
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }

        //[Theory]
        //[InlineData("", "")]
        public static void TestFix2(string fixableCode, string fixedCode)
        {
            const string sourceTemplate = @"
using System;
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
    }
}
";

            VerifyFix(sourceTemplate, fixableCode, fixedCode, DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }

        //[Fact]
        public static void TestNoFix()
        {
            VerifyNoFix(
@"
using System;
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
    }
}
", CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }
    }
}
