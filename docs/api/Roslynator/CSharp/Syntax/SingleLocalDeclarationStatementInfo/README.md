# SingleLocalDeclarationStatementInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about a local declaration statement with a single variable\.

#### Inheritance

Object &#x2192; ValueType &#x2192; SingleLocalDeclarationStatementInfo

#### Implements

* IEquatable\<SingleLocalDeclarationStatementInfo>

## Properties

| Property| Summary|
| --- | --- |
| [Statement](Statement/README.md) | The local declaration statement\. |
| [Declarator](Declarator/README.md) | The variable declarator\. |
| [Declaration](Declaration/README.md) | The variable declaration\. |
| [Initializer](Initializer/README.md) | The variable initializer, if any\. |
| [Value](Value/README.md) | The initialized value, if any\. |
| [Modifiers](Modifiers/README.md) | The modifier list\. |
| [Type](Type/README.md) | The type of a declaration\. |
| [Identifier](Identifier/README.md) | Variable identifier\. |
| [IdentifierText](IdentifierText/README.md) | Variable name\. |
| [EqualsToken](EqualsToken/README.md) | The equals token\. |
| [SemicolonToken](SemicolonToken/README.md) | The semicolon\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SingleLocalDeclarationStatementInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(SingleLocalDeclarationStatementInfo, SingleLocalDeclarationStatementInfo)](op_Equality/README.md) | |
| [operator !=(SingleLocalDeclarationStatementInfo, SingleLocalDeclarationStatementInfo)](op_Inequality/README.md) | |

