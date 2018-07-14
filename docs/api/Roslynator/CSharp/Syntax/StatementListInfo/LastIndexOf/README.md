# StatementListInfo\.LastIndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| LastIndexOf\(Func\<StatementSyntax, Boolean>\) | Searches for a statement that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| LastIndexOf\(StatementSyntax\) | Searches for a statement and returns zero\-based index of the last occurrence in the list\. |

## LastIndexOf\(Func\<StatementSyntax, Boolean>\)

### Summary

Searches for a statement that matches the predicate and returns returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(Func<StatementSyntax, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


## LastIndexOf\(StatementSyntax\)

### Summary

Searches for a statement and returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(StatementSyntax statement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| statement | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


