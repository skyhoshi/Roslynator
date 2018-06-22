// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CodeGeneration.CSharp.Factory;
using static Roslynator.CodeGeneration.CSharp.Symbols;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    public partial class MethodDeclarationGenerator
    {
        private static readonly SymbolDisplayFormat _symbolDisplayFormat = SymbolDisplayFormats.Default.WithParameterOptions(
            SymbolDisplayParameterOptions.IncludeDefaultValue
                | SymbolDisplayParameterOptions.IncludeExtensionThis
                | SymbolDisplayParameterOptions.IncludeName
                | SymbolDisplayParameterOptions.IncludeParamsRefOut
                | SymbolDisplayParameterOptions.IncludeType);

        private readonly CSharpSyntaxWalkerGenerator _generator;

        private IMethodSymbol _methodSymbol;

        private IParameterSymbol _parameterSymbol;
        private ITypeSymbol _parameterType;
        private string _parameterName;

        private IPropertySymbol _propertySymbol;
        private ITypeSymbol _propertyType;
        private string _propertyName;

        public MethodDeclarationGenerator(CSharpSyntaxWalkerGenerator generator)
        {
            Statements = new List<StatementSyntax>();
            LocalNames = new HashSet<string>();
            _generator = generator;
        }

        private List<StatementSyntax> Statements { get; }

        private HashSet<string> LocalNames { get; }

        public MethodDeclarationSyntax GenerateVisitMethodDeclaration(IMethodSymbol methodSymbol)
        {
            _methodSymbol = methodSymbol;

            _parameterSymbol = _methodSymbol.Parameters.Single();
            _parameterType = _parameterSymbol.Type;
            _parameterName = _parameterSymbol.Name;

            Debug.Assert(_parameterType.EqualsOrInheritsFrom(SyntaxNodeSymbol), _parameterType.ToDisplayString());

            BlockSyntax body = null;

            if (_generator.ShouldVisitFunction
                || !IsVisitFunction(_parameterType))
            {
                Clear();
                GenerateVisitStatements();
                body = Block(Statements);
                Clear();
            }
            else
            {
                body = Block();
            }

            return MethodDeclaration(
                Modifiers.PublicOverride(),
                VoidType(),
                Identifier(methodSymbol.Name),
                ParseParameterList($"({_parameterSymbol.ToDisplayString(_symbolDisplayFormat)})"),
                body);
        }

        private void GenerateVisitStatements()
        {
            foreach (IPropertySymbol propertySymbol in GetPropertySymbols(_parameterType))
            {
                if (!ShouldVisit(propertySymbol))
                    continue;

                _propertySymbol = propertySymbol;
                _propertyType = propertySymbol.Type;
                _propertyName = propertySymbol.Name;

                if (_propertyType.OriginalDefinition.Equals(SyntaxListSymbol))
                {
                    if (_generator.UseCustomVisitMethod)
                    {
                        GenerateVisitListStatement();
                    }
                    else
                    {
                        Statements.Add(VisitStatement("VisitList", _parameterName, _propertyName));
                    }
                }
                else if (_propertyType.OriginalDefinition.Equals(SeparatedSyntaxListSymbol))
                {
                    if (_generator.UseCustomVisitMethod)
                    {
                        GenerateVisitListStatement(isSeparatedList: true);
                    }
                    else
                    {
                        Statements.Add(VisitStatement("VisitSeparatedList", _parameterName, _propertyName));
                    }
                }
                else if (_propertyType.EqualsOrInheritsFrom(SyntaxNodeSymbol))
                {
                    switch (_propertyType.Name)
                    {
                        case "AccessorListSyntax":
                        case "ArgumentListSyntax":
                        case "AttributeArgumentListSyntax":
                        case "BracketedArgumentListSyntax":
                        case "BracketedParameterListSyntax":
                        case "ParameterListSyntax":
                        case "TypeArgumentListSyntax":
                        case "TypeParameterListSyntax":
                            {
                                if (_generator.UseCustomVisitMethod)
                                {
                                    if (_generator.InlineVisitListSyntax)
                                    {
                                        GenerateVisitListSyntaxStatements();
                                    }
                                    else
                                    {
                                        GenerateTypeVisitStatements();
                                    }
                                }
                                else
                                {
                                    Statements.Add(VisitStatement("Visit", _parameterName, _propertyName));
                                }

                                break;
                            }
                        case "ArrayTypeSyntax":
                        case "ArrowExpressionClauseSyntax":
                        case "AttributeTargetSpecifierSyntax":
                        case "BaseListSyntax":
                        case "BlockSyntax":
                        case "CatchDeclarationSyntax":
                        case "CatchFilterClauseSyntax":
                        case "ConstructorInitializerSyntax":
                        case "CrefBracketedParameterListSyntax":
                        case "CrefParameterListSyntax":
                        case "ElseClauseSyntax":
                        case "EqualsValueClauseSyntax":
                        case "ExplicitInterfaceSpecifierSyntax":
                        case "FinallyClauseSyntax":
                        case "FromClauseSyntax":
                        case "InterpolationAlignmentClauseSyntax":
                        case "InterpolationFormatClauseSyntax":
                        case "JoinIntoClauseSyntax":
                        case "NameColonSyntax":
                        case "NameEqualsSyntax":
                        case "ParameterSyntax":
                        case "QueryBodySyntax":
                        case "QueryContinuationSyntax":
                        case "VariableDeclarationSyntax":
                        case "WhenClauseSyntax":
                        case "XmlElementEndTagSyntax":
                        case "XmlElementStartTagSyntax":
                        case "XmlNameSyntax":
                        case "XmlPrefixSyntax":
                        case "CrefSyntax":
                        case "ExpressionSyntax":
                        case "IdentifierNameSyntax":
                        case "InitializerExpressionSyntax":
                        case "MemberCrefSyntax":
                        case "NameSyntax":
                        case "PatternSyntax":
                        case "SelectOrGroupClauseSyntax":
                        case "SimpleNameSyntax":
                        case "StatementSyntax":
                        case "TypeSyntax":
                        case "VariableDesignationSyntax":
                            {
                                if (_generator.UseCustomVisitMethod)
                                {
                                    GenerateTypeVisitStatements();
                                }
                                else
                                {
                                    Statements.Add(VisitStatement("Visit", _parameterName, _propertyName));
                                }

                                break;
                            }
                        case "CSharpSyntaxNode":
                            {
                                if (!_generator.UseCustomVisitMethod)
                                {
                                    Statements.Add(VisitStatement("Visit", _parameterName, _propertyName));
                                    break;
                                }

                                if (_generator.EliminateDefaultVisit
                                    && _propertyName == "Body"
                                    && (_methodSymbol.Name == "VisitAnonymousMethodExpression"
                                    || _methodSymbol.Name == "VisitParenthesizedLambdaExpression"
                                    || _methodSymbol.Name == "VisitSimpleLambdaExpression"))
                                {
                                    GenerateVisitAnonymousFunctionStatements();
                                }
                                else
                                {
                                    GenerateTypeVisitStatements();
                                }

                                break;
                            }
                        default:
                            {
                                throw new InvalidOperationException($"Unrecognized property type '{_propertyType.ToDisplayString()}'.");
                            }
                    }
                }
                else if (_propertyType.Equals(SyntaxTokenListSymbol))
                {
                    if (_generator.Depth >= SyntaxWalkerDepth.Token)
                    {
                        Statements.Add(VisitStatement("VisitTokenList", _parameterName, _propertyName));
                    }
                }
                else if (_propertyType.Equals(SyntaxTokenSymbol))
                {
                    if (_generator.Depth >= SyntaxWalkerDepth.Token)
                    {
                        Statements.Add(VisitStatement("VisitToken", _parameterName, _propertyName));
                    }
                }
                else if (!CSharpFacts.IsPredefinedType(_propertyType.SpecialType))
                {
                    throw new InvalidOperationException();
                }
            }

            _propertySymbol = null;
        }

        private void GenerateVisitAnonymousFunctionStatements()
        {
            string variableName = GenerateLocalName(_propertyName);

            Statements.Add(LocalDeclarationStatement(_propertyType, variableName, _parameterName, _propertyName));

            IfStatementSyntax ifStatement = IfStatement(
                IsPatternExpression(
                    IdentifierName(variableName),
                    DeclarationPattern(IdentifierName("ExpressionSyntax"), SingleVariableDesignation(Identifier("expression")))),
                Block(VisitStatement("VisitExpression", "expression")),
                ElseClause(
                    IfStatement(
                        IsPatternExpression(
                            IdentifierName(variableName),
                            DeclarationPattern(IdentifierName("StatementSyntax"), SingleVariableDesignation(Identifier("statement")))),
                        Block(VisitStatement("VisitStatement", "statement")),
                        ElseClause(Block(ThrowNewInvalidOperationException())))));

            Statements.Add(ifStatement);
        }

        private void GenerateVisitListStatement(bool isSeparatedList = false)
        {
            ITypeSymbol typeSymbol = ((INamedTypeSymbol)_propertyType).TypeArguments.Single();

            IMethodSymbol methodSymbol = FindVisitMethod(typeSymbol);

            string methodName = null;

            if (methodSymbol != null)
            {
                methodName = methodSymbol.Name;
            }
            else if (_generator.ShouldGenerateVisitType
                && _generator.EliminateDefaultVisit)
            {
                methodName = GetMethodName(typeSymbol);
            }

            if (methodName != null)
            {
                string typeName = typeSymbol.Name;

                string variableName = GenerateLocalName(typeName.Remove(typeName.Length - 6));

                ForEachStatementSyntax forEachStatement = ForEachVisitStatement(
                    typeName,
                    variableName,
                    SimpleMemberAccessExpression(IdentifierName(_parameterName), IdentifierName(_propertyName)),
                    VisitStatement(methodName, variableName),
                    checkShouldVisit: methodSymbol != null);

                Statements.Add(forEachStatement);
            }
            else
            {
                methodName = (isSeparatedList) ? "VisitSeparatedList" : "VisitList";

                Statements.Add(VisitStatement(methodName, _parameterName, _propertyName));
            }
        }

        private void GenerateTypeVisitStatements()
        {
            IMethodSymbol methodSymbol = FindVisitMethod(_propertyType);

            if (methodSymbol == null)
            {
                if (_generator.EliminateDefaultVisit)
                {
                    Statements.Add(IfNotShouldVisitReturnStatement());

                    string variableName = GenerateLocalName(_propertyName);

                    Statements.Add(LocalDeclarationStatement(_propertyType, variableName, _parameterName, _propertyName));

                    string methodName = GetMethodName(_propertyType);

                    IfStatementSyntax ifStatement = IfNotEqualsToNullStatement(
                        variableName,
                        VisitStatement(methodName, variableName));

                    Statements.Add(ifStatement);
                }
                else
                {
                    Statements.Add(VisitStatement("Visit", _parameterName, _propertyName));
                }
            }
            else
            {
                string variableName = null;

                if (_generator.Depth == SyntaxWalkerDepth.Node
                    && _generator.InlineVisitWithSingleProperty)
                {
                    IPropertySymbol propertySymbol2 = GetSinglePropertySymbolOrDefault(_propertyType);

                    if (propertySymbol2 != null)
                    {
                        variableName = GenerateLocalName(propertySymbol2.Name);

                        IMethodSymbol methodSymbol2 = FindVisitMethod(propertySymbol2.Type);

                        string methodName = null;

                        if (methodSymbol2 != null)
                        {
                            methodName = methodSymbol2.Name;
                        }
                        else if (_generator.EliminateDefaultVisit)
                        {
                            methodName = GetMethodName(propertySymbol2.Type);
                        }

                        if (methodName != null)
                            Statements.Add(IfNotShouldVisitReturnStatement());

                        LocalDeclarationStatementSyntax localDeclaration = LocalDeclarationStatement(
                            propertySymbol2.Type.ToTypeSyntax(_symbolDisplayFormat),
                            variableName,
                            ConditionalAccessExpression(
                                SimpleMemberAccessExpression(
                                    IdentifierName(_parameterName),
                                    IdentifierName(_propertyName)),
                                MemberBindingExpression(IdentifierName(propertySymbol2.Name))));

                        Statements.Add(localDeclaration);

                        IfStatementSyntax ifStatement = IfNotEqualsToNullStatement(
                            variableName,
                            VisitStatement(methodName ?? "Visit", variableName));

                        Statements.Add(ifStatement);
                        return;
                    }
                }

                variableName = GenerateLocalName(_propertyName);

                Statements.Add(IfNotShouldVisitReturnStatement());

                Statements.Add(LocalDeclarationStatement(_propertyType, variableName, _parameterName, _propertyName));

                Statements.Add(IfNotEqualsToNullStatement(variableName, VisitStatement(methodSymbol.Name, variableName)));
            }
        }

        private void GenerateVisitListSyntaxStatements()
        {
            string variableName = GenerateLocalName(_propertyName);

            Statements.Add(LocalDeclarationStatement(_propertyType, variableName, _parameterName, _propertyName));

            IPropertySymbol listPropertySymbol = FindListPropertySymbol(_propertySymbol);

            ITypeSymbol typeSymbol = ((INamedTypeSymbol)listPropertySymbol.Type).TypeArguments.Single();

            IMethodSymbol methodSymbol = FindVisitMethod(typeSymbol);

            string methodName = null;

            if (methodSymbol != null)
            {
                methodName = methodSymbol.Name;
            }
            else if (_generator.EliminateDefaultVisit)
            {
                methodName = GetMethodName(typeSymbol);
            }

            StatementSyntax statement;

            if (methodName != null)
            {
                string forEachVariableName = GenerateLocalName(typeSymbol.Name.Remove(typeSymbol.Name.Length - 6));

                statement = ForEachVisitStatement(
                    typeSymbol.Name,
                    forEachVariableName,
                    SimpleMemberAccessExpression(
                        IdentifierName(variableName),
                        IdentifierName(listPropertySymbol.Name)),
                    VisitStatement(methodName, forEachVariableName), checkShouldVisit: true);
            }
            else
            {
                methodName = (listPropertySymbol.Type.OriginalDefinition.Equals(SyntaxListSymbol)) ? "VisitList" : "VisitSeparatedList";

                statement = VisitStatement(methodName, variableName, listPropertySymbol.Name);
            }

            Statements.Add(IfNotEqualsToNullStatement(variableName, statement));
        }

        private static string GetMethodName(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.EqualsOrInheritsFrom(TypeSyntaxSymbol))
                return "VisitType";

            if (typeSymbol.EqualsOrInheritsFrom(StatementSyntaxSymbol))
                return "VisitStatement";

            if (typeSymbol.EqualsOrInheritsFrom(ExpressionSyntaxSymbol))
                return "VisitExpression";

            if (typeSymbol.EqualsOrInheritsFrom(PatternSyntaxSymbol))
                return "VisitPattern";

            if (typeSymbol.EqualsOrInheritsFrom(VariableDesignationSyntaxSymbol))
                return "VisitVariableDesignation";

            if (typeSymbol.EqualsOrInheritsFrom(MemberCrefSyntaxSymbol))
                return "VisitMemberCref";

            if (typeSymbol.EqualsOrInheritsFrom(SelectOrGroupClauseSyntaxSymbol))
                return "VisitSelectOrGroupClause";

            if (typeSymbol.EqualsOrInheritsFrom(CrefSyntaxSymbol))
                return "VisitCref";

            if (typeSymbol.EqualsOrInheritsFrom(BaseTypeSyntaxSymbol))
                return "VisitBaseType";

            if (typeSymbol.EqualsOrInheritsFrom(MemberDeclarationSyntaxSymbol))
                return "VisitMemberDeclaration";

            if (typeSymbol.EqualsOrInheritsFrom(XmlNodeSyntaxSymbol))
                return "VisitXmlNode";

            if (typeSymbol.EqualsOrInheritsFrom(InterpolatedStringContentSyntaxSymbol))
                return "VisitInterpolatedStringContent";

            if (typeSymbol.EqualsOrInheritsFrom(QueryClauseSyntaxSymbol))
                return "VisitQueryClause";

            if (typeSymbol.EqualsOrInheritsFrom(SwitchLabelSyntaxSymbol))
                return "VisitSwitchLabel";

            if (typeSymbol.EqualsOrInheritsFrom(TypeParameterConstraintSyntaxSymbol))
                return "VisitTypeParameterConstraint";

            if (typeSymbol.EqualsOrInheritsFrom(XmlAttributeSyntaxSymbol))
                return "VisitXmlAttribute";

            throw new InvalidOperationException(typeSymbol.Name);
        }

        public string GenerateLocalName(string name)
        {
            name = StringUtility.ToCamelCase(name);

            name = NameGenerator.Default.EnsureUniqueName(name, LocalNames);

            if (SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None)
                name = $"@{name}";

            LocalNames.Add(name);

            return name;
        }

        private void Clear()
        {
            Statements.Clear();
            LocalNames.Clear();
        }
    }
}
