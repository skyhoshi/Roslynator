// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    internal static class Factory
    {
        public static ConstructorDeclarationSyntax VisitConstructorDeclaration(SyntaxWalkerDepth depth)
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

        public static MethodDeclarationSyntax VisitListMethodDeclaration()
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

        public static MethodDeclarationSyntax VisitSeparatedListMethodDeclaration()
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

        public static MethodDeclarationSyntax VisitTokenListMethodDeclaration()
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

        public static MethodDeclarationSyntax VisitTypeMethodDeclaration()
        {
            return MethodDeclaration(
                Modifiers.ProtectedVirtual(),
                VoidType(),
                Identifier("VisitType"),
                ParameterList(Parameter(IdentifierName("TypeSyntax"), "node")),
                Block(VisitStatement("Visit", "node")));
        }

        public static PropertyDeclarationSyntax ShouldVisitPropertyDeclaration()
        {
            return PropertyDeclaration(
                Modifiers.ProtectedVirtual(),
                PredefinedBoolType(),
                Identifier("ShouldVisit"),
                AccessorList(GetAccessorDeclaration(Block(ReturnStatement(TrueLiteralExpression())))));
        }

        public static MethodDeclarationSyntax VisitMethodDeclaration()
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

        public static MethodDeclarationSyntax VisitMethodDeclaration(INamedTypeSymbol typeSymbol)
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
                foreach (INamedTypeSymbol typeSymbol2 in Symbols.SyntaxSymbols.Where(f => !f.IsAbstract && f.InheritsFrom(typeSymbol)))
                {
                    string name2 = typeSymbol2.Name;

                    string nameWithoutSyntax2 = name2.Remove(name2.Length - 6);

                    SyntaxList<SwitchLabelSyntax> labels = Symbols.GetKinds(typeSymbol2)
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

        public static ExpressionStatementSyntax VisitStatement(
            string methodName,
            string name,
            string propertyName = null)
        {
            ExpressionSyntax expression = IdentifierName(name);

            if (propertyName != null)
            {
                expression = SimpleMemberAccessExpression(
                    expression,
                    IdentifierName(propertyName));
            }

            return ExpressionStatement(
                InvocationExpression(
                IdentifierName(methodName),
                ArgumentList(
                Argument(
                expression))));
        }

        public static ForEachStatementSyntax ForEachVisitStatement(
            string typeName,
            string variableName,
            ExpressionSyntax expression,
            StatementSyntax statement,
            bool checkShouldVisit = false)
        {
            return ForEachStatement(
                IdentifierName(typeName),
                variableName,
                expression,
                (checkShouldVisit)
                    ? Block(IfNotShouldVisitReturnStatement(), statement)
                    : Block(statement));
        }

        public static StatementSyntax IfNotShouldVisitReturnStatement()
        {
            return IfStatement(LogicalNotExpression(IdentifierName("ShouldVisit")), Block(ReturnStatement()));
        }

        public static IfStatementSyntax IfEqualsToNullStatement(string name, StatementSyntax statement)
        {
            return IfStatement(
                EqualsExpression(
                    IdentifierName(name),
                    NullLiteralExpression()),
                (statement.IsKind(SyntaxKind.Block)) ? statement : Block(statement));
        }

        public static IfStatementSyntax IfNotEqualsToNullStatement(string name, StatementSyntax statement)
        {
            return IfStatement(
                NotEqualsExpression(
                    IdentifierName(name),
                    NullLiteralExpression()),
                (statement.IsKind(SyntaxKind.Block)) ? statement : Block(statement));
        }

        public static ThrowStatementSyntax ThrowNewInvalidOperationException(ExpressionSyntax expression = null)
        {
            ArgumentListSyntax argumentList;

            if (expression != null)
            {
                argumentList = ArgumentList(Argument(expression));
            }
            else
            {
                argumentList = ArgumentList();
            }

            return ThrowStatement(
                ObjectCreationExpression(
                IdentifierName("InvalidOperationException"), argumentList));
        }

        public static LocalDeclarationStatementSyntax LocalDeclarationStatement(
            ITypeSymbol typeSymbol,
            string name,
            string parameterName,
            string propertyName)
        {
            return CSharpFactory.LocalDeclarationStatement(
                typeSymbol.ToTypeSyntax(SymbolDisplayFormats.Default),
                name,
                SimpleMemberAccessExpression(
                    IdentifierName(parameterName),
                    IdentifierName(propertyName)));
        }
    }
}
