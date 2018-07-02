# StatementListInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about a list of statements\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; StatementListInfo

#### Implements

* [IEnumerable\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IEquatable\<StatementListInfo>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)
* [IReadOnlyCollection\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<StatementSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Properties

| Property| Summary|
| --- | --- |
| [Count](Count/README.md) | The number of statement in the list\. |
| [IsParentBlock](IsParentBlock/README.md) | Determines whether the statements are contained in a \. |
| [IsParentSwitchSection](IsParentSwitchSection/README.md) | Determines whether the statements are contained in a \. |
| [Parent](Parent/README.md) | The node that contains the statements\. It can be either a  or a \. |
| [ParentAsBlock](ParentAsBlock/README.md) | Gets a block that contains the statements\. Returns null if the statements are not contained in a block\. |
| [ParentAsSwitchSection](ParentAsSwitchSection/README.md) | Gets a switch section that contains the statements\. Returns null if the statements are not contained in a switch section\. |
| [Statements](Statements/README.md) | The list of statements\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [this\[Int32\]](this[]/README.md) | Gets the statement at the specified index in the list\. |

## Methods

| Method| Summary|
| --- | --- |
| [Add(StatementSyntax)](Add/README.md) | Creates a new  with the specified statement added at the end\. |
| [AddRange(IEnumerable\<StatementSyntax>)](AddRange/README.md) | Creates a new  with the specified statements added at the end\. |
| [Any()](Any/README.md) | True if the list has at least one statement\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(StatementListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [First()](First/README.md) | The first statement in the list\. |
| [FirstOrDefault()](FirstOrDefault/README.md) | The first statement in the list or null if the list is empty\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator the list of statements\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [IndexOf(Func\<StatementSyntax, Boolean>)](IndexOf/README.md) | Searches for a statement that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| [IndexOf(StatementSyntax)](IndexOf/README.md) | The index of the statement in the list\. |
| [Insert(Int32, StatementSyntax)](Insert/README.md) | Creates a new  with the specified statement inserted at the index\. |
| [InsertRange(Int32, IEnumerable\<StatementSyntax>)](InsertRange/README.md) | Creates a new  with the specified statements inserted at the index\. |
| [Last()](Last/README.md) | The last statement in the list\. |
| [LastIndexOf(Func\<StatementSyntax, Boolean>)](LastIndexOf/README.md) | Searches for a statement that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| [LastIndexOf(StatementSyntax)](LastIndexOf/README.md) | Searches for a statement and returns zero\-based index of the last occurrence in the list\. |
| [LastOrDefault()](LastOrDefault/README.md) | The last statement in the list or null if the list is empty\. |
| [Remove(StatementSyntax)](Remove/README.md) | Creates a new  with the specified statement removed\. |
| [RemoveAt(Int32)](RemoveAt/README.md) | Creates a new  with the statement at the specified index removed\. |
| [RemoveNode(SyntaxNode, SyntaxRemoveOptions)](RemoveNode/README.md) | Creates a new  with the specified node removed\. |
| [Replace(StatementSyntax, StatementSyntax)](Replace/README.md) | Creates a new  with the specified statement replaced with the new statement\. |
| [ReplaceAt(Int32, StatementSyntax)](ReplaceAt/README.md) | Creates a new  with the statement at the specified index replaced with a new statement\. |
| [ReplaceNode(SyntaxNode, SyntaxNode)](ReplaceNode/README.md) | Creates a new  with the specified old node replaced with a new node\. |
| [ReplaceRange(StatementSyntax, IEnumerable\<StatementSyntax>)](ReplaceRange/README.md) | Creates a new  with the specified statement replaced with new statements\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [WithStatements(IEnumerable\<StatementSyntax>)](WithStatements/README.md) | Creates a new  with the statements updated\. |
| [WithStatements(SyntaxList\<StatementSyntax>)](WithStatements/README.md) | Creates a new  with the statements updated\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator !=(StatementListInfo, StatementListInfo)](op_Inequality/README.md) | |
| [operator ==(StatementListInfo, StatementListInfo)](op_Equality/README.md) | |

