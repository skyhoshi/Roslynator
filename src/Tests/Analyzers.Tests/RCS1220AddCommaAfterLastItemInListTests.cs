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
    public class RCS1220AddCommaAfterLastItemInListTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AddCommaAfterLastItemInList;

        public override DiagnosticAnalyzer Analyzer { get; } = new AddCommaAfterLastItemInListAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new AddCommaAfterLastItemInListCodeFixProvider();

        [Fact]
        public async Task TestDiagnosticWithCodeFix()
        {
            await VerifyDiagnosticAndFixAsync(
@"
using System.Collections.Generic;

public class C
{
    public string P1 { get; set; }
    public string P2 { get; set; }

    public void M()
    {
        string s = null;

        var x = new C()
        {
            P1 = null,
            P2 = null[||]
        };

        var items = new string[]
        {
            null,
            null[||]
        };

        var dic1 = new Dictionary<string, string>()
        {
            { s, null },
            { s, null }[||]
        };

        var dic2 = new Dictionary<string, string>()
        {
            [s] = null,
            [s] = null[||]
        };
    }

    public enum EnumName
    {
        A,
        B[||]
    }
}
",
@"
using System.Collections.Generic;

public class C
{
    public string P1 { get; set; }
    public string P2 { get; set; }

    public void M()
    {
        string s = null;

        var x = new C()
        {
            P1 = null,
            P2 = null,
        };

        var items = new string[]
        {
            null,
            null,
        };

        var dic1 = new Dictionary<string, string>()
        {
            { s, null },
            { s, null },
        };

        var dic2 = new Dictionary<string, string>()
        {
            [s] = null,
            [s] = null,
        };
    }

    public enum EnumName
    {
        A,
        B,
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic()
        {
            await VerifyNoDiagnosticAsync(
@"
using System.Collections.Generic;

public class C
{
    public string P1 { get; set; }
    public string P2 { get; set; }

    public void M()
    {
        string s = null;

        var x = new C() { P1 = null, P2 = null };

        var items = new string[] { null, null };

        var dic1 = new Dictionary<string, string>() { { s, null }, { s, null } };

        var dic2 = new Dictionary<string, string>() { [s] = null, [s] = null };
    }

    public enum EnumName
    {
        A
    }

    public enum EnumName2
    {
        A, B
    }

    public enum EnumName3 { A }

    public enum EnumName4 { A, B }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_Empty()
        {
            await VerifyNoDiagnosticAsync(
@"
using System.Collections.Generic;

public class C
{
    public void M()
    {
        var x = new C() { };

        var items = new string[] { };

        var dic1 = new Dictionary<string, string>() { };

        var dic2 = new Dictionary<string, string>() { };
    }

    public enum EnumName
    {
    }

    public enum EnumName2 { }
}
");
        }
    }
}
