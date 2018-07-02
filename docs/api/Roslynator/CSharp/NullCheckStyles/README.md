# NullCheckStyles Enum

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll


Specifies a null check\.

#### Inheritance

* Object
  * ValueType
    * Enum
      * NullCheckStyles

#### Attributes

FlagsAttribute

## Fields

| Name| Value| Summary|
| --- | --- | --- |
| None | 0 | No null check specified\. |
| EqualsToNull | 1 | x == null |
| NotEqualsToNull | 2 | x \!= null |
| ComparisonToNull | 3 | Expression that uses equality/inequality operator\. |
| IsNull | 4 | x is null |
| NotIsNull | 8 | \!\(x is null\) |
| IsPattern | 12 | Expression that uses pattern syntax\. |
| NotHasValue | 16 | \!x\.HasValue |
| CheckingNull | 21 | Expression that checks whether an expression is null\. |
| HasValue | 32 | x\.HasValue |
| CheckingNotNull | 42 | Expression that checks whether an expression is not null\. |
| HasValueProperty | 48 | Expression that uses  property\. |
| All | 63 | All null check styles\. |

## Constructors

| Constructor| Summary|
| --- | --- |
| [NullCheckStyles()](.ctor/README.md) | |

