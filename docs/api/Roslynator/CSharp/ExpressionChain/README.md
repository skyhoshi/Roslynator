# ExpressionChain Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression\.

#### Inheritance

Object &#x2192; ValueType &#x2192; ExpressionChain

#### Implements

* IEnumerable\<ExpressionSyntax>
* IEquatable\<ExpressionChain>

## Properties

| Property| Summary|
| --- | --- |
| [BinaryExpression](BinaryExpression/README.md) | The binary expression\. |
| [Span](Span/README.md) | The span that represents selected expressions\. |

## Methods

| Method| Summary|
| --- | --- |
| [Reverse()](Reverse/README.md) | Returns a chain which contains all expressions of  in reversed order\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator for the expressions\. |
| [ToString()](ToString/README.md) | Returns the string representation of the expressions, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(ExpressionChain)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(ExpressionChain, ExpressionChain)](op_Equality/README.md) | |
| [operator !=(ExpressionChain, ExpressionChain)](op_Inequality/README.md) | |

