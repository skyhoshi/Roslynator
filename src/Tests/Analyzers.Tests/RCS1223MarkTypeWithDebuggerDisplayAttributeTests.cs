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
    public class RCS1223MarkTypeWithDebuggerDisplayAttributeTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.MarkTypeWithDebuggerDisplayAttribute;

        public override DiagnosticAnalyzer Analyzer { get; } = new MarkTypeWithDebuggerDisplayAttributeAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new MarkTypeWithDebuggerDisplayAttributeCodeFixProvider();

        [Fact]
        public async Task Test_PublicClass()
        {
            await VerifyDiagnosticAndFixAsync(@"
using System.Diagnostics;

DebuggerDisplay(""{DebuggerDisplay,nq}"")]
public class C
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get { return ToString(); }
    }
}
", @"
using System.Diagnostics;

DebuggerDisplay(""{DebuggerDisplay,nq}"")]
public class C
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private string DebuggerDisplay
    {
        get { return ToString(); }
    }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_ClassWithDebuggerDisplayAttribute()
        {
            await VerifyNoDiagnosticAsync(@"
using System.Diagnostics;

DebuggerDisplay("""")]
public class C
{
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_StaticClass()
        {
            await VerifyNoDiagnosticAsync(@"
static class C
{
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_Interface()
        {
            await VerifyNoDiagnosticAsync(@"
public interface IC
{
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_NonPubliclyVisibleType()
        {
            await VerifyNoDiagnosticAsync(@"
class C
{
    class C2 { }
    private class PC { }
    private protected class PPC { }
}

internal class IC
{
    class C { }
    private class PC { }
    private protected class PPC { }
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_NonPubliclyVisibleType2()
        {
            await VerifyNoDiagnosticAsync(@"
public class C
{
    class C2
    {
        public class PC { }
        internal class IC { }
        public struct PST { }
        internal struct IST { }
    }

    private class PC
    {
        public class PC { }
        internal class IC { }
        public struct PST { }
        internal struct IST { }
    }

    private protected class PPC
    {
        public class PC { }
        internal class IC { }
        public struct PST { }
        internal struct IST { }
    }
}
");
        }
    }
}
