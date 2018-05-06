// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
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

            SyntaxList<XmlNodeSyntax> content = summaryElement.Content;

            if (!content.Any())
                return;

            (int index1, int index2, int index3, int index4) = FindFixableTokens(content);

            if (index4 != -1)
            {
                context.ReportDiagnostic(
                    DiagnosticDescriptors.AddParagraphToDocumentationComment,
                    Location.Create(documentationComment.SyntaxTree, TextSpan.FromBounds(index1, index4)));
            }
        }

        internal static (int index1, int index2, int index3, int index4) FindFixableTokens(SyntaxList<XmlNodeSyntax> nodes)
        {
            int index1 = -1;
            int index2 = -1;
            int index3 = -1;
            int index4 = -1;

            var state = State.BeforeParagraph;

            foreach (XmlNodeSyntax node in nodes)
            {
                switch (node.Kind())
                {
                    case SyntaxKind.XmlElement:
                    case SyntaxKind.XmlEmptyElement:
                        {
                            switch (state)
                            {
                                case State.BeforeParagraph:
                                    {
                                        state = State.Paragraph;
                                        index1 = node.SpanStart;
                                        break;
                                    }
                                case State.Paragraph:
                                    {
                                        if (index3 != -1)
                                        {
                                            index4 = node.SpanStart;
                                        }
                                        else
                                        {
                                            index2 = node.SpanStart;
                                        }

                                        break;
                                    }
                                case State.NewLine:
                                case State.WhiteSpaceAfterNewLine:
                                    {
                                        state = State.Paragraph;

                                        if (index3 != -1)
                                        {
                                            index4 = node.SpanStart;
                                        }
                                        else
                                        {
                                            index2 = node.SpanStart;
                                        }

                                        break;
                                    }
                                case State.WhiteSpaceBetweenParagraphs:
                                    {
                                        state = State.Paragraph;
                                        index3 = node.SpanStart;
                                        break;
                                    }
                            }

                            break;
                        }
                    case SyntaxKind.XmlText:
                        {
                            var xmlText = (XmlTextSyntax)node;

                            foreach (SyntaxToken token in xmlText.TextTokens)
                            {
                                switch (token.Kind())
                                {
                                    case SyntaxKind.XmlTextLiteralToken:
                                        {
                                            switch (state)
                                            {
                                                case State.BeforeParagraph:
                                                    {
                                                        if (!StringUtility.IsWhitespace(token.ValueText))
                                                        {
                                                            state = State.Paragraph;
                                                            index1 = token.SpanStart;
                                                        }

                                                        break;
                                                    }
                                                case State.Paragraph:
                                                    {
                                                        if (index3 != -1)
                                                        {
                                                            index4 = token.SpanStart;
                                                        }
                                                        else
                                                        {
                                                            index2 = token.SpanStart;
                                                        }

                                                        break;
                                                    }
                                                case State.NewLine:
                                                    {
                                                        if (StringUtility.IsWhitespace(token.ValueText))
                                                        {
                                                            state = State.WhiteSpaceAfterNewLine;
                                                        }
                                                        else
                                                        {
                                                            state = State.Paragraph;
                                                        }

                                                        break;
                                                    }
                                                case State.WhiteSpaceAfterNewLine:
                                                    {
                                                        if (!StringUtility.IsWhitespace(token.ValueText))
                                                            state = State.Paragraph;

                                                        break;
                                                    }
                                                case State.WhiteSpaceBetweenParagraphs:
                                                    {
                                                        if (!StringUtility.IsWhitespace(token.ValueText))
                                                        {
                                                            state = State.Paragraph;
                                                            index3 = token.SpanStart;
                                                        }

                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case SyntaxKind.XmlTextLiteralNewLineToken:
                                        {
                                            switch (state)
                                            {
                                                case State.BeforeParagraph:
                                                    {
                                                        break;
                                                    }
                                                case State.Paragraph:
                                                    {
                                                        state = State.NewLine;
                                                        break;
                                                    }
                                                case State.NewLine:
                                                case State.WhiteSpaceAfterNewLine:
                                                    {
                                                        if (index3 != -1)
                                                            return (index1, index2, index3, index4);

                                                        state = State.WhiteSpaceBetweenParagraphs;
                                                        break;
                                                    }
                                                case State.WhiteSpaceBetweenParagraphs:
                                                    {
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                }
                            }

                            break;
                        }
                }
            }

            return (index1, index2, index3, index4);
        }

        //internal static (int index1, int index2, int index3, int index4) FindFixableTokens(SyntaxTokenList tokens)
        //{
        //    int tokenCount = tokens.Count;

        //    if (tokenCount == 0)
        //        return default;

        //    int maxToken = tokenCount - 1;
        //    int j = -1;
        //    SyntaxKind tokenKind = default;
        //    SyntaxToken currentToken = default;

        //    if (!FindParagraphStart())
        //        return default;

        //    int index1 = j;

        //    if (!MoveNewLine())
        //        return default;

        //    FindParagraphEnd();

        //    int index2 = j - 1;

        //    if (!FindParagraphStart())
        //        return default;

        //    int index3 = j;
        //    int index4 = index3;

        //    if (MoveNewLine())
        //    {
        //        FindParagraphEnd();
        //        index4 = j - 1;
        //    }

        //    return (index1, index2, index3, index4);

        //    bool MoveNext()
        //    {
        //        if (j < maxToken)
        //        {
        //            j++;
        //            currentToken = tokens[j];
        //            tokenKind = currentToken.Kind();

        //            Debug.Assert(tokenKind.Is(SyntaxKind.XmlTextLiteralToken, SyntaxKind.XmlTextLiteralNewLineToken), tokenKind.ToString());

        //            return true;
        //        }

        //        return false;
        //    }

        //    bool MoveNewLine()
        //    {
        //        return MoveNext()
        //            && tokenKind == SyntaxKind.XmlTextLiteralNewLineToken;
        //    }

        //    bool FindParagraphStart()
        //    {
        //        while (MoveNext())
        //        {
        //            if (tokenKind == SyntaxKind.XmlTextLiteralNewLineToken)
        //                continue;

        //            if (tokenKind == SyntaxKind.XmlTextLiteralToken)
        //            {
        //                if (StringUtility.IsWhitespace(currentToken.ValueText))
        //                    continue;

        //                return true;
        //            }
        //        }

        //        return false;
        //    }

        //    int FindParagraphEnd()
        //    {
        //        int index = j;

        //        while (j < maxToken - 1)
        //        {
        //            SyntaxToken token = tokens[j + 1];
        //            if (token.IsKind(SyntaxKind.XmlTextLiteralToken)
        //                && !StringUtility.IsWhitespace(token.ValueText))
        //            {
        //                token = tokens[j + 2];
        //                if (token.IsKind(SyntaxKind.XmlTextLiteralNewLineToken))
        //                {
        //                    j++;
        //                    index = j;
        //                    j++;
        //                    currentToken = token;
        //                    tokenKind = SyntaxKind.XmlTextLiteralNewLineToken;

        //                    Debug.Assert(tokenKind.Is(SyntaxKind.XmlTextLiteralToken, SyntaxKind.XmlTextLiteralNewLineToken), tokenKind.ToString());
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            else
        //            {
        //                break;
        //            }
        //        }

        //        return index;
        //    }
        //}

        private enum State
        {
            BeforeParagraph,
            Paragraph,
            NewLine,
            WhiteSpaceAfterNewLine,
            WhiteSpaceBetweenParagraphs,
        }
    }
}
