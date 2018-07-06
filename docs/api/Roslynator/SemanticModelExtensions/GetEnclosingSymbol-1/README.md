# SemanticModelExtensions\.GetEnclosingSymbol\<TSymbol>\(SemanticModel, Int32, CancellationToken\) Method

Namespace: [Roslynator](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

Returns the innermost symbol of type **TSymbol** that the specified position is considered inside of\.

```csharp
public static TSymbol GetEnclosingSymbol<TSymbol>(this SemanticModel semanticModel, int position, CancellationToken cancellationToken = default(CancellationToken)) where TSymbol : ISymbol
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| semanticModel | |
| position | |
| cancellationToken | |

### Type Parameters

| Type Parameter | Summary |
| -------------- | ------- |
| TSymbol | |

### Returns

[TSymbol](../TSymbol/README.md)




