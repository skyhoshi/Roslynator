// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CodeGeneration.CSharp.RuntimeMetadataReference;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    //TODO: Options: VisitType, VisitFunction, Partial
    public class CSharpSyntaxWalkerGenerator
    {
        private static readonly SymbolDisplayFormat _symbolDisplayFormat = new SymbolDisplayFormat(
            genericsOptions: SymbolDisplayGenericsOptions.IncludeTypeParameters,
            typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes,
            parameterOptions: SymbolDisplayParameterOptions.IncludeDefaultValue
                | SymbolDisplayParameterOptions.IncludeExtensionThis
                | SymbolDisplayParameterOptions.IncludeName
                | SymbolDisplayParameterOptions.IncludeParamsRefOut
                | SymbolDisplayParameterOptions.IncludeType,
            miscellaneousOptions: SymbolDisplayMiscellaneousOptions.UseSpecialTypes
                | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers
                | SymbolDisplayMiscellaneousOptions.RemoveAttributeSuffix);

        private Compilation _compilation;
        private INamedTypeSymbol _csharpSyntaxWalkerSymbol;
        private INamedTypeSymbol _syntaxNodeSymbol;
        private INamedTypeSymbol _syntaxListSymbol;
        private INamedTypeSymbol _separatedSyntaxListSymbol;
        private INamedTypeSymbol _syntaxTokenSymbol;
        private INamedTypeSymbol _syntaxTokenListSymbol;
        private INamedTypeSymbol _typeSyntaxSymbol;

        public Compilation Compilation
        {
            get
            {
                if (_compilation == null)
                {
                    Project project = new AdhocWorkspace()
                        .CurrentSolution
                        .AddProject("Project", "Assembly", LanguageNames.CSharp)
                        .WithMetadataReferences(ImmutableArray.Create(
                            CorLibReference,
                            CreateFromAssemblyName("Microsoft.CodeAnalysis.dll"),
                            CreateFromAssemblyName("Microsoft.CodeAnalysis.CSharp.dll")));

                    _compilation = project.GetCompilationAsync().Result;
                }

                return _compilation;
            }
        }

        public INamedTypeSymbol CSharpSyntaxWalkerSymbol => _csharpSyntaxWalkerSymbol ?? (_csharpSyntaxWalkerSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker"));
        public INamedTypeSymbol SyntaxNodeSymbol => _syntaxNodeSymbol ?? (_syntaxNodeSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode"));
        public INamedTypeSymbol SyntaxListSymbol => _syntaxListSymbol ?? (_syntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxList`1"));
        public INamedTypeSymbol SeparatedSyntaxListSymbol => _separatedSyntaxListSymbol ?? (_separatedSyntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SeparatedSyntaxList`1"));
        public INamedTypeSymbol SyntaxTokenSymbol => _syntaxTokenSymbol ?? (_syntaxTokenSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxToken"));
        public INamedTypeSymbol SyntaxTokenListSymbol => _syntaxTokenListSymbol ?? (_syntaxTokenListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxTokenList"));
        public INamedTypeSymbol TypeSyntaxSymbol => _typeSyntaxSymbol ?? (_typeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax"));

        public SyntaxList<MemberDeclarationSyntax> GenerateMemberDeclarations()
        {
            var members = new List<MemberDeclarationSyntax>()
            {
                GenerateVisitConstructorDeclaration(),
                GenerateVisitListMethodDeclaration(),
                GenerateVisitSeparatedSyntaxListMethodDeclaration(),
                GenerateVisitTokenListMethodDeclaration(),
                GenerateVisitTypeMethodDeclaration()
            };

            foreach (ISymbol symbol in CSharpSyntaxWalkerSymbol
                .BaseType
                .GetMembers()
                .Where(f => f.Kind == SymbolKind.Method
                    && f.DeclaredAccessibility == Accessibility.Public
                    && f.IsVirtual
                    && f.Name.Length > 5
                    && f.Name.StartsWith("Visit", StringComparison.Ordinal))
                .OrderBy(f => f.Name))
            {
                members.Add(GenerateVisitMethodDeclaration((IMethodSymbol)symbol));
            }

            return members.ToSyntaxList();
        }

        private MethodDeclarationSyntax GenerateVisitMethodDeclaration(IMethodSymbol methodSymbol)
        {
            IParameterSymbol parameter = methodSymbol.Parameters.Single();

            return MethodDeclaration(
                Modifiers.PublicOverride(),
                VoidType(),
                Identifier(methodSymbol.Name),
                ParseParameterList($"({parameter.ToDisplayString(_symbolDisplayFormat)})"),
                Block(GenerateVisitStatements(parameter)));
        }

        private IEnumerable<StatementSyntax> GenerateVisitStatements(IParameterSymbol parameterSymbol)
        {
            string parameterName = parameterSymbol.Name;

            foreach (ISymbol symbol in parameterSymbol.Type.GetMembers())
            {
                if (symbol.Kind != SymbolKind.Property)
                    continue;

                if (symbol.DeclaredAccessibility != Accessibility.Public)
                    continue;

                if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    continue;

                var propertySymbol = (IPropertySymbol)symbol;

                if (propertySymbol.IsIndexer)
                    continue;

                string propertyName = propertySymbol.Name;
                ITypeSymbol propertyType = propertySymbol.Type;

                if (propertyType.OriginalDefinition.Equals(SyntaxListSymbol))
                {
                    string typeArgumentName = ((INamedTypeSymbol)propertyType).TypeArguments.Single().Name;

                    switch (typeArgumentName)
                    {
                        case "InterpolatedStringContentSyntax":
                        case "MemberDeclarationSyntax":
                        case "QueryClauseSyntax":
                        case "StatementSyntax":
                        case "SwitchLabelSyntax":
                        case "XmlAttributeSyntax":
                        case "XmlNodeSyntax":
                            {
                                yield return GenerateVisitNonSpecificListStatement("VisitList", parameterName, propertyName);
                                break;
                            }
                        default:
                            {
                                yield return GenerateVisitSpecificListStatement(typeArgumentName, parameterName, propertyName);
                                break;
                            }
                    }
                }
                else if (propertyType.OriginalDefinition.Equals(SeparatedSyntaxListSymbol))
                {
                    string typeArgumentName = ((INamedTypeSymbol)propertyType).TypeArguments.Single().Name;

                    switch (typeArgumentName)
                    {
                        case "BaseTypeSyntax":
                        case "ExpressionSyntax":
                        case "TypeParameterConstraintSyntax":
                        case "VariableDesignationSyntax":
                            {
                                yield return GenerateVisitNonSpecificListStatement("VisitSeparatedList", parameterName, propertyName);
                                break;
                            }
                        default:
                            {
                                yield return GenerateVisitSpecificListStatement(typeArgumentName, parameterName, propertyName);
                                break;
                            }
                    }
                }
                else if (propertyType.EqualsOrInheritsFrom(SyntaxNodeSymbol))
                {
                    switch (propertyType.Name)
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
                                foreach (StatementSyntax statement in GenerateVisitListStatements(propertySymbol, parameterName))
                                    yield return statement;

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
                            {
                                foreach (StatementSyntax statement in GenerateVisitStatements(propertySymbol, parameterName, propertyType.Name.Remove(propertyType.Name.Length - 6)))
                                    yield return statement;

                                break;
                            }
                        case "CrefSyntax":
                        case "CSharpSyntaxNode":
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
                                foreach (StatementSyntax statement in GenerateVisitStatements(propertySymbol, parameterName))
                                    yield return statement;

                                break;
                            }
                        default:
                            {
                                throw new InvalidOperationException($"Unrecognized property type '{propertyType.ToDisplayString()}'.");
                            }
                    }
                }
                else if (!CSharpFacts.IsPredefinedType(propertyType.SpecialType)
                    && !propertyType.Equals(SyntaxTokenSymbol)
                    && !propertyType.Equals(SyntaxTokenListSymbol))
                {
                    throw new InvalidOperationException();
                }
            }
        }

        private static ExpressionStatementSyntax GenerateVisitNonSpecificListStatement(
            string methodName,
            string parameterName,
            string propertyName)
        {
            return ExpressionStatement(
                InvocationExpression(
                    IdentifierName(methodName),
                    ArgumentList(
                        Argument(
                            SimpleMemberAccessExpression(
                                IdentifierName(parameterName),
                                IdentifierName(propertyName))))));
        }

        private static ForEachStatementSyntax GenerateVisitSpecificListStatement(
            string typeName,
            string parameterName,
            string propertyName)
        {
            string typeNameWithoutSyntax = typeName.Remove(typeName.Length - 6);
            string variableName = StringUtility.ToCamelCase(typeNameWithoutSyntax);

            return ForEachStatement(
                IdentifierName(typeName),
                variableName,
                SimpleMemberAccessExpression(
                    IdentifierName(parameterName),
                    IdentifierName(propertyName)),
                ExpressionStatement(
                    InvocationExpression(
                        IdentifierName("Visit" + typeNameWithoutSyntax),
                        ArgumentList(Argument(IdentifierName(variableName))))));
        }

        private IEnumerable<StatementSyntax> GenerateVisitStatements(
            IPropertySymbol propertySymbol,
            string parameterName,
            string methodNameSuffix = null)
        {
            if (methodNameSuffix == null)
            {
                if (propertySymbol.Type.EqualsOrInheritsFrom(TypeSyntaxSymbol))
                {
                    yield return GenerateVisitStatement("VisitType");
                }
                else
                {
                    yield return GenerateVisitStatement("Visit");
                }
            }
            else
            {
                string variableName = StringUtility.ToCamelCase(propertySymbol.Name);

                switch (variableName)
                {
                    case "else":
                    case "default":
                    case "finally":
                        {
                            variableName = "@" + variableName;
                            break;
                        }
                }

                yield return LocalDeclarationStatement(
                    propertySymbol.Type.ToTypeSyntax(_symbolDisplayFormat),
                    variableName,
                    SimpleMemberAccessExpression(
                        IdentifierName(parameterName),
                        IdentifierName(propertySymbol.Name)));

                yield return IfStatement(
                    NotEqualsExpression(
                        IdentifierName(variableName),
                        NullLiteralExpression()),
                    ExpressionStatement(
                    InvocationExpression(
                        IdentifierName("Visit" + methodNameSuffix),
                        ArgumentList(
                            Argument(IdentifierName(variableName))))));
            }

            ExpressionStatementSyntax GenerateVisitStatement(string name)
            {
                return ExpressionStatement(
                    InvocationExpression(
                        IdentifierName(name),
                        ArgumentList(
                            Argument(
                                SimpleMemberAccessExpression(
                                    IdentifierName(parameterName),
                                    IdentifierName(propertySymbol.Name))))));
            }
        }

        private IEnumerable<StatementSyntax> GenerateVisitListStatements(IPropertySymbol propertySymbol, string parameterName)
        {
            string propertyName = propertySymbol.Name;
            ITypeSymbol propertyType = propertySymbol.Type;

            string variableName = StringUtility.ToCamelCase(propertyName);

            yield return LocalDeclarationStatement(
                propertyType.ToTypeSyntax(_symbolDisplayFormat),
                variableName,
                SimpleMemberAccessExpression(
                    IdentifierName(parameterName),
                    IdentifierName(propertyName)));

            string listPropertyName = GetListPropertyName();

            IPropertySymbol listPropertySymbol = FindListProperty();

            yield return IfStatement(
                NotEqualsExpression(IdentifierName(variableName), NullLiteralExpression()),
                ExpressionStatement(
                    InvocationExpression(
                        IdentifierName((listPropertySymbol.Type.OriginalDefinition.Equals(SyntaxListSymbol)) ? "VisitList" : "VisitSeparatedList"),
                        ArgumentList(
                            Argument(
                                SimpleMemberAccessExpression(
                                    IdentifierName(variableName),
                                    IdentifierName(listPropertyName)))))));

            string GetListPropertyName()
            {
                switch (propertyName)
                {
                    case "TypeArgumentList":
                        return "Arguments";
                    case "TypeParameterList":
                        return "Parameters";
                    default:
                        return propertyName.Remove(propertyName.Length - 4) + "s";
                }
            }

            IPropertySymbol FindListProperty()
            {
                foreach (ISymbol symbol in propertyType.GetMembers(listPropertyName))
                {
                    if (symbol.Kind == SymbolKind.Property
                        && symbol.DeclaredAccessibility == Accessibility.Public
                        && !symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    {
                        var propertySymbol2 = (IPropertySymbol)symbol;

                        if (!propertySymbol2.IsIndexer)
                        {
                            if (propertySymbol2.Type.OriginalDefinition.Equals(SyntaxListSymbol)
                                || propertySymbol2.Type.OriginalDefinition.Equals(SeparatedSyntaxListSymbol))
                            {
                                return propertySymbol2;
                            }
                        }
                    }
                }

                throw new InvalidOperationException();
            }
        }

        private static ConstructorDeclarationSyntax GenerateVisitConstructorDeclaration()
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
                            SimpleMemberAccessExpression(IdentifierName("SyntaxWalkerDepth"), IdentifierName("Node"))))),
                Block());
        }

        private static MethodDeclarationSyntax GenerateVisitListMethodDeclaration()
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
                Block(
                    ForEachStatement(
                        IdentifierName("TNode"),
                        "node",
                        IdentifierName("list"),
                        ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("Visit"),
                                ArgumentList(Argument(IdentifierName("node"))))))),
                default(ArrowExpressionClauseSyntax));
        }

        private static MethodDeclarationSyntax GenerateVisitSeparatedSyntaxListMethodDeclaration()
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
                Block(
                    ForEachStatement(
                        IdentifierName("TNode"),
                        "node",
                        IdentifierName("list"),
                        ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("Visit"),
                                ArgumentList(Argument(IdentifierName("node"))))))),
                default(ArrowExpressionClauseSyntax));
        }

        private static MethodDeclarationSyntax GenerateVisitTokenListMethodDeclaration()
        {
            return MethodDeclaration(
                Modifiers.Private(),
                VoidType(),
                Identifier("VisitTokenList"),
                ParameterList(Parameter(IdentifierName("SyntaxTokenList"), "list")),
                Block(
                    ForEachStatement(
                        IdentifierName("SyntaxToken"),
                        "token",
                        IdentifierName("list"),
                        ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("VisitToken"),
                                ArgumentList(Argument(IdentifierName("token"))))))));
        }

        private static MethodDeclarationSyntax GenerateVisitTypeMethodDeclaration()
        {
            return MethodDeclaration(
                Modifiers.ProtectedVirtual(),
                VoidType(),
                Identifier("VisitType"),
                ParameterList(Parameter(IdentifierName("TypeSyntax"), "node")),
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            IdentifierName("Visit"), ArgumentList(Argument(IdentifierName("node")))))));
        }
    }
}
