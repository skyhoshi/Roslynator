// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.CodeFixes;
using Roslynator.CSharp.Analysis.RemoveRedundantAsyncAwait;
using Xunit;

#pragma warning disable RCS1090

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCS1174RemoveRedundantAsyncAwaitTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.RemoveRedundantAsyncAwait;

        public override DiagnosticAnalyzer Analyzer { get; } = new RemoveRedundantAsyncAwaitAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new RemoveRedundantAsyncAwaitCodeFixProvider();

        [Fact]
        public async Task Test()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System.Threading.Tasks;

class C
{
    [|async|] Task<object> GetAsync()
    {
        return await GetAsync();
    }
}
", @"
using System.Threading.Tasks;

class C
{
    Task<object> GetAsync()
    {
        return GetAsync();
    }
}
");
        }

        //[Fact]
        public async Task TestNoDiagnostic()
        {
            await VerifyNoDiagnosticAsync(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class C
{
    void M()
    {
    }
}
");
        }
    }
}
