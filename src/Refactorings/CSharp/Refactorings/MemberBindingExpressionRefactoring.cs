// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp.Refactorings
{
    internal static class MemberBindingExpressionRefactoring
    {
        public static async Task ComputeRefactoringAsync(RefactoringContext context, MemberBindingExpressionSyntax memberBindingExpression)
        {
            if (context.IsRefactoringEnabled(RefactoringIdentifiers.FormatExpressionChain))
                await FormatExpressionChainRefactoring.ComputeRefactoringsAsync(context, memberBindingExpression).ConfigureAwait(false);
        }
    }
}
