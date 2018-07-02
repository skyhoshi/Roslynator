# SimpleIfStatementInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about a simple if statement\.
            Simple if statement is defined as follows: it is not a child of an else clause and it has no else clause\.

#### Inheritance

Object &#x2192; ValueType &#x2192; SimpleIfStatementInfo

#### Implements

* IEquatable\<SimpleIfStatementInfo>

## Properties

| Property| Summary|
| --- | --- |
| [IfStatement](IfStatement/README.md) | The if statement\. |
| [Condition](Condition/README.md) | The condition\. |
| [Statement](Statement/README.md) | The statement\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SimpleIfStatementInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(SimpleIfStatementInfo, SimpleIfStatementInfo)](op_Equality/README.md) | |
| [operator !=(SimpleIfStatementInfo, SimpleIfStatementInfo)](op_Inequality/README.md) | |

