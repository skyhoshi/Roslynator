# TextLineCollectionSelection Class

Namespace: [Roslynator.Text](../README.md)

Assembly: Roslynator\.dll

## Summary

Represents selected lines in a \.

#### Inheritance

Object &#x2192; [Selection\<TextLine>](../../Selection-1/README.md) &#x2192; TextLineCollectionSelection

#### Implements

* IEnumerable\<TextLine>
* IReadOnlyCollection\<TextLine>
* IReadOnlyList\<TextLine>

## Constructors

| Constructor| Summary|
| --- | --- |
| [TextLineCollectionSelection(TextLineCollection, TextSpan, Int32, Int32)](.ctor/README.md) | |

## Properties

| Property| Summary|
| --- | --- |
| [UnderlyingLines](UnderlyingLines/README.md) | Gets an underlying collection that contains selected lines\. |
| [Items](Items/README.md) | Gets an underlying collection that contains selected lines\. |

## Methods

| Method| Summary|
| --- | --- |
| [Create(TextLineCollection, TextSpan)](Create/README.md) | Creates a new  based on the specified list and span\. |
| [TryCreate(TextLineCollection, TextSpan, TextLineCollectionSelection)](TryCreate/README.md) | Creates a new  based on the specified list and span\. |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |

