# BaseTypeDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * BaseTypeDeclarationSyntax

## Syntax Properties

| Name            | Type                                                       |
| --------------- | ---------------------------------------------------------- |
| AttributeLists  | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)> |
| Modifiers       | SyntaxTokenList                                            |
| Identifier      | SyntaxToken                                                |
| BaseList        | [BaseListSyntax](BaseListSyntax.md)                        |
| OpenBraceToken  | SyntaxToken                                                |
| CloseBraceToken | SyntaxToken                                                |
| SemicolonToken  | SyntaxToken                                                |

### Directly Derived Types

* [EnumDeclarationSyntax](EnumDeclarationSyntax.md)

### Indirectly Derived Types

* [ClassDeclarationSyntax](ClassDeclarationSyntax.md)
* [InterfaceDeclarationSyntax](InterfaceDeclarationSyntax.md)
* [StructDeclarationSyntax](StructDeclarationSyntax.md)

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.basetypedeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*