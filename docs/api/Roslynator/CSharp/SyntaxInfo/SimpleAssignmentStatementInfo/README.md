# SyntaxInfo\.SimpleAssignmentStatementInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SimpleAssignmentStatementInfo\(AssignmentExpressionSyntax, Boolean, Boolean\) | Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified assignment expression\. |
| SimpleAssignmentStatementInfo\(ExpressionStatementSyntax, Boolean, Boolean\) | Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified expression statement\. |
| SimpleAssignmentStatementInfo\(StatementSyntax, Boolean, Boolean\) | Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified statement\. |

## SimpleAssignmentStatementInfo\(StatementSyntax, Boolean, Boolean\)

### Summary

Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified statement\.

```csharp
public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(StatementSyntax statement, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| statement | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md)


## SimpleAssignmentStatementInfo\(AssignmentExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified assignment expression\.

```csharp
public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| assignmentExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md)


## SimpleAssignmentStatementInfo\(ExpressionStatementSyntax, Boolean, Boolean\)

### Summary

Creates a new [SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md) from the specified expression statement\.

```csharp
public static SimpleAssignmentStatementInfo SimpleAssignmentStatementInfo(ExpressionStatementSyntax expressionStatement, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expressionStatement | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleAssignmentStatementInfo](../../Syntax/SimpleAssignmentStatementInfo/README.md)


