# InitializerExpressionSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [ExpressionSyntax](ExpressionSyntax.md)
        * InitializerExpressionSyntax

## Syntax Properties

| Name            | Type                                                 |
| --------------- | ---------------------------------------------------- |
| OpenBraceToken  | SyntaxToken                                          |
| Expressions     | SyntaxList\<[ExpressionSyntax](ExpressionSyntax.md)> |
| CloseBraceToken | SyntaxToken                                          |

## SyntaxKinds

* ArrayInitializerExpression
* CollectionInitializerExpression
* ComplexElementInitializerExpression
* ObjectInitializerExpression

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*