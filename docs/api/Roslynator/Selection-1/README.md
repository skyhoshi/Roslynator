# Selection\<T> Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents consecutive sequence of selected items in a collection\.

```csharp
public abstract class Selection<T> :
    System.Collections.Generic.IReadOnlyList<T>,
    System.Collections.Generic.IReadOnlyCollection<T>,
    System.Collections.Generic.IEnumerable<T>
```

### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| T | |

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; Selection\<T>

### Derived

* [Roslynator.CSharp.MemberDeclarationListSelection](../CSharp/MemberDeclarationListSelection/README.md)
* [Roslynator.CSharp.StatementListSelection](../CSharp/StatementListSelection/README.md)
* [Roslynator.SeparatedSyntaxListSelection\<TNode>](../SeparatedSyntaxListSelection-1/README.md)
* [Roslynator.SyntaxListSelection\<TNode>](../SyntaxListSelection-1/README.md)
* [Roslynator.Text.TextLineCollectionSelection](../Text/TextLineCollectionSelection/README.md)

### Implements

* [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [Selection(TextSpan, Int32, Int32)](-ctor/README.md) | Initializes a new instance of the [Selection\<T>](./README.md)\. |

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](Count/README.md) | Gets a number of selected items\. |
| [FirstIndex](FirstIndex/README.md) | Gets an index of the first selected item\. |
| [Item\[Int32\]](Item/README.md) | Gets the selected item at the specified index\. |
| [Items](Items/README.md) | Gets an underlying list that contains selected items\. |
| [LastIndex](LastIndex/README.md) | Gets an index of the last selected item\. |
| [OriginalSpan](OriginalSpan/README.md) | Gets the original span that was used to determine selected items\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](First/README.md) | Gets the first selected item\. |
| [GetEnumeratorCore()](GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](Last/README.md) | Gets the last selected item\. |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<T>.GetEnumerator()](System-Collections-Generic-IEnumerable-T--GetEnumerator/README.md) | |

