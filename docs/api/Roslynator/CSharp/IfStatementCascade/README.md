# IfStatementCascade Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Enables to enumerate if statement cascade\.

```csharp
readonly struct IfStatementCascade
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IfStatementCascade

### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[IfStatementOrElseClause](../IfStatementOrElseClause/README.md)>
* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[IfStatementCascade](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [IfStatement](IfStatement/README.md) | The if statement\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(IfStatementCascade)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator for the if\-else cascade\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [operator !=(IfStatementCascade, IfStatementCascade)](op_Inequality/README.md) | |
| [operator ==(IfStatementCascade, IfStatementCascade)](op_Equality/README.md) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<IfStatementOrElseClause>.GetEnumerator()](System-Collections-Generic-IEnumerable-Roslynator-CSharp-IfStatementOrElseClause--GetEnumerator/README.md) | |

