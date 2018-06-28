# ForStatementSyntax

## Properties

| Name                 | Type                                                      |
| -------------------- | --------------------------------------------------------- |
| ForKeyword           | SyntaxToken                                               |
| OpenParenToken       | SyntaxToken                                               |
| Declaration          | [VariableDeclarationSyntax](VariableDeclarationSyntax.md) |
| Initializers         | SeparatedSyntaxList\<ExpressionSyntax>                    |
| FirstSemicolonToken  | SyntaxToken                                               |
| Condition            | [ExpressionSyntax](ExpressionSyntax.md)                   |
| SecondSemicolonToken | SyntaxToken                                               |
| Incrementors         | SeparatedSyntaxList\<ExpressionSyntax>                    |
| CloseParenToken      | SyntaxToken                                               |
| Statement            | [StatementSyntax](StatementSyntax.md)                     |

## SyntaxKinds

* SyntaxKind\.ForStatement

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.forstatementsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*