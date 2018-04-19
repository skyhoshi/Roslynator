// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.Syntax;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class UsePatternMatchingInsteadOfIsAndCastAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.UsePatternMatchingInsteadOfIsAndCast); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeIsExpression, SyntaxKind.IsExpression);
        }

        public static void AnalyzeIsExpression(SyntaxNodeAnalysisContext context)
        {
            var isExpression = (BinaryExpressionSyntax)context.Node;

            IsExpressionInfo isExpressionInfo = SyntaxInfo.IsExpressionInfo((BinaryExpressionSyntax)context.Node);

            if (!isExpressionInfo.Success)
                return;

            ExpressionSyntax expression = isExpressionInfo.Expression;

            if (!(expression is IdentifierNameSyntax identifierName))
            {
                identifierName = ThisMemberAccessExpressionInfo.Create(expression).Name as IdentifierNameSyntax;

                if (identifierName == null)
                    return;
            }

            ExpressionSyntax left = isExpression.WalkUpParentheses();

            SyntaxNode parent = left.Parent;

            if (!parent.IsKind(SyntaxKind.LogicalAndExpression))
                return;

            if (parent.ContainsDiagnostics)
                return;

            var logicalAnd = (BinaryExpressionSyntax)parent;

            if (left != logicalAnd.Left)
                return;

            ExpressionSyntax right = logicalAnd.Right;

            if (right == null)
                return;

            string name = identifierName.Identifier.ValueText;

            ISymbol symbol = null;

            bool containsAny = false;

            //TODO: SyntaxWalker
            foreach (SyntaxNode node in right.DescendantNodes())
            {
                if (node is IdentifierNameSyntax identifierName2
                    && string.Equals(identifierName2.Identifier.ValueText, name))
                {
                    if (symbol == null)
                    {
                        symbol = context.SemanticModel.GetSymbol(identifierName, context.CancellationToken);

                        if (symbol?.IsErrorType() != false)
                            return;
                    }

                    if (symbol.Equals(context.SemanticModel.GetSymbol(identifierName2, context.CancellationToken)))
                    {
                        SyntaxNode parent2 = identifierName2.Parent;

                        ThisMemberAccessExpressionInfo thisMemberAccess = ThisMemberAccessExpressionInfo.Create(parent2);

                        if (thisMemberAccess.Success)
                            parent2 = thisMemberAccess.MemberAccessExpression.Parent;

                        if (!(parent2 is CastExpressionSyntax))
                            return;

                        containsAny = true;
                    }
                }
            }

            if (!containsAny)
                return;

            context.ReportDiagnostic(DiagnosticDescriptors.UsePatternMatchingInsteadOfIsAndCast, logicalAnd);
        }
    }
}
