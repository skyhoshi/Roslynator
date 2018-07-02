# GenericInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about generic syntax \(class, struct, interface, delegate, method or local function\)\.

#### Inheritance

Object &#x2192; ValueType &#x2192; GenericInfo

#### Implements

* IEquatable\<GenericInfo>

## Properties

| Property| Summary|
| --- | --- |
| [Node](Node/README.md) | The syntax node that can be generic \(for example  for a class or  for a local function\)\. |
| [Kind](Kind/README.md) | The kind of this syntax node\. |
| [TypeParameterList](TypeParameterList/README.md) | The type parameter list\. |
| [TypeParameters](TypeParameters/README.md) | A list of type parameters\. |
| [ConstraintClauses](ConstraintClauses/README.md) | A list of constraint clauses\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [FindTypeParameter(String)](FindTypeParameter/README.md) | Searches for a type parameter with the specified name and returns the first occurrence within the type parameters\. |
| [FindConstraintClause(String)](FindConstraintClause/README.md) | Searches for a constraint clause with the specified type parameter name and returns the first occurrence within the constraint clauses\. |
| [WithTypeParameterList(TypeParameterListSyntax)](WithTypeParameterList/README.md) | Creates a new  with the type parameter list updated\. |
| [RemoveTypeParameter(TypeParameterSyntax)](RemoveTypeParameter/README.md) | Creates a new  with the specified type parameter removed\. |
| [WithConstraintClauses(SyntaxList\<TypeParameterConstraintClauseSyntax>)](WithConstraintClauses/README.md) | Creates a new  with the constraint clauses updated\. |
| [RemoveConstraintClause(TypeParameterConstraintClauseSyntax)](RemoveConstraintClause/README.md) | Creates a new  with the specified constraint clause removed\. |
| [RemoveAllConstraintClauses()](RemoveAllConstraintClauses/README.md) | Creates a new  with all constraint clauses removed\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(GenericInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(GenericInfo, GenericInfo)](op_Equality/README.md) | |
| [operator !=(GenericInfo, GenericInfo)](op_Inequality/README.md) | |

