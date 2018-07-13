# SyntaxExtensions\.GetTopmostIf Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetTopmostIf\(ElseClauseSyntax\) | Returns topmost if statement of the if\-else cascade the specified else clause is part of\. |
| GetTopmostIf\(IfStatementSyntax\) | Returns topmost if statement of the if\-else cascade the specified if statement is part of\. |

## GetTopmostIf\(ElseClauseSyntax\)

### Summary

Returns topmost if statement of the if\-else cascade the specified else clause is part of\.

```csharp
public static IfStatementSyntax GetTopmostIf(this ElseClauseSyntax elseClause)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| elseClause | |

#### Returns

[IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax)


## GetTopmostIf\(IfStatementSyntax\)

### Summary

Returns topmost if statement of the if\-else cascade the specified if statement is part of\.

```csharp
public static IfStatementSyntax GetTopmostIf(this IfStatementSyntax ifStatement)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifStatement | |

#### Returns

[IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax)


