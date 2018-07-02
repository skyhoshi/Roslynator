# AsExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about "as" expression\.

#### Inheritance

* Object
  * ValueType
    * AsExpressionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [AsExpression](AsExpression/README.md) | The "as" expression\. |
| [Expression](Expression/README.md) | The expression that is being casted\. |
| [Type](Type/README.md) | The type to which the expression is being cast\. |
| [OperatorToken](OperatorToken/README.md) | The "as" operator token\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(AsExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(AsExpressionInfo, AsExpressionInfo)](op_Equality/README.md) | |
| [operator !=(AsExpressionInfo, AsExpressionInfo)](op_Inequality/README.md) | |

