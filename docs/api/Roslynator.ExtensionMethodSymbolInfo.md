# ExtensionMethodSymbolInfo Struct

Namespace: Roslynator
Assembly: Roslynator\.dll

Represents an extension method symbol\.

#### Inheritance

* Object
  * ValueType
    * ExtensionMethodSymbolInfo

#### Attributes

DebuggerDisplayAttribute

## Properties

| Property| Summary|
| --- | --- |
| ReducedSymbol | The definition of extension method from which this symbol was reduced, or null, if the symbol was not reduced\. |
| Symbol | The extension method symbol\. |
| ReducedSymbolOrSymbol | The reduced symbol or the symbol if the reduced symbol is null\. |
| IsReduced | True if the symbol was reduced\. |

## Methods

| Method| Summary|
| --- | --- |
| Equals\(Object\) | |
| Equals\(ExtensionMethodSymbolInfo\) | |
| GetHashCode\(\) | |

## Operators

| Operator| Summary|
| --- | --- |
| operator ==\(ExtensionMethodSymbolInfo, ExtensionMethodSymbolInfo\) | |
| operator \!=\(ExtensionMethodSymbolInfo, ExtensionMethodSymbolInfo\) | |

