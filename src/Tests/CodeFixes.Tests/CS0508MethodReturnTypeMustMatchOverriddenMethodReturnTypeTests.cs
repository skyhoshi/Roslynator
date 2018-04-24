// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS0508MethodReturnTypeMustMatchOverriddenMethodReturnTypeTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.MethodReturnTypeMustMatchOverriddenMethodReturnType;

        [Fact]
        public static void TestCodeFix()
        {
            VerifyFix(
@"
class C
{
    public override object ToString()
    {
    }
}
",
@"
class C
{
    public override string ToString()
    {
    }
}
",
                diagnosticId: DiagnosticId,
                fixProvider: new MemberDeclarationCodeFixProvider(),
                equivalenceKey: EquivalenceKey.Create(DiagnosticId));
        }
    }
}
