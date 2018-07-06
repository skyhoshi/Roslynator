# StringConcatenationExpressionInfo\.Expressions\(Boolean\) Method

Namespace: [Roslynator.CSharp.Syntax](../../README.md)

Assembly: Roslynator\.CSharp\.dll

**WARNING: This API is now obsolete\.**

This method is obsolete\. Use method 'AsChain' instead\.

## Summary

Returns expressions of this binary expression, including expressions of nested binary expressions of the same kind as parent binary expression\.

```csharp
public IEnumerable<ExpressionSyntax> Expressions(bool leftToRight = false)
```

### Parameters

| Parameter | Summary |
| --------- | ------- |
| leftToRight | If true expressions are enumerated as they are displayed in the source code\. |

### Returns

[IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\<[ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax)>



### Attributes

[ObsoleteAttribute](https://docs.microsoft.com/en-us/dotnet/api/system.obsoleteattribute)
