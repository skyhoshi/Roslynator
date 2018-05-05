// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddParagraphToDocumentationCommentAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.AddParagraphToDocumentationComment); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeSingleLineDocumentationCommentTrivia, SyntaxKind.SingleLineDocumentationCommentTrivia);
        }

        private static void AnalyzeSingleLineDocumentationCommentTrivia(SyntaxNodeAnalysisContext context)
        {
            var documentationComment = (DocumentationCommentTriviaSyntax)context.Node;

            if (!documentationComment.IsPartOfMemberDeclaration())
                return;

            XmlElementSyntax summaryElement = documentationComment.SummaryElement();

            if (summaryElement == null)
                return;

            foreach (XmlNodeSyntax xmlNode in summaryElement.Content)
            {
                if (!xmlNode.IsKind(SyntaxKind.XmlText))
                    continue;

                var xmlText = (XmlTextSyntax)xmlNode;

                SyntaxTokenList tokens = xmlText.TextTokens;

                (int index1, int index2, int index3, int index4) = FindFixableTokens(tokens);

                if (index3 > index1)
                {
                    context.ReportDiagnostic(
                        DiagnosticDescriptors.AddParagraphToDocumentationComment,
                        Location.Create(documentationComment.SyntaxTree, TextSpan.FromBounds(tokens[index1].SpanStart, tokens[index4].Span.End)));
                }
            }
        }

        internal static (int index1, int index2, int index3, int index4) FindFixableTokens(SyntaxTokenList tokens)
        {
            int count = tokens.Count;

            if (count == 0)
                return default;

            int max = count - 1;
            int i = -1;
            SyntaxKind kind = default;
            SyntaxToken current = default;

            if (!FindParagraphStart())
                return default;

            int index1 = i;

            if (!MoveNewLine())
                return default;

            FindParagraphEnd();

            int index2 = i - 1;

            if (!FindParagraphStart())
                return default;

            int index3 = i;
            int index4 = index3;

            if (MoveNewLine())
            {
                FindParagraphEnd();
                index4 = i - 1;
            }

            return (index1, index2, index3, index4);

            bool MoveNext()
            {
                if (i < max)
                {
                    i++;
                    current = tokens[i];
                    kind = current.Kind();

                    Debug.Assert(kind.Is(SyntaxKind.XmlTextLiteralToken, SyntaxKind.XmlTextLiteralNewLineToken), kind.ToString());

                    return true;
                }

                return false;
            }

            bool MoveNewLine()
            {
                return MoveNext()
                    && kind == SyntaxKind.XmlTextLiteralNewLineToken;
            }

            bool FindParagraphStart()
            {
                while (MoveNext())
                {
                    if (kind == SyntaxKind.XmlTextLiteralNewLineToken)
                        continue;

                    if (kind == SyntaxKind.XmlTextLiteralToken)
                    {
                        if (StringUtility.IsWhitespace(current.ValueText))
                            continue;

                        return true;
                    }
                }

                return false;
            }

            int FindParagraphEnd()
            {
                int index = i;

                while (i < max - 1)
                {
                    SyntaxToken token = tokens[i + 1];
                    if (token.IsKind(SyntaxKind.XmlTextLiteralToken)
                        && !StringUtility.IsWhitespace(token.ValueText))
                    {
                        token = tokens[i + 2];
                        if (token.IsKind(SyntaxKind.XmlTextLiteralNewLineToken))
                        {
                            i++;
                            index = i;
                            i++;
                            current = token;
                            kind = SyntaxKind.XmlTextLiteralNewLineToken;

                            Debug.Assert(kind.Is(SyntaxKind.XmlTextLiteralToken, SyntaxKind.XmlTextLiteralNewLineToken), kind.ToString());
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return index;
            }
        }
    }
}
