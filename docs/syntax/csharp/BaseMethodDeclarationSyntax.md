# BaseMethodDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * BaseMethodDeclarationSyntax

## Syntax Properties

| Name           | Type                                                          |
| -------------- | ------------------------------------------------------------- |
| AttributeLists | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>    |
| Modifiers      | SyntaxTokenList                                               |
| ParameterList  | [ParameterListSyntax](ParameterListSyntax.md)                 |
| Body           | [BlockSyntax](BlockSyntax.md)                                 |
| ExpressionBody | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md) |
| SemicolonToken | SyntaxToken                                                   |

### Directly Derived Types

* [ConstructorDeclarationSyntax](ConstructorDeclarationSyntax.md)
* [ConversionOperatorDeclarationSyntax](ConversionOperatorDeclarationSyntax.md)
* [DestructorDeclarationSyntax](DestructorDeclarationSyntax.md)
* [MethodDeclarationSyntax](MethodDeclarationSyntax.md)
* [OperatorDeclarationSyntax](OperatorDeclarationSyntax.md)

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.basemethoddeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*