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
    public static class RCS1225ReplaceEnumeratorWithForEachTests
    {
        private static DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.ReplaceEnumeratorWithForEach;

        private static DiagnosticAnalyzer Analyzer { get; } = new ReplaceEnumeratorWithForEachAnalyzer();

        private static CodeFixProvider CodeFixProvider { get; } = new ReplaceEnumeratorWithForEachCodeFixProvider();

        [Fact]
        public static void TestDiagnosticWithCodeFix()
        {
            VerifyDiagnosticAndFix(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        string item = null;
        var items = new List<string>();

        <<<using>>> (List<string>.Enumerator en = items.GetEnumerator())
        {
            while (en.MoveNext())
            {
                yield return en.Current;
            }
        }
    }
}
", @"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        string item = null;
        var items = new List<string>();

        foreach (var item2 in items)
        {
            yield return item2;
        }
    }
}
", Descriptor, Analyzer, CodeFixProvider);
        }

        [Fact]
        public static void TestDiagnosticWithCodeFix_EmbeddedStatement()
        {
            VerifyDiagnosticAndFix(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        <<<using>>> (List<string>.Enumerator en = items.GetEnumerator())
            while (en.MoveNext())
                yield return en.Current;
    }
}
", @"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        foreach (var item in items)
            yield return item;
    }
}
", Descriptor, Analyzer, CodeFixProvider);
        }

        [Fact]
        public static void TestDiagnosticWithCodeFix_Nested()
        {
            VerifyDiagnosticAndFix(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        <<<using>>> (List<string>.Enumerator en = items.GetEnumerator())
        {
            while (en.MoveNext())
            {
                yield return en.Current.Insert(0, en.Current);
            }
        }
    }
}
", @"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        foreach (var item in items)
        {
            yield return item.Insert(0, item);
        }
    }
}
", Descriptor, Analyzer, CodeFixProvider);
        }

        [Fact]
        public static void TestNoDiagnostic()
        {
            VerifyNoDiagnostic(@"
using System.Collections.Generic;

class C
{
    void M()
    {
        var items = new List<string>();

        using (List<string>.Enumerator en = items.GetEnumerator())
        {
            int i = 0;
            while (en.MoveNext())
            {
                var x = en.Current;
                i++;
            }
        }

        using (List<string>.Enumerator en = items.GetEnumerator())
        {
            if (en.MoveNext())
            {
                var x = en.Current;
            }
        }
}
}
", Descriptor, Analyzer);
        }
    }
}
