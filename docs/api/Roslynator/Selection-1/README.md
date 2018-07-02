# Selection\<T> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.dll

## Summary

Represents consecutive sequence of selected items in a collection\.

#### Type Parameters

| Type Parameter| Summary|
| --- | --- |
| T | |

#### Inheritance

Object &#x2192; Selection\<T>

#### Derived

* [Roslynator.CSharp.MemberDeclarationListSelection](MemberDeclarationListSelection/README.md)
* [Roslynator.CSharp.StatementListSelection](StatementListSelection/README.md)
* [Roslynator.SeparatedSyntaxListSelection\<TNode>](../SeparatedSyntaxListSelection-1/README.md)
* [Roslynator.SyntaxListSelection\<TNode>](../SyntaxListSelection-1/README.md)
* [Roslynator.Text.TextLineCollectionSelection](TextLineCollectionSelection/README.md)

#### Implements

* IEnumerable\<T>
* IReadOnlyCollection\<T>
* IReadOnlyList\<T>

## Constructors

| Constructor| Summary|
| --- | --- |
| [Selection(TextSpan, Int32, Int32)](.ctor/README.md) | Initializes a new instance of the \. |

## Properties

| Property| Summary|
| --- | --- |
| [Items](Items/README.md) | Gets an underlying list that contains selected items\. |
| [OriginalSpan](OriginalSpan/README.md) | Gets the original span that was used to determine selected items\. |
| [FirstIndex](FirstIndex/README.md) | Gets an index of the first selected item\. |
| [LastIndex](LastIndex/README.md) | Gets an index of the last selected item\. |
| [Count](Count/README.md) | Gets a number of selected items\. |
| [this\[Int32\]](this[]/README.md) | Gets the selected item at the specified index\. |

## Methods

| Method| Summary|
| --- | --- |
| [First()](First/README.md) | Gets the first selected item\. |
| [Last()](Last/README.md) | Gets the last selected item\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |

