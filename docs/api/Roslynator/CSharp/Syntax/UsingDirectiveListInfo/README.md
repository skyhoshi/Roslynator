# UsingDirectiveListInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about a list of using directives\.

```csharp
readonly struct UsingDirectiveListInfo
```

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; UsingDirectiveListInfo

#### Implements

* [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[UsingDirectiveSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.usingdirectivesyntax)>
* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[UsingDirectiveListInfo](./README.md)>
* [IReadOnlyCollection](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlycollection-1)\<[UsingDirectiveSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.usingdirectivesyntax)>
* [IReadOnlyList](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ireadonlylist-1)\<[UsingDirectiveSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.usingdirectivesyntax)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Count](Count/README.md) | A number of usings in the list\. |
| [Parent](Parent/README.md) | The declaration that contains the usings\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [this\[Int32\]](Item/README.md) | Gets the using directive at the specified index in the list\. |
| [Usings](Usings/README.md) | A list of usings\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Add(UsingDirectiveSyntax)](Add/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified using directive added at the end\. |
| [AddRange(IEnumerable\<UsingDirectiveSyntax>)](AddRange/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified usings added at the end\. |
| [Any()](Any/README.md) | True if the list has at least one using directive\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(UsingDirectiveListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [First()](First/README.md) | The first using directive in the list\. |
| [FirstOrDefault()](FirstOrDefault/README.md) | The first using directive in the list or null if the list is empty\. |
| [GetEnumerator()](GetEnumerator/README.md) | Gets the enumerator for the list of usings\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [IndexOf(Func\<UsingDirectiveSyntax, Boolean>)](IndexOf/README.md) | Searches for an using directive that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| [IndexOf(UsingDirectiveSyntax)](IndexOf/README.md) | The index of the using directive in the list\. |
| [Insert(Int32, UsingDirectiveSyntax)](Insert/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified using directive inserted at the index\. |
| [InsertRange(Int32, IEnumerable\<UsingDirectiveSyntax>)](InsertRange/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified usings inserted at the index\. |
| [Last()](Last/README.md) | The last using directive in the list\. |
| [LastIndexOf(Func\<UsingDirectiveSyntax, Boolean>)](LastIndexOf/README.md) | Searches for an using directive that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| [LastIndexOf(UsingDirectiveSyntax)](LastIndexOf/README.md) | Searches for an using directive and returns zero\-based index of the last occurrence in the list\. |
| [LastOrDefault()](LastOrDefault/README.md) | The last using directive in the list or null if the list is empty\. |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [Remove(UsingDirectiveSyntax)](Remove/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified using directive removed\. |
| [RemoveAt(Int32)](RemoveAt/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the using directive at the specified index removed\. |
| [RemoveNode(SyntaxNode, SyntaxRemoveOptions)](RemoveNode/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified node removed\. |
| [Replace(UsingDirectiveSyntax, UsingDirectiveSyntax)](Replace/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified using directive replaced with the new using directive\. |
| [ReplaceAt(Int32, UsingDirectiveSyntax)](ReplaceAt/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the using directive at the specified index replaced with a new using directive\. |
| [ReplaceNode(SyntaxNode, SyntaxNode)](ReplaceNode/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified old node replaced with a new node\. |
| [ReplaceRange(UsingDirectiveSyntax, IEnumerable\<UsingDirectiveSyntax>)](ReplaceRange/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the specified using directive replaced with new usings\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [WithUsings(IEnumerable\<UsingDirectiveSyntax>)](WithUsings/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the usings updated\. |
| [WithUsings(SyntaxList\<UsingDirectiveSyntax>)](WithUsings/README.md) | Creates a new [UsingDirectiveListInfo](./README.md) with the usings updated\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [operator !=(UsingDirectiveListInfo, UsingDirectiveListInfo)](op_Inequality/README.md) | |
| [operator ==(UsingDirectiveListInfo, UsingDirectiveListInfo)](op_Equality/README.md) | |

## Explicit Interface Implementations

| Member | Summary |
| ------ | ------- |
| [IEnumerable.GetEnumerator()](System-Collections-IEnumerable-GetEnumerator/README.md) | |
| [IEnumerable\<UsingDirectiveSyntax>.GetEnumerator()](System-Collections-Generic-IEnumerable-1-GetEnumerator/README.md) | |

