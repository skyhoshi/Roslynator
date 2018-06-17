// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp;
using Roslynator.CSharp.Syntax;
using Roslynator.CSharp.SyntaxWalkers;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class InlineLocalVariableAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get
            {
                return ImmutableArray.Create(
                    DiagnosticDescriptors.InlineLocalVariable,
                    DiagnosticDescriptors.InlineLocalVariableFadeOut);
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterSyntaxNodeAction(AnalyzeLocalDeclarationStatement, SyntaxKind.LocalDeclarationStatement);
        }

        public static void AnalyzeLocalDeclarationStatement(SyntaxNodeAnalysisContext context)
        {
            var localDeclarationStatement = (LocalDeclarationStatementSyntax)context.Node;

            if (localDeclarationStatement.ContainsDiagnostics)
                return;

            if (localDeclarationStatement.SpanOrTrailingTriviaContainsDirectives())
                return;

            SingleLocalDeclarationStatementInfo localDeclarationInfo = SyntaxInfo.SingleLocalDeclarationStatementInfo(localDeclarationStatement);

            if (!localDeclarationInfo.Success)
                return;

            ExpressionSyntax value = localDeclarationInfo.Value;

            if (value == null)
                return;

            SyntaxList<StatementSyntax> statements = SyntaxInfo.StatementListInfo(localDeclarationStatement).Statements;

            if (!statements.Any())
                return;

            int index = statements.IndexOf(localDeclarationStatement);

            if (index == statements.Count - 1)
                return;

            StatementSyntax nextStatement = statements[index + 1];

            if (nextStatement.ContainsDiagnostics)
                return;

            switch (nextStatement.Kind())
            {
                case SyntaxKind.ExpressionStatement:
                    {
                        Analyze(context, statements, localDeclarationInfo, index, (ExpressionStatementSyntax)nextStatement);
                        break;
                    }
                case SyntaxKind.LocalDeclarationStatement:
                    {
                        Analyze(context, statements, localDeclarationInfo, index, (LocalDeclarationStatementSyntax)nextStatement);
                        break;
                    }
                case SyntaxKind.ReturnStatement:
                    {
                        var returnStatement = (ReturnStatementSyntax)nextStatement;

                        if (!returnStatement.SpanOrLeadingTriviaContainsDirectives())
                        {
                            ExpressionSyntax expression = returnStatement.Expression;

                            if (expression?.Kind() == SyntaxKind.IdentifierName)
                            {
                                var identifierName = (IdentifierNameSyntax)expression;

                                if (string.Equals(localDeclarationInfo.IdentifierText, identifierName.Identifier.ValueText, StringComparison.Ordinal))
                                    ReportDiagnostic(context, localDeclarationInfo, expression);
                            }
                        }

                        break;
                    }
                case SyntaxKind.YieldReturnStatement:
                    {
                        var yieldStatement = (YieldStatementSyntax)nextStatement;

                        if (index == statements.Count - 2
                            && !yieldStatement.SpanOrLeadingTriviaContainsDirectives())
                        {
                            ExpressionSyntax expression = yieldStatement.Expression;

                            if (expression?.Kind() == SyntaxKind.IdentifierName)
                            {
                                var identifierName = (IdentifierNameSyntax)expression;

                                if (string.Equals(localDeclarationInfo.IdentifierText, identifierName.Identifier.ValueText, StringComparison.Ordinal))
                                    ReportDiagnostic(context, localDeclarationInfo, expression);
                            }
                        }

                        break;
                    }
                case SyntaxKind.ForEachStatement:
                    {
                        if (value.IsSingleLine()
                            && !value.IsKind(SyntaxKind.ArrayInitializerExpression))
                        {
                            var forEachStatement = (ForEachStatementSyntax)nextStatement;
                            Analyze(context, statements, localDeclarationInfo, forEachStatement.Expression);
                        }

                        break;
                    }
                case SyntaxKind.SwitchStatement:
                    {
                        if (value.IsSingleLine())
                        {
                            var switchStatement = (SwitchStatementSyntax)nextStatement;
                            Analyze(context, statements, localDeclarationInfo, switchStatement.Expression);
                        }

                        break;
                    }
            }
        }

        private static void Analyze(
            SyntaxNodeAnalysisContext context,
            SyntaxList<StatementSyntax> statements,
            in SingleLocalDeclarationStatementInfo localDeclarationInfo,
            int index,
            ExpressionStatementSyntax expressionStatement)
        {
            if (expressionStatement.SpanOrLeadingTriviaContainsDirectives())
                return;

            SimpleAssignmentExpressionInfo assignment = SyntaxInfo.SimpleAssignmentExpressionInfo(expressionStatement.Expression);

            if (!assignment.Success)
                return;

            if (assignment.Right.Kind() != SyntaxKind.IdentifierName)
                return;

            var identifierName = (IdentifierNameSyntax)assignment.Right;

            if (!string.Equals(localDeclarationInfo.IdentifierText, identifierName.Identifier.ValueText, StringComparison.Ordinal))
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            ISymbol localSymbol = semanticModel.GetDeclaredSymbol(localDeclarationInfo.Declarator, cancellationToken);

            if (localSymbol?.IsErrorType() != false)
                return;

            if (IsLocalVariableReferenced(localSymbol, assignment.Left, assignment.Left.Span, semanticModel, cancellationToken))
                return;

            if (index < statements.Count - 2)
            {
                TextSpan span = TextSpan.FromBounds(statements[index + 2].SpanStart, statements.Last().Span.End);

                if (IsLocalVariableReferenced(localSymbol, localDeclarationInfo.Statement.Parent, span, semanticModel, cancellationToken))
                    return;
            }

            ReportDiagnostic(context, localDeclarationInfo, identifierName);
        }

        private static void Analyze(
            SyntaxNodeAnalysisContext context,
            SyntaxList<StatementSyntax> statements,
            in SingleLocalDeclarationStatementInfo localDeclarationInfo,
            int index,
            LocalDeclarationStatementSyntax localDeclaration2)
        {
            if (localDeclaration2.SpanOrLeadingTriviaContainsDirectives())
                return;

            ExpressionSyntax value = localDeclaration2
                .Declaration?
                .Variables
                .SingleOrDefault(shouldThrow: false)?
                .Initializer?
                .Value;

            if (value?.Kind() != SyntaxKind.IdentifierName)
                return;

            var identifierName = (IdentifierNameSyntax)value;

            if (!string.Equals(localDeclarationInfo.IdentifierText, identifierName.Identifier.ValueText, StringComparison.Ordinal))
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            ISymbol localSymbol = semanticModel.GetDeclaredSymbol(localDeclarationInfo.Declarator, cancellationToken);

            if (localSymbol?.IsErrorType() != false)
                return;

            if (index < statements.Count - 2)
            {
                TextSpan span = TextSpan.FromBounds(statements[index + 2].SpanStart, statements.Last().Span.End);

                if (IsLocalVariableReferenced(localSymbol, localDeclarationInfo.Statement.Parent, span, semanticModel, cancellationToken))
                    return;
            }

            ReportDiagnostic(context, localDeclarationInfo, identifierName);
        }

        private static void Analyze(
            SyntaxNodeAnalysisContext context,
            SyntaxList<StatementSyntax> statements,
            in SingleLocalDeclarationStatementInfo localDeclarationInfo,
            ExpressionSyntax expression)
        {
            SyntaxNode parent = localDeclarationInfo.Statement.Parent;

            if (parent.ContainsDirectives(TextSpan.FromBounds(expression.Parent.FullSpan.Start, expression.Span.End)))
                return;

            if (expression?.Kind() != SyntaxKind.IdentifierName)
                return;

            var identifierName = (IdentifierNameSyntax)expression;

            if (!string.Equals(localDeclarationInfo.IdentifierText, identifierName.Identifier.ValueText, StringComparison.Ordinal))
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            ISymbol localSymbol = semanticModel.GetDeclaredSymbol(localDeclarationInfo.Declarator, cancellationToken);

            if (localSymbol?.IsErrorType() != false)
                return;

            TextSpan span = TextSpan.FromBounds(expression.Span.End, statements.Last().Span.End);

            if (IsLocalVariableReferenced(localSymbol, parent, span, semanticModel, cancellationToken))
                return;

            ReportDiagnostic(context, localDeclarationInfo, expression);
        }

        private static void ReportDiagnostic(
            SyntaxNodeAnalysisContext context,
            in SingleLocalDeclarationStatementInfo localDeclarationInfo,
            ExpressionSyntax expression)
        {
            context.ReportDiagnostic(DiagnosticDescriptors.InlineLocalVariable, localDeclarationInfo.Statement);

            foreach (SyntaxToken modifier in localDeclarationInfo.Modifiers)
                context.ReportToken(DiagnosticDescriptors.InlineLocalVariableFadeOut, modifier);

            context.ReportNode(DiagnosticDescriptors.InlineLocalVariableFadeOut, localDeclarationInfo.Type);
            context.ReportToken(DiagnosticDescriptors.InlineLocalVariableFadeOut, localDeclarationInfo.Identifier);
            context.ReportToken(DiagnosticDescriptors.InlineLocalVariableFadeOut, localDeclarationInfo.EqualsToken);
            context.ReportToken(DiagnosticDescriptors.InlineLocalVariableFadeOut, localDeclarationInfo.SemicolonToken);
            context.ReportNode(DiagnosticDescriptors.InlineLocalVariableFadeOut, expression);
        }

        private static bool IsLocalVariableReferenced(
            ISymbol symbol,
            SyntaxNode node,
            TextSpan span,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            return LocalOrParameterReferenceWalker.ContainsReference(node, symbol, semanticModel, span, cancellationToken);
        }
    }
}
