# SyntaxExtensions\.ParenthesesSpan Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ParenthesesSpan\(CastExpressionSyntax\) | The absolute span of the parentheses, not including its leading and trailing trivia\. |
| ParenthesesSpan\(CommonForEachStatementSyntax\) | The absolute span of the parentheses, not including its leading and trailing trivia\. |
| ParenthesesSpan\(ForStatementSyntax\) | Absolute span of the parentheses, not including the leading and trailing trivia\. |

## ParenthesesSpan\(CastExpressionSyntax\)

### Summary

The absolute span of the parentheses, not including its leading and trailing trivia\.

```csharp
public static TextSpan ParenthesesSpan(this CastExpressionSyntax castExpression)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| castExpression | |

#### Returns

[TextSpan](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textspan)

## ParenthesesSpan\(CommonForEachStatementSyntax\)

### Summary

The absolute span of the parentheses, not including its leading and trailing trivia\.

```csharp
public static TextSpan ParenthesesSpan(this CommonForEachStatementSyntax forEachStatement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| forEachStatement | |

#### Returns

[TextSpan](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textspan)

## ParenthesesSpan\(ForStatementSyntax\)

### Summary

Absolute span of the parentheses, not including the leading and trailing trivia\.

```csharp
public static TextSpan ParenthesesSpan(this ForStatementSyntax forStatement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| forStatement | |

#### Returns

[TextSpan](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textspan)

