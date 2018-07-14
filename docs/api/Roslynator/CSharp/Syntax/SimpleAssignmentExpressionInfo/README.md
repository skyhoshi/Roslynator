# SimpleAssignmentExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about simple assignment expression\.

```csharp
public readonly struct SimpleAssignmentExpressionInfo : System.IEquatable<SimpleAssignmentExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SimpleAssignmentExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SimpleAssignmentExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [AssignmentExpression](AssignmentExpression/README.md) | The simple assignment expression\. |
| [Left](Left/README.md) | The expression on the left of the assignment operator\. |
| [OperatorToken](OperatorToken/README.md) | The operator of the simple assignment expression\. |
| [Right](Right/README.md) | The expression on the right of the assignment operator\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SimpleAssignmentExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SimpleAssignmentExpressionInfo, SimpleAssignmentExpressionInfo)](op_Equality/README.md) | |
| [Inequality(SimpleAssignmentExpressionInfo, SimpleAssignmentExpressionInfo)](op_Inequality/README.md) | |

