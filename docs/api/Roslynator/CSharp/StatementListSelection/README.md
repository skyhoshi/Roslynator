# StatementListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected statements in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md) &#x2192; StatementListSelection

#### Implements

* [IEnumerable\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(BlockSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified block and span\. |
| [Create(StatementListInfo, TextSpan)](Create/README.md) | Creates a new  based on the specified  and span\. |
| [Create(SwitchSectionSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified switch section and span\. |
| [TryCreate(BlockSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new  based on the specified block and span\. |
| [TryCreate(SwitchSectionSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new  based on the specified switch section and span\. |

