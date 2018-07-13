# SyntaxExtensions\.Setter Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Setter\(AccessorListSyntax\) | Returns a set accessor contained in the specified list\. |
| Setter\(IndexerDeclarationSyntax\) | Returns a set accessor that is contained in the specified indexer declaration\. |
| Setter\(PropertyDeclarationSyntax\) | Returns property set accessor, if any\. |

## Setter\(AccessorListSyntax\)

### Summary

Returns a set accessor contained in the specified list\.

```csharp
public static AccessorDeclarationSyntax Setter(this AccessorListSyntax accessorList)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| accessorList | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


## Setter\(IndexerDeclarationSyntax\)

### Summary

Returns a set accessor that is contained in the specified indexer declaration\.

```csharp
public static AccessorDeclarationSyntax Setter(this IndexerDeclarationSyntax indexerDeclaration)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| indexerDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


## Setter\(PropertyDeclarationSyntax\)

### Summary

Returns property set accessor, if any\.

```csharp
public static AccessorDeclarationSyntax Setter(this PropertyDeclarationSyntax propertyDeclaration)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| propertyDeclaration | |

#### Returns

[AccessorDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.accessordeclarationsyntax)


