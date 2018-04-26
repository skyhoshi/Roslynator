// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS0508MethodReturnTypeMustMatchOverriddenMethodReturnTypeTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.MethodReturnTypeMustMatchOverriddenMethodReturnType;

        private static CodeFixProvider CodeFixProvider { get; } = new MemberDeclarationCodeFixProvider();

        [Fact]
        public static void TestFix()
        {
            VerifyFix(@"
class C
{
    public override object ToString()
    {
    }
}
", @"
class C
{
    public override string ToString()
    {
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }
    }
}
