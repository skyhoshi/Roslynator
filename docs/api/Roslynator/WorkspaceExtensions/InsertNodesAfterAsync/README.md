# WorkspaceExtensions\.InsertNodesAfterAsync\(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken\) Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Summary

Creates a new document with new nodes inserted after the specified node\.

```csharp
public static Task<Document> InsertNodesAfterAsync(this Document document, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken))
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| nodeInList | |
| newNodes | |
| cancellationToken | |

### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


