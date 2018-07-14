# ExtensionMethodSymbolInfo Struct

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents an extension method symbol\.

```csharp
public readonly struct ExtensionMethodSymbolInfo : System.IEquatable<ExtensionMethodSymbolInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ExtensionMethodSymbolInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[ExtensionMethodSymbolInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [IsReduced](IsReduced/README.md) | True if the symbol was reduced\. |
| [ReducedSymbol](ReducedSymbol/README.md) | The definition of extension method from which this symbol was reduced, or null, if the symbol was not reduced\. |
| [ReducedSymbolOrSymbol](ReducedSymbolOrSymbol/README.md) | The reduced symbol or the symbol if the reduced symbol is null\. |
| [Symbol](Symbol/README.md) | The extension method symbol\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(ExtensionMethodSymbolInfo)](Equals/README.md) | |
| [Equals(Object)](Equals/README.md) | |
| [GetHashCode()](GetHashCode/README.md) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype.tostring) |  \(Inherited from [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype)\) |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(ExtensionMethodSymbolInfo, ExtensionMethodSymbolInfo)](op_Equality/README.md) | |
| [Inequality(ExtensionMethodSymbolInfo, ExtensionMethodSymbolInfo)](op_Inequality/README.md) | |

