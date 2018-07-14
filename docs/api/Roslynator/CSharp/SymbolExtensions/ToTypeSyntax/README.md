# SymbolExtensions\.ToTypeSyntax Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ToTypeSyntax\(INamespaceOrTypeSymbol, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol\. |
| ToTypeSyntax\(INamespaceSymbol, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\. |
| ToTypeSyntax\(ITypeSymbol, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |

## ToTypeSyntax\(INamespaceOrTypeSymbol, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol\.

```csharp
public static TypeSyntax ToTypeSyntax(this INamespaceOrTypeSymbol namespaceOrTypeSymbol, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| namespaceOrTypeSymbol | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


## ToTypeSyntax\(INamespaceSymbol, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\.

```csharp
public static TypeSyntax ToTypeSyntax(this INamespaceSymbol namespaceSymbol, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| namespaceSymbol | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


## ToTypeSyntax\(ITypeSymbol, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\.

```csharp
public static TypeSyntax ToTypeSyntax(this ITypeSymbol typeSymbol, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| typeSymbol | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


