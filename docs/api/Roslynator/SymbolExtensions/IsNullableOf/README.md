# SymbolExtensions\.IsNullableOf Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsNullableOf\(INamedTypeSymbol, ITypeSymbol\) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| IsNullableOf\(INamedTypeSymbol, SpecialType\) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| IsNullableOf\(ITypeSymbol, ITypeSymbol\) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| IsNullableOf\(ITypeSymbol, SpecialType\) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |

## IsNullableOf\(INamedTypeSymbol, SpecialType\)

### Summary

Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\.

```csharp
public static bool IsNullableOf(this INamedTypeSymbol namedTypeSymbol, SpecialType specialType)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| namedTypeSymbol | |
| specialType | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## IsNullableOf\(INamedTypeSymbol, ITypeSymbol\)

### Summary

Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\.

```csharp
public static bool IsNullableOf(this INamedTypeSymbol namedTypeSymbol, ITypeSymbol typeArgument)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| namedTypeSymbol | |
| typeArgument | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## IsNullableOf\(ITypeSymbol, SpecialType\)

### Summary

Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\.

```csharp
public static bool IsNullableOf(this ITypeSymbol typeSymbol, SpecialType specialType)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| specialType | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## IsNullableOf\(ITypeSymbol, ITypeSymbol\)

### Summary

Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\.

```csharp
public static bool IsNullableOf(this ITypeSymbol typeSymbol, ITypeSymbol typeArgument)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| typeArgument | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




