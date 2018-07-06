# SyntaxInfo\.ConditionalExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ConditionalExpressionInfo\(ConditionalExpressionSyntax, Boolean, Boolean\) | Creates a new [ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md) from the specified conditional expression\. |
| ConditionalExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md) from the specified node\. |

## ConditionalExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md) from the specified node\.

```csharp
public static ConditionalExpressionInfo ConditionalExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md)




## ConditionalExpressionInfo\(ConditionalExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md) from the specified conditional expression\.

```csharp
public static ConditionalExpressionInfo ConditionalExpressionInfo(ConditionalExpressionSyntax conditionalExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| conditionalExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[ConditionalExpressionInfo](../../Syntax/ConditionalExpressionInfo/README.md)




