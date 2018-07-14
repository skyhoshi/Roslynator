# IfStatementOrElseClause Struct

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A wrapper for either an [IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax) or an [ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)\.

```csharp
public readonly struct IfStatementOrElseClause : System.IEquatable<IfStatementOrElseClause>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; IfStatementOrElseClause

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[IfStatementOrElseClause](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [FullSpan](FullSpan/README.md) | The absolute span of this node in characters, including its leading and trailing trivia\. |
| [IsElse](IsElse/README.md) | Determines whether this [IfStatementOrElseClause](./README.md) is wrapping an else clause\. |
| [IsIf](IsIf/README.md) | Determines whether this [IfStatementOrElseClause](./README.md) is wrapping an if statement\. |
| [Kind](Kind/README.md) | Gets an underlying node kind\. |
| [Parent](Parent/README.md) | The node that contains the underlying node in its [ChildNodes](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode.childnodes) collection\. |
| [Span](Span/README.md) | The absolute span of this node in characters, not including its leading and trailing trivia\. |
| [Statement](Statement/README.md) | Gets [Statement](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax.statement) or [Statement](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax.statement)\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [AsElse()](AsElse/README.md) | Returns the underlying else clause if this [ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax) is wrapping else clause\. |
| [AsIf()](AsIf/README.md) | Returns the underlying if statement if this [IfStatementOrElseClause](./README.md) is wrapping if statement\. |
| [Equals(IfStatementOrElseClause)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying node, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(IfStatementOrElseClause, IfStatementOrElseClause)](op_Equality/README.md) | |
| [Implicit(ElseClauseSyntax to IfStatementOrElseClause)](op_Implicit/README.md) | |
| [Implicit(IfStatementOrElseClause to ElseClauseSyntax)](op_Implicit/README.md) | |
| [Implicit(IfStatementOrElseClause to IfStatementSyntax)](op_Implicit/README.md) | |
| [Implicit(IfStatementSyntax to IfStatementOrElseClause)](op_Implicit/README.md) | |
| [Inequality(IfStatementOrElseClause, IfStatementOrElseClause)](op_Inequality/README.md) | |

