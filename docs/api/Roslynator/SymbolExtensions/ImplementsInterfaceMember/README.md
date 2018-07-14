# SymbolExtensions\.ImplementsInterfaceMember Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ImplementsInterfaceMember\(ISymbol, Boolean\) | Returns true if the the symbol implements any interface member\. |
| ImplementsInterfaceMember\(ISymbol, INamedTypeSymbol, Boolean\) | Returns true if the symbol implements any member of the specified interface\. |
| ImplementsInterfaceMember\<TSymbol>\(ISymbol, Boolean\) | Returns true if the symbol implements any interface member\. |
| ImplementsInterfaceMember\<TSymbol>\(ISymbol, INamedTypeSymbol, Boolean\) | Returns true if the symbol implements any member of the specified interface\. |

## ImplementsInterfaceMember\(ISymbol, Boolean\)

### Summary

Returns true if the the symbol implements any interface member\.

```csharp
public static bool ImplementsInterfaceMember(this ISymbol symbol, bool allInterfaces = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| symbol | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## ImplementsInterfaceMember\(ISymbol, INamedTypeSymbol, Boolean\)

### Summary

Returns true if the symbol implements any member of the specified interface\.

```csharp
public static bool ImplementsInterfaceMember(this ISymbol symbol, INamedTypeSymbol interfaceSymbol, bool allInterfaces = false)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| symbol | |
| interfaceSymbol | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## ImplementsInterfaceMember\<TSymbol>\(ISymbol, Boolean\)

### Summary

Returns true if the symbol implements any interface member\.

```csharp
public static bool ImplementsInterfaceMember<TSymbol>(this ISymbol symbol, bool allInterfaces = false) where TSymbol : Microsoft.CodeAnalysis.ISymbol
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TSymbol | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| symbol | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## ImplementsInterfaceMember\<TSymbol>\(ISymbol, INamedTypeSymbol, Boolean\)

### Summary

Returns true if the symbol implements any member of the specified interface\.

```csharp
public static bool ImplementsInterfaceMember<TSymbol>(this ISymbol symbol, INamedTypeSymbol interfaceSymbol, bool allInterfaces = false) where TSymbol : Microsoft.CodeAnalysis.ISymbol
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TSymbol | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| symbol | |
| interfaceSymbol | |
| allInterfaces | If true, use [AllInterfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.allinterfaces), otherwise use [Interfaces](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol.interfaces)\. |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


