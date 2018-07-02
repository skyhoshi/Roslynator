# IsExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about "is" expression\.

#### Inheritance

Object &#x2192; ValueType &#x2192; IsExpressionInfo

#### Implements

* IEquatable\<IsExpressionInfo>

## Properties

| Property| Summary|
| --- | --- |
| [IsExpression](IsExpression/README.md) | The "is" expression\. |
| [Expression](Expression/README.md) | The expression that is being casted\. |
| [Type](Type/README.md) | The type to which the expression is being cast\. |
| [OperatorToken](OperatorToken/README.md) | The "is" operator token\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(IsExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(IsExpressionInfo, IsExpressionInfo)](op_Equality/README.md) | |
| [operator !=(IsExpressionInfo, IsExpressionInfo)](op_Inequality/README.md) | |

