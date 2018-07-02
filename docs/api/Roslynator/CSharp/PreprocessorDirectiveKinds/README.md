# PreprocessorDirectiveKinds Enum

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.dll

## Summary

Specifies C\# preprocessor directives\.

#### Inheritance

Object &#x2192; ValueType &#x2192; Enum &#x2192; PreprocessorDirectiveKinds

#### Attributes

FlagsAttribute

## Fields

| Name| Value| Summary|
| --- | --- | --- |
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

## Constructors

| Constructor| Summary|
| --- | --- |
| [PreprocessorDirectiveKinds()](.ctor/README.md) | |

