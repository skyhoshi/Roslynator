# CSharpExtensions\.GetSymbol Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetSymbol\(SemanticModel, AttributeSyntax, CancellationToken\) | Returns what symbol, if any, the specified attribute syntax bound to\. |
| GetSymbol\(SemanticModel, ConstructorInitializerSyntax, CancellationToken\) | Returns what symbol, if any, the specified constructor initializer syntax bound to\. |
| GetSymbol\(SemanticModel, CrefSyntax, CancellationToken\) | Returns what symbol, if any, the specified cref syntax bound to\. |
| GetSymbol\(SemanticModel, ExpressionSyntax, CancellationToken\) | Returns what symbol, if any, the specified expression syntax bound to\. |
| GetSymbol\(SemanticModel, OrderingSyntax, CancellationToken\) | Returns what symbol, if any, the specified ordering syntax bound to\. |
| GetSymbol\(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken\) | Returns what symbol, if any, the specified select or group clause bound to\. |

## GetSymbol\(SemanticModel, AttributeSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified attribute syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, AttributeSyntax attribute, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| attribute | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


## GetSymbol\(SemanticModel, ConstructorInitializerSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified constructor initializer syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, ConstructorInitializerSyntax constructorInitializer, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| constructorInitializer | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


## GetSymbol\(SemanticModel, CrefSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified cref syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, CrefSyntax cref, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| cref | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


## GetSymbol\(SemanticModel, ExpressionSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified expression syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, ExpressionSyntax expression, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| expression | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


## GetSymbol\(SemanticModel, OrderingSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified ordering syntax bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, OrderingSyntax ordering, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| ordering | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


## GetSymbol\(SemanticModel, SelectOrGroupClauseSyntax, CancellationToken\)

### Summary

Returns what symbol, if any, the specified select or group clause bound to\.

```csharp
public static ISymbol GetSymbol(this SemanticModel semanticModel, SelectOrGroupClauseSyntax selectOrGroupClause, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| semanticModel | |
| selectOrGroupClause | |
| cancellationToken | |

#### Returns

[ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol)


