# SyntaxExtensions\.WithoutTrailingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| WithoutTrailingTrivia\(SyntaxNodeOrToken\) | Creates a new [SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken) with the trailing trivia removed\. |
| WithoutTrailingTrivia\(SyntaxToken\) | Creates a new token from this token with the trailing trivia removed\. |

## WithoutTrailingTrivia\(SyntaxNodeOrToken\)

### Summary

Creates a new [SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken) with the trailing trivia removed\.

```csharp
public static SyntaxNodeOrToken WithoutTrailingTrivia(this SyntaxNodeOrToken nodeOrToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| nodeOrToken | |

#### Returns

[SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken)

## WithoutTrailingTrivia\(SyntaxToken\)

### Summary

Creates a new token from this token with the trailing trivia removed\.

```csharp
public static SyntaxToken WithoutTrailingTrivia(this SyntaxToken token)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)

