# MemberDeclarationListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected member declarations in a [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<T>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<TNode>](../../SyntaxListSelection-1/README.md) &#x2192; MemberDeclarationListSelection

#### Implements

* [IEnumerable\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Properties

| Property | Summary |
| -------- | ------- |
| [Parent](Parent/README.md) | Gets a node that contains selected members\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Create(CompilationUnitSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified compilation unit and span\. |
| [Create(NamespaceDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified namespace declaration and span\. |
| [Create(TypeDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified type declaration and span\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) | |
| [First()](../../Selection-1/First/README.md) | |
| [GetEnumerator()](../../SyntaxListSelection-1/GetEnumerator/README.md) | |
| [GetEnumeratorCore()](../../SyntaxListSelection-1/GetEnumeratorCore/README.md) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [Last()](../../Selection-1/Last/README.md) | |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) | |
| [TryCreate(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new  based on the specified namespace declaration and span\. |
| [TryCreate(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new  based on the specified type declaration and span\. |

