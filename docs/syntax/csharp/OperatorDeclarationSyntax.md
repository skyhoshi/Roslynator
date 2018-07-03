# OperatorDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BaseMethodDeclarationSyntax](BaseMethodDeclarationSyntax.md)
          * OperatorDeclarationSyntax

## Syntax Properties

| Name            | Type                                                          |
| --------------- | ------------------------------------------------------------- |
| AttributeLists  | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>    |
| Modifiers       | SyntaxTokenList                                               |
| ReturnType      | [TypeSyntax](TypeSyntax.md)                                   |
| OperatorKeyword | SyntaxToken                                                   |
| OperatorToken   | SyntaxToken                                                   |
| ParameterList   | [ParameterListSyntax](ParameterListSyntax.md)                 |
| Body            | [BlockSyntax](BlockSyntax.md)                                 |
| ExpressionBody  | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md) |
| SemicolonToken  | SyntaxToken                                                   |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.operatordeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*