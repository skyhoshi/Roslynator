# SymbolExtensions\.ContainsMember Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ContainsMember\<TSymbol>\(ITypeSymbol, Func\<TSymbol, Boolean>\) | Returns true if the type contains member that matches the conditions defined by the specified predicate, if any\. |
| ContainsMember\<TSymbol>\(ITypeSymbol, String, Func\<TSymbol, Boolean>\) | Returns true if the type contains member that has the specified name and matches the conditions defined by the specified predicate, if any\. |

## ContainsMember\<TSymbol>\(ITypeSymbol, Func\<TSymbol, Boolean>\)

### Summary

Returns true if the type contains member that matches the conditions defined by the specified predicate, if any\.

```csharp
public static bool ContainsMember<TSymbol>(this ITypeSymbol typeSymbol, Func<TSymbol, bool> predicate = null) 
    where TSymbol : ISymbol
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TSymbol | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## ContainsMember\<TSymbol>\(ITypeSymbol, String, Func\<TSymbol, Boolean>\)

### Summary

Returns true if the type contains member that has the specified name and matches the conditions defined by the specified predicate, if any\.

```csharp
public static bool ContainsMember<TSymbol>(this ITypeSymbol typeSymbol, string name, Func<TSymbol, bool> predicate = null) 
    where TSymbol : ISymbol
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TSymbol | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| typeSymbol | |
| name | |
| predicate | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




