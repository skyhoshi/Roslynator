# MethodDeclarationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [MemberDeclarationSyntax](MemberDeclarationSyntax.md)
        * [BaseMethodDeclarationSyntax](BaseMethodDeclarationSyntax.md)
          * MethodDeclarationSyntax

## Syntax Properties

| Name                       | Type                                                                                       |
| -------------------------- | ------------------------------------------------------------------------------------------ |
| AttributeLists             | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>                                 |
| Modifiers                  | SyntaxTokenList                                                                            |
| ReturnType                 | [TypeSyntax](TypeSyntax.md)                                                                |
| ExplicitInterfaceSpecifier | [ExplicitInterfaceSpecifierSyntax](ExplicitInterfaceSpecifierSyntax.md)                    |
| Identifier                 | SyntaxToken                                                                                |
| TypeParameterList          | [TypeParameterListSyntax](TypeParameterListSyntax.md)                                      |
| ParameterList              | [ParameterListSyntax](ParameterListSyntax.md)                                              |
| ConstraintClauses          | SyntaxList\<[TypeParameterConstraintClauseSyntax](TypeParameterConstraintClauseSyntax.md)> |
| Body                       | [BlockSyntax](BlockSyntax.md)                                                              |
| ExpressionBody             | [ArrowExpressionClauseSyntax](ArrowExpressionClauseSyntax.md)                              |
| SemicolonToken             | SyntaxToken                                                                                |

## Other Properties

| Name  | Type |
| ----- | ---- |
| Arity | int  |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.methoddeclarationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*