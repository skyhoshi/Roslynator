# SyntaxExtensions\.IsEmbedded\(StatementSyntax, Boolean, Boolean, Boolean\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Returns true if the specified statement is an embedded statement\.

```csharp
public static bool IsEmbedded(this StatementSyntax statement, bool canBeBlock = false, bool canBeIfInsideElse = true, bool canBeUsingInsideUsing = true)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| statement | |
| canBeBlock | Block can be considered as embedded statement |
| canBeIfInsideElse | If statement that is a child of an else statement can be considered as an embedded statement\. |
| canBeUsingInsideUsing | Using statement that is a child of an using statement can be considered as en embedded statement\. |

### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


