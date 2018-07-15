# MemberDeclarationListInfo\.LastIndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| LastIndexOf\(Func\<MemberDeclarationSyntax, Boolean>\) | Searches for a member that matches the predicate and returns returns zero\-based index of the last occurrence in the list\. |
| LastIndexOf\(MemberDeclarationSyntax\) | Searches for a member and returns zero\-based index of the last occurrence in the list\. |

## LastIndexOf\(Func\<MemberDeclarationSyntax, Boolean>\)

### Summary

Searches for a member that matches the predicate and returns returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(Func<MemberDeclarationSyntax, bool> predicate)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

## LastIndexOf\(MemberDeclarationSyntax\)

### Summary

Searches for a member and returns zero\-based index of the last occurrence in the list\.

```csharp
public int LastIndexOf(MemberDeclarationSyntax member)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| member | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)

