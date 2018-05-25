// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslynator.CSharp.SyntaxWalkers;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MarkParameterWithInModifierAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.MarkParameterWithInModifier); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);
            context.EnableConcurrentExecution();

            //TODO: AnalyzeIndexerDeclaration
            context.RegisterSyntaxNodeAction(AnalyzeMethodDeclaration, SyntaxKind.MethodDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeConstructorDeclaration, SyntaxKind.ConstructorDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeOperatorDeclaration, SyntaxKind.OperatorDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeConversionOperatorDeclaration, SyntaxKind.ConversionOperatorDeclaration);
            context.RegisterSyntaxNodeAction(AnalyzeLocalFunction, SyntaxKind.LocalFunctionStatement);
        }

        private static void AnalyzeMethodDeclaration(SyntaxNodeAnalysisContext context)
        {
            var methodDeclaration = (MethodDeclarationSyntax)context.Node;

            if (methodDeclaration.Modifiers.Contains(SyntaxKind.AsyncKeyword))
                return;

            Analyze(context, methodDeclaration, methodDeclaration.ParameterList, methodDeclaration.BodyOrExpressionBody());
        }

        private static void AnalyzeConstructorDeclaration(SyntaxNodeAnalysisContext context)
        {
            var constructorDeclaration = (ConstructorDeclarationSyntax)context.Node;

            Analyze(context, constructorDeclaration, constructorDeclaration.ParameterList, constructorDeclaration.BodyOrExpressionBody());
        }

        private static void AnalyzeOperatorDeclaration(SyntaxNodeAnalysisContext context)
        {
            var operatorDeclaration = (OperatorDeclarationSyntax)context.Node;

            Analyze(context, operatorDeclaration, operatorDeclaration.ParameterList, operatorDeclaration.BodyOrExpressionBody());
        }

        private static void AnalyzeConversionOperatorDeclaration(SyntaxNodeAnalysisContext context)
        {
            var operatorDeclaration = (ConversionOperatorDeclarationSyntax)context.Node;

            Analyze(context, operatorDeclaration, operatorDeclaration.ParameterList, operatorDeclaration.BodyOrExpressionBody());
        }

        private static void AnalyzeLocalFunction(SyntaxNodeAnalysisContext context)
        {
            var localFunction = (LocalFunctionStatementSyntax)context.Node;

            if (localFunction.Modifiers.Contains(SyntaxKind.AsyncKeyword))
                return;

            Analyze(context, localFunction, localFunction.ParameterList, localFunction.BodyOrExpressionBody());
        }

        private static void Analyze(
            SyntaxNodeAnalysisContext context,
            SyntaxNode declaration,
            ParameterListSyntax parameterList,
            CSharpSyntaxNode bodyOrExpressionBody)
        {
            if (parameterList == null)
                return;

            if (bodyOrExpressionBody == null)
                return;

            if (!parameterList.Parameters.Any())
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            var methodSymbol = (IMethodSymbol)semanticModel.GetDeclaredSymbol(declaration, cancellationToken);

            if (methodSymbol.ImplementsInterfaceMember(allInterfaces: true))
                return;

            Dictionary<string, IParameterSymbol> parameters = null;

            foreach (IParameterSymbol parameter in methodSymbol.Parameters)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (parameter.RefKind != RefKind.None)
                    continue;

                ITypeSymbol type = parameter.Type;

                if (type.TypeKind != TypeKind.Struct)
                    continue;

                if (!(type.GetSyntaxOrDefault(cancellationToken) is StructDeclarationSyntax structDeclaration))
                    continue;

                if (!structDeclaration.Modifiers.Contains(SyntaxKind.ReadOnlyKeyword))
                    continue;

                (parameters ?? (parameters = new Dictionary<string, IParameterSymbol>())).Add(parameter.Name, parameter);
            }

            if (parameters == null)
                return;

            var walker = new SyntaxWalker(parameters, semanticModel, cancellationToken);

            walker.Visit(bodyOrExpressionBody);

            foreach (KeyValuePair<string, IParameterSymbol> kvp in walker.Parameters)
            {
                var parameter = (ParameterSyntax)kvp.Value.GetSyntax(cancellationToken);

                context.ReportDiagnostic(DiagnosticDescriptors.MarkParameterWithInModifier, parameter.Identifier);
            }
        }

        private class SyntaxWalker : AssignedExpressionWalker
        {
            private bool _isInAssignedExpression;
            private int _localFunctionNesting;
            private int _anonymousFunctionNesting;
            private readonly SemanticModel _semanticModel;
            private CancellationToken _cancellationToken;

            public SyntaxWalker(
                Dictionary<string, IParameterSymbol> parameters,
                SemanticModel semanticModel,
                CancellationToken cancellationToken)
            {
                Parameters = parameters;
                _semanticModel = semanticModel;
                _cancellationToken = cancellationToken;
            }

            public Dictionary<string, IParameterSymbol> Parameters { get; }

            public override void Visit(SyntaxNode node)
            {
                if (Parameters.Count > 0)
                    base.Visit(node);
            }

            public override void VisitIdentifierName(IdentifierNameSyntax node)
            {
                _cancellationToken.ThrowIfCancellationRequested();

                string name = node.Identifier.ValueText;

                if (Parameters.TryGetValue(name, out IParameterSymbol parameterSymbol)
                    && _semanticModel.GetSymbol(node, _cancellationToken).Equals(parameterSymbol))
                {
                    if (_isInAssignedExpression
                        || _localFunctionNesting > 0
                        || _anonymousFunctionNesting > 0)
                    {
                        Parameters.Remove(name);
                    }
                }

                base.VisitIdentifierName(node);
            }

            public override void VisitAssignedExpression(ExpressionSyntax expression)
            {
                Debug.Assert(!_isInAssignedExpression);

                _isInAssignedExpression = true;
                Visit(expression);
                _isInAssignedExpression = false;
            }

            public override void VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax node)
            {
                _anonymousFunctionNesting++;
                base.VisitAnonymousMethodExpression(node);
                _anonymousFunctionNesting--;
            }

            public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
            {
                _anonymousFunctionNesting++;
                base.VisitSimpleLambdaExpression(node);
                _anonymousFunctionNesting--;
            }

            public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
            {
                _anonymousFunctionNesting++;
                base.VisitParenthesizedLambdaExpression(node);
                _anonymousFunctionNesting--;
            }

            public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
            {
                _localFunctionNesting++;
                base.VisitLocalFunctionStatement(node);
                _localFunctionNesting--;
            }
        }
    }
}
