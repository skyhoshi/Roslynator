# SyntaxExtensions\.IndexOf Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IndexOf\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\) | Searches for a token that matches the predicate and returns the zero\-based index of the first occurrence within the entire [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| IndexOf\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\) | Searches for a trivia that matches the predicate and returns the zero\-based index of the first occurrence within the entire [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\. |

## IndexOf\(SyntaxTokenList, Func\<SyntaxToken, Boolean>\)

### Summary

Searches for a token that matches the predicate and returns the zero\-based index of the first occurrence within the entire [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\.

```csharp
public static int IndexOf(this SyntaxTokenList tokens, Func<SyntaxToken, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| tokens | |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)




## IndexOf\(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>\)

### Summary

Searches for a trivia that matches the predicate and returns the zero\-based index of the first occurrence within the entire [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\.

```csharp
public static int IndexOf(this SyntaxTriviaList triviaList, Func<SyntaxTrivia, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| triviaList | |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)




