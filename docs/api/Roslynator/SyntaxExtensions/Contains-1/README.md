# SyntaxExtensions\.Contains Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| Contains\(SyntaxTokenList, SyntaxToken\) | Returns true if the specified token is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| Contains\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\) | Returns true if the specified node is in the [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| Contains\<TNode>\(SyntaxList\<TNode>, TNode\) | Returns true if the specified node is in the [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |

## Contains\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is in the [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\.

```csharp
public static bool Contains<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| list | |
| node | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## Contains\<TNode>\(SyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is in the [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\.

```csharp
public static bool Contains<TNode>(this SyntaxList<TNode> list, TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| list | |
| node | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


## Contains\(SyntaxTokenList, SyntaxToken\)

### Summary

Returns true if the specified token is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\.

```csharp
public static bool Contains(this SyntaxTokenList tokens, SyntaxToken token)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| tokens | |
| token | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)


