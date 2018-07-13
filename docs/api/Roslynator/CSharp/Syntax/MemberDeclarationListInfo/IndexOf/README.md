# MemberDeclarationListInfo\.IndexOf Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IndexOf\(Func\<MemberDeclarationSyntax, Boolean>\) | Searches for a member that matches the predicate and returns returns zero\-based index of the first occurrence in the list\. |
| IndexOf\(MemberDeclarationSyntax\) | The index of the member in the list\. |

## IndexOf\(Func\<MemberDeclarationSyntax, Boolean>\)

### Summary

Searches for a member that matches the predicate and returns returns zero\-based index of the first occurrence in the list\.

```csharp
public int IndexOf(Func<MemberDeclarationSyntax, bool> predicate)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| predicate | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


## IndexOf\(MemberDeclarationSyntax\)

### Summary

The index of the member in the list\.

```csharp
public int IndexOf(MemberDeclarationSyntax member)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| member | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)


