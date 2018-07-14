# MemberDeclarationListSelection\.TryCreate Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| TryCreate\(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection\) | Creates a new [MemberDeclarationListSelection](../README.md) based on the specified namespace declaration and span\. |
| TryCreate\(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection\) | Creates a new [MemberDeclarationListSelection](../README.md) based on the specified type declaration and span\. |

## TryCreate\(NamespaceDeclarationSyntax, TextSpan, MemberDeclarationListSelection\)

### Summary

Creates a new [MemberDeclarationListSelection](../README.md) based on the specified namespace declaration and span\.

```csharp
public static bool TryCreate(NamespaceDeclarationSyntax namespaceDeclaration, TextSpan span, out MemberDeclarationListSelection selectedMembers)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| namespaceDeclaration | |
| span | |
| selectedMembers | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one member; otherwise, false\.

## TryCreate\(TypeDeclarationSyntax, TextSpan, MemberDeclarationListSelection\)

### Summary

Creates a new [MemberDeclarationListSelection](../README.md) based on the specified type declaration and span\.

```csharp
public static bool TryCreate(TypeDeclarationSyntax typeDeclaration, TextSpan span, out MemberDeclarationListSelection selectedMembers)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| typeDeclaration | |
| span | |
| selectedMembers | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)

True if the specified span contains at least one member; otherwise, false\.
