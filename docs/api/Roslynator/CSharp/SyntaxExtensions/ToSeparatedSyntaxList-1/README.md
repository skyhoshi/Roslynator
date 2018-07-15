# SyntaxExtensions\.ToSeparatedSyntaxList Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| ToSeparatedSyntaxList\<TNode>\(IEnumerable\<SyntaxNodeOrToken>\) | Creates a separated list of syntax nodes from a sequence of nodes and tokens\. |
| ToSeparatedSyntaxList\<TNode>\(IEnumerable\<TNode>\) | Creates a separated list of syntax nodes from a sequence of nodes\. |

## ToSeparatedSyntaxList\<TNode>\(IEnumerable\<TNode>\)

### Summary

Creates a separated list of syntax nodes from a sequence of nodes\.

```csharp
public static SeparatedSyntaxList<TNode> ToSeparatedSyntaxList<TNode>(this IEnumerable<TNode> nodes) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| nodes | |

#### Returns

[SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)

## ToSeparatedSyntaxList\<TNode>\(IEnumerable\<SyntaxNodeOrToken>\)

### Summary

Creates a separated list of syntax nodes from a sequence of nodes and tokens\.

```csharp
public static SeparatedSyntaxList<TNode> ToSeparatedSyntaxList<TNode>(this IEnumerable<SyntaxNodeOrToken> nodesAndTokens) where TNode : Microsoft.CodeAnalysis.SyntaxNode
```

#### Type Parameters

| Name | Summary |
| ---- | ------- |
| TNode | |

#### Parameters

| Name | Summary |
| ---- | ------- |
| nodesAndTokens | |

#### Returns

[SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)

