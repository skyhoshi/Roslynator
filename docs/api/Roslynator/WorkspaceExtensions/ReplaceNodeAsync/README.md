# WorkspaceExtensions\.ReplaceNodeAsync Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReplaceNodeAsync\(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken\) | Creates a new document with the specified old node replaced with new nodes\. |
| ReplaceNodeAsync\(Document, SyntaxNode, SyntaxNode, CancellationToken\) | Creates a new document with the specified old node replaced with a new node\. |
| ReplaceNodeAsync\<TNode>\(Solution, TNode, TNode, CancellationToken\) | Creates a new solution with the specified old node replaced with a new node\. |

## ReplaceNodeAsync\(Document, SyntaxNode, SyntaxNode, CancellationToken\)

### Summary

Creates a new document with the specified old node replaced with a new node\.

```csharp
public static Task<Document> ReplaceNodeAsync(this Document document, SyntaxNode oldNode, SyntaxNode newNode, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| oldNode | |
| newNode | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


## ReplaceNodeAsync\(Document, SyntaxNode, IEnumerable\<SyntaxNode>, CancellationToken\)

### Summary

Creates a new document with the specified old node replaced with new nodes\.

```csharp
public static Task<Document> ReplaceNodeAsync(this Document document, SyntaxNode oldNode, IEnumerable<SyntaxNode> newNodes, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| oldNode | |
| newNodes | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>


## ReplaceNodeAsync\<TNode>\(Solution, TNode, TNode, CancellationToken\)

### Summary

Creates a new solution with the specified old node replaced with a new node\.

```csharp
public static Task<Solution> ReplaceNodeAsync<TNode>(this Solution solution, TNode oldNode, TNode newNode, CancellationToken cancellationToken = default(CancellationToken)) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| solution | |
| oldNode | |
| newNode | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Solution](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.solution)>


