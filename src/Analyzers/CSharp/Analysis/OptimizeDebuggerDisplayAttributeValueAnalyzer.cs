// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.CSharp.Analysis
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class OptimizeDebuggerDisplayAttributeValueAnalyzer : BaseDiagnosticAnalyzer
    {
        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics
        {
            get { return ImmutableArray.Create(DiagnosticDescriptors.OptimizeDebuggerDisplayAttributeValue); }
        }

        public override void Initialize(AnalysisContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            base.Initialize(context);

            context.RegisterCompilationStartAction(startContext =>
            {
                INamedTypeSymbol debuggerDisplayAttribute = startContext.Compilation.GetTypeByMetadataName(MetadataNames.System_Diagnostics_DebuggerDisplayAttribute);

                startContext.RegisterSymbolAction(nodeContext => AnalyzeNamedType(nodeContext, debuggerDisplayAttribute), SymbolKind.NamedType);
            });
        }

        public static void AnalyzeNamedType(SymbolAnalysisContext context, INamedTypeSymbol debuggerDisplayAttribute)
        {
            var symbol = (INamedTypeSymbol)context.Symbol;

            if (!symbol.TypeKind.Is(TypeKind.Class, TypeKind.Struct))
                return;

            if (symbol.IsImplicitlyDeclared)
                return;

            if (symbol.IsImplicitClass)
                return;

            AttributeData attributeData = symbol.GetAttribute(debuggerDisplayAttribute);

            if (attributeData == null)
                return;

            string value = attributeData
                .ConstructorArguments
                .SingleOrDefault(shouldThrow: false)
                .Value?
                .ToString();

            if (value == null)
                return;

            if (!IsFixable(value))
                return;

            var attribute = (AttributeSyntax)attributeData.ApplicationSyntaxReference.GetSyntax(context.CancellationToken);

            ExpressionSyntax expression = attribute.ArgumentList.Arguments.First().Expression.WalkDownParentheses();

            Debug.Assert(expression.IsKind(SyntaxKind.StringLiteralExpression), expression.Kind().ToString());

            if (!expression.IsKind(SyntaxKind.StringLiteralExpression))
                return;

            context.ReportDiagnostic(DiagnosticDescriptors.OptimizeDebuggerDisplayAttributeValue, expression);
        }

        private static bool IsFixable(string value)
        {
            int length = value.Length;

            if (length == 0)
                return false;

            int count = 0;

            int i = 0;

            while (true)
            {
                FindOpenBrace();

                if (i == -1)
                    return false;

                if (i == length)
                    break;

                i++;

                FindCloseBrace();

                if (i == -1)
                    return false;

                if (i == length)
                    break;

                i++;

                count++;
            }

            return count > 1;

            void FindOpenBrace()
            {
                while (i < length)
                {
                    switch (value[i])
                    {
                        case '{':
                            {
                                return;
                            }
                        case '}':
                            {
                                i = -1;
                                return;
                            }
                        case '\\':
                            {
                                i++;

                                if (i < length)
                                {
                                    char ch = value[i];

                                    if (ch == '{'
                                        || ch == '}')
                                    {
                                        i++;
                                    }

                                    continue;
                                }
                                else
                                {
                                    return;
                                }
                            }
                    }

                    i++;
                }
            }

            void FindCloseBrace()
            {
                while (i < length)
                {
                    switch (value[i])
                    {
                        case '{':
                            {
                                i = -1;
                                return;
                            }
                        case '}':
                            {
                                return;
                            }
                        case '(':
                            {
                                i++;

                                if (i < length
                                    && value[i] == ')')
                                {
                                    break;
                                }

                                i = -1;
                                return;
                            }
                        case ',':
                            {
                                i++;
                                if (i < length
                                    && value[i] == 'n')
                                {
                                    i++;
                                    if (i < length
                                        && value[i] == 'q')
                                    {
                                        i++;
                                        if (i < length
                                            && value[i] == '}')
                                        {
                                            return;
                                        }
                                    }
                                }

                                i = -1;
                                return;
                            }
                    }

                    i++;
                }
            }
        }
    }
}
