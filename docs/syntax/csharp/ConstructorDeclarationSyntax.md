# ConstructorDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BaseMethodDeclarationSyntax](BaseMethodDeclarationSyntax.md)
          * ConstructorDeclarationSyntax

## Syntax Properties

| Name           | Type                                                            |
| -------------- | --------------------------------------------------------------- |
| AttributeLists | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>      |
| Modifiers      | SyntaxTokenList                                                 |
| Identifier     | SyntaxToken                                                     |
| ParameterList  | [ParameterListSyntax](ParameterListSyntax.md)                   |
| Initializer    | [ConstructorInitializerSyntax](ConstructorInitializerSyntax.md) |
| Body           | [BlockSyntax](BlockSyntax.md)                                   |
| ExpressionBody | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md)   |
| SemicolonToken | SyntaxToken                                                     |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.constructordeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*