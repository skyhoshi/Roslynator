# CSharpFactory\.CheckedExpression Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| CheckedExpression\(ExpressionSyntax\) | |
| CheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\) | |

## CheckedExpression\(ExpressionSyntax\)

```csharp
public static CheckedExpressionSyntax CheckedExpression(ExpressionSyntax expression)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| expression | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)


## CheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\)

```csharp
public static CheckedExpressionSyntax CheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| openParenToken | |
| expression | |
| closeParenToken | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)


