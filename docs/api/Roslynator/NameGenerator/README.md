# NameGenerator Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.dll


Provides methods to obtain an unique identifier\.

#### Inheritance

* Object
  * NameGenerator

## Constructors

| Constructor| Summary|
| --- | --- |
| [NameGenerator()](.ctor/README.md) | |

## Properties

| Property| Summary|
| --- | --- |
| [Default](Default/README.md) | Default implementation of  that adds number suffix to ensure uniqueness\. |

## Methods

| Method| Summary|
| --- | --- |
| [EnsureUniqueName(String, IEnumerable\<String>, Boolean)](EnsureUniqueName/README.md) | Returns an unique name using the specified list of reserved names\. |
| [EnsureUniqueName(String, ImmutableArray\<ISymbol>, Boolean)](EnsureUniqueName/README.md) | Returns an unique name using the specified list of symbols\. |
| [EnsureUniqueMemberName(String, SemanticModel, Int32, Boolean, CancellationToken)](EnsureUniqueMemberName/README.md) | Returns a member name that will be unique at the specified position\. |
| [EnsureUniqueMemberName(String, INamedTypeSymbol, Boolean)](EnsureUniqueMemberName/README.md) | |
| [EnsureUniqueLocalName(String, SemanticModel, Int32, Boolean, CancellationToken)](EnsureUniqueLocalName/README.md) | Return a local name that will be unique at the specified position\. |
| [IsUniqueName(String, ImmutableArray\<ISymbol>, Boolean)](IsUniqueName/README.md) | Returns true if the name is not contained in the specified list\.  is used to compare names\. |
| [IsUniqueName(String, IEnumerable\<String>, Boolean)](IsUniqueName/README.md) | Returns true if the name is not contained in the specified list\. |
| [CreateName(ITypeSymbol, Boolean)](CreateName/README.md) | Creates a syntax identifier from the specified type symbol\. |

