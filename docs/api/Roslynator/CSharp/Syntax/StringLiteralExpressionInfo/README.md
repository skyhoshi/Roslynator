# StringLiteralExpressionInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about string literal expression\.

```csharp
public readonly struct StringLiteralExpressionInfo : System.IEquatable<StringLiteralExpressionInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; StringLiteralExpressionInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[StringLiteralExpressionInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [ContainsEscapeSequence](ContainsEscapeSequence/README.md) | True if the string literal expression contains escape sequence\. |
| [ContainsLinefeed](ContainsLinefeed/README.md) | True if the string literal contains linefeed\. |
| [Expression](Expression/README.md) | The string literal expression\. |
| [InnerText](InnerText/README.md) | The token text, not including leading ampersand, if any, and enclosing quotation marks\. |
| [IsRegular](IsRegular/README.md) | True if this instance is regular string literal expression\. |
| [IsVerbatim](IsVerbatim/README.md) | True if this instance is verbatim string literal expression\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Text](Text/README.md) | The token text\. |
| [Token](Token/README.md) | The token representing the string literal expression\. |
| [ValueText](ValueText/README.md) | The token value text\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(StringLiteralExpressionInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(StringLiteralExpressionInfo, StringLiteralExpressionInfo)](op_Equality/README.md) | |
| [Inequality(StringLiteralExpressionInfo, StringLiteralExpressionInfo)](op_Inequality/README.md) | |

