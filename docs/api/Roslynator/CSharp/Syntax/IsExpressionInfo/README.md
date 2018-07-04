# IsExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about "is" expression\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IsExpressionInfo

#### Implements

* [IEquatable\<IsExpressionInfo>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)

## Properties

| Property | Summary |
| -------- | ------- |
| [Expression](Expression/README.md) | The expression that is being casted\. |
| [IsExpression](IsExpression/README.md) | The "is" expression\. |
| [OperatorToken](OperatorToken/README.md) | The "is" operator token\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Type](Type/README.md) | The type to which the expression is being cast\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(IsExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [operator !=(IsExpressionInfo, IsExpressionInfo)](op_Inequality/README.md) | |
| [operator ==(IsExpressionInfo, IsExpressionInfo)](op_Equality/README.md) | |

