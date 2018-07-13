# WorkspaceSyntaxExtensions\.WithRenameAnnotation\(SyntaxToken\) Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Adds "rename" annotation to the specified token, creating a new token of the same type with the "rename" annotation on it\.
"Rename" annotation is specified by [Kind](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.codeactions.renameannotation.kind)\.

```csharp
public static SyntaxToken WithRenameAnnotation(this SyntaxToken token)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| token | |

### Returns

[SyntaxToken](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtoken)


