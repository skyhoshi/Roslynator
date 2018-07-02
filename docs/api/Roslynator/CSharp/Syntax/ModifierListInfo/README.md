# ModifierListInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.dll

## Summary

Provides information about modifier list\.

#### Inheritance

Object &#x2192; ValueType &#x2192; ModifierListInfo

#### Implements

* IEquatable\<ModifierListInfo>

## Properties

| Property| Summary|
| --- | --- |
| [Parent](Parent/README.md) | The node that contains the modifiers\. |
| [Modifiers](Modifiers/README.md) | The modifier list\. |
| [ExplicitAccessibility](ExplicitAccessibility/README.md) | The explicit accessibility\. |
| [IsNew](IsNew/README.md) | True if the modifier list contains "new" modifier\. |
| [IsConst](IsConst/README.md) | True if the modifier list contains "const" modifier\. |
| [IsStatic](IsStatic/README.md) | True if the modifier list contains "static" modifier\. |
| [IsVirtual](IsVirtual/README.md) | True if the modifier list contains "virtual" modifier\. |
| [IsSealed](IsSealed/README.md) | True if the modifier list contains "sealed" modifier\. |
| [IsOverride](IsOverride/README.md) | True if the modifier list contains "override" modifier\. |
| [IsAbstract](IsAbstract/README.md) | True if the modifier list contains "abstract" modifier\. |
| [IsReadOnly](IsReadOnly/README.md) | True if the modifier list contains "readonly" modifier\. |
| [IsExtern](IsExtern/README.md) | True if the modifier list contains "extern" modifier\. |
| [IsUnsafe](IsUnsafe/README.md) | True if the modifier list contains "unsafe" modifier\. |
| [IsVolatile](IsVolatile/README.md) | True if the modifier list contains "volatile" modifier\. |
| [IsAsync](IsAsync/README.md) | True if the modifier list contains "async" modifier\. |
| [IsPartial](IsPartial/README.md) | True if the modifier list contains "partial" modifier\. |
| [IsRef](IsRef/README.md) | True if the modifier list contains "ref" modifier\. |
| [IsOut](IsOut/README.md) | True if the modifier list contains "out" modifier\. |
| [IsIn](IsIn/README.md) | True if the modifier list contains "in" modifier\. |
| [IsParams](IsParams/README.md) | True if the modifier list contains "params" modifier\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method| Summary|
| --- | --- |
| [WithoutExplicitAccessibility()](WithoutExplicitAccessibility/README.md) | Creates a new  with accessibility modifiers removed\. |
| [WithExplicitAccessibility(Accessibility, IComparer\<SyntaxKind>)](WithExplicitAccessibility/README.md) | Creates a new  with accessibility modifiers updated\. |
| [WithModifiers(SyntaxTokenList)](WithModifiers/README.md) | Creates a new  with the specified modifiers updated\. |
| [GetKinds()](GetKinds/README.md) | Gets the modifier kinds\. |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(ModifierListInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |

## Operators

| Operator| Summary|
| --- | --- |
| [operator ==(ModifierListInfo, ModifierListInfo)](op_Equality/README.md) | |
| [operator !=(ModifierListInfo, ModifierListInfo)](op_Inequality/README.md) | |

