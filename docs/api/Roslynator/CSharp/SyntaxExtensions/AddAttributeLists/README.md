# SyntaxExtensions\.AddAttributeLists Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| AddAttributeLists\(ClassDeclarationSyntax, Boolean, AttributeListSyntax\[\]\) | Creates a new class declaration with the specified attribute lists added\. |
| AddAttributeLists\(InterfaceDeclarationSyntax, Boolean, AttributeListSyntax\[\]\) | Creates a new interface declaration with the specified attribute lists added\. |
| AddAttributeLists\(StructDeclarationSyntax, Boolean, AttributeListSyntax\[\]\) | Creates a new struct declaration with the specified attribute lists added\. |

## AddAttributeLists\(ClassDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)

### Summary

Creates a new class declaration with the specified attribute lists added\.

```csharp
public static ClassDeclarationSyntax AddAttributeLists(this ClassDeclarationSyntax classDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| classDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[ClassDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.classdeclarationsyntax)




## AddAttributeLists\(InterfaceDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)

### Summary

Creates a new interface declaration with the specified attribute lists added\.

```csharp
public static InterfaceDeclarationSyntax AddAttributeLists(this InterfaceDeclarationSyntax interfaceDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| interfaceDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[InterfaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.interfacedeclarationsyntax)




## AddAttributeLists\(StructDeclarationSyntax, Boolean, AttributeListSyntax\[\]\)

### Summary

Creates a new struct declaration with the specified attribute lists added\.

```csharp
public static StructDeclarationSyntax AddAttributeLists(this StructDeclarationSyntax structDeclaration, bool keepDocumentationCommentOnTop, params AttributeListSyntax[] attributeLists)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| structDeclaration | |
| keepDocumentationCommentOnTop | If the declaration has no attribute lists and has a documentation comment the specified attribute lists will be inserted after the documentation comment\. |
| attributeLists | |

#### Returns

[StructDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.structdeclarationsyntax)




