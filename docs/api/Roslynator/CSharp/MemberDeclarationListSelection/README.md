# MemberDeclarationListSelection Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Represents selected member declarations in a \.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [Selection\<MemberDeclarationSyntax>](../../Selection-1/README.md) &#x2192; [SyntaxListSelection\<MemberDeclarationSyntax>](../../SyntaxListSelection-1/README.md) &#x2192; MemberDeclarationListSelection

#### Implements

* [IEnumerable\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IReadOnlyCollection\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Properties

| Property| Summary|
| --- | --- |
| [Parent](Parent/README.md) | Gets a node that contains selected members\. |

## Methods

| Method| Summary|
| --- | --- |
| [Create(CompilationUnitSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified compilation unit and span\. |
| [Create(NamespaceDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified namespace declaration and span\. |
| [Create(TypeDeclarationSyntax, TextSpan)](Create/README.md) | Creates a new  based on the specified type declaration and span\. |
| [TryCreate(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new  based on the specified namespace declaration and span\. |
| [TryCreate(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection)](TryCreate/README.md) | Creates a new  based on the specified type declaration and span\. |

