# SimpleIfElseInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about a simple if\-else\.
            Simple if\-else is defined as follows: it is not a child of an else clause and it has an else clause and the else clause does not continue with another if statement\.

#### Inheritance

Object &#x2192; ValueType &#x2192; SimpleIfElseInfo

#### Implements

* IEquatable\<SimpleIfElseInfo>

## Properties

| Property| Summary|
| --- | --- |
| [IfStatement](IfStatement/README.md) | The if statement\. |
| [Condition](Condition/README.md) | The condition\. |
| [WhenTrue](WhenTrue/README.md) | The statement that is executed if the condition evaluates to true\. |
| [WhenFalse](WhenFalse/README.md) | The statement that is executed if the condition evaluates to false\. |
| [Else](Else/README.md) | The else clause\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SimpleIfElseInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(SimpleIfElseInfo, SimpleIfElseInfo)](op_Equality/README.md) | |
| [operator !=(SimpleIfElseInfo, SimpleIfElseInfo)](op_Inequality/README.md) | |

