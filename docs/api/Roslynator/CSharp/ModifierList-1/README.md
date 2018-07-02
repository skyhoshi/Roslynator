# ModifierList\<TNode> Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

Represents a list of modifiers\.

#### Type Parameters

| Type Parameter| Summary|
| --- | --- |
| TNode | |

#### Inheritance

Object &#x2192; ModifierList\<TNode>

## Properties

| Property| Summary|
| --- | --- |
| [Instance](Instance/README.md) | Gets an instance of the  for a syntax specified by the generic argument\. |

## Methods

| Method| Summary|
| --- | --- |
| [Insert(TNode, SyntaxKind, IComparer\<SyntaxKind>)](Insert/README.md) | Creates a new node with a modifier of the specified kind inserted\. |
| [Insert(TNode, SyntaxToken, IComparer\<SyntaxToken>)](Insert/README.md) | Creates a new node with the specified modifier inserted\. |
| [Remove(TNode, SyntaxKind)](Remove/README.md) | Creates a new node with a modifier of the specified kind removed\. |
| [Remove(TNode, SyntaxToken)](Remove/README.md) | Creates a new node with the specified modifier removed\. |
| [RemoveAt(TNode, Int32)](RemoveAt/README.md) | Creates a new node with a modifier at the specified index removed\. |
| [RemoveAll(TNode)](RemoveAll/README.md) | Creates a new node with all modifiers removed\. |
| [RemoveAll(TNode, Func\<SyntaxToken, Boolean>)](RemoveAll/README.md) | Creates a new node with modifiers that matches the predicate removed\. |

