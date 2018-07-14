# CSharpFactory\.UncheckedExpression Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| UncheckedExpression\(ExpressionSyntax\) | |
| UncheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\) | |

## UncheckedExpression\(ExpressionSyntax\)

```csharp
public static CheckedExpressionSyntax UncheckedExpression(ExpressionSyntax expression)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expression | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)


## UncheckedExpression\(SyntaxToken, ExpressionSyntax, SyntaxToken\)

```csharp
public static CheckedExpressionSyntax UncheckedExpression(SyntaxToken openParenToken, ExpressionSyntax expression, SyntaxToken closeParenToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openParenToken | |
| expression | |
| closeParenToken | |

#### Returns

[CheckedExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.checkedexpressionsyntax)


