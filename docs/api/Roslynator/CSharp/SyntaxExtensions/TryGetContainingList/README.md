# SyntaxExtensions\.TryGetContainingList\(StatementSyntax, SyntaxList\<StatementSyntax>\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Gets a list the specified statement is contained in\.
This method succeeds if the statement is in a block's statements or a switch section's statements\.

```csharp
public static bool TryGetContainingList(this StatementSyntax statement, out SyntaxList<StatementSyntax> statements)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| statement | |
| statements | |

### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the statement is contained in the list; otherwise, false\.


