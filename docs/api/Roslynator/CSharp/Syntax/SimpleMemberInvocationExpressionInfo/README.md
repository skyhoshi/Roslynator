# SimpleMemberInvocationExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about invocation expression\.

#### Inheritance

Object &#x2192; ValueType &#x2192; SimpleMemberInvocationExpressionInfo

#### Implements

* IEquatable\<SimpleMemberInvocationExpressionInfo>

## Properties

| Property| Summary|
| --- | --- |
| [InvocationExpression](InvocationExpression/README.md) | The invocation expression\. |
| [MemberAccessExpression](MemberAccessExpression/README.md) | The member access expression\. |
| [Expression](Expression/README.md) | The expression that contains the member being invoked\. |
| [Name](Name/README.md) | The name of the member being invoked\. |
| [ArgumentList](ArgumentList/README.md) | The argumet list\. |
| [Arguments](Arguments/README.md) | The list of the arguments\. |
| [OperatorToken](OperatorToken/README.md) | The operator in the member access expression\. |
| [NameText](NameText/README.md) | The name of the member being invoked\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SimpleMemberInvocationExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Equality/README.md) | |
| [operator !=(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Inequality/README.md) | |

