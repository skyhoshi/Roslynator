# CSharpFactory\.PropertyDeclaration Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| PropertyDeclaration\(SyntaxTokenList, TypeSyntax, SyntaxToken, AccessorListSyntax, ExpressionSyntax\) | |
| PropertyDeclaration\(SyntaxTokenList, TypeSyntax, SyntaxToken, ArrowExpressionClauseSyntax\) | |

## PropertyDeclaration\(SyntaxTokenList, TypeSyntax, SyntaxToken, AccessorListSyntax, ExpressionSyntax\)

```csharp
public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, AccessorListSyntax accessorList, ExpressionSyntax value = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| modifiers | |
| type | |
| identifier | |
| accessorList | |
| value | |

#### Returns

[PropertyDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.propertydeclarationsyntax)


## PropertyDeclaration\(SyntaxTokenList, TypeSyntax, SyntaxToken, ArrowExpressionClauseSyntax\)

```csharp
public static PropertyDeclarationSyntax PropertyDeclaration(SyntaxTokenList modifiers, TypeSyntax type, SyntaxToken identifier, ArrowExpressionClauseSyntax expressionBody)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| modifiers | |
| type | |
| identifier | |
| expressionBody | |

#### Returns

[PropertyDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.propertydeclarationsyntax)


