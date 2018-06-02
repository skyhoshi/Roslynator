// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Roslynator.Tests;
using Xunit;

#pragma warning disable RCS1090

namespace Roslynator.CSharp.Refactorings.Tests
{
    public class RRX001WrapInElementTests : AbstractCSharpCodeRefactoringVerifier
    {
        public override string RefactoringId { get; } = RefactoringIdentifiers.WrapInElement;

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.WrapInElement)]
        public async Task Test()
        {
            await VerifyRefactoringAsync(@"
class C
{
    /// <summary>
    /// x is [|null|] or y is [|null|]
    /// </summary>
    void M()
    {
    }
}
", @"
class C
{
    /// <summary>
    /// x is <c>null</c> or y is <c>null</c>
    /// </summary>
    void M()
    {
    }
}
", equivalenceKey: RefactoringId);
        }

        //[Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.AddBraces)]
        public async Task TestNoRefactoring()
        {
            await VerifyNoRefactoringAsync(@"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

class C
{
    void M()
    {
    }
}
", equivalenceKey: RefactoringId);
        }
    }
}
