# StringLiteralExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll


Provides information about string literal expression\.

#### Inheritance

* Object
  * ValueType
    * StringLiteralExpressionInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| [Expression](Expression/README.md) | The string literal expression\. |
| [Token](Token/README.md) | The token representing the string literal expression\. |
| [Text](Text/README.md) | The token text\. |
| [InnerText](InnerText/README.md) | The token text, not including leading ampersand, if any, and enclosing quotation marks\. |
| [ValueText](ValueText/README.md) | The token value text\. |
| [IsRegular](IsRegular/README.md) | True if this instance is regular string literal expression\. |
| [IsVerbatim](IsVerbatim/README.md) | True if this instance is verbatim string literal expression\. |
| [ContainsLinefeed](ContainsLinefeed/README.md) | True if the string literal contains linefeed\. |
| [ContainsEscapeSequence](ContainsEscapeSequence/README.md) | True if the string literal expression contains escape sequence\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(StringLiteralExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(StringLiteralExpressionInfo, StringLiteralExpressionInfo)](op_Equality/README.md) | |
| [operator !=(StringLiteralExpressionInfo, StringLiteralExpressionInfo)](op_Inequality/README.md) | |

