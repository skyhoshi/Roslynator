# SimpleMemberInvocationExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about invocation expression\.

```csharp
public readonly struct SimpleMemberInvocationExpressionInfo : System.IEquatable<SimpleMemberInvocationExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SimpleMemberInvocationExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SimpleMemberInvocationExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [ArgumentList](ArgumentList/README.md) | The argumet list\. |
| [Arguments](Arguments/README.md) | The list of the arguments\. |
| [Expression](Expression/README.md) | The expression that contains the member being invoked\. |
| [InvocationExpression](InvocationExpression/README.md) | The invocation expression\. |
| [MemberAccessExpression](MemberAccessExpression/README.md) | The member access expression\. |
| [Name](Name/README.md) | The name of the member being invoked\. |
| [NameText](NameText/README.md) | The name of the member being invoked\. |
| [OperatorToken](OperatorToken/README.md) | The operator in the member access expression\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SimpleMemberInvocationExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Equality/README.md) | |
| [Inequality(SimpleMemberInvocationExpressionInfo, SimpleMemberInvocationExpressionInfo)](op_Inequality/README.md) | |

