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
    public static class RCS1220AddCommaAfterLastItemInListTests
    {
        private static DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AddCommaAfterLastItemInList;

        private static DiagnosticAnalyzer Analyzer { get; } = new AddCommaAfterLastItemInListAnalyzer();

        private static CodeFixProvider CodeFixProvider { get; } = new EnumDeclarationCodeFixProvider();

        [Fact]
        public static void TestDiagnosticWithCodeFix()
        {
            VerifyDiagnosticAndFix(
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
            P2 = null<<<>>>
        };

        var items = new string[]
        {
            null,
            null<<<>>>
        };

        var dic1 = new Dictionary<string, string>()
        {
            { s, null },
            { s, null }<<<>>>
        };

        var dic2 = new Dictionary<string, string>()
        {
            [s] = null,
            [s] = null<<<>>>
        };
    }

    public enum EnumName
    {
        A,
        B<<<>>>
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
",
                Descriptor,
                Analyzer,
                CodeFixProvider);
        }

        [Fact]
        public static void TestNoDiagnostic()
        {
            VerifyNoDiagnostic(
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
",
                descriptor: Descriptor,
                analyzer: Analyzer);
        }

        [Fact]
        public static void TestNoDiagnostic_Empty()
        {
            VerifyNoDiagnostic(
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
",
                descriptor: Descriptor,
                analyzer: Analyzer);
        }
    }
}
