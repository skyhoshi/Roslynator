// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp.SyntaxWalkers
{
    internal abstract class LocalOrParameterReferenceWalker : CSharpSyntaxNodeWalker
    {
        public TextSpan Span { get; protected set; }

        protected LocalOrParameterReferenceWalker(TextSpan span)
        {
            Span = span;
        }

        public static bool ContainsReference(
            SyntaxNode node,
            ISymbol localOrParameterSymbol,
            SemanticModel semanticModel,
            TextSpan? span = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return ContainsReferenceWalker.ContainsReference(node, localOrParameterSymbol, semanticModel, span, cancellationToken);
        }

        public override void Visit(SyntaxNode node)
        {
            if (IsInSpan(node.FullSpan))
                base.Visit(node);
        }

        private bool IsInSpan(TextSpan span)
        {
            return Span.OverlapsWith(span)
                || (span.Length == 0 && Span.IntersectsWith(span));
        }

        protected override void VisitType(TypeSyntax node)
        {
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
        }

        private sealed class ContainsReferenceWalker : LocalOrParameterReferenceWalker
        {
            [ThreadStatic]
            private static ContainsReferenceWalker _cachedInstance;

            public bool Result { get; private set; }

            public ISymbol Symbol { get; private set; }

            public SemanticModel SemanticModel { get; private set; }

            public CancellationToken CancellationToken { get; private set; }

            public ContainsReferenceWalker(
                ISymbol symbol,
                TextSpan span,
                SemanticModel semanticModel,
                CancellationToken cancellationToken) : base(span)
            {
                Symbol = symbol;
                Span = span;
                SemanticModel = semanticModel;
                CancellationToken = cancellationToken;
            }

            new public static bool ContainsReference(
                SyntaxNode node,
                ISymbol symbol,
                SemanticModel semanticModel,
                TextSpan? span = null,
                CancellationToken cancellationToken = default(CancellationToken))
            {
                bool result = false;
                ContainsReferenceWalker walker = null;

                try
                {
                    walker = GetInstance(
                        symbol,
                        span ?? node.FullSpan,
                        semanticModel,
                        cancellationToken);

                    walker.Visit(node);

                    result = walker.Result;

#if DEBUG
                    //TODO: test
                    bool debugResult = false;
                    foreach (SyntaxNode descendant in node.DescendantNodesAndSelf(span ?? node.FullSpan))
                    {
                        if (descendant.IsKind(SyntaxKind.IdentifierName))
                        {
                            var identifierName = (IdentifierNameSyntax)descendant;

                            if (string.Equals(identifierName.Identifier.ValueText, symbol.Name, StringComparison.Ordinal)
                                && semanticModel.GetSymbol(identifierName, cancellationToken)?.Equals(symbol) == true)
                            {
                                debugResult = true;
                                break;
                            }
                        }
                    }

                    Debug.Assert(result == debugResult);
#endif
                }
                finally
                {
                    if (walker != null)
                        Free(walker);
                }

                return result;
            }

            public override void Visit(SyntaxNode node)
            {
                if (!Result)
                    base.Visit(node);
            }

            public override void VisitIdentifierName(IdentifierNameSyntax node)
            {
                CancellationToken.ThrowIfCancellationRequested();

                if (string.Equals(node.Identifier.ValueText, Symbol.Name, StringComparison.Ordinal)
                    && SemanticModel.GetSymbol(node, CancellationToken)?.Equals(Symbol) == true)
                {
                    Result = true;
                }
            }

            public static ContainsReferenceWalker GetInstance(
                ISymbol symbol,
                TextSpan span,
                SemanticModel semanticModel,
                CancellationToken cancellationToken = default(CancellationToken))
            {
                ContainsReferenceWalker walker = _cachedInstance;

                if (walker != null)
                {
                    _cachedInstance = null;
                    walker.Symbol = symbol;
                    walker.Span = span;
                    walker.SemanticModel = semanticModel;
                    walker.CancellationToken = cancellationToken;
                    return walker;
                }

                return new ContainsReferenceWalker(symbol, span, semanticModel, cancellationToken);
            }

            public static void Free(ContainsReferenceWalker walker)
            {
                walker.Result = false;
                walker.Symbol = default;
                walker.Span = default;
                walker.SemanticModel = default;
                walker.CancellationToken = default;
                _cachedInstance = walker;
            }
        }
    }
}
