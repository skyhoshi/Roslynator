# SyntaxExtensions\.IsFirst Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| IsFirst\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\) | Returns true if the specified node is a first node in the list\. |
| IsFirst\<TNode>\(SyntaxList\<TNode>, TNode\) | Returns true if the specified node is a first node in the list\. |

## IsFirst\<TNode>\(SeparatedSyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is a first node in the list\.

```csharp
public static bool IsFirst<TNode>(this SeparatedSyntaxList<TNode> list, TNode node) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| node | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




## IsFirst\<TNode>\(SyntaxList\<TNode>, TNode\)

### Summary

Returns true if the specified node is a first node in the list\.

```csharp
public static bool IsFirst<TNode>(this SyntaxList<TNode> list, TNode node) 
    where TNode : SyntaxNode
```

#### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TNode | |

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| list | |
| node | |

#### Returns

[Boolean](https://docs.microsoft.com/en-us/dotnet/api/system.boolean)




