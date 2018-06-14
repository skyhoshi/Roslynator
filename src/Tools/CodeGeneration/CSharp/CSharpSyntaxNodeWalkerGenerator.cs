// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CodeGeneration.CSharp.RuntimeMetadataReference;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    public static class CSharpSyntaxNodeWalkerGenerator
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

        public static CompilationUnitSyntax Generate()
        {
            return CompilationUnit(
                UsingDirectives(
                    "Microsoft.CodeAnalysis",
                    "Microsoft.CodeAnalysis.CSharp",
                    "Microsoft.CodeAnalysis.CSharp.Syntax"),
                NamespaceDeclaration("Roslynator.CSharp.SyntaxWalkers",
                    ClassDeclaration(
                        default(SyntaxList<AttributeListSyntax>),
                        Modifiers.PublicAbstract(),
                        Identifier("CSharpSyntaxNodeWalker"),
                        default(TypeParameterListSyntax),
                        BaseList(SimpleBaseType(IdentifierName("CSharpSyntaxWalker"))),
                        default(SyntaxList<TypeParameterConstraintClauseSyntax>),
                        CreateMembers())));
        }

        private static SyntaxList<MemberDeclarationSyntax> CreateMembers()
        {
            Project project = new AdhocWorkspace()
            .CurrentSolution
            .AddProject("Project", "Assembly", LanguageNames.CSharp)
            .WithMetadataReferences(ImmutableArray.Create(
                CorLibReference,
                CreateFromAssemblyName("System.Core.dll"),
                CreateFromAssemblyName("System.Linq.dll"),
                CreateFromAssemblyName("System.Runtime.dll"),
                CreateFromAssemblyName("System.Collections.dll"),
                CreateFromAssemblyName("Microsoft.CodeAnalysis.dll"),
                CreateFromAssemblyName("Microsoft.CodeAnalysis.CSharp.dll")));

            Compilation compilation = project.GetCompilationAsync().Result;

            INamedTypeSymbol walkerSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker");
            INamedTypeSymbol syntaxNodeSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode");
            INamedTypeSymbol syntaxListSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxList`1");
            INamedTypeSymbol separatedSyntaxListSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SeparatedSyntaxList`1");
            INamedTypeSymbol syntaxTokenSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxToken");
            INamedTypeSymbol syntaxTokenListSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxTokenList");
            INamedTypeSymbol typeSyntaxSymbol = compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax");

            var members = new List<MemberDeclarationSyntax>();

            ConstructorDeclarationSyntax constructorDeclaration = ConstructorDeclaration(
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

            members.Add(constructorDeclaration);

            MethodDeclarationSyntax visitListMethodDeclaration = MethodDeclaration(
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

            members.Add(visitListMethodDeclaration);

            MethodDeclarationSyntax visitSeparatedListMethodDeclaration = MethodDeclaration(
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

            members.Add(visitSeparatedListMethodDeclaration);

            MethodDeclarationSyntax visitTokenListMethodDeclaration = MethodDeclaration(
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

            members.Add(visitTokenListMethodDeclaration);

            MethodDeclarationSyntax visitTypeMethodDeclaration = MethodDeclaration(
                Modifiers.ProtectedVirtual(),
                VoidType(),
                Identifier("VisitType"),
                ParameterList(Parameter(IdentifierName("TypeSyntax"), "node")),
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            IdentifierName("Visit"), ArgumentList(Argument(IdentifierName("node")))))));

            members.Add(visitTypeMethodDeclaration);

            foreach (ISymbol symbol in walkerSymbol
                .BaseType
                .GetMembers()
                .OrderBy(f => f.Name))
            {
                if (symbol.Kind == SymbolKind.Method
                    && symbol.DeclaredAccessibility == Accessibility.Public
                    && symbol.IsVirtual)
                {
                    string methodName = symbol.Name;

                    if (methodName.StartsWith("Visit", StringComparison.Ordinal)
                        && methodName.Length > 5)
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        IParameterSymbol parameter = methodSymbol.Parameters.Single();

                        MethodDeclarationSyntax methodDeclaration = MethodDeclaration(
                            Modifiers.PublicOverride(),
                            VoidType(),
                            Identifier(methodName),
                            ParseParameterList($"({parameter.ToDisplayString(_symbolDisplayFormat)})"),
                            Block(CreateStatements(parameter)));

                        members.Add(methodDeclaration);
                    }
                }
            }

            return members.ToSyntaxList();

            IEnumerable<StatementSyntax> CreateStatements(IParameterSymbol parameterSymbol)
            {
                string parameterName = parameterSymbol.Name;

                foreach (ISymbol symbol in parameterSymbol.Type.GetMembers())
                {
                    if (symbol.Kind == SymbolKind.Property
                        && symbol.DeclaredAccessibility == Accessibility.Public
                        && !symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        if (!propertySymbol.IsIndexer)
                        {
                            string propertyName = propertySymbol.Name;
                            ITypeSymbol propertyType = propertySymbol.Type;
                            ITypeSymbol originalDefinition = propertyType.OriginalDefinition;

                            if (originalDefinition.Equals(syntaxListSymbol))
                            {
                                ITypeSymbol typeArgument = ((INamedTypeSymbol)propertyType).TypeArguments.Single();
                                string typeArgumentName = typeArgument.Name;

                                switch (typeArgumentName)
                                {
                                    case "StatementSyntax":
                                    case "MemberDeclarationSyntax":
                                    case "XmlNodeSyntax":
                                    case "InterpolatedStringContentSyntax":
                                    case "QueryClauseSyntax":
                                    case "SwitchLabelSyntax":
                                    case "XmlAttributeSyntax":
                                        {
                                            yield return ExpressionStatement(
                                                InvocationExpression(
                                                    IdentifierName("VisitList"),
                                                    ArgumentList(
                                                        Argument(
                                                            SimpleMemberAccessExpression(
                                                                IdentifierName(parameterName),
                                                                IdentifierName(propertyName))))));

                                            break;
                                        }
                                    default:
                                        {
                                            string s = typeArgumentName.Remove(typeArgumentName.Length - 6);
                                            string variableName = StringUtility.ToCamelCase(s);

                                            yield return ForEachStatement(
                                                IdentifierName(typeArgumentName),
                                                variableName,
                                                SimpleMemberAccessExpression(
                                                    IdentifierName(parameterName),
                                                    IdentifierName(propertyName)),
                                                ExpressionStatement(
                                                    InvocationExpression(
                                                        IdentifierName("Visit" + s),
                                                        ArgumentList(Argument(IdentifierName(variableName))))));

                                            break;
                                        }
                                }
                            }
                            else if (originalDefinition.Equals(separatedSyntaxListSymbol))
                            {
                                ITypeSymbol typeArgument = ((INamedTypeSymbol)propertyType).TypeArguments.Single();
                                string typeArgumentName = typeArgument.Name;

                                switch (typeArgumentName)
                                {
                                    case "ExpressionSyntax":
                                    case "VariableDesignationSyntax":
                                    case "BaseTypeSyntax":
                                    case "TypeParameterConstraintSyntax":
                                        {
                                            yield return ExpressionStatement(
                                                InvocationExpression(
                                                    IdentifierName("VisitSeparatedList"),
                                                    ArgumentList(
                                                        Argument(
                                                            SimpleMemberAccessExpression(
                                                                IdentifierName(parameterName),
                                                                IdentifierName(propertyName))))));

                                            break;
                                        }
                                    default:
                                        {
                                            string s = typeArgumentName.Remove(typeArgumentName.Length - 6);
                                            string variableName = StringUtility.ToCamelCase(s);

                                            yield return ForEachStatement(
                                                IdentifierName(typeArgumentName),
                                                variableName,
                                                SimpleMemberAccessExpression(
                                                    IdentifierName(parameterName),
                                                    IdentifierName(propertyName)),
                                                ExpressionStatement(
                                                    InvocationExpression(
                                                        IdentifierName("Visit" + s),
                                                        ArgumentList(Argument(IdentifierName(variableName))))));

                                            break;
                                        }
                                }
                            }
                            else if (propertyType.EqualsOrInheritsFrom(syntaxNodeSymbol))
                            {
                                switch (propertyType.Name)
                                {
                                    case "ParameterListSyntax":
                                    case "BracketedParameterListSyntax":
                                    case "ArgumentListSyntax":
                                    case "BracketedArgumentListSyntax":
                                    case "AttributeArgumentListSyntax":
                                    case "AccessorListSyntax":
                                    case "TypeArgumentListSyntax":
                                    case "TypeParameterListSyntax":
                                        {
                                            foreach (StatementSyntax statement in CreateVisitListListMethodDeclaration(propertySymbol))
                                                yield return statement;

                                            break;
                                        }
                                    case "ArrowExpressionClauseSyntax":
                                    case "BlockSyntax":
                                    case "NameEqualsSyntax":
                                    case "NameColonSyntax":
                                    case "BaseListSyntax":
                                    case "WhenClauseSyntax":
                                    case "ExplicitInterfaceSpecifierSyntax":
                                    case "ParameterSyntax":
                                    case "ArrayTypeSyntax":
                                    case "AttributeTargetSpecifierSyntax":
                                    case "CatchDeclarationSyntax":
                                    case "CatchFilterClauseSyntax":
                                    case "EqualsValueClauseSyntax":
                                    case "VariableDeclarationSyntax":
                                    case "XmlNameSyntax":
                                    case "XmlPrefixSyntax":
                                    case "XmlElementStartTagSyntax":
                                    case "XmlElementEndTagSyntax":
                                    case "ElseClauseSyntax":
                                    case "FinallyClauseSyntax":
                                    case "CrefParameterListSyntax":
                                    case "InterpolationAlignmentClauseSyntax":
                                    case "InterpolationFormatClauseSyntax":
                                    case "QueryBodySyntax":
                                    case "JoinIntoClauseSyntax":
                                    case "QueryContinuationSyntax":
                                    case "FromClauseSyntax":
                                    case "CrefBracketedParameterListSyntax":
                                    case "ConstructorInitializerSyntax":
                                        {
                                            foreach (StatementSyntax statement in CreateVisitMethodDeclaration(propertySymbol, propertyType.Name.Remove(propertyType.Name.Length - 6)))
                                                yield return statement;

                                            break;
                                        }
                                    case "NameSyntax":
                                    case "IdentifierNameSyntax":
                                    case "SimpleNameSyntax":
                                    case "ExpressionSyntax":
                                    case "InitializerExpressionSyntax":
                                    case "TypeSyntax":
                                    case "PatternSyntax":
                                    case "StatementSyntax":
                                    case "VariableDesignationSyntax":
                                    case "SelectOrGroupClauseSyntax":
                                    case "MemberCrefSyntax":
                                    case "CrefSyntax":
                                    case "CSharpSyntaxNode":
                                        {
                                            foreach (StatementSyntax statement in CreateVisitMethodDeclaration(propertySymbol))
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
                                && !propertyType.Equals(syntaxTokenSymbol)
                                && !propertyType.Equals(syntaxTokenListSymbol))
                            {
                                throw new InvalidOperationException();
                            }
                        }
                    }
                }

                IEnumerable<StatementSyntax> CreateVisitMethodDeclaration(IPropertySymbol propertySymbol, string methodNameSuffix = null)
                {
                    if (methodNameSuffix == null)
                    {
                        if (propertySymbol.Type.EqualsOrInheritsFrom(typeSyntaxSymbol))
                        {
                            yield return CreateExpressionStatement("VisitType");
                        }
                        else
                        {
                            yield return CreateExpressionStatement("Visit");
                        }
                    }
                    else
                    {
                        string localName = StringUtility.ToCamelCase(propertySymbol.Name);

                        switch (localName)
                        {
                            case "else":
                            case "default":
                            case "finally":
                                {
                                    localName = "@" + localName;
                                    break;
                                }
                        }

                        yield return LocalDeclarationStatement(
                            propertySymbol.Type.ToTypeSyntax(_symbolDisplayFormat),
                            localName,
                            SimpleMemberAccessExpression(
                                IdentifierName(parameterName),
                                IdentifierName(propertySymbol.Name)));

                        yield return IfStatement(
                            NotEqualsExpression(
                                IdentifierName(localName),
                                NullLiteralExpression()),
                            ExpressionStatement(
                            InvocationExpression(
                                IdentifierName("Visit" + methodNameSuffix),
                                ArgumentList(
                                    Argument(IdentifierName(localName))))));
                    }

                    ExpressionStatementSyntax CreateExpressionStatement(string name)
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

                IEnumerable<StatementSyntax> CreateVisitListListMethodDeclaration(IPropertySymbol propertySymbol)
                {
                    string propertyName = propertySymbol.Name;

                    string localName = StringUtility.ToCamelCase(propertyName);

                    yield return LocalDeclarationStatement(
                        propertySymbol.Type.ToTypeSyntax(_symbolDisplayFormat),
                        localName,
                        SimpleMemberAccessExpression(
                            IdentifierName(parameterName),
                            IdentifierName(propertyName)));

                    string listPropertyName = null;

                    if (propertyName == "TypeArgumentList")
                    {
                        listPropertyName = "Arguments";
                    }
                    else if (propertyName == "TypeParameterList")
                    {
                        listPropertyName = "Parameters";
                    }
                    else
                    {
                        listPropertyName = propertyName.Remove(propertyName.Length - 4) + "s";
                    }

                    IPropertySymbol listProperty = FindListProperty();

                    yield return IfStatement(
                        NotEqualsExpression(IdentifierName(localName), NullLiteralExpression()),
                        ExpressionStatement(
                            InvocationExpression(
                                IdentifierName((listProperty.Type.OriginalDefinition.Equals(syntaxListSymbol)) ? "VisitList" : "VisitSeparatedList"),
                                ArgumentList(
                                    Argument(
                                        SimpleMemberAccessExpression(
                                            IdentifierName(localName),
                                            IdentifierName(listPropertyName)))))));

                    IPropertySymbol FindListProperty()
                    {
                        foreach (ISymbol symbol in propertySymbol.Type.GetMembers())
                        {
                            if (symbol.Kind == SymbolKind.Property
                                && symbol.DeclaredAccessibility == Accessibility.Public
                                && !symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                            {
                                var propertySymbol2 = (IPropertySymbol)symbol;

                                if (!propertySymbol2.IsIndexer
                                    && propertySymbol2.Name == listPropertyName
                                    && (propertySymbol2.Type.OriginalDefinition.Equals(syntaxListSymbol) || propertySymbol2.Type.OriginalDefinition.Equals(separatedSyntaxListSymbol)))
                                {
                                    return propertySymbol2;
                                }
                            }
                        }

                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
