// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp.SyntaxWalkers
{
    internal class StatementWalker : SkipFunctionWalker
    {
        public override void VisitBlock(BlockSyntax node)
        {
            foreach (StatementSyntax statement in node.Statements)
                Visit(statement);
        }

        public override void VisitBreakStatement(BreakStatementSyntax node)
        {
        }

        public override void VisitCheckedStatement(CheckedStatementSyntax node)
        {
            VisitBlock(node.Block);
        }

        public override void VisitContinueStatement(ContinueStatementSyntax node)
        {
        }

        public override void VisitDoStatement(DoStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitEmptyStatement(EmptyStatementSyntax node)
        {
        }

        public override void VisitExpressionStatement(ExpressionStatementSyntax node)
        {
        }

        public override void VisitFixedStatement(FixedStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitForEachStatement(ForEachStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitForEachVariableStatement(ForEachVariableStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitForStatement(ForStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitGlobalStatement(GlobalStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
        }

        public override void VisitIfStatement(IfStatementSyntax node)
        {
            Visit(node.Statement);

            ElseClauseSyntax elseClause = node.Else;

            if (elseClause != null)
                Visit(elseClause.Statement);
        }

        public override void VisitLabeledStatement(LabeledStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax node)
        {
        }

        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            VisitBlock(node.Body);
        }

        public override void VisitLockStatement(LockStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitReturnStatement(ReturnStatementSyntax node)
        {
        }

        public override void VisitSwitchStatement(SwitchStatementSyntax node)
        {
            foreach (SwitchSectionSyntax section in node.Sections)
            {
                foreach (StatementSyntax statement in section.Statements)
                    Visit(statement);
            }
        }

        public override void VisitThrowStatement(ThrowStatementSyntax node)
        {
        }

        public override void VisitTryStatement(TryStatementSyntax node)
        {
            VisitBlock(node.Block);

            foreach (CatchClauseSyntax catchClause in node.Catches)
                VisitCatchClause(catchClause);

            FinallyClauseSyntax finallyClause = node.Finally;

            if (finallyClause != null)
                VisitBlock(finallyClause.Block);
        }

        public override void VisitUnsafeStatement(UnsafeStatementSyntax node)
        {
            VisitBlock(node.Block);
        }

        public override void VisitUsingStatement(UsingStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitWhileStatement(WhileStatementSyntax node)
        {
            Visit(node.Statement);
        }

        public override void VisitYieldStatement(YieldStatementSyntax node)
        {
        }
    }
}
