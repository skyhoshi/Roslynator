// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.CodeRefactorings;
using Roslynator.Tests.CSharp;

namespace Roslynator.CSharp.Refactorings.Tests
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class AbstractCSharpCodeRefactoringVerifier : CSharpCodeRefactoringVerifier
    {
        public override CodeRefactoringProvider RefactoringProvider { get; } = new RoslynatorCodeRefactoringProvider();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{RefactoringId} {RefactoringProvider.GetType().Name}"; }
        }
    }
}
