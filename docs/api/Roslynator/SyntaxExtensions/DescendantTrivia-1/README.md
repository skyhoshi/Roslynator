# SyntaxExtensions\.DescendantTrivia Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| DescendantTrivia\<TNode>\(SyntaxList\<TNode>, Func\<SyntaxNode, Boolean>, Boolean\) | Get a list of all the trivia associated with the nodes in the list\. |
| DescendantTrivia\<TNode>\(SyntaxList\<TNode>, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\) | Get a list of all the trivia associated with the nodes in the list\. |

## DescendantTrivia\<TNode>\(SyntaxList\<TNode>, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Get a list of all the trivia associated with the nodes in the list\.

```csharp
public static IEnumerable<SyntaxTrivia> DescendantTrivia<TNode>(this SyntaxList<TNode> list, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| descendIntoChildren | |
| descendIntoTrivia | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[SyntaxTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivia)>



#### Attributes

[IteratorStateMachineAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.iteratorstatemachineattribute)

## DescendantTrivia\<TNode>\(SyntaxList\<TNode>, TextSpan, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Get a list of all the trivia associated with the nodes in the list\.

```csharp
public static IEnumerable<SyntaxTrivia> DescendantTrivia<TNode>(this SyntaxList<TNode> list, TextSpan span, Func<SyntaxNode, bool> descendIntoChildren = null, bool descendIntoTrivia = false) where TNode : SyntaxNode
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| span | |
| descendIntoChildren | |
| descendIntoTrivia | |

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[SyntaxTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivia)>



#### Attributes

[IteratorStateMachineAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.iteratorstatemachineattribute)
