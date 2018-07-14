# SyntaxExtensions\.FirstAncestorOrSelf Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| FirstAncestorOrSelf\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\) | Gets the first ancestor that matches the predicate\. |
| FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kind\. |
| FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kinds\. |
| FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kinds\. |

## FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kind\.

```csharp
public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)


## FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kinds\.

```csharp
public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)


## FirstAncestorOrSelf\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kinds\.

```csharp
public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind1 | |
| kind2 | |
| kind3 | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)


## FirstAncestorOrSelf\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Gets the first ancestor that matches the predicate\.

```csharp
public static SyntaxNode FirstAncestorOrSelf(this SyntaxNode node, Func<SyntaxNode, bool> predicate, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| predicate | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)


