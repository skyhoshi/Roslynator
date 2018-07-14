# SyntaxInfo\.LocalDeclarationStatementInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| LocalDeclarationStatementInfo\(ExpressionSyntax, Boolean\) | Creates a new [LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md) from the specified expression\. |
| LocalDeclarationStatementInfo\(LocalDeclarationStatementSyntax, Boolean\) | Creates a new [LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md) from the specified local declaration statement\. |

## LocalDeclarationStatementInfo\(LocalDeclarationStatementSyntax, Boolean\)

### Summary

Creates a new [LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md) from the specified local declaration statement\.

```csharp
public static LocalDeclarationStatementInfo LocalDeclarationStatementInfo(LocalDeclarationStatementSyntax localDeclarationStatement, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| localDeclarationStatement | |
| allowMissing | |

#### Returns

[LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md)


## LocalDeclarationStatementInfo\(ExpressionSyntax, Boolean\)

### Summary

Creates a new [LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md) from the specified expression\.

```csharp
public static LocalDeclarationStatementInfo LocalDeclarationStatementInfo(ExpressionSyntax value, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| value | |
| allowMissing | |

#### Returns

[LocalDeclarationStatementInfo](../../Syntax/LocalDeclarationStatementInfo/README.md)


