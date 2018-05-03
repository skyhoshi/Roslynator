// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp.Analysis.ReplaceEnumeratorWithForEach
{
    internal class ReplaceEnumeratorWithForEachWalker : CSharpSyntaxWalker
    {
        private ISymbol _symbol;
        private VariableDeclaratorSyntax _variableDeclarator;
        private string _name;
        private SemanticModel _semanticModel;
        private CancellationToken _cancellationToken;

        public bool? IsFixable { get; private set; }

        public void SetValues(
            VariableDeclaratorSyntax variableDeclarator,
            SemanticModel semanticModel,
            CancellationToken cancellationToken)
        {
            IsFixable = null;

            _symbol = null;
            _name = variableDeclarator?.Identifier.ValueText;
            _variableDeclarator = variableDeclarator;
            _semanticModel = semanticModel;
            _cancellationToken = cancellationToken;
        }

        public void Clear()
        {
            SetValues(default(VariableDeclaratorSyntax), default(SemanticModel), default(CancellationToken));
        }

        public override void Visit(SyntaxNode node)
        {
            if (IsFixable != false)
                base.Visit(node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            if (!string.Equals(node.Identifier.ValueText, _name))
                return;

            if (_symbol == null)
            {
                _symbol = _semanticModel.GetDeclaredSymbol(_variableDeclarator, _cancellationToken);

                if (_symbol?.IsErrorType() != false)
                {
                    IsFixable = false;
                    return;
                }
            }

            if (!_symbol.Equals(_semanticModel.GetSymbol(node, _cancellationToken)))
                return;

            if (!node.IsParentKind(SyntaxKind.SimpleMemberAccessExpression))
            {
                IsFixable = false;
                return;
            }

            var memberAccessExpression = (MemberAccessExpressionSyntax)node.Parent;

            if (memberAccessExpression.Expression != node)
            {
                IsFixable = false;
                return;
            }

            if (!(memberAccessExpression.Name is IdentifierNameSyntax identifierName))
            {
                IsFixable = false;
                return;
            }

            if (!string.Equals(identifierName.Identifier.ValueText, WellKnownMemberNames.CurrentPropertyName, StringComparison.Ordinal))
            {
                IsFixable = false;
                return;
            }

            IsFixable = true;
        }
    }
}

