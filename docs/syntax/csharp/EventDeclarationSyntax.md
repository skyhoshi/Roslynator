# EventDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BasePropertyDeclarationSyntax](BasePropertyDeclarationSyntax.md)
          * EventDeclarationSyntax

## Syntax Properties

| Name                       | Type                                                                    |
| -------------------------- | ----------------------------------------------------------------------- |
| AttributeLists             | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>              |
| Modifiers                  | SyntaxTokenList                                                         |
| EventKeyword               | SyntaxToken                                                             |
| Type                       | [TypeSyntax](TypeSyntax.md)                                             |
| ExplicitInterfaceSpecifier | [ExplicitInterfaceSpecifierSyntax](ExplicitInterfaceSpecifierSyntax.md) |
| Identifier                 | SyntaxToken                                                             |
| AccessorList               | [AccessorListSyntax](AccessorListSyntax.md)                             |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.eventdeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*