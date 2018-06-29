# ElseDirectiveTriviaSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StructuredTriviaSyntax](StructuredTriviaSyntax.md)
        * [DirectiveTriviaSyntax](DirectiveTriviaSyntax.md)
          * [BranchingDirectiveTriviaSyntax](BranchingDirectiveTriviaSyntax.md)
            * ElseDirectiveTriviaSyntax

## Syntax Properties

| Name                | Type        |
| ------------------- | ----------- |
| HashToken           | SyntaxToken |
| ElseKeyword         | SyntaxToken |
| EndOfDirectiveToken | SyntaxToken |

## Other Properties

| Name        | Type |
| ----------- | ---- |
| IsActive    | bool |
| BranchTaken | bool |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elsedirectivetriviasyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*