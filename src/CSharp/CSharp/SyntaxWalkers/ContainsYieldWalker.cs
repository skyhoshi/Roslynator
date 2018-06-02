// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp.SyntaxWalkers
{
    internal class ContainsYieldWalker : StatementWalker
    {
        private bool _success;
        private readonly TextSpan? _span;

        protected ContainsYieldWalker(TextSpan? span = null)
        {
            _span = span;
        }

        public virtual bool IsSearchingForYieldReturn
        {
            get { return true; }
        }

        public virtual bool IsSearchingForYieldBreak
        {
            get { return true; }
        }

        public static bool ContainsYieldReturn(StatementSyntax statement, TextSpan? span = null)
        {
            return ContainsYield(statement, span, yieldReturn: true, yieldBreak: false);
        }

        public static bool ContainsYieldBreak(StatementSyntax statement, TextSpan? span = null)
        {
            return ContainsYield(statement, span, yieldReturn: false, yieldBreak: true);
        }

        public static bool ContainsYield(StatementSyntax statement, TextSpan? span = null, bool yieldReturn = true, bool yieldBreak = true)
        {
            if (statement == null)
                throw new ArgumentNullException(nameof(statement));

            ContainsYieldWalker walker = Create(span, yieldReturn, yieldBreak);

            walker.Visit(statement);

            return walker._success;
        }

        private static ContainsYieldWalker Create(TextSpan? span = null, bool yieldReturn = true, bool yieldBreak = true)
        {
            if (yieldReturn)
            {
                if (yieldBreak)
                {
                    return new ContainsYieldWalker(span);
                }
                else
                {
                    return new ContainsYieldReturnWalker(span);
                }
            }
            else if (yieldBreak)
            {
                return new ContainsYieldBreakWalker(span);
            }

            throw new InvalidOperationException();
        }

        public override void VisitYieldStatement(YieldStatementSyntax node)
        {
            SyntaxKind kind = node.Kind();

            if (kind == SyntaxKind.YieldReturnStatement)
            {
                if (IsSearchingForYieldReturn
                    && _span?.Contains(node.FullSpan) != false)
                {
                    _success = true;
                }
            }
            else if (kind == SyntaxKind.YieldBreakStatement)
            {
                if (IsSearchingForYieldBreak
                    && _span?.Contains(node.FullSpan) != false)
                {
                    _success = true;
                }
            }
        }

        public override void Visit(SyntaxNode node)
        {
            if (!_success)
                base.Visit(node);
        }

        private sealed class ContainsYieldBreakWalker : ContainsYieldWalker
        {
            internal ContainsYieldBreakWalker(TextSpan? span = null) : base(span)
            {
            }

            public override bool IsSearchingForYieldReturn
            {
                get { return false; }
            }
        }

        private sealed class ContainsYieldReturnWalker : ContainsYieldWalker
        {
            internal ContainsYieldReturnWalker(TextSpan? span = null) : base(span)
            {
            }

            public override bool IsSearchingForYieldBreak
            {
                get { return false; }
            }
        }
    }
}
