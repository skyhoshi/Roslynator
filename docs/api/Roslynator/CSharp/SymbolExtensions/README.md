# SymbolExtensions Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A set of static methods for [ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol) and derived types\.

## Methods

| Method | Summary |
| ------ | ------- |
| [GetDefaultValueSyntax(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat)](GetDefaultValueSyntax/README.md) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |
| [GetDefaultValueSyntax(ITypeSymbol, TypeSyntax)](GetDefaultValueSyntax/README.md) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |
| [SupportsConstantValue(ITypeSymbol)](SupportsConstantValue/README.md) | Returns true if the specified type can be used to declare constant value\. |
| [ToMinimalTypeSyntax(INamespaceOrTypeSymbol, SemanticModel, Int32, SymbolDisplayFormat)](ToMinimalTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol |
| [ToMinimalTypeSyntax(INamespaceSymbol, SemanticModel, Int32, SymbolDisplayFormat)](ToMinimalTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\. |
| [ToMinimalTypeSyntax(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat)](ToMinimalTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |
| [ToTypeSyntax(INamespaceOrTypeSymbol, SymbolDisplayFormat)](ToTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace or type symbol\. |
| [ToTypeSyntax(INamespaceSymbol, SymbolDisplayFormat)](ToTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified namespace symbol\. |
| [ToTypeSyntax(ITypeSymbol, SymbolDisplayFormat)](ToTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |

