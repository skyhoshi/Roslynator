# InterpolationSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [InterpolatedStringContentSyntax](InterpolatedStringContentSyntax.md)
        * InterpolationSyntax

## Syntax Properties

| Name            | Type                                                                        |
| --------------- | --------------------------------------------------------------------------- |
| OpenBraceToken  | SyntaxToken                                                                 |
| Expression      | [ExpressionSyntax](ExpressionSyntax.md)                                     |
| AlignmentClause | [InterpolationAlignmentClauseSyntax](InterpolationAlignmentClauseSyntax.md) |
| FormatClause    | [InterpolationFormatClauseSyntax](InterpolationFormatClauseSyntax.md)       |
| CloseBraceToken | SyntaxToken                                                                 |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.interpolationsyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*