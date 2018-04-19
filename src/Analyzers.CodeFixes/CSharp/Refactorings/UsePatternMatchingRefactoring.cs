// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Roslynator.CSharp.Refactorings
{
    internal static class UsePatternMatchingRefactoring
    {
        public static async Task<Document> RefactorAsync(
            Document document,
            BinaryExpressionSyntax binaryExpression,
            CancellationToken cancellationToken)
        {
            IsExpressionInfo isInfo = SyntaxInfo.IsExpressionInfo(binaryExpression.Left);

            IdentifierNameSyntax identifierName = isInfo.Expression as IdentifierNameSyntax
                ?? (IdentifierNameSyntax)ThisMemberAccessExpressionInfo.Create(isInfo.Expression).Name;

            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);

            ISymbol symbol = semanticModel.GetSymbol(isInfo.Expression, cancellationToken);

            ITypeSymbol typeSymbol = semanticModel.GetTypeSymbol(isInfo.Type, cancellationToken);

            string name = NameGenerator.Default.CreateUniqueLocalName(typeSymbol, semanticModel, binaryExpression.SpanStart, cancellationToken: cancellationToken);

            IsPatternExpressionSyntax isPatternExpression = IsPatternExpression(
                isInfo.Expression,
                DeclarationPattern(
                    isInfo.Type.WithTrailingTrivia(ElasticSpace),
                    SingleVariableDesignation(Identifier(name).WithRenameAnnotation()).WithTrailingTrivia(isInfo.Type.GetTrailingTrivia())));

            IEnumerable<CastExpressionSyntax> nodes = binaryExpression
                .Right
                .DescendantNodes()
                .OfType<IdentifierNameSyntax>()
                .Where(f => symbol.Equals(semanticModel.GetSymbol(f, cancellationToken)))
                .Select(f => (ThisMemberAccessExpressionInfo.Create(f.Parent).Success) ? (ExpressionSyntax)f.Parent : f)
                .Select(f => (CastExpressionSyntax)f.WalkUpParentheses().Parent);

            IdentifierNameSyntax newIdentifierName = IdentifierName(name);

            ExpressionSyntax newRight = binaryExpression.Right.ReplaceNodes(nodes, (f, _) => newIdentifierName.WithTriviaFrom(f));

            BinaryExpressionSyntax newBinaryExpression = binaryExpression
                .WithLeft(isPatternExpression)
                .WithRight(newRight);

            return await document.ReplaceNodeAsync(binaryExpression, newBinaryExpression, cancellationToken).ConfigureAwait(false);
        }
    }
}
