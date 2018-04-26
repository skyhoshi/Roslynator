// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS0266CannotImplicitlyConvertTypeExplicitConversionExistsTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.CannotImplicitlyConvertTypeExplicitConversionExists;

        private static CodeFixProvider CodeFixProvider { get; } = new ExpressionCodeFixProvider();

        [Fact]
        public static void TestFix_ChangeTypeAccordingToInitializer()
        {
            VerifyFix(@"
using System.Collections.Generic;

public class Foo
{
    public static void Bar()
    {
        Foo x = GetValues();
    }

    public static IEnumerable<Foo> GetValues()
    {
        yield break;
    }
}
", @"
using System.Collections.Generic;

public class Foo
{
    public static void Bar()
    {
        IEnumerable<Foo> x = GetValues();
    }

    public static IEnumerable<Foo> GetValues()
    {
        yield break;
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, additionalKey1: CodeFixIdentifiers.ChangeTypeAccordingToInitializer));
        }
    }
}
