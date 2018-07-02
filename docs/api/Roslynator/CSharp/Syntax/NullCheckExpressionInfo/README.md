# NullCheckExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about a null check expression\.

#### Inheritance

* Object
  * ValueType
    * NullCheckExpressionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [NullCheckExpression](NullCheckExpression/README.md) | The null check expression, e\.g\. "x == null"\. |
| [Expression](Expression/README.md) | The expression that is evaluated whether is \(not\) null\. for example "x" in "x == null"\. |
| [Style](Style/README.md) | The style of this null check\. |
| [IsCheckingNull](IsCheckingNull/README.md) | Determines whether this null check is checking if the expression is null\. |
| [IsCheckingNotNull](IsCheckingNotNull/README.md) | Determines whether this null check is checking if the expression is not null\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(NullCheckExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(NullCheckExpressionInfo, NullCheckExpressionInfo)](op_Equality/README.md) | |
| [operator !=(NullCheckExpressionInfo, NullCheckExpressionInfo)](op_Inequality/README.md) | |

