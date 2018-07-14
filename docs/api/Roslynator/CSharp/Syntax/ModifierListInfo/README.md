# ModifierListInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about modifier list\.

```csharp
public readonly struct ModifierListInfo : System.IEquatable<ModifierListInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; ModifierListInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[ModifierListInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [ExplicitAccessibility](ExplicitAccessibility/README.md) | The explicit accessibility\. |
| [IsAbstract](IsAbstract/README.md) | True if the modifier list contains "abstract" modifier\. |
| [IsAsync](IsAsync/README.md) | True if the modifier list contains "async" modifier\. |
| [IsConst](IsConst/README.md) | True if the modifier list contains "const" modifier\. |
| [IsExtern](IsExtern/README.md) | True if the modifier list contains "extern" modifier\. |
| [IsIn](IsIn/README.md) | True if the modifier list contains "in" modifier\. |
| [IsNew](IsNew/README.md) | True if the modifier list contains "new" modifier\. |
| [IsOut](IsOut/README.md) | True if the modifier list contains "out" modifier\. |
| [IsOverride](IsOverride/README.md) | True if the modifier list contains "override" modifier\. |
| [IsParams](IsParams/README.md) | True if the modifier list contains "params" modifier\. |
| [IsPartial](IsPartial/README.md) | True if the modifier list contains "partial" modifier\. |
| [IsReadOnly](IsReadOnly/README.md) | True if the modifier list contains "readonly" modifier\. |
| [IsRef](IsRef/README.md) | True if the modifier list contains "ref" modifier\. |
| [IsSealed](IsSealed/README.md) | True if the modifier list contains "sealed" modifier\. |
| [IsStatic](IsStatic/README.md) | True if the modifier list contains "static" modifier\. |
| [IsUnsafe](IsUnsafe/README.md) | True if the modifier list contains "unsafe" modifier\. |
| [IsVirtual](IsVirtual/README.md) | True if the modifier list contains "virtual" modifier\. |
| [IsVolatile](IsVolatile/README.md) | True if the modifier list contains "volatile" modifier\. |
| [Modifiers](Modifiers/README.md) | The modifier list\. |
| [Parent](Parent/README.md) | The node that contains the modifiers\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(ModifierListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetKinds()](GetKinds/README.md) | Gets the modifier kinds\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [WithExplicitAccessibility(Accessibility, IComparer\<SyntaxKind>)](WithExplicitAccessibility/README.md) | Creates a new [ModifierListInfo](./README.md) with accessibility modifiers updated\. |
| [WithModifiers(SyntaxTokenList)](WithModifiers/README.md) | Creates a new [ModifierListInfo](./README.md) with the specified modifiers updated\. |
| [WithoutExplicitAccessibility()](WithoutExplicitAccessibility/README.md) | Creates a new [ModifierListInfo](./README.md) with accessibility modifiers removed\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(ModifierListInfo, ModifierListInfo)](op_Equality/README.md) | |
| [Inequality(ModifierListInfo, ModifierListInfo)](op_Inequality/README.md) | |

