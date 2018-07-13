# MemberDeclarationListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected member declarations in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public sealed class MemberDeclarationListSelection : Roslynator.SyntaxListSelection<Microsoft.CodeAnalysis.CSharp.Syntax.MemberDeclarationSyntax>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md) &#x2192; MemberDeclarationListSelection

### Implements

* [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>
* [IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>
* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](../../Selection-1/Count/README.md) | Gets a number of selected items\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [FirstIndex](../../Selection-1/FirstIndex/README.md) | Gets an index of the first selected item\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Item\[Int32\]](../../Selection-1/Item/README.md) | Gets the selected item at the specified index\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Items](../../SyntaxListSelection-1/Items/README.md) | Gets an underlying list that contains selected nodes\. \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [LastIndex](../../Selection-1/LastIndex/README.md) | Gets an index of the last selected item\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [OriginalSpan](../../Selection-1/OriginalSpan/README.md) | Gets the original span that was used to determine selected items\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Parent](Parent/README.md) | Gets a node that contains selected members\. |
| [UnderlyingList](../../SyntaxListSelection-1/UnderlyingList/README.md) | Gets an underlying list that contains selected nodes\. \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(CompilationUnitSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified compilation unit and span\. |
| [Create(NamespaceDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified namespace declaration and span\. |
| [Create(TypeDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified type declaration and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](../../Selection-1/First/README.md) | Gets the first selected item\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [GetEnumerator()](../../SyntaxListSelection-1/GetEnumerator/README.md) | Returns an enumerator that iterates through selected items\. \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetEnumeratorCore()](../../SyntaxListSelection-1/GetEnumeratorCore/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](../../Selection-1/Last/README.md) | Gets the last selected item\. \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified namespace declaration and span\. |
| [TryCreate(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified type declaration and span\. |

