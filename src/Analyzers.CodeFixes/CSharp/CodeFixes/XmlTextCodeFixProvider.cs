// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CodeFixes;
using Roslynator.CSharp.Analysis;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(XmlTextCodeFixProvider))]
    [Shared]
    public class XmlTextCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.AddParagraphToDocumentationComment); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(root, context.Span, out XmlTextSyntax xmlText, findInsideTrivia: true))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];

            CodeAction codeAction = CodeAction.Create(
                "Add paragraphs",
                ct => RefactorAsync(context.Document, xmlText, context.Span, ct),
                GetEquivalenceKey(diagnostic));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        private static Task<Document> RefactorAsync(
            Document document,
            XmlTextSyntax xmlText,
            TextSpan span,
            CancellationToken cancellationToken)
        {
            SyntaxTokenList tokens = xmlText.TextTokens;

            (int index1, int index2, int index3, int index4) = AddParagraphToDocumentationCommentAnalyzer.FindFixableTokens(tokens);

            var textChanges = new List<TextChange>();

            string newLine = tokens.First(f => f.IsKind(SyntaxKind.XmlTextLiteralNewLineToken)).ValueText;
            string indentation = xmlText.GetIndentation(cancellationToken).ToString();

            AddParagraph(tokens[index1], tokens[index2]);

            textChanges.Add(new TextChange(TextSpan.FromBounds(tokens[index2].Span.End, tokens[index3].Span.Start), $"{newLine}{indentation}///"));

            AddParagraph(tokens[index3], tokens[index4]);

            return document.WithTextChangesAsync(textChanges, cancellationToken);

            void AddParagraph(SyntaxToken token1, SyntaxToken token2)
            {
                int spanStart = token1.SpanStart;
                if (token1.ValueText[0] == ' ')
                    spanStart++;

                span = TextSpan.FromBounds(spanStart, token2.Span.End);

                bool isMultiline = xmlText.SyntaxTree.IsMultiLineSpan(span, cancellationToken);

                string text = "<para>";

                if (isMultiline)
                    text += $"{newLine}{indentation}/// ";

                textChanges.Add(new TextChange(new TextSpan(span.Start, 0), text));

                text = "";

                if (isMultiline)
                    text += $"{newLine}{indentation}/// ";

                text += "</para>";

                textChanges.Add(new TextChange(new TextSpan(span.End, 0), text));
            }
        }
    }
}
