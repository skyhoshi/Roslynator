# SyntaxExtensions\.WithoutLeadingTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| WithoutLeadingTrivia\(SyntaxNodeOrToken\) | Creates a new [SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken) with the leading trivia removed\. |
| WithoutLeadingTrivia\(SyntaxToken\) | Creates a new token from this token with the leading trivia removed\. |

## WithoutLeadingTrivia\(SyntaxNodeOrToken\)

### Summary

Creates a new [SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken) with the leading trivia removed\.

```csharp
public static SyntaxNodeOrToken WithoutLeadingTrivia(this SyntaxNodeOrToken nodeOrToken)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| nodeOrToken | |

#### Returns

[SyntaxNodeOrToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnodeortoken)


## WithoutLeadingTrivia\(SyntaxToken\)

### Summary

Creates a new token from this token with the leading trivia removed\.

```csharp
public static SyntaxToken WithoutLeadingTrivia(this SyntaxToken token)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


