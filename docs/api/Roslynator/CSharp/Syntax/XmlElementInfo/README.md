# XmlElementInfo Struct

Namespace: [Roslynator.CSharp.Syntax](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Provides information about a [XmlElementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.xmlelementsyntax) or [XmlEmptyElementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.xmlemptyelementsyntax)\.

```csharp
public readonly struct XmlElementInfo : System.IEquatable<XmlElementInfo>
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; XmlElementInfo

### Implements

* [IEquatable](https://docs.microsoft.com/en-us/dotnet/api/system.iequatable-1)\<[XmlElementInfo](./README.md)>

## Properties

| Property | Summary |
| -------- | ------- |
| [Element](Element/README.md) | The xml element\. |
| [IsEmptyElement](IsEmptyElement/README.md) | Determines whether the element is [XmlEmptyElement](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.xmlemptyelement)\. |
| [Kind](Kind/README.md) | Element kind\. |
| [LocalName](LocalName/README.md) | Local name of the element\. |
| [Success](Success/README.md) | Determines whether this struct was initialized with an actual syntax\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [Equals(Object)](Equals/README.md) | Determines whether this instance and a specified object are equal\. |
| [Equals(XmlElementInfo)](Equals/README.md) | Determines whether this instance is equal to another object of the same type\. |
| [GetHashCode()](GetHashCode/README.md) | Returns the hash code for this instance\. |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) |  \(Inherited from [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\) |
| [ToString()](ToString/README.md) | Returns the string representation of the underlying syntax, not including its leading and trailing trivia\. |

## Operators

| Operator | Summary |
| -------- | ------- |
| [Equality(XmlElementInfo, XmlElementInfo)](op_Equality/README.md) | |
| [Inequality(XmlElementInfo, XmlElementInfo)](op_Inequality/README.md) | |

