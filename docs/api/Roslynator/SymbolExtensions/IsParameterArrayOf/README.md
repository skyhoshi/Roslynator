# SymbolExtensions\.IsParameterArrayOf Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsParameterArrayOf\(IParameterSymbol, SpecialType\) | Returns true if the parameter was declared as a parameter array that has a specified element type\. |
| IsParameterArrayOf\(IParameterSymbol, SpecialType, SpecialType\) | Returns true if the parameter was declared as a parameter array that has one of specified element types\. |
| IsParameterArrayOf\(IParameterSymbol, SpecialType, SpecialType, SpecialType\) | Returns true if the parameter was declared as a parameter array that has one of specified element types\. |

## IsParameterArrayOf\(IParameterSymbol, SpecialType\)

### Summary

Returns true if the parameter was declared as a parameter array that has a specified element type\.

```csharp
public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| parameterSymbol | |
| elementType | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## IsParameterArrayOf\(IParameterSymbol, SpecialType, SpecialType\)

### Summary

Returns true if the parameter was declared as a parameter array that has one of specified element types\.

```csharp
public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType1, SpecialType elementType2)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| parameterSymbol | |
| elementType1 | |
| elementType2 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## IsParameterArrayOf\(IParameterSymbol, SpecialType, SpecialType, SpecialType\)

### Summary

Returns true if the parameter was declared as a parameter array that has one of specified element types\.

```csharp
public static bool IsParameterArrayOf(this IParameterSymbol parameterSymbol, SpecialType elementType1, SpecialType elementType2, SpecialType elementType3)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| parameterSymbol | |
| elementType1 | |
| elementType2 | |
| elementType3 | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


