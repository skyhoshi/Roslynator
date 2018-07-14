# SymbolExtensions\.ToMinimalTypeSyntax Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ToMinimalTypeSyntax\(INamespaceOrTypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol |
| ToMinimalTypeSyntax\(INamespaceSymbol, SemanticModel, Int32, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\. |
| ToMinimalTypeSyntax\(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |

## ToMinimalTypeSyntax\(INamespaceOrTypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol

```csharp
public static TypeSyntax ToMinimalTypeSyntax(this INamespaceOrTypeSymbol namespaceOrTypeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| namespaceOrTypeSymbol | |
| semanticModel | |
| position | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


## ToMinimalTypeSyntax\(INamespaceSymbol, SemanticModel, Int32, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\.

```csharp
public static TypeSyntax ToMinimalTypeSyntax(this INamespaceSymbol namespaceSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| namespaceSymbol | |
| semanticModel | |
| position | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


## ToMinimalTypeSyntax\(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat\)

### Summary

Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\.

```csharp
public static TypeSyntax ToMinimalTypeSyntax(this ITypeSymbol typeSymbol, SemanticModel semanticModel, int position, SymbolDisplayFormat format = null)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| typeSymbol | |
| semanticModel | |
| position | |
| format | |

#### Returns

[TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax)


