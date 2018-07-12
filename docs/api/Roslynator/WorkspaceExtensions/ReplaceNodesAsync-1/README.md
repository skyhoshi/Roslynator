# WorkspaceExtensions\.ReplaceNodesAsync Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReplaceNodesAsync\<TNode>\(Document, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken\) | Creates a new document with the specified old nodes replaced with new nodes\. |
| ReplaceNodesAsync\<TNode>\(Solution, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken\) | Creates a new solution with the specified old nodes replaced with new nodes\. |

## ReplaceNodesAsync\<TNode>\(Document, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken\)

### Summary

Creates a new document with the specified old nodes replaced with new nodes\.

```csharp
public static Task<Document> ReplaceNodesAsync<TNode>(this Document document, IEnumerable<TNode> nodes, Func<TNode, TNode, SyntaxNode> computeReplacementNode, CancellationToken cancellationToken = default(CancellationToken)) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| nodes | |
| computeReplacementNode | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>




## ReplaceNodesAsync\<TNode>\(Solution, IEnumerable\<TNode>, Func\<TNode, TNode, SyntaxNode>, CancellationToken\)

### Summary

Creates a new solution with the specified old nodes replaced with new nodes\.

```csharp
public static Task<Solution> ReplaceNodesAsync<TNode>(this Solution solution, IEnumerable<TNode> nodes, Func<TNode, TNode, SyntaxNode> computeReplacementNodes, CancellationToken cancellationToken = default(CancellationToken)) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| solution | |
| nodes | |
| computeReplacementNodes | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Solution](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.solution)>




