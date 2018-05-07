// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Diagnostics;
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
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(XmlElementCodeFixProvider))]
    [Shared]
    public class XmlElementCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.AddParagraphToDocumentationComment); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(root, context.Span, out XmlElementSyntax xmlElement, findInsideTrivia: true))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];

            CodeAction codeAction = CodeAction.Create(
                "Add paragraphs",
                ct => RefactorAsync(context.Document, xmlElement, ct),
                GetEquivalenceKey(diagnostic));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        private static Task<Document> RefactorAsync(
            Document document,
            XmlElementSyntax xmlElement,
            CancellationToken cancellationToken)
        {
            SyntaxList<XmlNodeSyntax> nodes = xmlElement.Content;

            (TextSpan span1, TextSpan span2, IList<TextSpan> spans) = AddParagraphToDocumentationCommentAnalyzer.FindFixableTokens(nodes, new List<TextSpan>());

            Debug.Assert(spans.Count > 1);

            var textChanges = new List<TextChange>();

            string newLine = nodes
                .OfType<XmlTextSyntax>()
                .SelectMany(f => f.TextTokens)
                .First(f => f.IsKind(SyntaxKind.XmlTextLiteralNewLineToken))
                .ValueText;

            string indentation = xmlElement.GetIndentation(cancellationToken).ToString();

            int prevEnd = -1;

            foreach (TextSpan span in spans)
            {
                if (prevEnd != -1)
                    textChanges.Add(new TextChange(TextSpan.FromBounds(prevEnd, span.Start), $"{newLine}{indentation}///"));

                SyntaxToken token = xmlElement.FindToken(span.Start);
                SyntaxToken endToken = xmlElement.FindToken(span.End - 1);

                AddParagraph(token, endToken);

                prevEnd = endToken.Span.End;
            }

            return document.WithTextChangesAsync(textChanges, cancellationToken);

            void AddParagraph(SyntaxToken first, SyntaxToken last)
            {
                int start = first.SpanStart;
                if (first.ValueText[0] == ' ')
                    start++;

                TextSpan span = TextSpan.FromBounds(start, last.Span.End);

                bool isMultiline = xmlElement.SyntaxTree.IsMultiLineSpan(span, cancellationToken);

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
