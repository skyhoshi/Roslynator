# ConditionalExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about conditional expression\.

#### Inheritance

Object &#x2192; ValueType &#x2192; ConditionalExpressionInfo

#### Implements

* IEquatable\<ConditionalExpressionInfo>

## Properties

| Property| Summary|
| --- | --- |
| [ConditionalExpression](ConditionalExpression/README.md) | The conditional expression\. |
| [Condition](Condition/README.md) | The condition expression\. |
| [WhenTrue](WhenTrue/README.md) | The expression to be executed when the expression is true\. |
| [WhenFalse](WhenFalse/README.md) | The expression to be executed when the expression is false\. |
| [QuestionToken](QuestionToken/README.md) | The token representing the question mark\. |
| [ColonToken](ColonToken/README.md) | The token representing the colon\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(ConditionalExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(ConditionalExpressionInfo, ConditionalExpressionInfo)](op_Equality/README.md) | |
| [operator !=(ConditionalExpressionInfo, ConditionalExpressionInfo)](op_Inequality/README.md) | |

