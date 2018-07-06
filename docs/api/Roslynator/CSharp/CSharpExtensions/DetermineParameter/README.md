# CSharpExtensions\.DetermineParameter Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| DetermineParameter\(SemanticModel, ArgumentSyntax, Boolean, Boolean, CancellationToken\) | Determines a parameter symbol that matches to the specified argument\. Returns null if no matching parameter is found\. |
| DetermineParameter\(SemanticModel, AttributeArgumentSyntax, Boolean, Boolean, CancellationToken\) | Determines a parameter symbol that matches to the specified attribute argument\. Returns null if not matching parameter is found\. |

## DetermineParameter\(SemanticModel, ArgumentSyntax, Boolean, Boolean, CancellationToken\)

### Summary

Determines a parameter symbol that matches to the specified argument\.
Returns null if no matching parameter is found\.

```csharp
public static IParameterSymbol DetermineParameter(this SemanticModel semanticModel, ArgumentSyntax argument, bool allowParams = false, bool allowCandidate = false, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| semanticModel | |
| argument | |
| allowParams | |
| allowCandidate | |
| cancellationToken | |

#### Returns

[IParameterSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.iparametersymbol)




## DetermineParameter\(SemanticModel, AttributeArgumentSyntax, Boolean, Boolean, CancellationToken\)

### Summary

Determines a parameter symbol that matches to the specified attribute argument\.
Returns null if not matching parameter is found\.

```csharp
public static IParameterSymbol DetermineParameter(this SemanticModel semanticModel, AttributeArgumentSyntax attributeArgument, bool allowParams = false, bool allowCandidate = false, CancellationToken cancellationToken = default(CancellationToken))
```

#### Parameters

| Parameter | Summary |
| --------- | ------- |
| semanticModel | |
| attributeArgument | |
| allowParams | |
| allowCandidate | |
| cancellationToken | |

#### Returns

[IParameterSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.iparametersymbol)




