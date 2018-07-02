# ExpressionChain Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ExpressionChain

#### Implements

* [IEnumerable\<ExpressionSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IEquatable\<ExpressionChain>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)

## Properties

| Property| Summary|
| --- | --- |
| [BinaryExpression](BinaryExpression/README.md) | The binary expression\. |
| [Span](Span/README.md) | The span that represents selected expressions\. |

## Methods

| Method| Summary|
| --- | --- |
| [Equals(ExpressionChain)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator for the expressions\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [Reverse()](Reverse/README.md) | Returns a chain which contains all expressions of  in reversed order\. |
| [ToString()](ToString/README.md) | Returns the string representation of the expressions, not including its leading and trailing trivia\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator !=(ExpressionChain, ExpressionChain)](op_Inequality/README.md) | |
| [operator ==(ExpressionChain, ExpressionChain)](op_Equality/README.md) | |

