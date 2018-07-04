# NameGenerator Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides methods to obtain an unique identifier\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; NameGenerator

## Constructors

| Constructor | Summary |
| ----------- | ------- |
| [NameGenerator()](-ctor/README.md) | |

## Properties

| Property | Summary |
| -------- | ------- |
| [Default](Default/README.md) | Default implementation of  that adds number suffix to ensure uniqueness\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [CreateName(ITypeSymbol, Boolean)](CreateName/README.md) | Creates a syntax identifier from the specified type symbol\. |
| [EnsureUniqueLocalName(String, SemanticModel, Int32, Boolean, CancellationToken)](EnsureUniqueLocalName/README.md) | Return a local name that will be unique at the specified position\. |
| [EnsureUniqueMemberName(String, INamedTypeSymbol, Boolean)](EnsureUniqueMemberName/README.md) | |
| [EnsureUniqueMemberName(String, SemanticModel, Int32, Boolean, CancellationToken)](EnsureUniqueMemberName/README.md) | Returns a member name that will be unique at the specified position\. |
| [EnsureUniqueName(String, IEnumerable\<String>, Boolean)](EnsureUniqueName/README.md) | Returns an unique name using the specified list of reserved names\. |
| [EnsureUniqueName(String, ImmutableArray\<ISymbol>, Boolean)](EnsureUniqueName/README.md) | Returns an unique name using the specified list of symbols\. |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.object.equals) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [IsUniqueName(String, IEnumerable\<String>, Boolean)](IsUniqueName/README.md) | Returns true if the name is not contained in the specified list\. |
| [IsUniqueName(String, ImmutableArray\<ISymbol>, Boolean)](IsUniqueName/README.md) | Returns true if the name is not contained in the specified list\.  is used to compare names\. |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.object.tostring) | |

