# NullCheckStyles Enum

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Specifies a null check\.

```csharp
[System.FlagsAttribute]
public enum NullCheckStyles
```

### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) &#x2192; NullCheckStyles

### Attributes

[FlagsAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute)

## Fields

| Name | Value | Summary |
| ---- | ----- | ------- |
| None | 0 | No null check specified\. |
| EqualsToNull | 1 | `x == null` |
| NotEqualsToNull | 2 | `x != null` |
| ComparisonToNull | 3 | Expression that uses equality/inequality operator\. |
| IsNull | 4 | `x is null` |
| NotIsNull | 8 | `!(x is null)` |
| IsPattern | 12 | Expression that uses pattern syntax\. |
| NotHasValue | 16 | `!x.HasValue` |
| CheckingNull | 21 | Expression that checks whether an expression is null\. |
| HasValue | 32 | `x.HasValue` |
| CheckingNotNull | 42 | Expression that checks whether an expression is not null\. |
| HasValueProperty | 48 | Expression that uses [HasValue](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1.hasvalue) property\. |
| All | 63 | All null check styles\. |

