# SyntaxInfo\.AsExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| AsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified binary expression\. |
| AsExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified node\. |

## AsExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified node\.

```csharp
public static AsExpressionInfo AsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md)


## AsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md) from the specified binary expression\.

```csharp
public static AsExpressionInfo AsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| binaryExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[AsExpressionInfo](../../Syntax/AsExpressionInfo/README.md)


