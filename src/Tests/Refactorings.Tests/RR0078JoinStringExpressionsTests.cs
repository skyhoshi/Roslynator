// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Roslynator.Tests;
using Xunit;

#pragma warning disable RCS1090

namespace Roslynator.CSharp.Refactorings.Tests
{
    public class RR0078JoinStringExpressionsTests : AbstractCSharpCodeRefactoringVerifier
    {
        public override string RefactoringId { get; } = RefactoringIdentifiers.JoinStringExpressions;

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.JoinStringExpressions)]
        public async Task Test()
        {
            await VerifyRefactoringAsync(@"
class C
{
    void M(string s)
    {
        s = s + [|""a"" + @""b""|];
    }
}
", @"
class C
{
    void M(string s)
    {
        s = s + ""ab"";
    }
}
", equivalenceKey: RefactoringId);
        }

        //[Theory, Trait(Traits.Refactoring, RefactoringIdentifiers.JoinStringExpressions)]
        //[InlineData("", "")]
        public async Task Test2(string fromData, string toData)
        {
            await VerifyRefactoringAsync(@"
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
", fromData, toData, equivalenceKey: RefactoringId);
        }

        //[Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.JoinStringExpressions)]
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
