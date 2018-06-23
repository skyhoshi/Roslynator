// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CodeGeneration.CSharp.CSharpFactory2;
using static Roslynator.CodeGeneration.CSharp.Symbols;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    public partial class CSharpSyntaxWalkerGenerator
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
            var members = new List<MemberDeclarationSyntax>() { CreateConstructorDeclaration(Depth) };

            members.Add(CreateShouldVisitPropertyDeclaration());

            if (!EliminateDefaultVisit)
            {
                members.Add(CreateVisitNodeMethodDeclaration());

                if (Depth >= SyntaxWalkerDepth.Node)
                {
                    members.Add(CreateVisitListMethodDeclaration());
                    members.Add(CreateVisitSeparatedListMethodDeclaration());
                }
            }

            if (Depth >= SyntaxWalkerDepth.Token)
                members.Add(CreateVisitTokenListMethodDeclaration());

            var generator = new MethodDeclarationGenerator(this);

            foreach (ISymbol symbol in VisitMethodSymbols)
            {
                members.Add(generator.GenerateVisitMethodDeclaration((IMethodSymbol)symbol));
            }

            if (EliminateDefaultVisit)
            {
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(BaseTypeSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(CrefSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(ExpressionSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(InterpolatedStringContentSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(MemberCrefSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(MemberDeclarationSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(PatternSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(QueryClauseSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(SelectOrGroupClauseSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(StatementSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(SwitchLabelSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(TypeParameterConstraintSyntaxSymbol));

                if (ShouldGenerateVisitType)
                    members.Add(CreateVisitAbstractSyntaxMethodDeclaration(TypeSyntaxSymbol));

                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(VariableDesignationSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(XmlAttributeSyntaxSymbol));
                members.Add(CreateVisitAbstractSyntaxMethodDeclaration(XmlNodeSyntaxSymbol));
            }

            return members.ToSyntaxList();
        }

        public virtual ConstructorDeclarationSyntax CreateConstructorDeclaration(SyntaxWalkerDepth depth)
        {
            return ConstructorDeclaration(
                default(SyntaxList<AttributeListSyntax>),
                Modifiers.Protected(),
                Identifier("CSharpSyntaxNodeWalker"),
                ParameterList(),
                BaseConstructorInitializer(
                    ArgumentList(
                        Argument(
                            NameColon("depth"),
                            SimpleMemberAccessExpression(IdentifierName("SyntaxWalkerDepth"), IdentifierName(depth.ToString()))))),
                Block());
        }

        public virtual PropertyDeclarationSyntax CreateShouldVisitPropertyDeclaration()
        {
            return PropertyDeclaration(
                Modifiers.ProtectedVirtual(),
                PredefinedBoolType(),
                Identifier("ShouldVisit"),
                AccessorList(GetAccessorDeclaration(Block(ReturnStatement(TrueLiteralExpression())))));
        }

        public virtual MethodDeclarationSyntax CreateVisitNodeMethodDeclaration()
        {
            return MethodDeclaration(
                Modifiers.PublicOverride(),
                VoidType(),
                Identifier("Visit"),
                ParameterList(Parameter(IdentifierName("SyntaxNode"), "node")),
                Block(
                    IfNotShouldVisitReturnStatement(),
                    ExpressionStatement(
                        SimpleMemberInvocationExpression(
                            BaseExpression(),
                            IdentifierName("Visit"),
                            ArgumentList(Argument(IdentifierName("node")))))));
        }

        public virtual MethodDeclarationSyntax CreateVisitListMethodDeclaration()
        {
            return MethodDeclaration(
                default(SyntaxList<AttributeListSyntax>),
                Modifiers.Private(),
                VoidType(),
                default(ExplicitInterfaceSpecifierSyntax),
                Identifier("VisitList"),
                TypeParameterList(TypeParameter("TNode")),
                ParameterList(Parameter(GenericName("SyntaxList", IdentifierName("TNode")), "list")),
                SingletonList(TypeParameterConstraintClause("TNode", TypeConstraint(IdentifierName("SyntaxNode")))),
                Block(ForEachVisitStatement(
                    "TNode",
                    "node",
                    IdentifierName("list"),
                    VisitStatement("Visit", "node"),
                    checkShouldVisit: true)),
                default(ArrowExpressionClauseSyntax));
        }

        public virtual MethodDeclarationSyntax CreateVisitSeparatedListMethodDeclaration()
        {
            return MethodDeclaration(
                default(SyntaxList<AttributeListSyntax>),
                Modifiers.Private(),
                VoidType(),
                default(ExplicitInterfaceSpecifierSyntax),
                Identifier("VisitSeparatedList"),
                TypeParameterList(TypeParameter("TNode")),
                ParameterList(Parameter(GenericName("SeparatedSyntaxList", IdentifierName("TNode")), "list")),
                SingletonList(TypeParameterConstraintClause("TNode", TypeConstraint(IdentifierName("SyntaxNode")))),
                Block(ForEachVisitStatement("TNode", "node", IdentifierName("list"),
                    VisitStatement("Visit", "node"), checkShouldVisit: true)),
                default(ArrowExpressionClauseSyntax));
        }

        public virtual MethodDeclarationSyntax CreateVisitTokenListMethodDeclaration()
        {
            return MethodDeclaration(
                Modifiers.Private(),
                VoidType(),
                Identifier("VisitTokenList"),
                ParameterList(Parameter(IdentifierName("SyntaxTokenList"), "list")),
                Block(
                    IfStatement(
                        SimpleMemberInvocationExpression(IdentifierName("list"), IdentifierName("Any")),
                        Block(
                            ForEachVisitStatement(
                                "SyntaxToken",
                                "token",
                                IdentifierName("list"),
                                VisitStatement("VisitToken", "token"), checkShouldVisit: true)))));
        }

        public virtual MethodDeclarationSyntax CreateVisitAbstractSyntaxMethodDeclaration(INamedTypeSymbol typeSymbol)
        {
            string name = typeSymbol.Name;

            string nameWithoutSyntax = name.Remove(name.Length - 6);

            return MethodDeclaration(
                Modifiers.ProtectedVirtual(),
                VoidType(),
                Identifier($"Visit{nameWithoutSyntax}"),
                ParameterList(Parameter(IdentifierName(name), "node")),
                Block(
                    SwitchStatement(
                        SimpleMemberInvocationExpression(IdentifierName("node"), IdentifierName("Kind")),
                        GenerateSections().ToSyntaxList())));

            IEnumerable<SwitchSectionSyntax> GenerateSections()
            {
                foreach (INamedTypeSymbol typeSymbol2 in SyntaxSymbols.Where(f => !f.IsAbstract && f.InheritsFrom(typeSymbol)))
                {
                    string name2 = typeSymbol2.Name;

                    string nameWithoutSyntax2 = name2.Remove(name2.Length - 6);

                    SyntaxList<SwitchLabelSyntax> labels = GetKinds(typeSymbol2)
                        .Select(f => CaseSwitchLabel(SimpleMemberAccessExpression(IdentifierName("SyntaxKind"), IdentifierName(f.ToString()))))
                        .ToSyntaxList<SwitchLabelSyntax>();

                    yield return SwitchSection(
                        labels,
                        List(new StatementSyntax[]
                        {
                            ExpressionStatement(
                                InvocationExpression(
                                    IdentifierName("Visit" + nameWithoutSyntax2),
                                    ArgumentList(Argument(CastExpression(IdentifierName(name2), IdentifierName("node")))))),
                            BreakStatement()
                        }));
                }

                yield return DefaultSwitchSection(ThrowNewInvalidOperationException());
            }
        }
    }
}
