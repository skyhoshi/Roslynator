# SyntaxInfo\.SingleLocalDeclarationStatementInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SingleLocalDeclarationStatementInfo\(ExpressionSyntax\) | Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified value\. |
| SingleLocalDeclarationStatementInfo\(LocalDeclarationStatementSyntax, Boolean\) | Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified local declaration statement\. |
| SingleLocalDeclarationStatementInfo\(VariableDeclarationSyntax, Boolean\) | Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified variable declaration\. |

## SingleLocalDeclarationStatementInfo\(LocalDeclarationStatementSyntax, Boolean\)

### Summary

Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified local declaration statement\.

```csharp
public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(LocalDeclarationStatementSyntax localDeclarationStatement, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| localDeclarationStatement | |
| allowMissing | |

#### Returns

[SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md)

## SingleLocalDeclarationStatementInfo\(VariableDeclarationSyntax, Boolean\)

### Summary

Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified variable declaration\.

```csharp
public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(VariableDeclarationSyntax variableDeclaration, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| variableDeclaration | |
| allowMissing | |

#### Returns

[SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md)

## SingleLocalDeclarationStatementInfo\(ExpressionSyntax\)

### Summary

Creates a new [SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md) from the specified value\.

```csharp
public static SingleLocalDeclarationStatementInfo SingleLocalDeclarationStatementInfo(ExpressionSyntax value)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| value | |

#### Returns

[SingleLocalDeclarationStatementInfo](../../Syntax/SingleLocalDeclarationStatementInfo/README.md)

