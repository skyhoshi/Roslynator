// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.CodeFixes;
using Xunit;

#pragma warning disable RCS1090

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCSX001Tests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.MarkParameterWithInModifier;

        public override DiagnosticAnalyzer Analyzer { get; } = new MarkParameterWithInModifierAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new MarkParameterWithInModifierCodeFixProvider();

        [Fact]
        public async Task Test()
        {
            await VerifyDiagnosticAndFixAsync(@"
readonly struct C
{
    void M(C [|c|])
    {
    }
}
", @"
readonly struct C
{
    void M(in C c)
    {
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_Assigned()
        {
            await VerifyNoDiagnosticAsync(@"
readonly struct C
{
    void M(C c)
    {
        c = default(C);
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_ReferencedInLocalFunction()
        {
            await VerifyNoDiagnosticAsync(@"
readonly struct C
{
    void M(C c)
    {
        void LF()
        {
            var x = c;
        }
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_ReferencedInLambda()
        {
            await VerifyNoDiagnosticAsync(@"
using System.Linq;

readonly struct C
{
    void M(C c)
    {
        var items = Enumerable.Empty<C>().Select(f => c);
    }
}
");
        }
    }
}
