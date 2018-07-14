# SingleParameterLambdaExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about a lambda expression with a single parameter\.

```csharp
public readonly struct SingleParameterLambdaExpressionInfo : System.IEquatable<SingleParameterLambdaExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SingleParameterLambdaExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SingleParameterLambdaExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Body](Body/README.md) | The body of the lambda expression\. |
| [IsParenthesizedLambda](IsParenthesizedLambda/README.md) | True if this instance is a parenthesized lambda expression\. |
| [IsSimpleLambda](IsSimpleLambda/README.md) | True if this instance is a simple lambda expression\. |
| [LambdaExpression](LambdaExpression/README.md) | The lambda expression\. |
| [Parameter](Parameter/README.md) | The parameter\. |
| [ParameterList](ParameterList/README.md) | The parameter list that contains the parameter\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SingleParameterLambdaExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SingleParameterLambdaExpressionInfo, SingleParameterLambdaExpressionInfo)](op_Equality/README.md) | |
| [Inequality(SingleParameterLambdaExpressionInfo, SingleParameterLambdaExpressionInfo)](op_Inequality/README.md) | |

