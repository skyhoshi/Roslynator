# SyntaxInfo\.SimpleAssignmentExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SimpleAssignmentExpressionInfo\(AssignmentExpressionSyntax, Boolean, Boolean\) | Creates a new [SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md) from the specified assignment expression\. |
| SimpleAssignmentExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md) from the specified node\. |

## SimpleAssignmentExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md) from the specified node\.

```csharp
public static SimpleAssignmentExpressionInfo SimpleAssignmentExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md)




## SimpleAssignmentExpressionInfo\(AssignmentExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md) from the specified assignment expression\.

```csharp
public static SimpleAssignmentExpressionInfo SimpleAssignmentExpressionInfo(AssignmentExpressionSyntax assignmentExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| assignmentExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleAssignmentExpressionInfo](../../Syntax/SimpleAssignmentExpressionInfo/README.md)




