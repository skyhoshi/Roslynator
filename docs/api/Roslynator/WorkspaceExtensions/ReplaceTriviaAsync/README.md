# WorkspaceExtensions\.ReplaceTriviaAsync Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.Workspaces\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReplaceTriviaAsync\(Document, SyntaxTrivia, IEnumerable\<SyntaxTrivia>, CancellationToken\) | Creates a new document with the specified old trivia replaced with a new trivia\. |
| ReplaceTriviaAsync\(Document, SyntaxTrivia, SyntaxTrivia, CancellationToken\) | Creates a new document with the specified old trivia replaced with a new trivia\. |

## ReplaceTriviaAsync\(Document, SyntaxTrivia, SyntaxTrivia, CancellationToken\)

### Summary

Creates a new document with the specified old trivia replaced with a new trivia\.

```csharp
public static Task<Document> ReplaceTriviaAsync(this Document document, SyntaxTrivia oldTrivia, SyntaxTrivia newTrivia, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| oldTrivia | |
| newTrivia | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>




## ReplaceTriviaAsync\(Document, SyntaxTrivia, IEnumerable\<SyntaxTrivia>, CancellationToken\)

### Summary

Creates a new document with the specified old trivia replaced with a new trivia\.

```csharp
public static Task<Document> ReplaceTriviaAsync(this Document document, SyntaxTrivia oldTrivia, IEnumerable<SyntaxTrivia> newTrivia, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| document | |
| oldTrivia | |
| newTrivia | |
| cancellationToken | |

#### Returns

[Task](https://docs.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)\<[Document](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.document)>




