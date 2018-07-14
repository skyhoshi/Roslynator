# SyntaxExtensions\.FirstAncestor\<TNode>\(SyntaxNode, Func\<TNode, Boolean>, Boolean\) Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Returns the first node of type **TNode** that matches the predicate\.

```csharp
public static TNode FirstAncestor<TNode>(this SyntaxNode node, Func<TNode, bool> predicate = null, bool ascendOutOfTrivia = true) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| predicate | |
| ascendOutOfTrivia | |

### Returns

TNode


