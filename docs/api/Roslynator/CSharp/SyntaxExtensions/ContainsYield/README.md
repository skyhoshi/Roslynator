# SyntaxExtensions\.ContainsYield Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ContainsYield\(LocalFunctionStatementSyntax\) | Returns true if the specified local function contains yield statement\. Nested local functions are excluded\. |
| ContainsYield\(MethodDeclarationSyntax\) | Returns true if the specified method contains yield statement\. Nested local functions are excluded\. |

## ContainsYield\(LocalFunctionStatementSyntax\)

### Summary

Returns true if the specified local function contains yield statement\. Nested local functions are excluded\.

```csharp
public static bool ContainsYield(this LocalFunctionStatementSyntax localFunctionStatement)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| localFunctionStatement | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## ContainsYield\(MethodDeclarationSyntax\)

### Summary

Returns true if the specified method contains yield statement\. Nested local functions are excluded\.

```csharp
public static bool ContainsYield(this MethodDeclarationSyntax methodDeclaration)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| methodDeclaration | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




