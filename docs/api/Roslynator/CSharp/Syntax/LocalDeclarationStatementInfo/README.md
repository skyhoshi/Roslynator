# LocalDeclarationStatementInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about local declaration statement\.

```csharp
public readonly struct LocalDeclarationStatementInfo : System.IEquatable<LocalDeclarationStatementInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; LocalDeclarationStatementInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[LocalDeclarationStatementInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Declaration](Declaration/README.md) | The variable declaration\. |
| [Modifiers](Modifiers/README.md) | The modifier list\. |
| [SemicolonToken](SemicolonToken/README.md) | The semicolon token\. |
| [Statement](Statement/README.md) | The local declaration statement\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |
| [Type](Type/README.md) | The type of the declaration\. |
| [Variables](Variables/README.md) | A list of variables\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(LocalDeclarationStatementInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(LocalDeclarationStatementInfo, LocalDeclarationStatementInfo)](op_Equality/README.md) | |
| [Inequality(LocalDeclarationStatementInfo, LocalDeclarationStatementInfo)](op_Inequality/README.md) | |

