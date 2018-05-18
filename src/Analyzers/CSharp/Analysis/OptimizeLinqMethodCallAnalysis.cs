// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslynator.CSharp.Syntax;

namespace Roslynator.CSharp.Analysis
{
    internal static class OptimizeLinqMethodCallAnalysis
    {
        public static void AnalyzeWhere(SyntaxNodeAnalysisContext context, SimpleMemberInvocationExpressionInfo invocationInfo)
        {
            SimpleMemberInvocationExpressionInfo invocationInfo2 = SyntaxInfo.SimpleMemberInvocationExpressionInfo(invocationInfo.Expression);

            if (!invocationInfo2.Success)
                return;

            if (invocationInfo2.Arguments.Count != 1)
                return;

            if (invocationInfo2.NameText != "Where")
                return;

            InvocationExpressionSyntax invocation = invocationInfo.InvocationExpression;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            IMethodSymbol methodSymbol = semanticModel.GetExtensionMethodInfo(invocation, cancellationToken).Symbol;

            if (methodSymbol == null)
                return;

            if (!SymbolUtility.IsLinqExtensionOfIEnumerableOfTWithoutParameters(methodSymbol, invocationInfo.NameText, semanticModel))
                return;

            IMethodSymbol methodSymbol2 = semanticModel.GetExtensionMethodInfo(invocationInfo2.InvocationExpression, cancellationToken).Symbol;

            if (methodSymbol2 == null)
                return;

            if (!SymbolUtility.IsLinqWhere(methodSymbol2, semanticModel, allowImmutableArrayExtension: true))
                return;

            TextSpan span = TextSpan.FromBounds(invocationInfo2.Name.SpanStart, invocation.Span.End);

            if (invocation.ContainsDirectives(span))
                return;

            context.ReportDiagnostic(
                DiagnosticDescriptors.OptimizeLinqMethodCall,
                Location.Create(invocation.SyntaxTree, span));
        }

        public static void AnalyzeFirstOrDefault(SyntaxNodeAnalysisContext context, in SimpleMemberInvocationExpressionInfo invocationInfo)
        {
            InvocationExpressionSyntax invocation = invocationInfo.InvocationExpression;

            ExtensionMethodSymbolInfo extensionMethodSymbolInfo = context.SemanticModel.GetReducedExtensionMethodInfo(invocation, context.CancellationToken);

            IMethodSymbol methodSymbol = extensionMethodSymbolInfo.Symbol;

            if (methodSymbol == null)
                return;

            if (methodSymbol.DeclaredAccessibility != Accessibility.Public)
                return;

            INamedTypeSymbol containingType = methodSymbol.ContainingType;

            if (containingType == null)
                return;

            bool success = false;

            if (containingType.HasFullyQualifiedMetadataName(FullyQualifiedMetadataNames.System_Linq_Enumerable))
            {
                ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

                int parameterCount = parameters.Length;

                if (parameterCount == 2)
                {
                    if (parameters[0].Type.OriginalDefinition.IsIEnumerableOfT()
                        && SymbolUtility.IsPredicateFunc(parameters[1].Type, methodSymbol.TypeArguments[0]))
                    {
                        ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(invocationInfo.Expression, context.CancellationToken);

                        if (typeSymbol != null)
                        {
                            if (typeSymbol.Kind == SymbolKind.ArrayType
                                && ((IArrayTypeSymbol)typeSymbol).Rank == 1)
                            {
                                context.ReportDiagnostic(DiagnosticDescriptors.OptimizeLinqMethodCall, invocationInfo.Name);
                                return;
                            }
                            else if (typeSymbol.OriginalDefinition.HasFullyQualifiedMetadataName(FullyQualifiedMetadataNames.System_Collections_Generic_List_1))
                            {
                                context.ReportDiagnostic(DiagnosticDescriptors.OptimizeLinqMethodCall, invocationInfo.Name);
                                return;
                            }
                        }

                        success = true;
                    }
                }
                else if (parameterCount == 1)
                {
                    if (parameters[0].Type.OriginalDefinition.IsIEnumerableOfT())
                    {
                        success = true;
                    }
                }
            }
            else if (containingType.HasFullyQualifiedMetadataName(FullyQualifiedMetadataNames.System_Linq_ImmutableArrayExtensions))
            {
                ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

                int parameterCount = parameters.Length;

                if (parameterCount == 2)
                {
                    success = SymbolUtility.IsImmutableArrayOfT(parameters[0].Type)
                        && SymbolUtility.IsPredicateFunc(parameters[1].Type, methodSymbol.TypeArguments[0]);
                }
                else if (parameterCount == 1)
                {
                    success = SymbolUtility.IsImmutableArrayOfT(parameters[0].Type);
                }
            }

            if (!success)
                return;

            SyntaxNode parent = invocation.WalkUpParentheses().Parent;

            NullCheckExpressionInfo nullCheck = SyntaxInfo.NullCheckExpressionInfo(parent, NullCheckStyles.ComparisonToNull | NullCheckStyles.IsNull);

            if (!nullCheck.Success)
                return;

            SyntaxNode node = nullCheck.NullCheckExpression;

            if (node.ContainsDirectives)
                return;

            if (!extensionMethodSymbolInfo
                .ReducedSymbol
                .ReturnType
                .IsReferenceTypeOrNullableType())
            {
                return;
            }

            context.ReportDiagnostic(DiagnosticDescriptors.OptimizeLinqMethodCall, node);
        }

        public static void AnalyzeWhereAndAny(SyntaxNodeAnalysisContext context)
        {
            var invocationExpression = (InvocationExpressionSyntax)context.Node;

            if (invocationExpression.ContainsDiagnostics)
                return;

            if (invocationExpression.SpanContainsDirectives())
                return;

            SimpleMemberInvocationExpressionInfo invocationInfo = SyntaxInfo.SimpleMemberInvocationExpressionInfo(invocationExpression);

            if (!invocationInfo.Success)
                return;

            if (invocationInfo.NameText != "Any")
                return;

            ArgumentSyntax argument1 = invocationInfo.Arguments.SingleOrDefault(shouldThrow: false);

            if (argument1 == null)
                return;

            SimpleMemberInvocationExpressionInfo invocationInfo2 = SyntaxInfo.SimpleMemberInvocationExpressionInfo(invocationInfo.Expression);

            if (!invocationInfo2.Success)
                return;

            if (invocationInfo2.NameText != "Where")
                return;

            ArgumentSyntax argument2 = invocationInfo2.Arguments.SingleOrDefault(shouldThrow: false);

            if (argument2 == null)
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            IMethodSymbol methodSymbol = semanticModel.GetExtensionMethodInfo(invocationExpression, cancellationToken).Symbol;

            if (methodSymbol == null)
                return;

            if (!SymbolUtility.IsLinqExtensionOfIEnumerableOfTWithPredicate(methodSymbol, semanticModel, "Any"))
                return;

            IMethodSymbol methodSymbol2 = semanticModel.GetExtensionMethodInfo(invocationInfo2.InvocationExpression, cancellationToken).Symbol;

            if (methodSymbol2 == null)
                return;

            if (!SymbolUtility.IsLinqWhere(methodSymbol2, semanticModel, allowImmutableArrayExtension: true))
                return;

            SingleParameterLambdaExpressionInfo lambda = SyntaxInfo.SingleParameterLambdaExpressionInfo(argument1.Expression);

            if (!lambda.Success)
                return;

            if (!(lambda.Body is ExpressionSyntax))
                return;

            SingleParameterLambdaExpressionInfo lambda2 = SyntaxInfo.SingleParameterLambdaExpressionInfo(argument2.Expression);

            if (!lambda2.Success)
                return;

            if (!(lambda2.Body is ExpressionSyntax))
                return;

            if (!lambda.Parameter.Identifier.ValueText.Equals(lambda2.Parameter.Identifier.ValueText, StringComparison.Ordinal))
                return;

            context.ReportDiagnostic(
                DiagnosticDescriptors.OptimizeLinqMethodCall,
                Location.Create(invocationExpression.SyntaxTree, TextSpan.FromBounds(invocationInfo2.Name.SpanStart, invocationExpression.Span.End)));
        }

        public static void AnalyzeWhereAndCast(SyntaxNodeAnalysisContext context, SimpleMemberInvocationExpressionInfo invocationInfo)
        {
            SimpleMemberInvocationExpressionInfo invocationInfo2 = SyntaxInfo.SimpleMemberInvocationExpressionInfo(invocationInfo.Expression);

            if (!invocationInfo2.Success)
                return;

            ArgumentSyntax argument = invocationInfo2.Arguments.SingleOrDefault(shouldThrow: false);

            if (argument == null)
                return;

            if (!string.Equals(invocationInfo2.NameText, "Where", StringComparison.Ordinal))
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            IMethodSymbol methodSymbol = semanticModel.GetReducedExtensionMethodInfo(invocationInfo.InvocationExpression, cancellationToken).Symbol;

            if (methodSymbol == null)
                return;

            if (!SymbolUtility.IsLinqCast(methodSymbol, semanticModel))
                return;

            IMethodSymbol methodSymbol2 = semanticModel.GetReducedExtensionMethodInfo(invocationInfo2.InvocationExpression, cancellationToken).Symbol;

            if (methodSymbol2 == null)
                return;

            if (!SymbolUtility.IsLinqWhere(methodSymbol2, semanticModel))
                return;

            IsExpressionInfo isExpressionInfo = SyntaxInfo.IsExpressionInfo(GetLambdaExpression(argument.Expression));

            if (!isExpressionInfo.Success)
                return;

            TypeSyntax type2 = (invocationInfo.Name as GenericNameSyntax)?.TypeArgumentList?.Arguments.SingleOrDefault(shouldThrow: false);

            if (type2 == null)
                return;

            ITypeSymbol typeSymbol = semanticModel.GetTypeSymbol(isExpressionInfo.Type, cancellationToken);

            if (typeSymbol == null)
                return;

            ITypeSymbol typeSymbol2 = semanticModel.GetTypeSymbol(type2, cancellationToken);

            if (!typeSymbol.Equals(typeSymbol2))
                return;

            TextSpan span = TextSpan.FromBounds(invocationInfo2.Name.SpanStart, invocationInfo.InvocationExpression.Span.End);

            if (invocationInfo.InvocationExpression.ContainsDirectives(span))
                return;

            context.ReportDiagnostic(
                DiagnosticDescriptors.OptimizeLinqMethodCall,
                Location.Create(invocationInfo.InvocationExpression.SyntaxTree, span));

            SyntaxNode GetLambdaExpression(ExpressionSyntax expression)
            {
                CSharpSyntaxNode body = (expression as LambdaExpressionSyntax)?.Body;

                if (body?.Kind() == SyntaxKind.Block)
                {
                    StatementSyntax statement = ((BlockSyntax)body).Statements.SingleOrDefault(shouldThrow: false);

                    if (statement?.Kind() == SyntaxKind.ReturnStatement)
                    {
                        var returnStatement = (ReturnStatementSyntax)statement;

                        return returnStatement.Expression;
                    }
                }

                return body;
            }
        }

        public static void AnalyzeOfType(
            SyntaxNodeAnalysisContext context,
            in SimpleMemberInvocationExpressionInfo invocationInfo)
        {
            TypeSyntax typeArgument = (invocationInfo.Name as GenericNameSyntax)?
                .TypeArgumentList
                .Arguments
                .SingleOrDefault(shouldThrow: false);

            if (typeArgument == null)
                return;

            SemanticModel semanticModel = context.SemanticModel;
            CancellationToken cancellationToken = context.CancellationToken;

            ExtensionMethodSymbolInfo extensionMethodSymbolInfo = semanticModel.GetReducedExtensionMethodInfo(invocationInfo.InvocationExpression, cancellationToken);

            IMethodSymbol methodSymbol = extensionMethodSymbolInfo.Symbol;

            if (methodSymbol == null)
                return;

            if (!SymbolUtility.IsLinqOfType(methodSymbol))
                return;

            ITypeSymbol typeSymbol = semanticModel.GetTypeSymbol(typeArgument, cancellationToken);

            if (typeSymbol?.IsErrorType() != false)
                return;

            ITypeSymbol typeSymbol2 = semanticModel.GetTypeSymbol(invocationInfo.Expression, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            if (!typeSymbol2.Implements((INamedTypeSymbol)extensionMethodSymbolInfo.ReducedSymbol.ReturnType, allInterfaces: true))
                return;

            TextSpan span = TextSpan.FromBounds(invocationInfo.Name.SpanStart, invocationInfo.InvocationExpression.Span.End);

            context.ReportDiagnostic(
                DiagnosticDescriptors.OptimizeLinqMethodCall,
                Location.Create(invocationInfo.InvocationExpression.SyntaxTree, span));
        }

        public static void AnalyzeFirst(
            SyntaxNodeAnalysisContext context,
            in SimpleMemberInvocationExpressionInfo invocationInfo)
        {
            ExtensionMethodSymbolInfo extensionMethodSymbolInfo = context.SemanticModel.GetReducedExtensionMethodInfo(invocationInfo.InvocationExpression, context.CancellationToken);

            IMethodSymbol methodSymbol = extensionMethodSymbolInfo.Symbol;

            if (methodSymbol == null)
                return;

            if (!SymbolUtility.IsLinqExtensionOfIEnumerableOfTWithoutParameters(methodSymbol, "First"))
                return;

            ITypeSymbol typeSymbol = context.SemanticModel.GetTypeSymbol(invocationInfo.Expression, context.CancellationToken);

            if (typeSymbol?.IsErrorType() != false)
                return;

            if (!typeSymbol.IsMetadataName("Queue`1", "Stack`1"))
                return;

            if (!typeSymbol.HasFullyQualifiedMetadataName(FullyQualifiedMetadataNames.System_Collections_Generic))
                return;

            context.ReportDiagnostic(
                DiagnosticDescriptors.OptimizeLinqMethodCall,
                invocationInfo.Name.GetLocation(),
                ImmutableDictionary.CreateRange(new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("MethodName", "Peek") }));
        }
    }
}
