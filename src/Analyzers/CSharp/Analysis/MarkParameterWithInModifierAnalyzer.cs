// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class MarkParameterWithInModifierAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.MarkParameterWithInModifier); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);
            context.EnableConcurrentExecution();

            context.RegisterSymbolAction(AnalyzeMethodSymbol, SymbolKind.Method);
            context.RegisterSymbolAction(AnalyzePropertySymbol, SymbolKind.Property);
        }

        private static void AnalyzeMethodSymbol(SymbolAnalysisContext context)
        {
            var methodSymbol = (IMethodSymbol)context.Symbol;

            if (methodSymbol.IsAsync)
                return;

            if (!methodSymbol.MethodKind.Is(
                MethodKind.Constructor,
                MethodKind.Destructor,
                MethodKind.Ordinary,
                MethodKind.StaticConstructor,
                MethodKind.UserDefinedOperator))
            {
                return;
            }

            if (methodSymbol.ImplementsInterfaceMember(allInterfaces: true))
                return;

            ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

            Analyze(context, parameters);
        }

        private static void AnalyzePropertySymbol(SymbolAnalysisContext context)
        {
            var propertySymbol = (IPropertySymbol)context.Symbol;

            if (!propertySymbol.IsIndexer)
                return;

            if (propertySymbol.ImplementsInterfaceMember(allInterfaces: true))
                return;

            ImmutableArray<IParameterSymbol> parameters = propertySymbol.Parameters;

            Analyze(context, parameters);
        }

        private static void Analyze(SymbolAnalysisContext context, ImmutableArray<IParameterSymbol> parameters)
        {
            foreach (IParameterSymbol parameter in parameters)
            {
                if (parameter.RefKind != RefKind.None)
                    continue;

                ITypeSymbol type = parameter.Type;

                if (type.TypeKind != TypeKind.Struct)
                    continue;

                if (!(type.GetSyntaxOrDefault(context.CancellationToken) is StructDeclarationSyntax structDeclaration))
                    continue;

                if (!structDeclaration.Modifiers.Contains(SyntaxKind.ReadOnlyKeyword))
                    continue;

                if (!(parameter.GetSyntaxOrDefault(context.CancellationToken) is ParameterSyntax parameterSyntax))
                    continue;

                context.ReportDiagnostic(DiagnosticDescriptors.MarkParameterWithInModifier, parameterSyntax.Identifier);
            }
        }
    }
}
