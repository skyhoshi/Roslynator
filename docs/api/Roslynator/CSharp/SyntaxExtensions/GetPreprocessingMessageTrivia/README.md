# SyntaxExtensions\.GetPreprocessingMessageTrivia Method

Namespace: [Roslynator.CSharp](../../README.md)

Assembly: Roslynator\.CSharp\.dll

## Overloads

| Method | Summary |
| ------ | ------- |
| GetPreprocessingMessageTrivia\(EndRegionDirectiveTriviaSyntax\) | Gets preprocessing message for the specified endregion directive if such message exists\. |
| GetPreprocessingMessageTrivia\(RegionDirectiveTriviaSyntax\) | Gets preprocessing message for the specified region directive if such message exists\. |

## GetPreprocessingMessageTrivia\(EndRegionDirectiveTriviaSyntax\)

### Summary

Gets preprocessing message for the specified endregion directive if such message exists\.

```csharp
public static SyntaxTrivia GetPreprocessingMessageTrivia(this EndRegionDirectiveTriviaSyntax endRegionDirective)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| endRegionDirective | |

#### Returns

[SyntaxTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivia)


## GetPreprocessingMessageTrivia\(RegionDirectiveTriviaSyntax\)

### Summary

Gets preprocessing message for the specified region directive if such message exists\.

```csharp
public static SyntaxTrivia GetPreprocessingMessageTrivia(this RegionDirectiveTriviaSyntax regionDirective)
```

#### Parameters

| Name | Summary |
| ---- | ------- |
| regionDirective | |

#### Returns

[SyntaxTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivia)


