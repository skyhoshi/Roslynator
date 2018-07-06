# IfStatementOrElseClause\.implicit operator IfStatementOrElseClause Operator

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Operator | Summary |
| -------- | ------- |
| implicit operator ElseClauseSyntax\(IfStatementOrElseClause\) | |
| implicit operator IfStatementOrElseClause\(ElseClauseSyntax\) | |
| implicit operator IfStatementOrElseClause\(IfStatementSyntax\) | |
| implicit operator IfStatementSyntax\(IfStatementOrElseClause\) | |

## implicit operator IfStatementOrElseClause\(IfStatementSyntax\)

```csharp
public static implicit operator IfStatementOrElseClause(IfStatementSyntax ifStatement)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifStatement | |

#### Returns

[IfStatementOrElseClause](../README.md)


## implicit operator IfStatementSyntax\(IfStatementOrElseClause\)

```csharp
public static implicit operator IfStatementSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifOrElse | |

#### Returns

[IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax)


## implicit operator IfStatementOrElseClause\(ElseClauseSyntax\)

```csharp
public static implicit operator IfStatementOrElseClause(ElseClauseSyntax elseClause)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| elseClause | |

#### Returns

[IfStatementOrElseClause](../README.md)


## implicit operator ElseClauseSyntax\(IfStatementOrElseClause\)

```csharp
public static implicit operator ElseClauseSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifOrElse | |

#### Returns

[ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)


