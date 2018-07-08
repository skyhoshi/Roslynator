# WorkspaceExtensions\.RemovePreprocessorDirectivesAsync Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| RemovePreprocessorDirectivesAsync\(Document, PreprocessorDirectiveKinds, CancellationToken\) | Creates a new document with preprocessor directives of the specified kind removed\. |
| RemovePreprocessorDirectivesAsync\(Document, TextSpan, PreprocessorDirectiveKinds, CancellationToken\) | Creates a new document with preprocessor directives of the specified kind removed\. |

## RemovePreprocessorDirectivesAsync\(Document, PreprocessorDirectiveKinds, CancellationToken\)

### Summary

Creates a new document with preprocessor directives of the specified kind removed\.

```csharp
public static Task<Document> RemovePreprocessorDirectivesAsync(this Document document, PreprocessorDirectiveKinds directiveKinds, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| directiveKinds | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>




## RemovePreprocessorDirectivesAsync\(Document, TextSpan, PreprocessorDirectiveKinds, CancellationToken\)

### Summary

Creates a new document with preprocessor directives of the specified kind removed\.

```csharp
public static Task<Document> RemovePreprocessorDirectivesAsync(this Document document, TextSpan span, PreprocessorDirectiveKinds directiveKinds, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| span | |
| directiveKinds | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>




