# SyntaxInfo\.IsExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\) | Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified binary expression\. |
| IsExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified node\. |

## IsExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified node\.

```csharp
public static IsExpressionInfo IsExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md)


## IsExpressionInfo\(BinaryExpressionSyntax, Boolean, Boolean\)

### Summary

Creates a new [IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md) from the specified binary expression\.

```csharp
public static IsExpressionInfo IsExpressionInfo(BinaryExpressionSyntax binaryExpression, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| binaryExpression | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[IsExpressionInfo](../../Syntax/IsExpressionInfo/README.md)


