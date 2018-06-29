# ForEachStatementSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StatementSyntax](StatementSyntax.md)
        * [CommonForEachStatementSyntax](CommonForEachStatementSyntax.md)
          * ForEachStatementSyntax

## Syntax Properties

| Name            | Type                                    |
| --------------- | --------------------------------------- |
| ForEachKeyword  | SyntaxToken                             |
| OpenParenToken  | SyntaxToken                             |
| Type            | [TypeSyntax](TypeSyntax.md)             |
| Identifier      | SyntaxToken                             |
| InKeyword       | SyntaxToken                             |
| Expression      | [ExpressionSyntax](ExpressionSyntax.md) |
| CloseParenToken | SyntaxToken                             |
| Statement       | [StatementSyntax](StatementSyntax.md)   |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.foreachstatementsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*