# SyntaxInfo\.NullCheckExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| NullCheckExpressionInfo\(SyntaxNode, NullCheckStyles, Boolean, Boolean\) | Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\. |
| NullCheckExpressionInfo\(SyntaxNode, SemanticModel, NullCheckStyles, Boolean, Boolean, CancellationToken\) | Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\. |

## NullCheckExpressionInfo\(SyntaxNode, NullCheckStyles, Boolean, Boolean\)

### Summary

Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\.

```csharp
public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, NullCheckStyles allowedStyles = ComparisonToNull | IsPattern, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| allowedStyles | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md)

## NullCheckExpressionInfo\(SyntaxNode, SemanticModel, NullCheckStyles, Boolean, Boolean, CancellationToken\)

### Summary

Creates a new [NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md) from the specified node\.

```csharp
public static NullCheckExpressionInfo NullCheckExpressionInfo(SyntaxNode node, SemanticModel semanticModel, NullCheckStyles allowedStyles = All, bool walkDownParentheses = true, bool allowMissing = false, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| semanticModel | |
| allowedStyles | |
| walkDownParentheses | |
| allowMissing | |
| cancellationToken | |

#### Returns

[NullCheckExpressionInfo](../../Syntax/NullCheckExpressionInfo/README.md)

