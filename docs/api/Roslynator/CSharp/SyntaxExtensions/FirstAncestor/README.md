# SyntaxExtensions\.FirstAncestor Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| FirstAncestor\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\) | Gets the first ancestor that matches the predicate\. |
| FirstAncestor\(SyntaxNode, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kind\. |
| FirstAncestor\(SyntaxNode, SyntaxKind, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kinds\. |
| FirstAncestor\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean\) | Gets the first ancestor of the specified kinds\. |

## FirstAncestor\(SyntaxNode, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kind\.

```csharp
public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| kind | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)

## FirstAncestor\(SyntaxNode, SyntaxKind, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kinds\.

```csharp
public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, bool ascendOutOfTrivia = true)
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

## FirstAncestor\(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean\)

### Summary

Gets the first ancestor of the specified kinds\.

```csharp
public static SyntaxNode FirstAncestor(this SyntaxNode node, SyntaxKind kind1, SyntaxKind kind2, SyntaxKind kind3, bool ascendOutOfTrivia = true)
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

## FirstAncestor\(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean\)

### Summary

Gets the first ancestor that matches the predicate\.

```csharp
public static SyntaxNode FirstAncestor(this SyntaxNode node, Func<SyntaxNode, bool> predicate, bool ascendOutOfTrivia = true)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| node | |
| predicate | |
| ascendOutOfTrivia | |

#### Returns

[SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode)

