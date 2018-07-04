# SyntaxListSelection\<TNode> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected nodes in a [SyntaxListSelection\<TNode>](./README.md)\.

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../Selection-1/README.md) &#x2192; SyntaxListSelection\<TNode>

#### Derived

* [Roslynator.CSharp.MemberDeclarationListSelection](../CSharp/MemberDeclarationListSelection/README.md)
* [Roslynator.CSharp.StatementListSelection](../CSharp/StatementListSelection/README.md)

#### Implements

* [IEnumerable\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [SyntaxListSelection(SyntaxList\<TNode>, TextSpan, Int32, Int32)](-ctor/README.md) | Initializes a new instance of the \. |

## Properties

| Property | Summary |
| -------- | ------- |
| [Items](Items/README.md) | Gets an underlying list that contains selected nodes\. |
| [UnderlyingList](UnderlyingList/README.md) | Gets an underlying list that contains selected nodes\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(SyntaxList\<TNode>, TextSpan)](Create/README.md) | Creates a new  based on the specified list and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) | |
| [First()](../Selection-1/First/README.md) | |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [Last()](../Selection-1/Last/README.md) | |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) | |
| [TryCreate(SyntaxList\<TNode>, TextSpan, SyntaxListSelection\<TNode>)](TryCreate/README.md) | Creates a new  based on the specified list and span\. |

