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

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; Selection\<T>

#### Derived

* [Roslynator.CSharp.MemberDeclarationListSelection](../CSharp/MemberDeclarationListSelection/README.md)
* [Roslynator.CSharp.StatementListSelection](../CSharp/StatementListSelection/README.md)
* [Roslynator.SeparatedSyntaxListSelection\<TNode>](../SeparatedSyntaxListSelection-1/README.md)
* [Roslynator.SyntaxListSelection\<TNode>](../SyntaxListSelection-1/README.md)
* [Roslynator.Text.TextLineCollectionSelection](../Text/TextLineCollectionSelection/README.md)

#### Implements

* [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor| Summary|
| --- | --- |
| [Selection(TextSpan, Int32, Int32)](-ctor/README.md) | Initializes a new instance of the \. |

## Properties

| Property| Summary|
| --- | --- |
| [Count](Count/README.md) | Gets a number of selected items\. |
| [FirstIndex](FirstIndex/README.md) | Gets an index of the first selected item\. |
| [Items](Items/README.md) | Gets an underlying list that contains selected items\. |
| [LastIndex](LastIndex/README.md) | Gets an index of the last selected item\. |
| [OriginalSpan](OriginalSpan/README.md) | Gets the original span that was used to determine selected items\. |
| [this\[Int32\]](this[]/README.md) | Gets the selected item at the specified index\. |

## Methods

| Method| Summary|
| --- | --- |
| [First()](First/README.md) | Gets the first selected item\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [Last()](Last/README.md) | Gets the last selected item\. |

