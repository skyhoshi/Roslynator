# WorkspaceSyntaxExtensions Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Summary

A set of extension methods for syntax\. These methods are dependent on the workspace layer\.

```csharp
public static class WorkspaceSyntaxExtensions
```


## Methods

| Method | Summary |
| ------ | ------- |
| [Parenthesize(ExpressionSyntax, Boolean, Boolean)](Parenthesize/README.md) | Creates parenthesized expression that is parenthesizing the specified expression\. |
| [WithFormatterAnnotation(SyntaxToken)](WithFormatterAnnotation/README.md) | Adds [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) to the specified token, creating a new token of the same type with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) on it\. |
| [WithFormatterAnnotation\<TNode>(TNode)](WithFormatterAnnotation-1/README.md) | Creates a new node with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.formatting.formatter.annotation) attached\. |
| [WithRenameAnnotation(SyntaxToken)](WithRenameAnnotation/README.md) | Adds "rename" annotation to the specified token, creating a new token of the same type with the "rename" annotation on it\. "Rename" annotation is specified by [Kind](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.codeactions.renameannotation.kind)\. |
| [WithSimplifierAnnotation(SyntaxToken)](WithSimplifierAnnotation/README.md) | Adds [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) to the specified token, creating a new token of the same type with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) on it\. "Rename" annotation is specified by [Kind](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.codeactions.renameannotation.kind)\. |
| [WithSimplifierAnnotation\<TNode>(TNode)](WithSimplifierAnnotation-1/README.md) | Creates a new node with the [Annotation](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.simplification.simplifier.annotation) attached\. |

