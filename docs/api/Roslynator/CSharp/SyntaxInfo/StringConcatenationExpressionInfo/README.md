# SyntaxInfo\.StringConcatenationExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| StringConcatenationExpressionInfo\(BinaryExpressionSyntax, SemanticModel, CancellationToken\) | Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified node\. |
| StringConcatenationExpressionInfo\(ExpressionChain, SemanticModel, CancellationToken\) | Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified expression chain\. |
| StringConcatenationExpressionInfo\(SyntaxNode, SemanticModel, Boolean, CancellationToken\) | Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified node\. |

## StringConcatenationExpressionInfo\(SyntaxNode, SemanticModel, Boolean, CancellationToken\)

### Summary

Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified node\.

```csharp
public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(SyntaxNode node, SemanticModel semanticModel, bool walkDownParentheses = true, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| semanticModel | |
| walkDownParentheses | |
| cancellationToken | |

#### Returns

[StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md)




## StringConcatenationExpressionInfo\(BinaryExpressionSyntax, SemanticModel, CancellationToken\)

### Summary

Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified node\.

```csharp
public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(BinaryExpressionSyntax binaryExpression, SemanticModel semanticModel, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| binaryExpression | |
| semanticModel | |
| cancellationToken | |

#### Returns

[StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md)




## StringConcatenationExpressionInfo\(ExpressionChain, SemanticModel, CancellationToken\)

### Summary

Creates a new [StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md) from the specified expression chain\.

```csharp
public static StringConcatenationExpressionInfo StringConcatenationExpressionInfo(in ExpressionChain expressionChain, SemanticModel semanticModel, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| expressionChain | |
| semanticModel | |
| cancellationToken | |

#### Returns

[StringConcatenationExpressionInfo](../../Syntax/StringConcatenationExpressionInfo/README.md)




