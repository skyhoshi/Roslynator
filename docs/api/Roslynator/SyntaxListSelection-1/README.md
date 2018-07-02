# SyntaxListSelection\<TNode> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.dll


Represents selected nodes in a \.

#### Type Parameters

| Type Parameter| Summary|
| --- | --- |
| [TNode](TNode/README.md) | |

#### Inheritance

* Object
  * Selection\<TNode>
    * SyntaxListSelection\<TNode>

#### Attributes

DebuggerDisplayAttribute

## Constructors

| Constructor| Summary|
| --- | --- |
| [SyntaxListSelection(SyntaxList\<TNode>, TextSpan, Int32, Int32)](.ctor/README.md) | Initializes a new instance of the \. |

## Properties

| Property| Summary|
| --- | --- |
| [UnderlyingList](UnderlyingList/README.md) | Gets an underlying list that contains selected nodes\. |
| [Items](Items/README.md) | Gets an underlying list that contains selected nodes\. |

## Methods

| Method| Summary|
| --- | --- |
| [Create(SyntaxList\<TNode>, TextSpan)](Create/README.md) | Creates a new  based on the specified list and span\. |
| [TryCreate(SyntaxList\<TNode>, TextSpan, SyntaxListSelection\<TNode>)](TryCreate/README.md) | Creates a new  based on the specified list and span\. |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |

