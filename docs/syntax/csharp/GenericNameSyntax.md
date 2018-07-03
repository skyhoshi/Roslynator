# GenericNameSyntax

## Inheritance

* Object
  * SyntaxNode
    * CSharpSyntaxNode
      * [ExpressionSyntax](ExpressionSyntax.md)
        * [TypeSyntax](TypeSyntax.md)
          * [NameSyntax](NameSyntax.md)
            * [SimpleNameSyntax](SimpleNameSyntax.md)
              * GenericNameSyntax

## Syntax Properties

| Name             | Type                                                |
| ---------------- | --------------------------------------------------- |
| Identifier       | SyntaxToken                                         |
| TypeArgumentList | [TypeArgumentListSyntax](TypeArgumentListSyntax.md) |

## Other Properties

| Name                 | Type |
| -------------------- | ---- |
| IsUnboundGenericName | bool |

## See Also

* [Official Documentation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.genericnamesyntax)


*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*