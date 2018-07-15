# UsingDirectiveListInfo\.LastIndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| LastIndexOf\(Func\<UsingDirectiveSyntax, Boolean>\) | Searches for an using directive that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| LastIndexOf\(UsingDirectiveSyntax\) | Searches for an using directive and returns zero\-based index of the last occurrence in the list\. |

## LastIndexOf\(Func\<UsingDirectiveSyntax, Boolean>\)

### Summary

Searches for an using directive that matches the predicate and returns returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(Func<UsingDirectiveSyntax, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

## LastIndexOf\(UsingDirectiveSyntax\)

### Summary

Searches for an using directive and returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(UsingDirectiveSyntax usingDirective)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| usingDirective | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

