# SymbolExtensions\.ImplementsAny Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ImplementsAny\(ITypeSymbol, SpecialType, SpecialType, Boolean\) | Returns true if the type implements any of specified interfaces\. |
| ImplementsAny\(ITypeSymbol, SpecialType, SpecialType, SpecialType, Boolean\) | Returns true if the type implements any of specified interfaces\. |

## ImplementsAny\(ITypeSymbol, SpecialType, SpecialType, Boolean\)

### Summary

Returns true if the type implements any of specified interfaces\.

```csharp
public static bool ImplementsAny(this ITypeSymbol typeSymbol, SpecialType interfaceType1, SpecialType interfaceType2, bool allInterfaces = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| interfaceType1 | |
| interfaceType2 | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## ImplementsAny\(ITypeSymbol, SpecialType, SpecialType, SpecialType, Boolean\)

### Summary

Returns true if the type implements any of specified interfaces\.

```csharp
public static bool ImplementsAny(this ITypeSymbol typeSymbol, SpecialType interfaceType1, SpecialType interfaceType2, SpecialType interfaceType3, bool allInterfaces = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| interfaceType1 | |
| interfaceType2 | |
| interfaceType3 | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




