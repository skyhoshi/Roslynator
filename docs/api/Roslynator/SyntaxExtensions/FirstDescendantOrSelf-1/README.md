# SyntaxExtensions\.FirstDescendantOrSelf Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| FirstDescendantOrSelf\<TNode>\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\. |
| FirstDescendantOrSelf\<TNode>\(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\. |

## FirstDescendantOrSelf\<TNode>\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\.

```csharp
public static TNode FirstDescendantOrSelf<TNode>(this SyntaxNode node, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| descendIntoChildren | |
| descendIntoTrivia | |

#### Returns

TNode


## FirstDescendantOrSelf\<TNode>\(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\.

```csharp
public static TNode FirstDescendantOrSelf<TNode>(this SyntaxNode node, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| node | |
| span | |
| descendIntoChildren | |
| descendIntoTrivia | |

#### Returns

TNode


