# SingleParameterLambdaExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about a lambda expression with a single parameter\.

#### Inheritance

* Object
  * ValueType
    * SingleParameterLambdaExpressionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [LambdaExpression](LambdaExpression/README.md) | The lambda expression\. |
| [Parameter](Parameter/README.md) | The parameter\. |
| [Body](Body/README.md) | The body of the lambda expression\. |
| [ParameterList](ParameterList/README.md) | The parameter list that contains the parameter\. |
| [IsSimpleLambda](IsSimpleLambda/README.md) | True if this instance is a simple lambda expression\. |
| [IsParenthesizedLambda](IsParenthesizedLambda/README.md) | True if this instance is a parenthesized lambda expression\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SingleParameterLambdaExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(SingleParameterLambdaExpressionInfo, SingleParameterLambdaExpressionInfo)](op_Equality/README.md) | |
| [operator !=(SingleParameterLambdaExpressionInfo, SingleParameterLambdaExpressionInfo)](op_Inequality/README.md) | |

