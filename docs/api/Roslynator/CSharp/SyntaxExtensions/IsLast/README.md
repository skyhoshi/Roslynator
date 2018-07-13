# SyntaxExtensions\.IsLast\(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Returns true if the specified statement is a last statement in the list\.

```csharp
public static bool IsLast(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| statements | |
| statement | |
| ignoreLocalFunctions | Ignore local function statements at the end of the list\. |

### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


