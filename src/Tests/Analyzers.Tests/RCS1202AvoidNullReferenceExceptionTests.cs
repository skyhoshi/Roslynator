﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.CodeFixes;
using Xunit;

namespace Roslynator.CSharp.Analysis.Tests
{
    public abstract class RCS1202AvoidNullReferenceExceptionTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AvoidNullReferenceException;

        public override CodeFixProvider FixProvider { get; } = new AvoidNullReferenceExceptionCodeFixProvider();

        public class AccessExpressionTests : RCS1202AvoidNullReferenceExceptionTests
        {
            public override DiagnosticAnalyzer Analyzer { get; } = new InvocationExpressionAnalyzer();

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("x.ElementAtOrDefault(1)[|.|]ToString()", "x.ElementAtOrDefault(1)?.ToString()")]
            [InlineData("x.FirstOrDefault()[|.|]ToString()", "x.FirstOrDefault()?.ToString()")]
            [InlineData("x.LastOrDefault()[|.|]ToString()", "x.LastOrDefault()?.ToString()")]
            [InlineData("Enumerable.ElementAtOrDefault(x, 1)[|.|]ToString()", "Enumerable.ElementAtOrDefault(x, 1)?.ToString()")]
            [InlineData("Enumerable.FirstOrDefault(x)[|.|]ToString()", "Enumerable.FirstOrDefault(x)?.ToString()")]
            [InlineData("Enumerable.LastOrDefault(x)[|.|]ToString()", "Enumerable.LastOrDefault(x)?.ToString()")]
            public async Task Test_MemberAccessExpression(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        var y = [||];
    }
}
", fromData, toData);
            }

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("x.ElementAtOrDefault(1)[|.|]ToString()", "x.ElementAtOrDefault(1)?.ToString()")]
            [InlineData("x.FirstOrDefault()[|.|]ToString()", "x.FirstOrDefault()?.ToString()")]
            [InlineData("x.LastOrDefault()[|.|]ToString()", "x.LastOrDefault()?.ToString()")]
            [InlineData("Enumerable.ElementAtOrDefault(x, 1)[|.|]ToString()", "Enumerable.ElementAtOrDefault(x, 1)?.ToString()")]
            [InlineData("Enumerable.FirstOrDefault(x)[|.|]ToString()", "Enumerable.FirstOrDefault(x)?.ToString()")]
            [InlineData("Enumerable.LastOrDefault(x)[|.|]ToString()", "Enumerable.LastOrDefault(x)?.ToString()")]
            public async Task Test_MemberAccessExpression2(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class C : IEnumerable<object>
{
    void M()
    {
        var x = Enumerable.Empty<C>();
        var y = [||];
    }

    public object FirstOrDefault() => null;
    public object LastOrDefault() => null;
    public object ElementAtOrDefault(int index) => null;

    public object this[int index] => null;

    public IEnumerator<object> GetEnumerator() => null;
    IEnumerator IEnumerable.GetEnumerator() => null;
}
", fromData, toData);
            }

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("((x.ElementAtOrDefault(1)))[|.|]ToString()", "((x.ElementAtOrDefault(1)))?.ToString()")]
            [InlineData("((Enumerable.ElementAtOrDefault(x, 1)))[|.|]ToString()", "((Enumerable.ElementAtOrDefault(x, 1)))?.ToString()")]
            public async Task Test_MemberAccessExpression_Parenthesized(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        var y = [||];
    }
}
", fromData, toData);
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task Test_MemberAccessExpression_AddCoalesceExpression()
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        char y = x.ElementAtOrDefault(1)[|.|]First();
    }
}
", @"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        char y = x.ElementAtOrDefault(1)?.First() ?? default;
    }
}
");
            }

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("x.ElementAtOrDefault(1)[|[[|]0]", "x.ElementAtOrDefault(1)?[0]")]
            [InlineData("x.FirstOrDefault()[|[[|]0]", "x.FirstOrDefault()?[0]")]
            [InlineData("x.LastOrDefault()[|[[|]0]", "x.LastOrDefault()?[0]")]
            [InlineData("Enumerable.ElementAtOrDefault(x, 1)[|[[|]0]", "Enumerable.ElementAtOrDefault(x, 1)?[0]")]
            [InlineData("Enumerable.FirstOrDefault(x)[|[[|]0]", "Enumerable.FirstOrDefault(x)?[0]")]
            [InlineData("Enumerable.LastOrDefault(x)[|[[|]0]", "Enumerable.LastOrDefault(x)?[0]")]
            public async Task Test_ElementAccessExpression(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        var y = [||];
    }
}
", fromData, toData);
            }

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("x.ElementAtOrDefault(1)[|[[|]0]", "x.ElementAtOrDefault(1)?[0]")]
            [InlineData("x.FirstOrDefault()[|[[|]0]", "x.FirstOrDefault()?[0]")]
            [InlineData("x.LastOrDefault()[|[[|]0]", "x.LastOrDefault()?[0]")]
            [InlineData("Enumerable.ElementAtOrDefault(x, 1)[|[[|]0]", "Enumerable.ElementAtOrDefault(x, 1)?[0]")]
            [InlineData("Enumerable.FirstOrDefault(x)[|[[|]0]", "Enumerable.FirstOrDefault(x)?[0]")]
            [InlineData("Enumerable.LastOrDefault(x)[|[[|]0]", "Enumerable.LastOrDefault(x)?[0]")]
            public async Task Test_ElementAccessExpression2(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections;
using System.Collections.Generic;
using System.Linq;

class C : IEnumerable<object>
{
    void M()
    {
        var x = Enumerable.Empty<C>();
        var y = [||];
    }

    public object FirstOrDefault() => null;
    public object LastOrDefault() => null;
    public object ElementAtOrDefault(int index) => null;

    public object this[int index] => null;

    public IEnumerator<object> GetEnumerator() => null;
    IEnumerator IEnumerable.GetEnumerator() => null;
}
", fromData, toData);
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task Test_ElementAccessExpression_AddCoalesceExpression()
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        char y = x.ElementAtOrDefault(1)[|[[|]0];
    }
}
", @"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        char y = x.ElementAtOrDefault(1)?[0] ?? default;
    }
}
");
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task TestNoDiagnostic_ValueType()
            {
                await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var values = new List<int>();

        int i = 1;

        i = values.ElementAtOrDefault(1).GetHashCode();
        i = values.FirstOrDefault().GetHashCode();
        i = values.LastOrDefault().GetHashCode();
    }
}
");
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task TestNoDiagnostic_NullableType()
            {
                await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var values = new List<int?>();

        int i = 1;

        i = values.ElementAtOrDefault(1).GetHashCode();
        i = values.FirstOrDefault().GetHashCode();
        i = values.LastOrDefault().GetHashCode();
    }
}
");
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task TestNoDiagnostic_NoMemberAccessOrElementAccess()
            {
                await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        string s = null;

        s = x.ElementAtOrDefault(1);
        s = x.FirstOrDefault();
        s = x.LastOrDefault();
        s = (s as string);
    }
}
");
            }

            [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            public async Task TestNoDiagnostic_ConditionalAccess()
            {
                await VerifyNoDiagnosticAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        var x = Enumerable.Empty<string>();
        string s = null;
        char ch = '\0';

        s = x.ElementAtOrDefault(1)?.ToUpper();
        s = x.FirstOrDefault()?.ToUpper();
        s = x.LastOrDefault()?.ToUpper();
        s = (s as string)?.ToUpper();

        s = Enumerable.ElementAtOrDefault(x, 1)?.ToUpper();
        s = Enumerable.FirstOrDefault(x)?.ToUpper();
        s = Enumerable.LastOrDefault(x)?.ToUpper();

        s = x.SingleOrDefault().ToUpper();
        s = Enumerable.SingleOrDefault(x).ToUpper();

        ch = x.ElementAtOrDefault(1)?[0] ?? default(char);
        ch = x.FirstOrDefault()?[0] ?? default(char);
        ch = x.LastOrDefault()?[0] ?? default(char);
        ch = (s as string)?[0] ?? default(char);

        ch = Enumerable.ElementAtOrDefault(x, 1)?[0] ?? default(char);
        ch = Enumerable.FirstOrDefault(x)?[0] ?? default(char);
        ch = Enumerable.LastOrDefault(x)?[0] ?? default(char);
    }
}
");
            }
        }

        public class AsExpressionTests : RCS1202AvoidNullReferenceExceptionTests
        {
            public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AvoidNullReferenceException;

            public override DiagnosticAnalyzer Analyzer { get; } = new AvoidNullReferenceExceptionAnalyzer();

            public override CodeFixProvider FixProvider { get; } = new AvoidNullReferenceExceptionCodeFixProvider();

            [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.AvoidNullReferenceException)]
            [InlineData("(x as string)[|.|]ToString()", "(x as string)?.ToString()")]
            [InlineData("(x as string)[|[[|]0]", "(x as string)?[0]")]
            public async Task Test_AsExpression(string fromData, string toData)
            {
                await VerifyDiagnosticAndFixAsync(@"
using System.Collections.Generic;
using System.Linq;

class C
{
    void M()
    {
        object x = null;
        var y = [||];
    }
}
", fromData, toData);
            }
        }
    }
}
