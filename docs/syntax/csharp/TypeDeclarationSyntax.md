# TypeDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BaseTypeDeclarationSyntax](BaseTypeDeclarationSyntax.md)
          * TypeDeclarationSyntax

## Syntax Properties

| Name              | Type                                                                                       |
| ----------------- | ------------------------------------------------------------------------------------------ |
| Keyword           | SyntaxToken                                                                                |
| TypeParameterList | [TypeParameterListSyntax](TypeParameterListSyntax.md)                                      |
| ConstraintClauses | SyntaxList\<[TypeParameterConstraintClauseSyntax](TypeParameterConstraintClauseSyntax.md)> |
| Members           | SyntaxList\<[MemberDeclarationSyntax](MemberDeclarationSyntax.md)>                         |

## Other Properties

| Name  | Type |
| ----- | ---- |
| Arity | int  |

### Directly Derived Types

* [ClassDeclarationSyntax](ClassDeclarationSyntax.md)
* [InterfaceDeclarationSyntax](InterfaceDeclarationSyntax.md)
* [StructDeclarationSyntax](StructDeclarationSyntax.md)

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typedeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*