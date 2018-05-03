// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.Analysis.ReplaceEnumeratorWithForEach;
using Roslynator.CSharp.CodeFixes;
using Xunit;

#pragma warning disable RCS1090

namespace Roslynator.CSharp.Analysis.Tests
{
    public class RCS1225ReplaceEnumeratorWithForEachTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.ReplaceEnumeratorWithForEach;

        public override DiagnosticAnalyzer Analyzer { get; } = new ReplaceEnumeratorWithForEachAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new ReplaceEnumeratorWithForEachCodeFixProvider();

        [Fact]
        public async Task Test()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        string item = null;
        var items = new List<string>();

        [|using|] (List<string>.Enumerator en = items.GetEnumerator())
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
");
        }

        [Fact]
        public async Task Test_EmbeddedStatement()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        [|using|] (List<string>.Enumerator en = items.GetEnumerator())
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
");
        }

        [Fact]
        public async Task Test_NestedCurrent()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        var items = new List<string>();

        [|using|] (List<string>.Enumerator en = items.GetEnumerator())
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
");
        }

        [Fact]
        public async Task TestNoDiagnostic_WhileDoesNotContainCurrent()
        {
            await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;

class C
{
    void M()
    {
        var items = new List<string>();

        using (List<string>.Enumerator en = items.GetEnumerator())
        {
            while (en.MoveNext())
            {
            }
        }
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_UsingContainsMultipleStatements()
        {
            await VerifyNoDiagnosticAsync(@"
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
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_IfInsteadOfWhile()
        {
            await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;

class C
{
    void M()
    {
        var items = new List<string>();

        using (List<string>.Enumerator en = items.GetEnumerator())
        {
            if (en.MoveNext())
            {
                var x = en.Current;
            }
        }
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_WhileContainsMoveNext()
        {
            await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;

class C
{
    static bool CountExceeds<T>(IEnumerable<T> collection, int value)
    {
        int cnt = 0;
        using (IEnumerator<T> en = collection.GetEnumerator())
        {
            while (en.MoveNext())
            {
                cnt++;
                if (cnt == value)
                    return en.MoveNext();
            }
        }

        return false;
    }
}
");
        }
    }
}
