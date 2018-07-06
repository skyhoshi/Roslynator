# NameGenerator\.IsUniqueName Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsUniqueName\(String, IEnumerable\<String>, Boolean\) | Returns true if the name is not contained in the specified list\. |
| IsUniqueName\(String, ImmutableArray\<ISymbol>, Boolean\) | Returns true if the name is not contained in the specified list\. [Name](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol.name) is used to compare names\. |

## IsUniqueName\(String, ImmutableArray\<ISymbol>, Boolean\)

### Summary

Returns true if the name is not contained in the specified list\. [Name](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol.name) is used to compare names\.

```csharp
public static bool IsUniqueName(string name, ImmutableArray<ISymbol> symbols, bool isCaseSensitive = true)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| name | |
| symbols | |
| isCaseSensitive | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## IsUniqueName\(String, IEnumerable\<String>, Boolean\)

### Summary

Returns true if the name is not contained in the specified list\.

```csharp
public static bool IsUniqueName(string name, IEnumerable<string> reservedNames, bool isCaseSensitive = true)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| name | |
| reservedNames | |
| isCaseSensitive | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




