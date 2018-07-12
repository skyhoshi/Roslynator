# SyntaxExtensions\.FirstDescendant Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| FirstDescendant\<TNode>\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\) | Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\. |
| FirstDescendant\<TNode>\(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\) | Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\. |

## FirstDescendant\<TNode>\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\.

```csharp
public static TNode FirstDescendant<TNode>(this SyntaxNode node, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode
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




## FirstDescendant\<TNode>\(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\.

```csharp
public static TNode FirstDescendant<TNode>(this SyntaxNode node, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode
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




