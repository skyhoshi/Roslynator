# MethodDeclarationSyntax

## Properties

| Name                       | Type                                                                    |
| -------------------------- | ----------------------------------------------------------------------- |
| Arity                      | int                                                                     |
| AttributeLists             | SyntaxList\<AttributeListSyntax>                                        |
| Modifiers                  | SyntaxTokenList                                                         |
| ReturnType                 | [TypeSyntax](TypeSyntax.md)                                             |
| ExplicitInterfaceSpecifier | [ExplicitInterfaceSpecifierSyntax](ExplicitInterfaceSpecifierSyntax.md) |
| Identifier                 | SyntaxToken                                                             |
| TypeParameterList          | [TypeParameterListSyntax](TypeParameterListSyntax.md)                   |
| ParameterList              | [ParameterListSyntax](ParameterListSyntax.md)                           |
| ConstraintClauses          | SyntaxList\<TypeParameterConstraintClauseSyntax>                        |
| Body                       | [BlockSyntax](BlockSyntax.md)                                           |
| ExpressionBody             | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md)           |
| SemicolonToken             | SyntaxToken                                                             |

## SyntaxKinds

* SyntaxKind\.MethodDeclaration

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.methoddeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*