// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CodeFixes;
using Roslynator.CSharp;
using Roslynator.CSharp.CodeFixes;
using Xunit;
using static Roslynator.Tests.CSharp.CSharpCompilerCodeFixVerifier;

namespace Roslynator.CodeFixes.Tests
{
    public static class CS1624BodyCannotBeIteratorBlockBecauseTypeIsNotIteratorInterfaceTypeTests
    {
        private const string DiagnosticId = CompilerDiagnosticIdentifiers.BodyCannotBeIteratorBlockBecauseTypeIsNotIteratorInterfaceType;

        private static CodeFixProvider CodeFixProvider { get; } = new MethodDeclarationOrLocalFunctionStatementCodeFixProvider();

        private const string Source = @"
using System;
using System.Collections.Generic;

class C
{
    void M()
    {
        yield return default(string);
        yield return DateTime.Now;

        void LF()
        {
            yield return default(string);
            yield return DateTime.Now;
        }
    }
}
";

        [Fact]
        public static void TestFix_String()
        {
            VerifyFix(Source, @"
using System;
using System.Collections.Generic;

class C
{
    IEnumerable<string> M()
    {
        yield return default(string);
        yield return DateTime.Now;

        IEnumerable<string> LF()
        {
            yield return default(string);
            yield return DateTime.Now;
        }
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, "string"));
        }

        [Fact]
        public static void TestFix_DateTime()
        {
            VerifyFix(Source, @"
using System;
using System.Collections.Generic;

class C
{
    IEnumerable<DateTime> M()
    {
        yield return default(string);
        yield return DateTime.Now;

        IEnumerable<DateTime> LF()
        {
            yield return default(string);
            yield return DateTime.Now;
        }
    }
}
", DiagnosticId, CodeFixProvider, EquivalenceKey.Create(DiagnosticId, "DateTime"));
        }

        [Fact]
        public static void TestNoFix()
        {
            VerifyNoFix(@"
class C
{
    void M()
    {
        yield return ;

        void LF()
        {
            yield return ;
        }
    }

    void M()
    {
        yield break;

        void LF()
        {
            yield break;
        }
    }
}
", CodeFixProvider, EquivalenceKey.Create(DiagnosticId));
        }
    }
}
