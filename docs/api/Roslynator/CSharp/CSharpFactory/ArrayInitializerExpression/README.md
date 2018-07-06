# CSharpFactory\.ArrayInitializerExpression Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ArrayInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\) | |
| ArrayInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\) | |

## ArrayInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\)

```csharp
public static InitializerExpressionSyntax ArrayInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| expressions | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)


## ArrayInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\)

```csharp
public static InitializerExpressionSyntax ArrayInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| openBraceToken | |
| expressions | |
| closeBraceToken | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)


