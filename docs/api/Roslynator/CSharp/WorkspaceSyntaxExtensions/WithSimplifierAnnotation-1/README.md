# WorkspaceSyntaxExtensions\.WithSimplifierAnnotation Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| WithSimplifierAnnotation\(SyntaxToken\) | Adds [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) to the specified token, creating a new token of the same type with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) on it\. "Rename" annotation is specified by [Kind](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.codeactions.renameannotation.kind)\. |
| WithSimplifierAnnotation\<TNode>\(TNode\) | Creates a new node with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) attached\. |

## WithSimplifierAnnotation\<TNode>\(TNode\)

### Summary

Creates a new node with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) attached\.

```csharp
public static TNode WithSimplifierAnnotation<TNode>(this TNode node) where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |

#### Returns

TNode




## WithSimplifierAnnotation\(SyntaxToken\)

### Summary

Adds [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) to the specified token, creating a new token of the same type with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) on it\.
"Rename" annotation is specified by [Kind](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.codeactions.renameannotation.kind)\.

```csharp
public static SyntaxToken WithSimplifierAnnotation(this SyntaxToken token)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |

#### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)




