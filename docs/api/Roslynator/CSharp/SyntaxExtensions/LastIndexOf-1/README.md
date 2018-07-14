# SyntaxExtensions\.LastIndexOf Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| LastIndexOf\(SyntaxTriviaList, SyntaxKind\) | Searches for a trivia of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\. |
| LastIndexOf\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxKind\) | Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| LastIndexOf\<TNode>\(SyntaxList\<TNode>, SyntaxKind\) | Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |

## LastIndexOf\<TNode>\(SeparatedSyntaxList\<TNode>, SyntaxKind\)

### Summary

Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\.

```csharp
public static int LastIndexOf<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| kind | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


## LastIndexOf\<TNode>\(SyntaxList\<TNode>, SyntaxKind\)

### Summary

Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public static int LastIndexOf<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| kind | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


## LastIndexOf\(SyntaxTriviaList, SyntaxKind\)

### Summary

Searches for a trivia of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\.

```csharp
public static int LastIndexOf(this SyntaxTriviaList triviaList, SyntaxKind kind)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| triviaList | |
| kind | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


