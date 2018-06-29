# ForStatementSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StatementSyntax](StatementSyntax.md)
        * ForStatementSyntax

## Syntax Properties

| Name                 | Type                                                      |
| -------------------- | --------------------------------------------------------- |
| ForKeyword           | SyntaxToken                                               |
| OpenParenToken       | SyntaxToken                                               |
| Declaration          | [VariableDeclarationSyntax](VariableDeclarationSyntax.md) |
| Initializers         | SyntaxList\<[ExpressionSyntax](ExpressionSyntax.md)>      |
| FirstSemicolonToken  | SyntaxToken                                               |
| Condition            | [ExpressionSyntax](ExpressionSyntax.md)                   |
| SecondSemicolonToken | SyntaxToken                                               |
| Incrementors         | SyntaxList\<[ExpressionSyntax](ExpressionSyntax.md)>      |
| CloseParenToken      | SyntaxToken                                               |
| Statement            | [StatementSyntax](StatementSyntax.md)                     |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.forstatementsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*