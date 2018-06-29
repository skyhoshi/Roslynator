# IfDirectiveTriviaSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StructuredTriviaSyntax](StructuredTriviaSyntax.md)
        * [DirectiveTriviaSyntax](DirectiveTriviaSyntax.md)
          * [BranchingDirectiveTriviaSyntax](BranchingDirectiveTriviaSyntax.md)
            * [ConditionalDirectiveTriviaSyntax](ConditionalDirectiveTriviaSyntax.md)
              * IfDirectiveTriviaSyntax

## Syntax Properties

| Name                | Type                                    |
| ------------------- | --------------------------------------- |
| HashToken           | SyntaxToken                             |
| IfKeyword           | SyntaxToken                             |
| Condition           | [ExpressionSyntax](ExpressionSyntax.md) |
| EndOfDirectiveToken | SyntaxToken                             |

## Other Properties

| Name           | Type |
| -------------- | ---- |
| IsActive       | bool |
| BranchTaken    | bool |
| ConditionValue | bool |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifdirectivetriviasyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*