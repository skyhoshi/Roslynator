# TryStatementSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StatementSyntax](StatementSyntax.md)
        * TryStatementSyntax

## Syntax Properties

| Name       | Type                                                   |
| ---------- | ------------------------------------------------------ |
| TryKeyword | SyntaxToken                                            |
| Block      | [BlockSyntax](BlockSyntax.md)                          |
| Catches    | SyntaxList\<[CatchClauseSyntax](CatchClauseSyntax.md)> |
| Finally    | [FinallyClauseSyntax](FinallyClauseSyntax.md)          |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.trystatementsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*