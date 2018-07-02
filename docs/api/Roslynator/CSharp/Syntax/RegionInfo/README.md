# RegionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about a region\.

#### Inheritance

* Object
  * ValueType
    * RegionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [Directive](Directive/README.md) | \#region directive\. |
| [EndDirective](EndDirective/README.md) | \#endregion directive\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Span](Span/README.md) | The absolute span of this region, not including its leading and trailing trivia\. |
| [FullSpan](FullSpan/README.md) | The absolute span of this region, including its leading and trailing trivia\. |
| [IsEmpty](IsEmpty/README.md) | Determines whether this region is empty, i\.e\. contains only white\-space\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(RegionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(RegionInfo, RegionInfo)](op_Equality/README.md) | |
| [operator !=(RegionInfo, RegionInfo)](op_Inequality/README.md) | |

