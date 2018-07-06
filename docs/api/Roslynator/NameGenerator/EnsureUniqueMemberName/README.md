# NameGenerator\.EnsureUniqueMemberName Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| EnsureUniqueMemberName\(String, INamedTypeSymbol, Boolean\) | |
| EnsureUniqueMemberName\(String, SemanticModel, Int32, Boolean, CancellationToken\) | Returns a member name that will be unique at the specified position\. |

## EnsureUniqueMemberName\(String, SemanticModel, Int32, Boolean, CancellationToken\)

### Summary

Returns a member name that will be unique at the specified position\.

```csharp
public string EnsureUniqueMemberName(string baseName, SemanticModel semanticModel, int position, bool isCaseSensitive = true, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| baseName | |
| semanticModel | |
| position | |
| isCaseSensitive | |
| cancellationToken | |

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)




## EnsureUniqueMemberName\(String, INamedTypeSymbol, Boolean\)

```csharp
public string EnsureUniqueMemberName(string baseName, INamedTypeSymbol typeSymbol, bool isCaseSensitive = true)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| baseName | |
| typeSymbol | |
| isCaseSensitive | |

#### Returns

[String](https://docs.microsoft.com/en-us/dotnet/api/system.string)


