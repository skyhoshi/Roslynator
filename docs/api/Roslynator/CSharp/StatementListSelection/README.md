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
| [Create(BlockSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified block and span\. |
| [Create(StatementListInfo, TextSpan)](Create/README.md) | Creates a new  based on the specified  and span\. |
| [Create(SwitchSectionSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified switch section and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) | |
| [First()](../../Selection-1/First/README.md) | |
| [GetEnumerator()](../../SyntaxListSelection-1/GetEnumerator/README.md) | |
| [GetEnumeratorCore()](../../SyntaxListSelection-1/GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [Last()](../../Selection-1/Last/README.md) | |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) | |
| [TryCreate(BlockSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new  based on the specified block and span\. |
| [TryCreate(SwitchSectionSyntax, TextSpan, StatementListSelection)](TryCreate/README.md) | Creates a new  based on the specified switch section and span\. |

