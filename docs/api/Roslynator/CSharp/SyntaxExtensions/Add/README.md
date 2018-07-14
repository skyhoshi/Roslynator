# SyntaxExtensions\.Add\(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Creates a new list with the specified node added or inserted\.

```csharp
public static SyntaxList<StatementSyntax> Add(this SyntaxList<StatementSyntax> statements, StatementSyntax statement, bool ignoreLocalFunctions)
```

### Parameters

| Name | Summary |
| ---- | ------- |
| statements | |
| statement | |
| ignoreLocalFunctions | Insert statement before local function statements at the end of the list\. |

### Returns

[SyntaxList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\<[StatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.statementsyntax)>


