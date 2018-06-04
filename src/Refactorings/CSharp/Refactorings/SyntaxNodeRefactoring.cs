// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp.Refactorings.WrapSelectedLines;
using Roslynator.Text;

namespace Roslynator.CSharp.Refactorings
{
    internal static class SyntaxNodeRefactoring
    {
        public static async Task ComputeRefactoringsAsync(RefactoringContext context, SyntaxNode node)
        {
            if (context.IsAnyRefactoringEnabled(
                RefactoringIdentifiers.WrapInRegion,
                RefactoringIdentifiers.WrapInIfDirective,
                RefactoringIdentifiers.RemoveEmptyLines))
            {
                SyntaxNode root = context.Root;
                TextSpan span = context.Span;

                if (!IsFullLineSelection(node, span))
                    return;

                Document document = context.Document;
                SourceText sourceText = await document.GetTextAsync(context.CancellationToken).ConfigureAwait(false);

                if (!TextLineCollectionSelection.TryCreate(sourceText.Lines, span, out TextLineCollectionSelection selectedLines))
                    return;

                if (!IsInMultiLineDocumentationComment(root, span.Start)
                    && !IsInMultiLineDocumentationComment(root, span.End))
                {
                    if (context.IsRefactoringEnabled(RefactoringIdentifiers.WrapInRegion))
                    {
                        context.RegisterRefactoring(
                            "Wrap in region",
                            ct => WrapInRegionRefactoring.Instance.RefactorAsync(document, selectedLines, ct),
                            RefactoringIdentifiers.WrapInRegion);
                    }

                    if (context.IsRefactoringEnabled(RefactoringIdentifiers.WrapInIfDirective))
                    {
                        context.RegisterRefactoring(
                            "Wrap in #if",
                            ct => WrapInIfDirectiveRefactoring.Instance.RefactorAsync(document, selectedLines, ct),
                            RefactoringIdentifiers.WrapInIfDirective);
                    }
                }

                if (context.IsRefactoringEnabled(RefactoringIdentifiers.RemoveEmptyLines))
                {
                    bool containsEmptyLine = false;

                    foreach (TextLine line in selectedLines)
                    {
                        if (line.IsEmptyOrWhiteSpace()
                            && root.FindTrivia(line.End, findInsideTrivia: true).IsEndOfLineTrivia())
                        {
                            containsEmptyLine = true;
                            break;
                        }
                    }

                    if (containsEmptyLine)
                    {
                        context.RegisterRefactoring(
                            "Remove empty lines",
                            ct =>
                            {
                                IEnumerable<TextChange> textChanges = selectedLines
                                    .Where(line => line.IsEmptyOrWhiteSpace() && root.FindTrivia(line.End, findInsideTrivia: true).IsEndOfLineTrivia())
                                    .Select(line => new TextChange(line.SpanIncludingLineBreak, ""));

                                return document.WithTextChangesAsync(textChanges, ct);
                            },
                            RefactoringIdentifiers.RemoveEmptyLines);
                    }
                }
            }
        }

        private static bool IsFullLineSelection(SyntaxNode node, TextSpan span)
        {
            if (span.IsEmpty)
                return false;

            if (!IsStartOfLine(node, span.Start))
                return false;

            int end = span.End;

            if (node.FullSpan.End == end)
            {
                SyntaxTrivia trivia = node.FindTrivia(end - 1);

                return trivia.IsEndOfLineTrivia()
                    && trivia.Span.End == end;
            }

            return IsStartOfLine(node, end, orEndOfLine: true);
        }

        private static bool IsStartOfLine(SyntaxNode node, int position, bool orEndOfLine = false)
        {
            SyntaxToken token = node.FindToken(position, findInsideTrivia: true);

            if (orEndOfLine
                && token.IsKind(SyntaxKind.XmlTextLiteralNewLineToken))
            {
                return true;
            }

            SyntaxTriviaList leadingTrivia = token.LeadingTrivia;

            if (leadingTrivia.Any()
                && leadingTrivia.Span.Contains(position))
            {
                foreach (SyntaxTrivia trivia in leadingTrivia)
                {
                    if (trivia.IsEndOfLineTrivia()
                        && trivia.Span.End == position)
                    {
                        return true;
                    }
                }
            }
            else if (orEndOfLine)
            {
                SyntaxTriviaList trailingTrivia = token.TrailingTrivia;

                if (trailingTrivia.Any()
                    && trailingTrivia.Span.Contains(position))
                {
                    foreach (SyntaxTrivia trivia in trailingTrivia)
                    {
                        if (trivia.IsEndOfLineTrivia()
                            && trivia.Span.Start == position)
                        {
                            return true;
                        }
                    }
                }
            }

            SyntaxToken prevToken = token.GetPreviousToken(includeDocumentationComments: true);

            if (prevToken.IsKind(SyntaxKind.XmlTextLiteralNewLineToken)
                && prevToken.Span.End == position)
            {
                return true;
            }

            prevToken = token.GetPreviousToken();

            foreach (SyntaxTrivia trivia in prevToken.TrailingTrivia)
            {
                if (trivia.IsEndOfLineTrivia()
                    && trivia.Span.End == position)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsInMultiLineDocumentationComment(SyntaxNode root, int position)
        {
            SyntaxToken token = root.FindToken(position, findInsideTrivia: true);

            for (SyntaxNode node = token.Parent; node != null; node = node.Parent)
            {
                if (node.IsKind(SyntaxKind.MultiLineDocumentationCommentTrivia))
                    return true;
            }

            return false;
        }
    }
}
