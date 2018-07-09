# MemberDeclarationListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected member declarations in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public sealed class MemberDeclarationListSelection : SyntaxListSelection<MemberDeclarationSyntax>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md) &#x2192; MemberDeclarationListSelection

### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>
* [IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>
* [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[MemberDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.memberdeclarationsyntax)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](../../Selection-1/Count/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [FirstIndex](../../Selection-1/FirstIndex/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Item\[Int32\]](../../Selection-1/Item/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Items](../../SyntaxListSelection-1/Items/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [LastIndex](../../Selection-1/LastIndex/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [OriginalSpan](../../Selection-1/OriginalSpan/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [Parent](Parent/README.md) | Gets a node that contains selected members\. |
| [UnderlyingList](../../SyntaxListSelection-1/UnderlyingList/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(CompilationUnitSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified compilation unit and span\. |
| [Create(NamespaceDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified namespace declaration and span\. |
| [Create(TypeDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified type declaration and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [First()](../../Selection-1/First/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [GetEnumerator()](../../SyntaxListSelection-1/GetEnumerator/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetEnumeratorCore()](../../SyntaxListSelection-1/GetEnumeratorCore/README.md) |  \(Inherited from [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md)\) |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Last()](../../Selection-1/Last/README.md) |  \(Inherited from [Selection\<T>](../../Selection-1/README.md)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [TryCreate(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified namespace declaration and span\. |
| [TryCreate(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new [MemberDeclarationListSelection](./README.md) based on the specified type declaration and span\. |

