# SyntaxInfo\.AssignmentExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| AssignmentExpressionInfo\(AssignmentExpressionSyntax, Boolean, Boolean\) | Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md) from the specified assignment expression\. |
| AssignmentExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md) from the specified node\. |

## AssignmentExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md) from the specified node\.

```csharp
public static AssignmentExpressionInfo AssignmentExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md)


## AssignmentExpressionInfo\(AssignmentExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md) from the specified assignment expression\.

```csharp
public static AssignmentExpressionInfo AssignmentExpressionInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| assignmentExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AssignmentExpressionInfo](../../Syntax/AssignmentExpressionInfo/README.md)


