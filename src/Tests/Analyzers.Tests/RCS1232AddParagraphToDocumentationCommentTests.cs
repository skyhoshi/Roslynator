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
    public class RCS1232AddParagraphToDocumentationCommentTests : AbstractCSharpCodeFixVerifier
    {
        public override DiagnosticDescriptor Descriptor { get; } = DiagnosticDescriptors.AddParagraphToDocumentationComment;

        public override DiagnosticAnalyzer Analyzer { get; } = new AddParagraphToDocumentationCommentAnalyzer();

        public override CodeFixProvider FixProvider { get; } = new XmlTextCodeFixProvider();

        [Fact]
        public async Task Test()
        {
            await VerifyDiagnosticAndFixAsync(@"
/// <summary>
///[| a
/// 
/// b|]
/// </summary>
class C
{
}
", @"
/// <summary>
/// <para>a</para>
/// <para>b</para>
/// </summary>
class C
{
}
");
        }

        [Fact]
        public async Task Test_Multiline()
        {
            await VerifyDiagnosticAndFixAsync(@"
/// <summary>
///[| a
/// b
/// 
/// c
/// d|]
/// </summary>
class C
{
}
", @"
/// <summary>
/// <para>
/// a
/// b
/// </para>
/// <para>
/// c
/// d
/// </para>
/// </summary>
class C
{
}
");
        }

        [Fact]
        public async Task Test_Multiline2()
        {
            await VerifyDiagnosticAndFixAsync(@"
/// <summary>
///[| a
/// b
/// 
/// c|]
/// </summary>
class C
{
}
", @"
/// <summary>
/// <para>
/// a
/// b
/// </para>
/// <para>c</para>
/// </summary>
class C
{
}
");
        }

        [Fact]
        public async Task Test_Multiline3()
        {
            await VerifyDiagnosticAndFixAsync(@"
/// <summary>
///[| a
/// 
/// c
/// d|]
/// </summary>
class C
{
}
", @"
/// <summary>
/// <para>a</para>
/// <para>
/// c
/// d
/// </para>
/// </summary>
class C
{
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_SimpleComment()
        {
            await VerifyNoDiagnosticAsync(@"
/// <summary>
/// a
/// </summary>
class C
{
}
");
        }

        [Fact]
        public async Task TestNoDiagnostic_NoEmptyLine()
        {
            await VerifyNoDiagnosticAsync(@"
/// <summary>
/// a
/// b
/// </summary>
class C
{
}
");
        }
    }
}
