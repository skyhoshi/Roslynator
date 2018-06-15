// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
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
            var generator = new CSharpSyntaxWalkerGenerator();

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
                        generator.GenerateMemberDeclarations())));
        }
    }
}
