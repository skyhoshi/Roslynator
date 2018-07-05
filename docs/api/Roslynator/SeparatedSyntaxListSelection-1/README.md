# SeparatedSyntaxListSelection\<TNode> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected nodes in a [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\.

```csharp
class SeparatedSyntaxListSelection<TNode> where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../Selection-1/README.md) &#x2192; SeparatedSyntaxListSelection\<TNode>

#### Implements

* [IEnumerable\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [SeparatedSyntaxListSelection(SeparatedSyntaxList\<TNode>, TextSpan, Int32, Int32)](-ctor/README.md) | Initializes a new instance of the [SeparatedSyntaxListSelection\<TNode>](./README.md)\. |

## Properties

| Property | Summary |
| -------- | ------- |
| [Items](Items/README.md) | Gets an underlying list that contains selected nodes\. |
| [UnderlyingList](UnderlyingList/README.md) | Gets an underlying list that contains selected nodes\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(SeparatedSyntaxList\<TNode>, TextSpan)](Create/README.md) | Creates a new [SeparatedSyntaxListSelection\<TNode>](./README.md) based on the specified list and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](../Selection-1/First/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](../Selection-1/Last/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(SeparatedSyntaxList\<TNode>, TextSpan, SeparatedSyntaxListSelection\<TNode>)](TryCreate/README.md) | Creates a new [SeparatedSyntaxListSelection\<TNode>](./README.md) based on the specified list and span\. |

