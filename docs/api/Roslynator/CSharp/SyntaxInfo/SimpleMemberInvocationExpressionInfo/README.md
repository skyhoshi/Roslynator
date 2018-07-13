# SyntaxInfo\.SimpleMemberInvocationExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SimpleMemberInvocationExpressionInfo\(InvocationExpressionSyntax, Boolean\) | Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md) from the specified invocation expression\. |
| SimpleMemberInvocationExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md) from the specified node\. |

## SimpleMemberInvocationExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md) from the specified node\.

```csharp
public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md)


## SimpleMemberInvocationExpressionInfo\(InvocationExpressionSyntax, Boolean\)

### Summary

Creates a new [SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md) from the specified invocation expression\.

```csharp
public static SimpleMemberInvocationExpressionInfo SimpleMemberInvocationExpressionInfo(InvocationExpressionSyntax invocationExpression, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| invocationExpression | |
| allowMissing | |

#### Returns

[SimpleMemberInvocationExpressionInfo](../../Syntax/SimpleMemberInvocationExpressionInfo/README.md)


