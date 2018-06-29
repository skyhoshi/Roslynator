# ClassDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BaseTypeDeclarationSyntax](BaseTypeDeclarationSyntax.md)
          * [TypeDeclarationSyntax](TypeDeclarationSyntax.md)
            * ClassDeclarationSyntax

## Syntax Properties

| Name              | Type                                                                                       |
| ----------------- | ------------------------------------------------------------------------------------------ |
| AttributeLists    | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>                                 |
| Modifiers         | SyntaxTokenList                                                                            |
| Keyword           | SyntaxToken                                                                                |
| Identifier        | SyntaxToken                                                                                |
| TypeParameterList | [TypeParameterListSyntax](TypeParameterListSyntax.md)                                      |
| BaseList          | [BaseListSyntax](BaseListSyntax.md)                                                        |
| ConstraintClauses | SyntaxList\<[TypeParameterConstraintClauseSyntax](TypeParameterConstraintClauseSyntax.md)> |
| OpenBraceToken    | SyntaxToken                                                                                |
| Members           | SyntaxList\<[MemberDeclarationSyntax](MemberDeclarationSyntax.md)>                         |
| CloseBraceToken   | SyntaxToken                                                                                |
| SemicolonToken    | SyntaxToken                                                                                |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.classdeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*