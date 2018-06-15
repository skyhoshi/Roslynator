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
    public class JoinStringExpressionsAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.JoinStringExpressions); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeAddExpression, SyntaxKind.AddExpression);
        }

        public static void AnalyzeAddExpression(SyntaxNodeAnalysisContext context)
        {
            SyntaxNode node = context.Node;

            if (node.ContainsDiagnostics)
                return;

            if (node.SpanContainsDirectives())
                return;

            if (node.IsParentKind(SyntaxKind.AddExpression))
                return;

            var addExpression = (BinaryExpressionSyntax)node;

            ExpressionSyntax firstExpression = null;
            ExpressionSyntax lastExpression = null;
            bool isStringLiteral = false;
            bool isVerbatim = false;

            foreach (ExpressionSyntax expression in addExpression.AsChain())
            {
                switch (expression.Kind())
                {
                    case SyntaxKind.StringLiteralExpression:
                        {
                            bool isVerbatim2 = SyntaxInfo.StringLiteralExpressionInfo(expression).IsVerbatim;

                            if (firstExpression == null)
                            {
                                firstExpression = expression;
                                isStringLiteral = true;
                                isVerbatim = isVerbatim2;
                            }
                            else if (!isStringLiteral
                                || isVerbatim != isVerbatim2)
                            {
                                if (lastExpression != null)
                                    ReportDiagnostic(context, firstExpression, lastExpression, isVerbatim);

                                firstExpression = null;
                                lastExpression = null;
                            }
                            else
                            {
                                lastExpression = expression;
                            }

                            break;
                        }
                    case SyntaxKind.InterpolatedStringExpression:
                        {
                            bool isVerbatim2 = ((InterpolatedStringExpressionSyntax)expression).IsVerbatim();

                            if (firstExpression == null)
                            {
                                firstExpression = expression;
                                isStringLiteral = false;
                                isVerbatim = isVerbatim2;
                            }
                            else if (isStringLiteral
                                || isVerbatim != isVerbatim2)
                            {
                                if (lastExpression != null)
                                    ReportDiagnostic(context, firstExpression, lastExpression, isVerbatim);

                                firstExpression = null;
                                lastExpression = null;
                            }
                            else
                            {
                                lastExpression = expression;
                            }

                            break;
                        }
                }
            }

            if (lastExpression != null)
                ReportDiagnostic(context, firstExpression, lastExpression, isVerbatim);
        }

        private static void ReportDiagnostic(SyntaxNodeAnalysisContext context, ExpressionSyntax firstExpression, ExpressionSyntax lastExpression, bool isVerbatim)
        {
            SyntaxTree tree = firstExpression.SyntaxTree;
            TextSpan span = TextSpan.FromBounds(lastExpression.SpanStart, firstExpression.Span.End);

            if (isVerbatim
                || tree.IsSingleLineSpan(span, context.CancellationToken))
            {
                context.ReportDiagnostic(
                    DiagnosticDescriptors.JoinStringExpressions,
                    Location.Create(tree, span));
            }
        }
    }
}
