# ModifierList Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A set of static methods that allows manipulation with modifiers\.

## Methods

| Method| Summary|
| --- | --- |
| [GetInsertIndex(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>)](GetInsertIndex/README.md) | Returns an index a token with the specified kind should be inserted at\. |
| [GetInsertIndex(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>)](GetInsertIndex/README.md) | Returns an index the specified token should be inserted at\. |
| [Insert(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>)](Insert/README.md) | Creates a new list of modifiers with the modifier of the specified kind inserted\. |
| [Insert(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>)](Insert/README.md) | Creates a new list of modifiers with a specified modifier inserted\. |
| [Insert\<TNode>(TNode, SyntaxKind, IComparer\<SyntaxKind>)](Insert-1/README.md) | Creates a new node with a modifier of the specified kind inserted\. |
| [Insert\<TNode>(TNode, SyntaxToken, IComparer\<SyntaxToken>)](Insert-1/README.md) | Creates a new node with the specified modifier inserted\. |
| [Remove\<TNode>(TNode, SyntaxKind)](Remove-1/README.md) | Creates a new node with a modifier of the specified kind removed\. |
| [Remove\<TNode>(TNode, SyntaxToken)](Remove-1/README.md) | Creates a new node with the specified modifier removed\. |
| [RemoveAll\<TNode>(TNode)](RemoveAll-1/README.md) | Creates a new node with all modifiers removed\. |
| [RemoveAll\<TNode>(TNode, Func\<SyntaxToken, Boolean>)](RemoveAll-1/README.md) | Creates a new node with modifiers that matches the predicate removed\. |
| [RemoveAt\<TNode>(TNode, Int32)](RemoveAt-1/README.md) | Creates a new node with a modifier at the specified index removed\. |

