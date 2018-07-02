# IfStatementOrElseClause Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

A wrapper for either an  or an \.

#### Inheritance

Object &#x2192; ValueType &#x2192; IfStatementOrElseClause

#### Implements

* IEquatable\<IfStatementOrElseClause>

## Properties

| Property| Summary|
| --- | --- |
| [Kind](Kind/README.md) | Gets an underlying node kind\. |
| [IsIf](IsIf/README.md) | Determines whether this  is wrapping an if statement\. |
| [IsElse](IsElse/README.md) | Determines whether this  is wrapping an else clause\. |
| [Statement](Statement/README.md) | Gets  or \. |
| [Parent](Parent/README.md) | The node that contains the underlying node in its  collection\. |
| [Span](Span/README.md) | The absolute span of this node in characters, not including its leading and trailing trivia\. |
| [FullSpan](FullSpan/README.md) | The absolute span of this node in characters, including its leading and trailing trivia\. |

## Methods

| Method| Summary|
| --- | --- |
| [AsIf()](AsIf/README.md) | Returns the underlying if statement if this  is wrapping if statement\. |
| [AsElse()](AsElse/README.md) | Returns the underlying else clause if this  is wrapping else clause\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying node, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(IfStatementOrElseClause)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(IfStatementOrElseClause, IfStatementOrElseClause)](op_Equality/README.md) | |
| [operator !=(IfStatementOrElseClause, IfStatementOrElseClause)](op_Inequality/README.md) | |
| [implicit operator IfStatementOrElseClause(IfStatementSyntax)](op_Implicit/README.md) | |
| [implicit operator IfStatementSyntax(IfStatementOrElseClause)](op_Implicit/README.md) | |
| [implicit operator IfStatementOrElseClause(ElseClauseSyntax)](op_Implicit/README.md) | |
| [implicit operator ElseClauseSyntax(IfStatementOrElseClause)](op_Implicit/README.md) | |

