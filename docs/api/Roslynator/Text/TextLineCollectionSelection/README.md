# TextLineCollectionSelection Class

Namespace: [Roslynator.Text](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected lines in a [TextLineCollection](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.text.textlinecollection)\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; TextLineCollectionSelection

#### Implements

* [IEnumerable\<TextLine>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<TextLine>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<TextLine>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [TextLineCollectionSelection(TextLineCollection, TextSpan, Int32, Int32)](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [Items](Items/README.md) | Gets an underlying collection that contains selected lines\. |
| [UnderlyingLines](UnderlyingLines/README.md) | Gets an underlying collection that contains selected lines\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(TextLineCollection, TextSpan)](Create/README.md) | Creates a new  based on the specified list and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) | |
| [First()](../../Selection-1/First/README.md) | |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [Last()](../../Selection-1/Last/README.md) | |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) | |
| [TryCreate(TextLineCollection, TextSpan, TextLineCollectionSelection)](TryCreate/README.md) | Creates a new  based on the specified list and span\. |

