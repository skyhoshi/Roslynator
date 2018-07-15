# CSharpFactory\.CollectionInitializerExpression Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| CollectionInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\) | |
| CollectionInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\) | |

## CollectionInitializerExpression\(SeparatedSyntaxList\<ExpressionSyntax>\)

```csharp
public static InitializerExpressionSyntax CollectionInitializerExpression(SeparatedSyntaxList<ExpressionSyntax> expressions = default(SeparatedSyntaxList<ExpressionSyntax>))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| expressions | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

## CollectionInitializerExpression\(SyntaxToken, SeparatedSyntaxList\<ExpressionSyntax>, SyntaxToken\)

```csharp
public static InitializerExpressionSyntax CollectionInitializerExpression(SyntaxToken openBraceToken, SeparatedSyntaxList<ExpressionSyntax> expressions, SyntaxToken closeBraceToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| openBraceToken | |
| expressions | |
| closeBraceToken | |

#### Returns

[InitializerExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.initializerexpressionsyntax)

