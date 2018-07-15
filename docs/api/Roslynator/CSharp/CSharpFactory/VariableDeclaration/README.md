# CSharpFactory\.VariableDeclaration Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| VariableDeclaration\(TypeSyntax, String, ExpressionSyntax\) | |
| VariableDeclaration\(TypeSyntax, SyntaxToken, EqualsValueClauseSyntax\) | |
| VariableDeclaration\(TypeSyntax, SyntaxToken, ExpressionSyntax\) | |
| VariableDeclaration\(TypeSyntax, VariableDeclaratorSyntax\) | |

## VariableDeclaration\(TypeSyntax, String, ExpressionSyntax\)

```csharp
public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, string identifier, ExpressionSyntax value = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| type | |
| identifier | |
| value | |

#### Returns

[VariableDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.variabledeclarationsyntax)

## VariableDeclaration\(TypeSyntax, SyntaxToken, ExpressionSyntax\)

```csharp
public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier, ExpressionSyntax value = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| type | |
| identifier | |
| value | |

#### Returns

[VariableDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.variabledeclarationsyntax)

## VariableDeclaration\(TypeSyntax, SyntaxToken, EqualsValueClauseSyntax\)

```csharp
public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, SyntaxToken identifier, EqualsValueClauseSyntax initializer)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| type | |
| identifier | |
| initializer | |

#### Returns

[VariableDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.variabledeclarationsyntax)

## VariableDeclaration\(TypeSyntax, VariableDeclaratorSyntax\)

```csharp
public static VariableDeclarationSyntax VariableDeclaration(TypeSyntax type, VariableDeclaratorSyntax variable)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| type | |
| variable | |

#### Returns

[VariableDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.variabledeclarationsyntax)

