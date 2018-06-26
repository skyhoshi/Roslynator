// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.SyntaxWalkers;

namespace Roslynator.CSharp.Analysis.UnusedMember
{
    internal class UnusedMemberWalker : CSharpSyntaxNodeWalker
    {
        private bool _isEmpty;

        private IMethodSymbol _containingMethodSymbol;

        public Collection<NodeSymbolInfo> Nodes { get; } = new Collection<NodeSymbolInfo>();

        public SemanticModel SemanticModel { get; private set; }

        public CancellationToken CancellationToken { get; private set; }

        public bool IsAnyNodeConst { get; private set; }

        public bool IsAnyNodeDelegate { get; private set; }

        protected override bool ShouldVisit
        {
            get { return !_isEmpty; }
        }

        public void SetValues(SemanticModel semanticModel, CancellationToken cancellationToken)
        {
            _isEmpty = false;
            _containingMethodSymbol = null;

            Nodes.Clear();
            SemanticModel = semanticModel;
            CancellationToken = cancellationToken;
            IsAnyNodeConst = false;
            IsAnyNodeDelegate = false;
        }

        public void Clear()
        {
            SetValues(default(SemanticModel), default(CancellationToken));
        }

        private void CheckName(string name, SimpleNameSyntax node)
        {
            for (int i = Nodes.Count - 1; i >= 0; i--)
            {
                NodeSymbolInfo info = Nodes[i];

                if (info.Name == name)
                {
                    if (info.Symbol == null)
                    {
                        ISymbol declaredSymbol = SemanticModel.GetDeclaredSymbol(info.Node, CancellationToken);

                        Debug.Assert(declaredSymbol != null, "");

                        if (declaredSymbol == null)
                        {
                            RemoveNodeAt(i);
                            continue;
                        }

                        info = new NodeSymbolInfo(info.Name, info.Node, declaredSymbol);

                        Nodes[i] = info;
                    }

                    SymbolInfo symbolInfo = SemanticModel.GetSymbolInfo(node, CancellationToken);

                    if (symbolInfo.Symbol != null)
                    {
                        ISymbol symbol = symbolInfo.Symbol;

                        if (symbol.Kind == SymbolKind.Method)
                        {
                            var methodSymbol = ((IMethodSymbol)symbol);

                            if (methodSymbol.MethodKind == MethodKind.ReducedExtension)
                                symbol = methodSymbol.ReducedFrom;
                        }

                        symbol = symbol.OriginalDefinition;

                        if (info.Symbol.Equals(symbol)
                            && _containingMethodSymbol?.Equals(symbol) != true)
                        {
                            RemoveNodeAt(i);
                        }
                    }
                    else if (symbolInfo.CandidateReason == CandidateReason.MemberGroup)
                    {
                        ImmutableArray<ISymbol> candidateSymbols = symbolInfo.CandidateSymbols;

                        for (int j = 0; j < candidateSymbols.Length; j++)
                        {
                            ISymbol symbol = candidateSymbols[j].OriginalDefinition;

                            if (info.Symbol.Equals(symbol)
                                && _containingMethodSymbol?.Equals(symbol) != true)
                            {
                                RemoveNodeAt(i);
                            }
                        }
                    }
                    else
                    {
                        Debug.Fail(symbolInfo.CandidateReason.ToString());
                    }
                }
            }
        }

        public void AddDelegate(string name, SyntaxNode node)
        {
            AddNode(name, node);

            IsAnyNodeDelegate = true;
        }

        public void AddNode(string name, SyntaxNode node)
        {
            Nodes.Add(new NodeSymbolInfo(name, node));
        }

        public void AddNodes(VariableDeclarationSyntax declaration, bool isConst = false)
        {
            foreach (VariableDeclaratorSyntax declarator in declaration.Variables)
                AddNode(declarator.Identifier.ValueText, declarator);

            if (isConst)
                IsAnyNodeConst = true;
        }

        private void RemoveNodeAt(int index)
        {
            Nodes.RemoveAt(index);

            if (Nodes.Count == 0)
                _isEmpty = true;
        }

        protected override void VisitType(TypeSyntax node)
        {
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
            CheckName(node.Identifier.ValueText, node);
        }

        public override void VisitIdentifierName(IdentifierNameSyntax node)
        {
            CheckName(node.Identifier.ValueText, node);
        }

        private void VisitMembers(SyntaxList<MemberDeclarationSyntax> members)
        {
            foreach (MemberDeclarationSyntax memberDeclaration in members)
                VisitMemberDeclaration(memberDeclaration);
        }

        private void VisitBody(CSharpSyntaxNode body)
        {
            if (!ShouldVisit)
            {
                return;
            }

            if (body is ExpressionSyntax expression)
            {
                VisitExpression(expression);
            }
            else if (body is StatementSyntax statement)
            {
                VisitStatement(statement);
            }
            else
            {
                Visit(body);
            }
        }

        private void VisitBodyOrExpressionBody(BlockSyntax body, ArrowExpressionClauseSyntax expressionBody)
        {
            if (!ShouldVisit)
            {
                return;
            }

            if (body != null)
            {
                VisitBlock(body);
            }

            if (!ShouldVisit)
            {
                return;
            }

            if (expressionBody != null)
            {
                VisitArrowExpressionClause(expressionBody);
            }
        }

        //private void VisitAccessorListOrExpressionBody(AccessorListSyntax accessorList, ArrowExpressionClauseSyntax expressionBody)
        //{
        //    Visit(accessorList);
        //    Visit(expressionBody);
        //}

        private void VisitParameterListIfNotNull(ParameterListSyntax parameterList)
        {
            if (parameterList != null)
            {
                VisitParameterList(parameterList);
            }
        }

        private void VisitAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
        {
            foreach (AttributeListSyntax attributeList in attributeLists)
            {
                if (!ShouldVisit)
                {
                    return;
                }

                VisitAttributeList(attributeList);
            }
        }

        public override void VisitUsingDirective(UsingDirectiveSyntax node)
        {
        }

        public override void VisitGotoStatement(GotoStatementSyntax node)
        {
        }

        public override void VisitLiteralExpression(LiteralExpressionSyntax node)
        {
        }

        public override void VisitNameColon(NameColonSyntax node)
        {
        }

        public override void VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax node)
        {
        }

        //TODO: ?
        public override void VisitTypeParameterList(TypeParameterListSyntax node)
        {
        }

        //TODO: ?
        public override void VisitBaseList(BaseListSyntax node)
        {
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitMembers(node.Members);
        }

        public override void VisitCompilationUnit(CompilationUnitSyntax node)
        {
            VisitMembers(node.Members);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitParameterListIfNotNull(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);
            Visit(node.Initializer);
        }

        public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitParameterListIfNotNull(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            Visit(node.ParameterList);
        }

        public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            Visit(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);

            if (IsAnyNodeConst)
            {
                foreach (EnumMemberDeclarationSyntax member in node.Members)
                    Visit(member);
            }
        }

        public override void VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            Visit(node.EqualsValue);
        }

        //public override void VisitEventDeclaration(EventDeclarationSyntax node)
        //{
        //    VisitAttributeLists(node.AttributeLists);
        //}

        //public override void VisitEventFieldDeclaration(EventFieldDeclarationSyntax node)
        //{
        //    VisitAttributeLists(node.AttributeLists);
        //}

        //public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        //{
        //    VisitAttributeLists(node.AttributeLists);
        //    Visit(node.Declaration);
        //}

        //public override void VisitIndexerDeclaration(IndexerDeclarationSyntax node)
        //{
        //    VisitAttributeLists(node.AttributeLists);
        //    Visit(node.ParameterList);
        //    VisitAccessorListOrExpressionBody(node.AccessorList, node.ExpressionBody);
        //}

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitMembers(node.Members);
        }

        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            VisitParameterListIfNotNull(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Debug.Assert(_containingMethodSymbol == null);

            _containingMethodSymbol = SemanticModel.GetDeclaredSymbol(node, CancellationToken);

            VisitAttributeLists(node.AttributeLists);
            VisitParameterListIfNotNull(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);

            _containingMethodSymbol = null;
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            VisitMembers(node.Members);
        }

        public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitParameterListIfNotNull(node.ParameterList);
            VisitBodyOrExpressionBody(node.Body, node.ExpressionBody);
        }

        public override void VisitParameter(ParameterSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            Visit(node.Default);

            if (IsAnyNodeDelegate)
                base.VisitType(node.Type);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            Visit(node.Initializer);
            VisitAccessorListOrExpressionBody(node.AccessorList, node.ExpressionBody);
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            VisitAttributeLists(node.AttributeLists);
            VisitMembers(node.Members);
        }

        public override void VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax node)
        {
            VisitBody(node.Body);
        }

        public override void VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax node)
        {
            VisitBody(node.Body);
        }
    }
}
