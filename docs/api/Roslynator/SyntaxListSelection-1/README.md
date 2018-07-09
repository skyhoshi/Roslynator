# SyntaxListSelection\<TNode> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected nodes in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public class SyntaxListSelection<TNode> : Selection<TNode> where TNode : SyntaxNode
```

### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../Selection-1/README.md) &#x2192; SyntaxListSelection\<TNode>

### Derived

* [Roslynator.CSharp.MemberDeclarationListSelection](../CSharp/MemberDeclarationListSelection/README.md)
* [Roslynator.CSharp.StatementListSelection](../CSharp/StatementListSelection/README.md)

### Implements

* [IEnumerable\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [SyntaxListSelection(SyntaxList\<TNode>, TextSpan, Int32, Int32)](-ctor/README.md) | Initializes a new instance of the [SyntaxListSelection\<TNode>](./README.md)\. |

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](../Selection-1/Count/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [FirstIndex](../Selection-1/FirstIndex/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [Item\[Int32\]](../Selection-1/Item/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [Items](Items/README.md) | Gets an underlying list that contains selected nodes\. |
| [LastIndex](../Selection-1/LastIndex/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [OriginalSpan](../Selection-1/OriginalSpan/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [UnderlyingList](UnderlyingList/README.md) | Gets an underlying list that contains selected nodes\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(SyntaxList\<TNode>, TextSpan)](Create/README.md) | Creates a new [SyntaxListSelection\<TNode>](./README.md) based on the specified list and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](../Selection-1/First/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [GetEnumerator()](GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](../Selection-1/Last/README.md) |  \(Inherited from [Selection\<T>](../Selection-1/README.md)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(SyntaxList\<TNode>, TextSpan, SyntaxListSelection\<TNode>)](TryCreate/README.md) | Creates a new [SyntaxListSelection\<TNode>](./README.md) based on the specified list and span\. |

