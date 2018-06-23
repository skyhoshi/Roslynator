// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CodeGeneration.CSharp
{
    public class IdentifierNameWalkerGenerator : CSharpSyntaxWalkerGenerator
    {
        public IdentifierNameWalkerGenerator(
            SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node,
            bool shouldVisitFunction = true,
            bool useCustomVisitMethod = false,
            bool shouldGenerateVisitType = false,
            bool eliminateDefaultVisit = false,
            bool inlineVisitWithSingleProperty = false,
            bool inlineVisitListSyntax = false) : base(depth, shouldVisitFunction, useCustomVisitMethod, shouldGenerateVisitType, eliminateDefaultVisit, inlineVisitWithSingleProperty, inlineVisitListSyntax)
        {
        }

        public static CompilationUnitSyntax Generate()
        {
            var generator = new IdentifierNameWalkerGenerator(
                depth: SyntaxWalkerDepth.Node,
                shouldVisitFunction: true,
                useCustomVisitMethod: true,
                shouldGenerateVisitType: true,
                eliminateDefaultVisit: true,
                inlineVisitWithSingleProperty: false,
                inlineVisitListSyntax: false);

            return CompilationUnit(
                UsingDirectives(
                    "System",
                    "Microsoft.CodeAnalysis",
                    "Microsoft.CodeAnalysis.CSharp",
                    "Microsoft.CodeAnalysis.CSharp.Syntax"),
                NamespaceDeclaration("Roslynator.CSharp.SyntaxWalkers",
                    ClassDeclaration(
                        default(SyntaxList<AttributeListSyntax>),
                        Modifiers.PublicAbstract(),
                        Identifier("IdentifierNameWalker"),
                        default(TypeParameterListSyntax),
                        default(BaseListSyntax),
                        default(SyntaxList<TypeParameterConstraintClauseSyntax>),
                        generator.GenerateMemberDeclarations())));
        }

        public override ConstructorDeclarationSyntax CreateConstructorDeclaration(SyntaxWalkerDepth depth)
        {
            return ConstructorDeclaration(
                default(SyntaxList<AttributeListSyntax>),
                Modifiers.Protected(),
                Identifier("IdentifierNameWalker"),
                ParameterList(),
                default,
                Block());
        }
    }
}
