# CommonForEachStatementSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StatementSyntax](StatementSyntax.md)
        * CommonForEachStatementSyntax

## Syntax Properties

| Name            | Type                                    |
| --------------- | --------------------------------------- |
| ForEachKeyword  | SyntaxToken                             |
| OpenParenToken  | SyntaxToken                             |
| InKeyword       | SyntaxToken                             |
| Expression      | [ExpressionSyntax](ExpressionSyntax.md) |
| CloseParenToken | SyntaxToken                             |
| Statement       | [StatementSyntax](StatementSyntax.md)   |

### Directly Derived Types

* [ForEachStatementSyntax](ForEachStatementSyntax.md)
* [ForEachVariableStatementSyntax](ForEachVariableStatementSyntax.md)

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.commonforeachstatementsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*