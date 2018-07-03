# ConditionalDirectiveTriviaSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [StructuredTriviaSyntax](StructuredTriviaSyntax.md)
        * [DirectiveTriviaSyntax](DirectiveTriviaSyntax.md)
          * [BranchingDirectiveTriviaSyntax](BranchingDirectiveTriviaSyntax.md)
            * ConditionalDirectiveTriviaSyntax

## Syntax Properties

| Name      | Type                                    |
| --------- | --------------------------------------- |
| Condition | [ExpressionSyntax](ExpressionSyntax.md) |

## Other Properties

| Name           | Type |
| -------------- | ---- |
| ConditionValue | bool |

## Derived Types

### Directly Derived Types

* [ElifDirectiveTriviaSyntax](ElifDirectiveTriviaSyntax.md)
* [IfDirectiveTriviaSyntax](IfDirectiveTriviaSyntax.md)

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.conditionaldirectivetriviasyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*