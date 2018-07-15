# StatementListInfo\.IndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IndexOf\(Func\<StatementSyntax, Boolean>\) | Searches for a statement that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| IndexOf\(StatementSyntax\) | The index of the statement in the list\. |

## IndexOf\(Func\<StatementSyntax, Boolean>\)

### Summary

Searches for a statement that matches the predicate and returns returns zero\-based index of the first occurrence in the list\.

```csharp
public int IndexOf(Func<StatementSyntax, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

## IndexOf\(StatementSyntax\)

### Summary

The index of the statement in the list\.

```csharp
public int IndexOf(StatementSyntax statement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| statement | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

