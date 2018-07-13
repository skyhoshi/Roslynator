# WorkspaceExtensions\.WithTextChangesAsync Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| WithTextChangesAsync\(Document, IEnumerable\<TextChange>, CancellationToken\) | Creates a new document updated with the specified text changes\. |
| WithTextChangesAsync\(Document, TextChange\[\], CancellationToken\) | Creates a new document updated with the specified text changes\. |

## WithTextChangesAsync\(Document, TextChange\[\], CancellationToken\)

### Summary

Creates a new document updated with the specified text changes\.

```csharp
public static Task<Document> WithTextChangesAsync(this Document document, TextChange[] textChanges, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| textChanges | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


## WithTextChangesAsync\(Document, IEnumerable\<TextChange>, CancellationToken\)

### Summary

Creates a new document updated with the specified text changes\.

```csharp
public static Task<Document> WithTextChangesAsync(this Document document, IEnumerable<TextChange> textChanges, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| textChanges | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


