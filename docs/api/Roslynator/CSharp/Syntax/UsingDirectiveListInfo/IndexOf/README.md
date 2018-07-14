# UsingDirectiveListInfo\.IndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IndexOf\(Func\<UsingDirectiveSyntax, Boolean>\) | Searches for an using directive that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| IndexOf\(UsingDirectiveSyntax\) | The index of the using directive in the list\. |

## IndexOf\(Func\<UsingDirectiveSyntax, Boolean>\)

### Summary

Searches for an using directive that matches the predicate and returns returns zero\-based index of the first occurrence in the list\.

```csharp
public int IndexOf(Func<UsingDirectiveSyntax, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


## IndexOf\(UsingDirectiveSyntax\)

### Summary

The index of the using directive in the list\.

```csharp
public int IndexOf(UsingDirectiveSyntax usingDirective)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| usingDirective | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


