// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Roslynator.CodeGeneration.CSharp.Factory;
using static Roslynator.CodeGeneration.CSharp.Symbols;

namespace Roslynator.CodeGeneration.CSharp
{
    public class CSharpSyntaxWalkerGenerator
    {
        public CSharpSyntaxWalkerGenerator(
            SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node,
            bool shouldVisitFunction = true,
            bool useCustomVisitMethod = false,
            bool shouldGenerateVisitType = false,
            bool eliminateDefaultVisit = false,
            bool inlineVisitWithSingleProperty = false,
            bool inlineVisitListSyntax = false)
        {
            Depth = depth;
            ShouldVisitFunction = shouldVisitFunction;
            UseCustomVisitMethod = useCustomVisitMethod;
            ShouldGenerateVisitType = shouldGenerateVisitType;
            EliminateDefaultVisit = eliminateDefaultVisit;
            InlineVisitWithSingleProperty = inlineVisitWithSingleProperty;
            InlineVisitListSyntax = inlineVisitListSyntax;
        }

        public SyntaxWalkerDepth Depth { get; }

        public bool ShouldVisitFunction { get; }

        public bool UseCustomVisitMethod { get; }

        public bool ShouldGenerateVisitType { get; }

        public bool EliminateDefaultVisit { get; }

        public bool InlineVisitWithSingleProperty { get; }

        public bool InlineVisitListSyntax { get; }

        public SyntaxList<MemberDeclarationSyntax> GenerateMemberDeclarations()
        {
            var members = new List<MemberDeclarationSyntax>() { VisitConstructorDeclaration(Depth) };

            members.Add(ShouldVisitPropertyDeclaration());

            if (!EliminateDefaultVisit)
            {
                members.Add(VisitMethodDeclaration());

                if (Depth >= SyntaxWalkerDepth.Node)
                {
                    members.Add(VisitListMethodDeclaration());
                    members.Add(VisitSeparatedListMethodDeclaration());
                }
            }

            if (Depth >= SyntaxWalkerDepth.Token)
                members.Add(VisitTokenListMethodDeclaration());

            var generator = new MethodDeclarationGenerator(this);

            foreach (ISymbol symbol in VisitMethodSymbols)
            {
                members.Add(generator.GenerateVisitMethodDeclaration((IMethodSymbol)symbol));
            }

            if (EliminateDefaultVisit)
            {
                members.Add(VisitMethodDeclaration(BaseTypeSyntaxSymbol));
                members.Add(VisitMethodDeclaration(CrefSyntaxSymbol));
                members.Add(VisitMethodDeclaration(ExpressionSyntaxSymbol));
                members.Add(VisitMethodDeclaration(InterpolatedStringContentSyntaxSymbol));
                members.Add(VisitMethodDeclaration(MemberCrefSyntaxSymbol));
                members.Add(VisitMethodDeclaration(MemberDeclarationSyntaxSymbol));
                members.Add(VisitMethodDeclaration(PatternSyntaxSymbol));
                members.Add(VisitMethodDeclaration(QueryClauseSyntaxSymbol));
                members.Add(VisitMethodDeclaration(SelectOrGroupClauseSyntaxSymbol));
                members.Add(VisitMethodDeclaration(StatementSyntaxSymbol));
                members.Add(VisitMethodDeclaration(SwitchLabelSyntaxSymbol));
                members.Add(VisitMethodDeclaration(TypeParameterConstraintSyntaxSymbol));

                if (ShouldGenerateVisitType)
                    members.Add(VisitMethodDeclaration(TypeSyntaxSymbol));

                members.Add(VisitMethodDeclaration(VariableDesignationSyntaxSymbol));
                members.Add(VisitMethodDeclaration(XmlAttributeSyntaxSymbol));
                members.Add(VisitMethodDeclaration(XmlNodeSyntaxSymbol));
            }

            return members.ToSyntaxList();
        }
    }
}
