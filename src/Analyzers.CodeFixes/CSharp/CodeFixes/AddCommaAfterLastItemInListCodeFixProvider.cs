// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Composition;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CodeFixes;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(AddCommaAfterLastItemInListCodeFixProvider))]
    [Shared]
    public class AddCommaAfterLastItemInListCodeFixProvider : BaseCodeFixProvider
    {
        private const string Title = "Add comma";

        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.AddCommaAfterLastItemInList); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(root, context.Span, out SyntaxNode node, predicate: f => f.IsKind(
                SyntaxKind.EnumDeclaration,
                SyntaxKind.ArrayInitializerExpression,
                SyntaxKind.CollectionInitializerExpression,
                SyntaxKind.ObjectInitializerExpression)))
            {
                return;
            }

            Diagnostic diagnostic = context.Diagnostics[0];

            if (node is EnumDeclarationSyntax enumDeclaration)
            {
                CodeAction codeAction = CodeAction.Create(
                    Title,
                    cancellationToken => RefactorAsync(context.Document, enumDeclaration.Members, cancellationToken),
                    GetEquivalenceKey(diagnostic));

                context.RegisterCodeFix(codeAction, diagnostic);
            }
            else
            {
                CodeAction codeAction = CodeAction.Create(
                    Title,
                    cancellationToken => RefactorAsync(context.Document, ((InitializerExpressionSyntax)node).Expressions, cancellationToken),
                    base.GetEquivalenceKey(diagnostic));

                context.RegisterCodeFix(codeAction, diagnostic);
            }
        }

        private static Task<Document> RefactorAsync<TNode>(
            Document document,
            SeparatedSyntaxList<TNode> expressions,
            CancellationToken cancellationToken) where TNode : SyntaxNode
        {
            var textChange = new TextChange(new TextSpan(expressions.Last().Span.End, 0), ",");

            return document.WithTextChangeAsync(textChange, cancellationToken);
        }
    }
}
