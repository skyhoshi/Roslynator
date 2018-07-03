# PropertyDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BasePropertyDeclarationSyntax](BasePropertyDeclarationSyntax.md)
          * PropertyDeclarationSyntax

## Syntax Properties

| Name                       | Type                                                                    |
| -------------------------- | ----------------------------------------------------------------------- |
| AttributeLists             | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>              |
| Modifiers                  | SyntaxTokenList                                                         |
| Type                       | [TypeSyntax](TypeSyntax.md)                                             |
| ExplicitInterfaceSpecifier | [ExplicitInterfaceSpecifierSyntax](ExplicitInterfaceSpecifierSyntax.md) |
| Identifier                 | SyntaxToken                                                             |
| AccessorList               | [AccessorListSyntax](AccessorListSyntax.md)                             |
| ExpressionBody             | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md)           |
| Initializer                | [EqualsValueClauseSyntax](EqualsValueClauseSyntax.md)                   |
| SemicolonToken             | SyntaxToken                                                             |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.propertydeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*