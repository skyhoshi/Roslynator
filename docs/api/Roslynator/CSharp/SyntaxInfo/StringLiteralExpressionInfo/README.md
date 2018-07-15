# SyntaxInfo\.StringLiteralExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| StringLiteralExpressionInfo\(LiteralExpressionSyntax\) | Creates a new [StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md) from the specified literal expression\. |
| StringLiteralExpressionInfo\(SyntaxNode, Boolean\) | Creates a new [StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md) from the specified node\. |

## StringLiteralExpressionInfo\(SyntaxNode, Boolean\)

### Summary

Creates a new [StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md) from the specified node\.

```csharp
public static StringLiteralExpressionInfo StringLiteralExpressionInfo(SyntaxNode node, bool walkDownParentheses = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |

#### Returns

[StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md)

## StringLiteralExpressionInfo\(LiteralExpressionSyntax\)

### Summary

Creates a new [StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md) from the specified literal expression\.

```csharp
public static StringLiteralExpressionInfo StringLiteralExpressionInfo(LiteralExpressionSyntax literalExpression)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| literalExpression | |

#### Returns

[StringLiteralExpressionInfo](../../Syntax/StringLiteralExpressionInfo/README.md)

