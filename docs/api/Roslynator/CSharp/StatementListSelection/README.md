# StatementListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected statements in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md) &#x2192; StatementListSelection

#### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>
* [IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>
* [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(BlockSyntax, TextSpan)](Create/README.md) | Creates a new [StatementListSelection](./README.md) based on the specified block and span\. |
| [Create(StatementListInfo, TextSpan)](Create/README.md) | Creates a new [StatementListSelection](./README.md) based on the specified [StatementListInfo](../Syntax/StatementListInfo/README.md) and span\. |
| [Create(SwitchSectionSyntax, TextSpan)](Create/README.md) | Creates a new [StatementListSelection](./README.md) based on the specified switch section and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](../../Selection-1/First/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [GetEnumerator()](../../SyntaxListSelection-1/GetEnumerator/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetEnumeratorCore()](../../SyntaxListSelection-1/GetEnumeratorCore/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](../../Selection-1/Last/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(BlockSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new [StatementListSelection](./README.md) based on the specified block and span\. |
| [TryCreate(SwitchSectionSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new [StatementListSelection](./README.md) based on the specified switch section and span\. |

