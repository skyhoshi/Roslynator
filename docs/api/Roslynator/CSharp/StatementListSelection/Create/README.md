# StatementListSelection\.Create Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Create\(BlockSyntax, TextSpan\) | Creates a new [StatementListSelection](../README.md) based on the specified block and span\. |
| Create\(StatementListInfo, TextSpan\) | Creates a new [StatementListSelection](../README.md) based on the specified [StatementListInfo](../../Syntax/StatementListInfo/README.md) and span\. |
| Create\(SwitchSectionSyntax, TextSpan\) | Creates a new [StatementListSelection](../README.md) based on the specified switch section and span\. |

## Create\(BlockSyntax, TextSpan\)

### Summary

Creates a new [StatementListSelection](../README.md) based on the specified block and span\.

```csharp
public static StatementListSelection Create(BlockSyntax block, TextSpan span)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| block | |
| span | |

#### Returns

[StatementListSelection](../README.md)




## Create\(SwitchSectionSyntax, TextSpan\)

### Summary

Creates a new [StatementListSelection](../README.md) based on the specified switch section and span\.

```csharp
public static StatementListSelection Create(SwitchSectionSyntax switchSection, TextSpan span)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| switchSection | |
| span | |

#### Returns

[StatementListSelection](../README.md)




## Create\(StatementListInfo, TextSpan\)

### Summary

Creates a new [StatementListSelection](../README.md) based on the specified [StatementListInfo](../../Syntax/StatementListInfo/README.md) and span\.

```csharp
public static StatementListSelection Create(in StatementListInfo statementsInfo, TextSpan span)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| statementsInfo | |
| span | |

#### Returns

[StatementListSelection](../README.md)




