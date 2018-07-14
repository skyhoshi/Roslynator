# WorkspaceExtensions\.RemoveCommentsAsync Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| RemoveCommentsAsync\(Document, CommentKinds, CancellationToken\) | Creates a new document with comments of the specified kind removed\. |
| RemoveCommentsAsync\(Document, TextSpan, CommentKinds, CancellationToken\) | Creates a new document with comments of the specified kind removed\. |

## RemoveCommentsAsync\(Document, CommentKinds, CancellationToken\)

### Summary

Creates a new document with comments of the specified kind removed\.

```csharp
public static Task<Document> RemoveCommentsAsync(this Document document, CommentKinds kinds, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| document | |
| kinds | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


## RemoveCommentsAsync\(Document, TextSpan, CommentKinds, CancellationToken\)

### Summary

Creates a new document with comments of the specified kind removed\.

```csharp
public static Task<Document> RemoveCommentsAsync(this Document document, TextSpan span, CommentKinds kinds, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| document | |
| span | |
| kinds | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


