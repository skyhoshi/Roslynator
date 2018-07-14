# BinaryExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about binary expression\.

```csharp
public readonly struct BinaryExpressionInfo : System.IEquatable<BinaryExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; BinaryExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[BinaryExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [BinaryExpression](BinaryExpression/README.md) | The binary expression\. |
| [Kind](Kind/README.md) | The kind of the binary expression\. |
| [Left](Left/README.md) | The expression on the left of the binary operator\. |
| [Right](Right/README.md) | The expression on the right of the binary operator\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [AsChain()](AsChain/README.md) | |
| [Equals(BinaryExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Expressions(Boolean)](Expressions/README.md) | Returns expressions of this binary expression, including expressions of nested binary expressions of the same kind as parent binary expression\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(BinaryExpressionInfo, BinaryExpressionInfo)](op_Equality/README.md) | |
| [Inequality(BinaryExpressionInfo, BinaryExpressionInfo)](op_Inequality/README.md) | |

