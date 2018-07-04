# PreprocessorDirectiveKinds Enum

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Specifies C\# preprocessor directives\.

#### Inheritance

[Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) &#x2192; [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) &#x2192; [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) &#x2192; PreprocessorDirectiveKinds

#### Attributes

[FlagsAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.flagsattribute)

## Fields

| Name | Value | Summary |
| ---- | ----- | ------- |
| None | 0 | No preprocessor directive\. |
| If | 1 | \#if preprocessor directive\. |
| Elif | 2 | \#elif preprocessor directive\. |
| Else | 4 | \#else preprocessor directive\. |
| EndIf | 8 | \#endif preprocessor directive\. |
| Region | 16 | \#region preprocessor directive\. |
| EndRegion | 32 | \#endregion preprocessor directive\. |
| Define | 64 | \#define preprocessor directive\. |
| Undef | 128 | \#undef preprocessor directive\. |
| Error | 256 | \#error preprocessor directive\. |
| Warning | 512 | \#warning preprocessor directive\. |
| Line | 1024 | \#line preprocessor directive\. |
| PragmaWarning | 2048 | \#pragma warning preprocessor directive\. |
| PragmaChecksum | 4096 | \#pragma checksum preprocessor directive\. |
| Pragma | 6144 | \#pragma preprocessor directive\. |
| Reference | 8192 | \#r preprocessor directive\. |
| Load | 16384 | \#load preprocessor directive\. |
| Bad | 32768 | Bad preprocessor directive\. |
| Shebang | 65536 | Shebang preprocessor directive\. |
| All | 131071 | All preprocessor directives\. |

## Methods

| Method | Summary |
| ------ | ------- |
| [CompareTo(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.compareto) | |
| [Equals(Object)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.equals) | |
| [GetHashCode()](https://docs.microsoft.com/en-us/dotnet/api/system.enum.gethashcode) | |
| [GetType()](https://docs.microsoft.com/en-us/dotnet/api/system.object.gettype) | |
| [GetTypeCode()](https://docs.microsoft.com/en-us/dotnet/api/system.enum.gettypecode) | |
| [HasFlag(Enum)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.hasflag) | |
| [MemberwiseClone()](https://docs.microsoft.com/en-us/dotnet/api/system.object.memberwiseclone) | |
| [ToString()](https://docs.microsoft.com/en-us/dotnet/api/system.enum.tostring) | |
| [ToString(IFormatProvider)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.tostring) | |
| [ToString(String)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.tostring) | |
| [ToString(String, IFormatProvider)](https://docs.microsoft.com/en-us/dotnet/api/system.enum.tostring) | |

