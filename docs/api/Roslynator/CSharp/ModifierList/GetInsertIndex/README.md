# ModifierList\.GetInsertIndex Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetInsertIndex\(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>\) | Returns an index a token with the specified kind should be inserted at\. |
| GetInsertIndex\(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>\) | Returns an index the specified token should be inserted at\. |

## GetInsertIndex\(SyntaxTokenList, SyntaxToken, IComparer\<SyntaxToken>\)

### Summary

Returns an index the specified token should be inserted at\.

```csharp
public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxToken token, IComparer<SyntaxToken> comparer = null)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| tokens | |
| token | |
| comparer | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)




## GetInsertIndex\(SyntaxTokenList, SyntaxKind, IComparer\<SyntaxKind>\)

### Summary

Returns an index a token with the specified kind should be inserted at\.

```csharp
public static int GetInsertIndex(SyntaxTokenList tokens, SyntaxKind kind, IComparer<SyntaxKind> comparer = null)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| tokens | |
| kind | |
| comparer | |

#### Returns

[Int32](https://docs.microsoft.com/en-us/dotnet/api/system.int32)




