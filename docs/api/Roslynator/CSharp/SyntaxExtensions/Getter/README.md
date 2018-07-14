# SyntaxExtensions\.Getter Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Getter\(AccessorListSyntax\) | Returns a get accessor contained in the specified list\. |
| Getter\(IndexerDeclarationSyntax\) | Returns a get accessor that is contained in the specified indexer declaration\. |
| Getter\(PropertyDeclarationSyntax\) | Returns property get accessor, if any\. |

## Getter\(AccessorListSyntax\)

### Summary

Returns a get accessor contained in the specified list\.

```csharp
public static AccessorDeclarationSyntax Getter(this AccessorListSyntax accessorList)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| accessorList | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


## Getter\(IndexerDeclarationSyntax\)

### Summary

Returns a get accessor that is contained in the specified indexer declaration\.

```csharp
public static AccessorDeclarationSyntax Getter(this IndexerDeclarationSyntax indexerDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| indexerDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


## Getter\(PropertyDeclarationSyntax\)

### Summary

Returns property get accessor, if any\.

```csharp
public static AccessorDeclarationSyntax Getter(this PropertyDeclarationSyntax propertyDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| propertyDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


