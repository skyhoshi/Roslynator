# DelegateDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * DelegateDeclarationSyntax

## Syntax Properties

| Name              | Type                                                                                       |
| ----------------- | ------------------------------------------------------------------------------------------ |
| AttributeLists    | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>                                 |
| Modifiers         | SyntaxTokenList                                                                            |
| DelegateKeyword   | SyntaxToken                                                                                |
| ReturnType        | [TypeSyntax](TypeSyntax.md)                                                                |
| Identifier        | SyntaxToken                                                                                |
| TypeParameterList | [TypeParameterListSyntax](TypeParameterListSyntax.md)                                      |
| ParameterList     | [ParameterListSyntax](ParameterListSyntax.md)                                              |
| ConstraintClauses | SyntaxList\<[TypeParameterConstraintClauseSyntax](TypeParameterConstraintClauseSyntax.md)> |
| SemicolonToken    | SyntaxToken                                                                                |

## Other Properties

| Name  | Type |
| ----- | ---- |
| Arity | int  |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.delegatedeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*