# SyntaxExtensions\.IsSimpleIf\(IfStatementSyntax\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Returns true if the specified if statement is a simple if statement\.
Simple if statement is defined as follows: it is not a child of an else clause and it has no else clause\.

```csharp
public static bool IsSimpleIf(this IfStatementSyntax ifStatement)
```

### Parameters

| Name | Summary |
| ---- | ------- |
| ifStatement | |

### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


