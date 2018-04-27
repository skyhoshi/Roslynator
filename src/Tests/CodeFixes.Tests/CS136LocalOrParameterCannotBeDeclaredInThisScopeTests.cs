// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS136LocalOrParameterCannotBeDeclaredInThisScopeTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.LocalOrParameterCannotBeDeclaredInThisScopeBecauseThatNameIsUsedInEnclosingScopeToDefineLocalOrParameter;

        private static CodeFixProvider ParameterCodeFixProvider { get; } = new ParameterCannotBeDeclaredInThisScopeCodeFixProvider();
        private static CodeFixProvider LocalVariableCodeFixProvider { get; } = new VariableDeclarationCodeFixProvider();

        [Fact]
        public static void TestFix_RemoveParameter()
        {
            VerifyFix(@"
class C
{
    void M()
    {
        string value = null;

        void LF(string value)
        {
        }
    }
}
", @"
class C
{
    void M()
    {
        string value = null;

        void LF()
        {
        }
    }
}
", DiagnosticId, ParameterCodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }

        [Fact]
        public static void TestFix_ReplaceVariableDeclarationWithAssignment()
        {
            VerifyFix(@"
class C
{
    void M()
    {
        bool f = false;
        string s = null;

        if (f)
        {
            string s = null;
        }
    }
}
", @"
class C
{
    void M()
    {
        bool f = false;
        string s = null;

        if (f)
        {
            s = null;
        }
    }
}
", DiagnosticId, LocalVariableCodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }

        [Fact]
        public static void TestNoFix()
        {
            VerifyNoFix(
@"
class C
{
    void M()
    {
            if (true)
            {
                string s = "";
            }

            string s = null;
    }
}
", LocalVariableCodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }
    }
}
