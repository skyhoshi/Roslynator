# ConditionalExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about conditional expression\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ConditionalExpressionInfo

#### Implements

* [IEquatable\<ConditionalExpressionInfo>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)

## Properties

| Property | Summary |
| -------- | ------- |
| [ColonToken](ColonToken/README.md) | The token representing the colon\. |
| [Condition](Condition/README.md) | The condition expression\. |
| [ConditionalExpression](ConditionalExpression/README.md) | The conditional expression\. |
| [QuestionToken](QuestionToken/README.md) | The token representing the question mark\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [WhenFalse](WhenFalse/README.md) | The expression to be executed when the expression is false\. |
| [WhenTrue](WhenTrue/README.md) | The expression to be executed when the expression is true\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(ConditionalExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [operator !=(ConditionalExpressionInfo, ConditionalExpressionInfo)](op_Inequality/README.md) | |
| [operator ==(ConditionalExpressionInfo, ConditionalExpressionInfo)](op_Equality/README.md) | |

