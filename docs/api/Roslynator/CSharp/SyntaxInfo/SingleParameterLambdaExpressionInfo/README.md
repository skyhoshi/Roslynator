# SyntaxInfo\.SingleParameterLambdaExpressionInfo Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| SingleParameterLambdaExpressionInfo\(LambdaExpressionSyntax, Boolean\) | Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified lambda expression\. |
| SingleParameterLambdaExpressionInfo\(SyntaxNode, Boolean, Boolean\) | Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified node\. |

## SingleParameterLambdaExpressionInfo\(SyntaxNode, Boolean, Boolean\)

### Summary

Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified node\.

```csharp
public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(SyntaxNode node, bool walkDownParentheses = true, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| walkDownParentheses | |
| allowMissing | |

#### Returns

[SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md)


## SingleParameterLambdaExpressionInfo\(LambdaExpressionSyntax, Boolean\)

### Summary

Creates a new [SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md) from the specified lambda expression\.

```csharp
public static SingleParameterLambdaExpressionInfo SingleParameterLambdaExpressionInfo(LambdaExpressionSyntax lambdaExpression, bool allowMissing = false)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| lambdaExpression | |
| allowMissing | |

#### Returns

[SingleParameterLambdaExpressionInfo](../../Syntax/SingleParameterLambdaExpressionInfo/README.md)


