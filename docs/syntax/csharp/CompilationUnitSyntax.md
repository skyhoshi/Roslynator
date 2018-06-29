# CompilationUnitSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * CompilationUnitSyntax

## Syntax Properties

| Name           | Type                                                                     |
| -------------- | ------------------------------------------------------------------------ |
| Externs        | SyntaxList\<[ExternAliasDirectiveSyntax](ExternAliasDirectiveSyntax.md)> |
| Usings         | SyntaxList\<[UsingDirectiveSyntax](UsingDirectiveSyntax.md)>             |
| AttributeLists | SyntaxList\<[AttributeListSyntax](AttributeListSyntax.md)>               |
| Members        | SyntaxList\<[MemberDeclarationSyntax](MemberDeclarationSyntax.md)>       |
| EndOfFileToken | SyntaxToken                                                              |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*