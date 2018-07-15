# CSharpFactory\.IsExpression Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsExpression\(ExpressionSyntax, ExpressionSyntax\) | |
| IsExpression\(ExpressionSyntax, SyntaxToken, ExpressionSyntax\) | |

## IsExpression\(ExpressionSyntax, ExpressionSyntax\)

```csharp
public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, ExpressionSyntax right)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| left | |
| right | |

#### Returns

[BinaryExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.binaryexpressionsyntax)

## IsExpression\(ExpressionSyntax, SyntaxToken, ExpressionSyntax\)

```csharp
public static BinaryExpressionSyntax IsExpression(ExpressionSyntax left, SyntaxToken operatorToken, ExpressionSyntax right)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| left | |
| operatorToken | |
| right | |

#### Returns

[BinaryExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.binaryexpressionsyntax)

