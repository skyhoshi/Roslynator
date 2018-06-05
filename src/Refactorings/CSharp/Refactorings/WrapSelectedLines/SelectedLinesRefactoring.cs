// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Text;

namespace Roslynator.CSharp.Refactorings.WrapSelectedLines
{
    internal abstract class SelectedLinesRefactoring
    {
        public abstract ImmutableArray<TextChange> GetTextChanges(IEnumerable<TextLine> selectedLines);

        public Task<Document> RefactorAsync(
            Document document,
            TextLineCollectionSelection selectedLines,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            ImmutableArray<TextChange> textChanges = GetTextChanges(selectedLines);

            return document.WithTextChangesAsync(textChanges, cancellationToken);
        }
    }
}
