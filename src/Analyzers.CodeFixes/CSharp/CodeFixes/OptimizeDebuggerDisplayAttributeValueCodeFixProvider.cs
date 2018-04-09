// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Composition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CodeFixes;
using Roslynator.Text;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Roslynator.CSharp.CSharpFactory;

namespace Roslynator.CSharp.CodeFixes
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(OptimizeDebuggerDisplayAttributeValueCodeFixProvider))]
    [Shared]
    public class OptimizeDebuggerDisplayAttributeValueCodeFixProvider : BaseCodeFixProvider
    {
        public sealed override ImmutableArray<string> FixableDiagnosticIds
        {
            get { return ImmutableArray.Create(DiagnosticIdentifiers.OptimizeDebuggerDisplayAttributeValue); }
        }

        public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            SyntaxNode root = await context.GetSyntaxRootAsync().ConfigureAwait(false);

            if (!TryFindFirstAncestorOrSelf(root, context.Span, out AttributeSyntax attribute))
                return;

            Diagnostic diagnostic = context.Diagnostics[0];

            CodeAction codeAction = CodeAction.Create(
                "Add property 'DebuggerDisplay'",
                cancellationToken => RefactorAsync(context.Document, attribute, cancellationToken),
                GetEquivalenceKey(diagnostic));

            context.RegisterCodeFix(codeAction, diagnostic);
        }

        private static async Task<Document> RefactorAsync(
            Document document,
            AttributeSyntax attribute,
            CancellationToken cancellationToken)
        {
            TypeDeclarationSyntax typeDeclaration = attribute.FirstAncestor<TypeDeclarationSyntax>();

            SemanticModel semanticModel = await document.GetSemanticModelAsync(cancellationToken).ConfigureAwait(false);

            string propertyName = NameGenerator.Default.EnsureUniqueMemberName("DebuggerDisplay", semanticModel, typeDeclaration.OpenBraceToken.Span.End, cancellationToken: cancellationToken);

            AttributeArgumentSyntax argument = attribute.ArgumentList.Arguments.First();

            TypeDeclarationSyntax newTypeDeclaration = typeDeclaration.ReplaceNode(
                argument,
                argument.WithExpression(
                    StringLiteralExpression($"{{{propertyName},nq}}")).WithTriviaFrom(argument.Expression));

            string value = semanticModel
                .GetDeclaredSymbol(typeDeclaration, cancellationToken)
                .GetAttribute(semanticModel.GetTypeByMetadataName(MetadataNames.System_Diagnostics_DebuggerDisplayAttribute))
                .ConstructorArguments[0]
                .Value
                .ToString();

            ExpressionSyntax returnExpression = GetReturnExpression(value, SyntaxInfo.StringLiteralExpressionInfo(argument.Expression).IsVerbatim);

            PropertyDeclarationSyntax propertyDeclaration = PropertyDeclaration(
                SingletonList(
                    AttributeList(
                        Attribute(
                            ParseName("System.Diagnostics.DebuggerBrowsableAttribute"),
                            AttributeArgument(
                                SimpleMemberAccessExpression(
                                    ParseName("System.Diagnostics.DebuggerBrowsableState").WithSimplifierAnnotation(),
                                    IdentifierName("Never"))
                            )
                        ).WithSimplifierAnnotation()
                    )
                ),
                Modifiers.Private(),
                CSharpTypeFactory.StringType(),
                default(ExplicitInterfaceSpecifierSyntax),
                Identifier(propertyName),
                AccessorList(
                    GetAccessorDeclaration(
                        Block(
                            ReturnStatement(returnExpression)))));

            propertyDeclaration = propertyDeclaration.WithFormatterAnnotation();

            newTypeDeclaration = MemberDeclarationInserter.Default.Insert(newTypeDeclaration, propertyDeclaration);

            return await document.ReplaceNodeAsync(typeDeclaration, newTypeDeclaration, cancellationToken).ConfigureAwait(false);
        }

        private static ExpressionSyntax GetReturnExpression(string value, bool isVerbatim)
        {
            StringBuilder sb = StringBuilderCache.GetInstance(capacity: value.Length);

            sb.Append('$');

            if (isVerbatim)
                sb.Append('@');

            sb.Append('"');

            int length = value.Length;

            int i = 0;

            int lastPos = i;

            while (true)
            {
                lastPos = i;

                AppendInterpolatedText();

                if (i == length)
                    break;

                i++;

                lastPos = i;

                AppendInterpolation();

                i++;
            }

            sb.Append(value, lastPos, i - lastPos);
            sb.Append("\"");

            return ParseExpression(StringBuilderCache.GetStringAndFree(sb));

            void AppendInterpolatedText()
            {
                while (i < length)
                {
                    switch (value[i])
                    {
                        case '{':
                            {
                                sb.Append(value, lastPos, i - lastPos);
                                return;
                            }
                        case '\\':
                            {
                                sb.Append(value, lastPos, i - lastPos);

                                i++;

                                if (i < length)
                                {
                                    char ch = value[i];

                                    if (ch == '{'
                                        || ch == '}')
                                    {
                                        sb.Append(ch);
                                        sb.Append(ch);
                                        i++;
                                        lastPos = i;
                                        continue;
                                    }
                                }

                                sb.Append((isVerbatim) ? "\\" : "\\\\");
                                lastPos = i;
                                continue;
                            }
                        case '"':
                            {
                                sb.Append(value, lastPos, i - lastPos);
                                sb.Append((isVerbatim) ? "\"\"" : "\\\"");
                                i++;
                                lastPos = i;
                                continue;
                            }
                    }

                    i++;
                }
            }

            void AppendInterpolation()
            {
                while (i < length)
                {
                    switch (value[i])
                    {
                        case '}':
                            {
                                sb.Append((isVerbatim) ? "\"\"" : "\\\"");
                                sb.Append('{');
                                sb.Append(value, lastPos, i - lastPos);
                                sb.Append('}');
                                sb.Append((isVerbatim) ? "\"\"" : "\\\"");
                                return;
                            }
                        case '(':
                            {
                                i++;
                                break;
                            }
                        case ',':
                            {
                                sb.Append('{');
                                sb.Append(value, lastPos, i - lastPos);
                                sb.Append('}');

                                i += 3;
                                return;
                            }
                    }

                    i++;
                }
            }
        }
    }
}