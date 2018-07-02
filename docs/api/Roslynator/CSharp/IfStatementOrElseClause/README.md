# IfStatementOrElseClause Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

A wrapper for either an  or an \.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IfStatementOrElseClause

#### Implements

* [IEquatable\<IfStatementOrElseClause>](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)

## Properties

| Property| Summary|
| --- | --- |
| [FullSpan](FullSpan/README.md) | The absolute span of this node in characters, including its leading and trailing trivia\. |
| [IsElse](IsElse/README.md) | Determines whether this  is wrapping an else clause\. |
| [IsIf](IsIf/README.md) | Determines whether this  is wrapping an if statement\. |
| [Kind](Kind/README.md) | Gets an underlying node kind\. |
| [Parent](Parent/README.md) | The node that contains the underlying node in its  collection\. |
| [Span](Span/README.md) | The absolute span of this node in characters, not including its leading and trailing trivia\. |
| [Statement](Statement/README.md) | Gets  or \. |

## Methods

| Method| Summary|
| --- | --- |
| [AsElse()](AsElse/README.md) | Returns the underlying else clause if this  is wrapping else clause\. |
| [AsIf()](AsIf/README.md) | Returns the underlying if statement if this  is wrapping if statement\. |
| [Equals(IfStatementOrElseClause)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying node, not including its leading and trailing trivia\. |

## Operators

| Operator| Summary|
| --- | --- |
| [implicit operator ElseClauseSyntax(IfStatementOrElseClause)](op_Implicit/README.md) | |
| [implicit operator IfStatementOrElseClause(ElseClauseSyntax)](op_Implicit/README.md) | |
| [implicit operator IfStatementOrElseClause(IfStatementSyntax)](op_Implicit/README.md) | |
| [implicit operator IfStatementSyntax(IfStatementOrElseClause)](op_Implicit/README.md) | |
| [operator !=(IfStatementOrElseClause, IfStatementOrElseClause)](op_Inequality/README.md) | |
| [operator ==(IfStatementOrElseClause, IfStatementOrElseClause)](op_Equality/README.md) | |

