// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Xunit;

namespace Roslynator.CSharp.Refactorings.Tests
{
    public class RRX004SortCaseLabelsTests : AbstractCSharpCodeRefactoringVerifier
    {
        public override string RefactoringId { get; } = RefactoringIdentifiers.SortCaseLabels;

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.AddBraces)]
        public async Task Test()
        {
            await VerifyRefactoringAsync(@"
class C
{
    void M(string s)
    {
        switch (s)
        {
            [||]case ""a"":
            case ""d"":
            case ""c"":
            case ""b"":
                break;
            default:
                break;
        }
    }
}
", @"
class C
{
    void M(string s)
    {
        switch (s)
        {
            case ""a"":
            case ""b"":
            case ""c"":
            case ""d"":
                break;
            default:
                break;
        }
    }
}
", equivalenceKey: RefactoringId);
        }

        [Fact, Trait(Traits.Refactoring, RefactoringIdentifiers.AddBraces)]
        public async Task TestNoRefactoring_IsSorted()
        {
            await VerifyNoRefactoringAsync(@"
class C
{
    void M(string s)
    {
        switch (s)
        {
            [||]case ""a"":
            case ""b"":
            case ""c"":
            case ""d"":
                break;
            default:
                break;
        }
    }
}
", equivalenceKey: RefactoringId);
        }
    }
}
