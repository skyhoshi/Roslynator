# ExpressionChain\.Reversed Struct

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Enables to enumerate expressions of [ExpressionChain](../README.md) in a reversed order\.

```csharp
public readonly struct Reversed : IEnumerable<ExpressionSyntax>, IEquatable<ExpressionChain.Reversed>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ExpressionChain\.Reversed

### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax)>
* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[ExpressionChain](../README.md)\.[Reversed](./README.md)>

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [Reversed(ExpressionChain)](-ctor/README.md) | |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | |
| [Equals(Reversed)](Equals/README.md) | |
| [GetEnumerator()](GetEnumerator/README.md) | |
| [GetHashCode()](GetHashCode/README.md) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(Reversed, Reversed)](op_Equality/README.md) | |
| [Inequality(Reversed, Reversed)](op_Inequality/README.md) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<ExpressionSyntax>.GetEnumerator()](System-Collections-Generic-IEnumerable-Microsoft-CodeAnalysis-CSharp-Syntax-ExpressionSyntax--GetEnumerator/README.md) | |

