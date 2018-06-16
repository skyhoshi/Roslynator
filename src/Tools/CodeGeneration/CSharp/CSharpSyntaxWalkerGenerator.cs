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

        public CSharpSyntaxWalkerGenerator(
            SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node,
            bool shouldVisitFunction = true,
            bool shouldGenerateVisitType = false)
        {
            Depth = depth;
            ShouldVisitFunction = shouldVisitFunction;
            ShouldGenerateVisitType = shouldGenerateVisitType;
        }

        public SyntaxWalkerDepth Depth { get; }

        public bool ShouldVisitFunction { get; }

        public bool ShouldGenerateVisitType { get; }

        private INamedTypeSymbol CSharpSyntaxWalkerSymbol => _csharpSyntaxWalkerSymbol ?? (_csharpSyntaxWalkerSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker"));
        private INamedTypeSymbol SyntaxNodeSymbol => _syntaxNodeSymbol ?? (_syntaxNodeSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode"));
        private INamedTypeSymbol SyntaxListSymbol => _syntaxListSymbol ?? (_syntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxList`1"));
        private INamedTypeSymbol SeparatedSyntaxListSymbol => _separatedSyntaxListSymbol ?? (_separatedSyntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SeparatedSyntaxList`1"));
        private INamedTypeSymbol SyntaxTokenSymbol => _syntaxTokenSymbol ?? (_syntaxTokenSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxToken"));
        private INamedTypeSymbol SyntaxTokenListSymbol => _syntaxTokenListSymbol ?? (_syntaxTokenListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxTokenList"));
        private INamedTypeSymbol TypeSyntaxSymbol => _typeSyntaxSymbol ?? (_typeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax"));

        private Compilation Compilation
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

        public SyntaxList<MemberDeclarationSyntax> GenerateMemberDeclarations()
        {
            var members = new List<MemberDeclarationSyntax>() { GenerateVisitConstructorDeclaration() };

            if (Depth >= SyntaxWalkerDepth.Node)
            {
                members.Add(GenerateVisitListMethodDeclaration());
                members.Add(GenerateVisitSeparatedSyntaxListMethodDeclaration());
            }

            if (Depth >= SyntaxWalkerDepth.Token)
                members.Add(GenerateVisitTokenListMethodDeclaration());

            if (ShouldGenerateVisitType)
                members.Add(GenerateVisitTypeMethodDeclaration());

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

            BlockSyntax body = null;

            if (ShouldVisitFunction
                || !IsVisitFunction())
            {
                body = Block(GenerateVisitStatements(parameter));
            }
            else
            {
                body = Block();
            }

            return MethodDeclaration(
                Modifiers.PublicOverride(),
                VoidType(),
                Identifier(methodSymbol.Name),
                ParseParameterList($"({parameter.ToDisplayString(_symbolDisplayFormat)})"),
                body);

            bool IsVisitFunction()
            {
                switch (methodSymbol.Name)
                {
                    case "VisitSimpleLambdaExpression":
                    case "VisitParenthesizedLambdaExpression":
                    case "VisitAnonymousMethodExpression":
                    case "VisitLocalFunctionStatement":
                        return true;
                    default:
                        return false;
                }
            }
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
                    ITypeSymbol typeArgumentSymbol = ((INamedTypeSymbol)propertyType).TypeArguments.Single();

                    switch (typeArgumentSymbol.Name)
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
                        case "AccessorDeclarationSyntax":
                        case "ArrayRankSpecifierSyntax":
                        case "AttributeListSyntax":
                        case "CatchClauseSyntax":
                        case "ExternAliasDirectiveSyntax":
                        case "SwitchSectionSyntax":
                        case "TypeParameterConstraintClauseSyntax":
                        case "UsingDirectiveSyntax":
                            {
                                yield return GenerateVisitSpecificListStatement(typeArgumentSymbol, parameterName, propertyName);
                                break;
                            }
                        default:
                            {
                                throw new InvalidOperationException($"Unrecognized type '{typeArgumentSymbol.ToDisplayString()}'.");
                            }
                    }
                }
                else if (propertyType.OriginalDefinition.Equals(SeparatedSyntaxListSymbol))
                {
                    ITypeSymbol typeArgumentSymbol = ((INamedTypeSymbol)propertyType).TypeArguments.Single();

                    switch (typeArgumentSymbol.Name)
                    {
                        case "BaseTypeSyntax":
                        case "ExpressionSyntax":
                        case "TypeParameterConstraintSyntax":
                        case "VariableDesignationSyntax":
                            {
                                yield return GenerateVisitNonSpecificListStatement("VisitSeparatedList", parameterName, propertyName);
                                break;
                            }
                        case "AnonymousObjectMemberDeclaratorSyntax":
                        case "ArgumentSyntax":
                        case "AttributeArgumentSyntax":
                        case "AttributeSyntax":
                        case "CrefParameterSyntax":
                        case "EnumMemberDeclarationSyntax":
                        case "OrderingSyntax":
                        case "ParameterSyntax":
                        case "TupleElementSyntax":
                        case "TypeSyntax":
                        case "TypeParameterSyntax":
                        case "VariableDeclaratorSyntax":
                            {
                                yield return GenerateVisitSpecificListStatement(typeArgumentSymbol, parameterName, propertyName);
                                break;
                            }
                        default:
                            {
                                throw new InvalidOperationException($"Unrecognized type '{typeArgumentSymbol.ToDisplayString()}'.");
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
                                string methodNameSuffix = propertyType.Name.Remove(propertyType.Name.Length - 6);

                                foreach (StatementSyntax statement in GenerateVisitStatements(propertySymbol, parameterName, methodNameSuffix))
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
                else if (propertyType.Equals(SyntaxTokenListSymbol))
                {
                    if (Depth >= SyntaxWalkerDepth.Token)
                    {
                        yield return ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("VisitTokenList"),
                                ArgumentList(
                                    Argument(
                                        SimpleMemberAccessExpression(
                                            IdentifierName(parameterName),
                                            IdentifierName(propertyName))))));
                    }
                }
                else if (propertyType.Equals(SyntaxTokenSymbol))
                {
                    if (Depth >= SyntaxWalkerDepth.Token)
                    {
                        yield return ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("VisitToken"),
                                ArgumentList(
                                    Argument(
                                        SimpleMemberAccessExpression(
                                            IdentifierName(parameterName),
                                            IdentifierName(propertyName))))));
                    }
                }
                else if (!CSharpFacts.IsPredefinedType(propertyType.SpecialType))
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

        private ForEachStatementSyntax GenerateVisitSpecificListStatement(
            ITypeSymbol typeSymbol,
            string parameterName,
            string propertyName)
        {
            string typeName = typeSymbol.Name;

            string typeNameWithoutSyntax = typeName.Remove(typeName.Length - 6);
            string variableName = StringUtility.ToCamelCase(typeNameWithoutSyntax);

            string methodName = "Visit" + typeNameWithoutSyntax;

            if (!ShouldGenerateVisitType
                || !typeSymbol.Equals(TypeSyntaxSymbol))
            {
                if (!CSharpSyntaxWalkerSymbol
                    .BaseType
                    .GetMembers(methodName)
                    .Any(f => f.Kind == SymbolKind.Method
                        && f.DeclaredAccessibility == Accessibility.Public
                        && f.IsVirtual))
                {
                    methodName = "Visit";
                }
            }

            return ForEachStatement(
                IdentifierName(typeName),
                variableName,
                SimpleMemberAccessExpression(
                    IdentifierName(parameterName),
                    IdentifierName(propertyName)),
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            IdentifierName(methodName),
                            ArgumentList(Argument(IdentifierName(variableName)))))));
        }

        private IEnumerable<StatementSyntax> GenerateVisitStatements(
            IPropertySymbol propertySymbol,
            string parameterName,
            string methodNameSuffix = null)
        {
            if (methodNameSuffix == null)
            {
                if (ShouldGenerateVisitType
                    && propertySymbol.Type.EqualsOrInheritsFrom(TypeSyntaxSymbol))
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
                    Block(
                        ExpressionStatement(
                        InvocationExpression(
                            IdentifierName("Visit" + methodNameSuffix),
                            ArgumentList(
                                Argument(IdentifierName(variableName)))))));
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
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            IdentifierName((listPropertySymbol.Type.OriginalDefinition.Equals(SyntaxListSymbol)) ? "VisitList" : "VisitSeparatedList"),
                            ArgumentList(
                                Argument(
                                    SimpleMemberAccessExpression(
                                        IdentifierName(variableName),
                                        IdentifierName(listPropertyName))))))));

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

        private ConstructorDeclarationSyntax GenerateVisitConstructorDeclaration()
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
                            SimpleMemberAccessExpression(IdentifierName("SyntaxWalkerDepth"), IdentifierName(Depth.ToString()))))),
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
                        Block(
                            ExpressionStatement(
                                InvocationExpression(
                                    IdentifierName("Visit"),
                                    ArgumentList(Argument(IdentifierName("node")))))))),
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
                        Block(
                            ExpressionStatement(
                                InvocationExpression(
                                    IdentifierName("Visit"),
                                    ArgumentList(Argument(IdentifierName("node")))))))),
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
                    IfStatement(
                        SimpleMemberInvocationExpression(IdentifierName("list"), IdentifierName("Any")),
                        Block(
                            ForEachStatement(
                                IdentifierName("SyntaxToken"),
                                "token",
                                IdentifierName("list"),
                                Block(
                                    ExpressionStatement(
                                        InvocationExpression(
                                            IdentifierName("VisitToken"),
                                            ArgumentList(Argument(IdentifierName("token")))))))))));
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
