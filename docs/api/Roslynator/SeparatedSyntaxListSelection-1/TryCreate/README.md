# SeparatedSyntaxListSelection\<TNode>\.TryCreate\(SeparatedSyntaxList\<TNode>, TextSpan, SeparatedSyntaxListSelection\<TNode>\) Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Creates a new [SeparatedSyntaxListSelection\<TNode>](../README.md) based on the specified list and span\.

```csharp
public static bool TryCreate(SeparatedSyntaxList<TNode> list, TextSpan span, out SeparatedSyntaxListSelection<TNode> selection)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| span | |
| selection | |

### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one node; otherwise, false\.
