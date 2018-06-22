// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;
using static Roslynator.CodeGeneration.CSharp.RuntimeMetadataReference;

namespace Roslynator.CodeGeneration.CSharp
{
    internal static partial class Symbols
    {
        private static Compilation _compilation;

        private static ImmutableArray<IMethodSymbol> _visitMethods;
        private static ImmutableArray<INamedTypeSymbol> _syntaxSymbols;
        private static readonly ImmutableDictionary<ITypeSymbol, IMethodSymbol> _typeSymbolMethodSymbolMap = VisitMethodSymbols.ToImmutableDictionary(f => f.Parameters.Single().Type);

        private static INamedTypeSymbol _csharpSyntaxWalkerSymbol;
        private static INamedTypeSymbol _syntaxNodeSymbol;
        private static INamedTypeSymbol _syntaxListSymbol;
        private static INamedTypeSymbol _separatedSyntaxListSymbol;
        private static INamedTypeSymbol _syntaxTokenSymbol;
        private static INamedTypeSymbol _syntaxTokenListSymbol;
        private static INamedTypeSymbol _typeSyntaxSymbol;
        private static INamedTypeSymbol _statementSyntaxSymbol;
        private static INamedTypeSymbol _expressionSyntaxSymbol;
        private static INamedTypeSymbol _patternSyntaxSymbol;
        private static INamedTypeSymbol _variableDesignationSyntaxSymbol;
        private static INamedTypeSymbol _memberCrefSyntaxSymbol;
        private static INamedTypeSymbol _selectOrGroupClauseSyntaxSymbol;
        private static INamedTypeSymbol _crefSyntaxSymbol;
        private static INamedTypeSymbol _baseTypeSyntaxSymbol;
        private static INamedTypeSymbol _memberDeclarationSyntaxSymbol;
        private static INamedTypeSymbol _xmlNodeSyntaxSymbol;
        private static INamedTypeSymbol _interpolatedStringContentSyntaxSymbol;
        private static INamedTypeSymbol _queryClauseSyntaxSymbol;
        private static INamedTypeSymbol _switchLabelSyntaxSymbol;
        private static INamedTypeSymbol _typeParameterConstraintSyntaxSymbol;
        private static INamedTypeSymbol _xmlAttributeSyntaxSymbol;

        public static INamedTypeSymbol CSharpSyntaxWalkerSymbol => _csharpSyntaxWalkerSymbol ?? (_csharpSyntaxWalkerSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxWalker"));
        public static INamedTypeSymbol SyntaxNodeSymbol => _syntaxNodeSymbol ?? (_syntaxNodeSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode"));
        public static INamedTypeSymbol SyntaxListSymbol => _syntaxListSymbol ?? (_syntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxList`1"));
        public static INamedTypeSymbol SeparatedSyntaxListSymbol => _separatedSyntaxListSymbol ?? (_separatedSyntaxListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SeparatedSyntaxList`1"));
        public static INamedTypeSymbol SyntaxTokenSymbol => _syntaxTokenSymbol ?? (_syntaxTokenSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxToken"));
        public static INamedTypeSymbol SyntaxTokenListSymbol => _syntaxTokenListSymbol ?? (_syntaxTokenListSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxTokenList"));
        public static INamedTypeSymbol TypeSyntaxSymbol => _typeSyntaxSymbol ?? (_typeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.TypeSyntax"));
        public static INamedTypeSymbol StatementSyntaxSymbol => _statementSyntaxSymbol ?? (_statementSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.StatementSyntax"));
        public static INamedTypeSymbol ExpressionSyntaxSymbol => _expressionSyntaxSymbol ?? (_expressionSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.ExpressionSyntax"));
        public static INamedTypeSymbol PatternSyntaxSymbol => _patternSyntaxSymbol ?? (_patternSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.PatternSyntax"));
        public static INamedTypeSymbol VariableDesignationSyntaxSymbol => _variableDesignationSyntaxSymbol ?? (_variableDesignationSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.VariableDesignationSyntax"));
        public static INamedTypeSymbol MemberCrefSyntaxSymbol => _memberCrefSyntaxSymbol ?? (_memberCrefSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.MemberCrefSyntax"));
        public static INamedTypeSymbol SelectOrGroupClauseSyntaxSymbol => _selectOrGroupClauseSyntaxSymbol ?? (_selectOrGroupClauseSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.SelectOrGroupClauseSyntax"));
        public static INamedTypeSymbol CrefSyntaxSymbol => _crefSyntaxSymbol ?? (_crefSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.CrefSyntax"));
        public static INamedTypeSymbol BaseTypeSyntaxSymbol => _baseTypeSyntaxSymbol ?? (_baseTypeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.BaseTypeSyntax"));
        public static INamedTypeSymbol MemberDeclarationSyntaxSymbol => _memberDeclarationSyntaxSymbol ?? (_memberDeclarationSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax"));
        public static INamedTypeSymbol XmlNodeSyntaxSymbol => _xmlNodeSyntaxSymbol ?? (_xmlNodeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.XmlNodeSyntax"));
        public static INamedTypeSymbol InterpolatedStringContentSyntaxSymbol => _interpolatedStringContentSyntaxSymbol ?? (_interpolatedStringContentSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.InterpolatedStringContentSyntax"));
        public static INamedTypeSymbol QueryClauseSyntaxSymbol => _queryClauseSyntaxSymbol ?? (_queryClauseSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.QueryClauseSyntax"));
        public static INamedTypeSymbol SwitchLabelSyntaxSymbol => _switchLabelSyntaxSymbol ?? (_switchLabelSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.SwitchLabelSyntax"));
        public static INamedTypeSymbol TypeParameterConstraintSyntaxSymbol => _typeParameterConstraintSyntaxSymbol ?? (_typeParameterConstraintSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.TypeParameterConstraintSyntax"));
        public static INamedTypeSymbol XmlAttributeSyntaxSymbol => _xmlAttributeSyntaxSymbol ?? (_xmlAttributeSyntaxSymbol = Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.XmlAttributeSyntax"));

        public static ImmutableArray<IMethodSymbol> VisitMethodSymbols
        {
            get
            {
                if (_visitMethods.IsDefault)
                {
                    _visitMethods = CSharpSyntaxWalkerSymbol
                        .BaseType
                        .GetMembers()
                        .Where(f => f.Kind == SymbolKind.Method
                            && f.DeclaredAccessibility == Accessibility.Public
                            && f.IsVirtual
                            && f.Name.Length > 5
                            && f.Name.StartsWith("Visit", StringComparison.Ordinal))
                        .OrderBy(f => f.Name)
                        .Cast<IMethodSymbol>()
                        .ToImmutableArray();
                }

                return _visitMethods;
            }
        }

        public static ImmutableArray<INamedTypeSymbol> SyntaxSymbols
        {
            get
            {
                if (_syntaxSymbols.IsDefault)
                {
                    _syntaxSymbols = Compilation
                        .GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.Syntax.AccessorDeclarationSyntax")
                        .ContainingNamespace
                        .GetTypeMembers()
                        .Where(f => f.TypeKind == TypeKind.Class && f.InheritsFrom(SyntaxNodeSymbol))
                        .OrderBy(f => f.Name)
                        .ToImmutableArray();
                }

                return _syntaxSymbols;
            }
        }

        internal static Compilation Compilation
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

        public static IMethodSymbol FindVisitMethod(ITypeSymbol typeSymbol)
        {
            return (_typeSymbolMethodSymbolMap.TryGetValue(typeSymbol, out IMethodSymbol methodSymbol))
                ? methodSymbol
                : null;
        }

        public static IEnumerable<IPropertySymbol> GetPropertySymbols(ITypeSymbol typeSymbol, string name = null)
        {
            foreach (ISymbol symbol in (name != null) ? typeSymbol.GetMembers(name) : typeSymbol.GetMembers())
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

                yield return propertySymbol;
            }
        }

        public static IPropertySymbol GetSinglePropertySymbolOrDefault(ITypeSymbol typeSymbol)
        {
            IPropertySymbol singlePropertySymbol = null;

            foreach (IPropertySymbol propertySymbol in GetPropertySymbols(typeSymbol))
            {
                ITypeSymbol propertyType = propertySymbol.Type.OriginalDefinition;

                if (propertyType.Equals(SyntaxListSymbol))
                    return null;

                if (propertyType.Equals(SeparatedSyntaxListSymbol))
                    return null;

                if (propertyType.Equals(SyntaxTokenListSymbol))
                    continue;

                if (propertyType.Equals(SyntaxTokenSymbol))
                    continue;

                if (propertyType.EqualsOrInheritsFrom(SyntaxNodeSymbol))
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
                                return null;
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
                                if (singlePropertySymbol == null)
                                {
                                    singlePropertySymbol = propertySymbol;
                                    continue;
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        default:
                            {
                                throw new InvalidOperationException($"Unrecognized property type '{propertyType.ToDisplayString()}'.");
                            }
                    }
                }

                if (!CSharpFacts.IsPredefinedType(propertyType.SpecialType))
                    throw new InvalidOperationException();
            }

            return singlePropertySymbol;
        }

        public static IPropertySymbol FindListPropertySymbol(IPropertySymbol propertySymbol)
        {
            string propertyName = propertySymbol.Name;

            string name = GetListPropertyName();

            foreach (IPropertySymbol propertySymbol2 in GetPropertySymbols(propertySymbol.Type, name))
            {
                if (propertySymbol2.Type.OriginalDefinition.Equals(SyntaxListSymbol)
                    || propertySymbol2.Type.OriginalDefinition.Equals(SeparatedSyntaxListSymbol))
                {
                    return propertySymbol2;
                }
            }

            return null;

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
        }

        public static bool IsVisitFunction(ITypeSymbol typeSymbol)
        {
            switch (typeSymbol.Name)
            {
                case "SimpleLambdaExpressionSyntax":
                case "ParenthesizedLambdaExpressionSyntax":
                case "AnonymousMethodExpressionSyntax":
                case "LocalFunctionStatementSyntax":
                    return true;
                default:
                    return false;
            }
        }
    }
}
