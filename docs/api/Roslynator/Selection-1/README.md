# Selection\<T> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.dll


Represents consecutive sequence of selected items in a collection\.

#### Type Parameters

| Type Parameter| Summary|
| --- | --- |
| [T](T/README.md) | |

#### Inheritance

* Object
  * Selection\<T>

#### Attributes

DefaultMemberAttribute, DebuggerDisplayAttribute

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

