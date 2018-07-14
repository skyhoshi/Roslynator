# SingleLocalDeclarationStatementInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about a local declaration statement with a single variable\.

```csharp
public readonly struct SingleLocalDeclarationStatementInfo : System.IEquatable<SingleLocalDeclarationStatementInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; SingleLocalDeclarationStatementInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[SingleLocalDeclarationStatementInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Declaration](Declaration/README.md) | The variable declaration\. |
| [Declarator](Declarator/README.md) | The variable declarator\. |
| [EqualsToken](EqualsToken/README.md) | The equals token\. |
| [Identifier](Identifier/README.md) | Variable identifier\. |
| [IdentifierText](IdentifierText/README.md) | Variable name\. |
| [Initializer](Initializer/README.md) | The variable initializer, if any\. |
| [Modifiers](Modifiers/README.md) | The modifier list\. |
| [SemicolonToken](SemicolonToken/README.md) | The semicolon\. |
| [Statement](Statement/README.md) | The local declaration statement\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Type](Type/README.md) | The type of a declaration\. |
| [Value](Value/README.md) | The initialized value, if any\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(SingleLocalDeclarationStatementInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(SingleLocalDeclarationStatementInfo, SingleLocalDeclarationStatementInfo)](op_Equality/README.md) | |
| [Inequality(SingleLocalDeclarationStatementInfo, SingleLocalDeclarationStatementInfo)](op_Inequality/README.md) | |

