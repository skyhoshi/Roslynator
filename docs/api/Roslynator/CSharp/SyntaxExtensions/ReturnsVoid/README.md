# SyntaxExtensions\.ReturnsVoid Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ReturnsVoid\(DelegateDeclarationSyntax\) | Returns true the specified delegate return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| ReturnsVoid\(LocalFunctionStatementSyntax\) | Returns true if the specified local function' return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| ReturnsVoid\(MethodDeclarationSyntax\) | Returns true if the specified method return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |

## ReturnsVoid\(DelegateDeclarationSyntax\)

### Summary

Returns true the specified delegate return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\.

```csharp
public static bool ReturnsVoid(this DelegateDeclarationSyntax delegateDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| delegateDeclaration | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## ReturnsVoid\(LocalFunctionStatementSyntax\)

### Summary

Returns true if the specified local function' return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\.

```csharp
public static bool ReturnsVoid(this LocalFunctionStatementSyntax localFunctionStatement)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| localFunctionStatement | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## ReturnsVoid\(MethodDeclarationSyntax\)

### Summary

Returns true if the specified method return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\.

```csharp
public static bool ReturnsVoid(this MethodDeclarationSyntax methodDeclaration)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| methodDeclaration | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


