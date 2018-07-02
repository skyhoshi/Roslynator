# BinaryExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about binary expression\.

#### Inheritance

* Object
  * ValueType
    * BinaryExpressionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [BinaryExpression](BinaryExpression/README.md) | The binary expression\. |
| [Left](Left/README.md) | The expression on the left of the binary operator\. |
| [Right](Right/README.md) | The expression on the right of the binary operator\. |
| [Kind](Kind/README.md) | The kind of the binary expression\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [Expressions(Boolean)](Expressions/README.md) | Returns expressions of this binary expression, including expressions of nested binary expressions of the same kind\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(BinaryExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(BinaryExpressionInfo, BinaryExpressionInfo)](op_Equality/README.md) | |
| [operator !=(BinaryExpressionInfo, BinaryExpressionInfo)](op_Inequality/README.md) | |

