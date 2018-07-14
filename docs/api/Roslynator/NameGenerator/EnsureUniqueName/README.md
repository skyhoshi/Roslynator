# NameGenerator\.EnsureUniqueName Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| EnsureUniqueName\(String, IEnumerable\<String>, Boolean\) | Returns an unique name using the specified list of reserved names\. |
| EnsureUniqueName\(String, ImmutableArray\<ISymbol>, Boolean\) | Returns an unique name using the specified list of symbols\. |

## EnsureUniqueName\(String, IEnumerable\<String>, Boolean\)

### Summary

Returns an unique name using the specified list of reserved names\.

```csharp
public abstract string EnsureUniqueName(string baseName, IEnumerable<string> reservedNames, bool isCaseSensitive = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| baseName | |
| reservedNames | |
| isCaseSensitive | |

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)


## EnsureUniqueName\(String, ImmutableArray\<ISymbol>, Boolean\)

### Summary

Returns an unique name using the specified list of symbols\.

```csharp
public abstract string EnsureUniqueName(string baseName, ImmutableArray<ISymbol> symbols, bool isCaseSensitive = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| baseName | |
| symbols | |
| isCaseSensitive | |

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)


