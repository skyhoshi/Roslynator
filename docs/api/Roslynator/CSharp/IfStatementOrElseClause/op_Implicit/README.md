# IfStatementOrElseClause\.Implicit Operator

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Operator | Summary |
| -------- | ------- |
| Implicit\(ElseClauseSyntax to IfStatementOrElseClause\) | |
| Implicit\(IfStatementOrElseClause to ElseClauseSyntax\) | |
| Implicit\(IfStatementOrElseClause to IfStatementSyntax\) | |
| Implicit\(IfStatementSyntax to IfStatementOrElseClause\) | |

## Implicit\(IfStatementSyntax to IfStatementOrElseClause\)

```csharp
public static implicit operator IfStatementOrElseClause(IfStatementSyntax ifStatement)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifStatement | |

#### Returns

[IfStatementOrElseClause](../README.md)


## Implicit\(IfStatementOrElseClause to IfStatementSyntax\)

```csharp
public static implicit operator IfStatementSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifOrElse | |

#### Returns

[IfStatementSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.ifstatementsyntax)


## Implicit\(ElseClauseSyntax to IfStatementOrElseClause\)

```csharp
public static implicit operator IfStatementOrElseClause(ElseClauseSyntax elseClause)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| elseClause | |

#### Returns

[IfStatementOrElseClause](../README.md)


## Implicit\(IfStatementOrElseClause to ElseClauseSyntax\)

```csharp
public static implicit operator ElseClauseSyntax(in IfStatementOrElseClause ifOrElse)
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| ifOrElse | |

#### Returns

[ElseClauseSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.elseclausesyntax)


