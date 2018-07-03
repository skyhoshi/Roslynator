# PragmaWarningDirectiveTriviaSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StructuredTriviaSyntax](StructuredTriviaSyntax.md)
        * [DirectiveTriviaSyntax](DirectiveTriviaSyntax.md)
          * PragmaWarningDirectiveTriviaSyntax

## Syntax Properties

| Name                    | Type                                                 |
| ----------------------- | ---------------------------------------------------- |
| HashToken               | SyntaxToken                                          |
| PragmaKeyword           | SyntaxToken                                          |
| WarningKeyword          | SyntaxToken                                          |
| DisableOrRestoreKeyword | SyntaxToken                                          |
| ErrorCodes              | SyntaxList\<[ExpressionSyntax](ExpressionSyntax.md)> |
| EndOfDirectiveToken     | SyntaxToken                                          |

## Other Properties

| Name     | Type |
| -------- | ---- |
| IsActive | bool |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.pragmawarningdirectivetriviasyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*