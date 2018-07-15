# StatementListSelection\.TryCreate Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| TryCreate\(BlockSyntax, TextSpan, StatementListSelection\) | Creates a new [StatementListSelection](../README.md) based on the specified block and span\. |
| TryCreate\(SwitchSectionSyntax, TextSpan, StatementListSelection\) | Creates a new [StatementListSelection](../README.md) based on the specified switch section and span\. |

## TryCreate\(BlockSyntax, TextSpan, StatementListSelection\)

### Summary

Creates a new [StatementListSelection](../README.md) based on the specified block and span\.

```csharp
public static bool TryCreate(BlockSyntax block, TextSpan span, out StatementListSelection selectedStatements)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| block | |
| span | |
| selectedStatements | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one statement; otherwise, false\.

## TryCreate\(SwitchSectionSyntax, TextSpan, StatementListSelection\)

### Summary

Creates a new [StatementListSelection](../README.md) based on the specified switch section and span\.

```csharp
public static bool TryCreate(SwitchSectionSyntax switchSection, TextSpan span, out StatementListSelection selectedStatements)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| switchSection | |
| span | |
| selectedStatements | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one statement; otherwise, false\.