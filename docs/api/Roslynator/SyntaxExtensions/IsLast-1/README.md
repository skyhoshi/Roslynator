# SyntaxExtensions\.IsLast Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsLast\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\) | Returns true if the specified node is a last node in the list\. |
| IsLast\<TNode>\(SyntaxList\<TNode>, TNode\) | Returns true if the specified node is a last node in the list\. |

## IsLast\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is a last node in the list\.

```csharp
public static bool IsLast<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
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


## IsLast\<TNode>\(SyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is a last node in the list\.

```csharp
public static bool IsLast<TNode>(this SyntaxList<TNode> list, TNode node) where TNode : Microsoft.CodeAnalysis.SyntaxNode
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


