// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator.Text;

namespace Roslynator.CSharp.Refactorings
{
    internal static class JoinStringExpressionsRefactoring
    {
        public static Task<Document> RefactorAsync(
            Document document,
            BinaryExpressionSyntax binaryExpression,
            TextSpan span,
            CancellationToken cancellationToken)
        {
            ExpressionSyntax[] expressions = binaryExpression
                .AsChain()
                .Where(f => span.Contains(f.Span))
                .Reverse()
                .ToArray();

            ExpressionSyntax firstExpression = expressions[0];

            bool isVerbatim = false;
            bool isInterpolated = false;

            if (firstExpression is InterpolatedStringExpressionSyntax interpolatedString)
            {
                isVerbatim = interpolatedString.IsVerbatim();
                isInterpolated = true;
            }
            else
            {
                isVerbatim = SyntaxInfo.StringLiteralExpressionInfo(firstExpression).IsVerbatim;
            }

            StringBuilder sb = StringBuilderCache.GetInstance();

            var b = new StringTextBuilder(sb, isVerbatim: isVerbatim, isInterpolated: isInterpolated);

            b.AppendStart();

            foreach (ExpressionSyntax expression in expressions)
            {
                switch (expression.Kind())
                {
                    case SyntaxKind.StringLiteralExpression:
                        {
                            b.Append((LiteralExpressionSyntax)expression);
                            break;
                        }
                    case SyntaxKind.InterpolatedStringExpression:
                        {
                            b.Append((InterpolatedStringExpressionSyntax)expression);
                            break;
                        }
                }
            }

            b.AppendEnd();

            string newText = b.ToString();

            StringBuilderCache.Free(sb);

            var textChange = new TextChange(TextSpan.FromBounds(firstExpression.SpanStart, expressions.Last().Span.End), newText);

            return document.WithTextChangeAsync(textChange, cancellationToken);
        }
    }
}
