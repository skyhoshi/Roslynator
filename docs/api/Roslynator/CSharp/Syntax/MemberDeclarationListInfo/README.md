# MemberDeclarationListInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about a list of member declaration list\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; MemberDeclarationListInfo

#### Implements

* [IEnumerable\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)
* [IEquatable\<MemberDeclarationListInfo>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)
* [IReadOnlyCollection\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)
* [IReadOnlyList\<MemberDeclarationSyntax>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](Count/README.md) | A number of members in the list\. |
| [Members](Members/README.md) | A list of members\. |
| [Parent](Parent/README.md) | The declaration that contains the members\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [this\[Int32\]](this[]/README.md) | Gets the member at the specified index in the list\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Add(MemberDeclarationSyntax)](Add/README.md) | Creates a new  with the specified member added at the end\. |
| [AddRange(IEnumerable\<MemberDeclarationSyntax>)](AddRange/README.md) | Creates a new  with the specified members added at the end\. |
| [Any()](Any/README.md) | True if the list has at least one member\. |
| [Equals(MemberDeclarationListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [First()](First/README.md) | The first member in the list\. |
| [FirstOrDefault()](FirstOrDefault/README.md) | The first member in the list or null if the list is empty\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator for the list of members\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [IndexOf(Func\<MemberDeclarationSyntax, Boolean>)](IndexOf/README.md) | Searches for a member that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| [IndexOf(MemberDeclarationSyntax)](IndexOf/README.md) | The index of the member in the list\. |
| [Insert(Int32, MemberDeclarationSyntax)](Insert/README.md) | Creates a new  with the specified member inserted at the index\. |
| [InsertRange(Int32, IEnumerable\<MemberDeclarationSyntax>)](InsertRange/README.md) | Creates a new  with the specified members inserted at the index\. |
| [Last()](Last/README.md) | The last member in the list\. |
| [LastIndexOf(Func\<MemberDeclarationSyntax, Boolean>)](LastIndexOf/README.md) | Searches for a member that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| [LastIndexOf(MemberDeclarationSyntax)](LastIndexOf/README.md) | Searches for a member and returns zero\-based index of the last occurrence in the list\. |
| [LastOrDefault()](LastOrDefault/README.md) | The last member in the list or null if the list is empty\. |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [Remove(MemberDeclarationSyntax)](Remove/README.md) | Creates a new  with the specified member removed\. |
| [RemoveAt(Int32)](RemoveAt/README.md) | Creates a new  with the member at the specified index removed\. |
| [RemoveNode(SyntaxNode, SyntaxRemoveOptions)](RemoveNode/README.md) | Creates a new  with the specified node removed\. |
| [Replace(MemberDeclarationSyntax, MemberDeclarationSyntax)](Replace/README.md) | Creates a new  with the specified member replaced with the new member\. |
| [ReplaceAt(Int32, MemberDeclarationSyntax)](ReplaceAt/README.md) | Creates a new  with the member at the specified index replaced with a new member\. |
| [ReplaceNode(SyntaxNode, SyntaxNode)](ReplaceNode/README.md) | Creates a new  with the specified old node replaced with a new node\. |
| [ReplaceRange(MemberDeclarationSyntax, IEnumerable\<MemberDeclarationSyntax>)](ReplaceRange/README.md) | Creates a new  with the specified member replaced with new members\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [WithMembers(IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new  with the members updated\. |
| [WithMembers(SyntaxList\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new  with the members updated\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [operator !=(MemberDeclarationListInfo, MemberDeclarationListInfo)](op_Inequality/README.md) | |
| [operator ==(MemberDeclarationListInfo, MemberDeclarationListInfo)](op_Equality/README.md) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [GetEnumerator()](System-Collections-Generic-IEnumerable-1-GetEnumerator/README.md) | |
| [GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |

