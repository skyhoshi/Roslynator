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
    public class RCS1190JoinStringExpressionsTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.JoinStringExpressions;

        public override DiagnosticAnalyzer Analyzer { get; } = new JoinStringExpressionsAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new BinaryExpressionCodeFixProvider();

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        public async Task Test_Regular()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M(string s)
    {
        s = [|""a"" + ""b""|];
    }
}
", @"
class C
{
    void M(string s)
    {
        s = ""ab"";
    }
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        public async Task Test_Verbatim_Multiline()
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M(string s)
    {
            s = [|@""""""
\{}"" + @""""""
\{}""|];
    }
}
", @"
class C
{
    void M(string s)
    {
            s = @""""""
\{}""""
\{}"";
    }
}
");
        }

        [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        [InlineData(@"""a"" + ""b""", @"""ab""")]
        [InlineData(@"""\""\r\n\\{}"" + ""a""", @"""\""\r\n\\{}a""")]
        public async Task Test_Regular_SpecialChars(string fromData, string toData)
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M(string s)
    {
        s = [||];
    }
}
", fromData, toData);
        }

        [Theory, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        [InlineData(@"@""a"" + @""b""", @"@""ab""")]
        [InlineData(@"@""""""\r\n\\{}"" + @""a""", @"@""""""\r\n\\{}a""")]
        public async Task Test_Verbatim_SpecialChars(string fromData, string toData)
        {
            await VerifyDiagnosticAndFixAsync(@"
class C
{
    void M(string s)
    {
        s = [||];
    }
}
", fromData, toData);
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        public async Task TestNoDiagnostic_RegularAndVerbatim()
        {
            await VerifyNoDiagnosticAsync(@"
class C
{
    void M()
    {
        string s = ""a"" + @""b"";
    }
}
");
        }

        [Fact, Trait(Traits.Analyzer, DiagnosticIdentifiers.JoinStringExpressions)]
        public async Task TestNoDiagnostic_LiteralAndInterpolated()
        {
            await VerifyNoDiagnosticAsync(@"
class C
{
    void M()
    {
        string s = ""a"" + $""b"";
    }
}
");
        }
    }
}
