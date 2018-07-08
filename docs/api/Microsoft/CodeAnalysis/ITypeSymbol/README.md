# [ITypeSymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.itypesymbol) Interface Extensions

| Extension Method | Summary |
| ---------------- | ------- |
| [BaseTypes(ITypeSymbol)](../../../Roslynator/SymbolExtensions/BaseTypes/README.md) | Gets a list of base types of this type\. |
| [BaseTypesAndSelf(ITypeSymbol)](../../../Roslynator/SymbolExtensions/BaseTypesAndSelf/README.md) | Gets a list of base types of this type \(including this type\)\. |
| [ContainsMember\<TSymbol>(ITypeSymbol, Func\<TSymbol, Boolean>)](../../../Roslynator/SymbolExtensions/ContainsMember-1/README.md) | Returns true if the type contains member that matches the conditions defined by the specified predicate, if any\. |
| [ContainsMember\<TSymbol>(ITypeSymbol, String, Func\<TSymbol, Boolean>)](../../../Roslynator/SymbolExtensions/ContainsMember-1/README.md) | Returns true if the type contains member that has the specified name and matches the conditions defined by the specified predicate, if any\. |
| [EqualsOrInheritsFrom(ITypeSymbol, ITypeSymbol, Boolean)](../../../Roslynator/SymbolExtensions/EqualsOrInheritsFrom/README.md) | Returns true if the type is equal or inherits from a specified base type\. |
| [FindMember\<TSymbol>(ITypeSymbol, Func\<TSymbol, Boolean>)](../../../Roslynator/SymbolExtensions/FindMember-1/README.md) | Searches for a member that matches the conditions defined by the specified predicate, if any, and returns the first occurrence within the type's members\. |
| [FindMember\<TSymbol>(ITypeSymbol, String, Func\<TSymbol, Boolean>)](../../../Roslynator/SymbolExtensions/FindMember-1/README.md) | Searches for a member that has the specified name and matches the conditions defined by the specified predicate, if any, and returns the first occurrence within the type's members\. |
| [GetDefaultValueSyntax(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat)](../../../Roslynator/CSharp/SymbolExtensions/GetDefaultValueSyntax/README.md) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |
| [GetDefaultValueSyntax(ITypeSymbol, TypeSyntax)](../../../Roslynator/CSharp/SymbolExtensions/GetDefaultValueSyntax/README.md) | Creates a new [ExpressionSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.expressionsyntax) that represents default value of the specified type symbol\. |
| [HasAttribute(ITypeSymbol, INamedTypeSymbol, Boolean)](../../../Roslynator/SymbolExtensions/HasAttribute/README.md) | Returns true if the type symbol has the specified attribute\. |
| [Implements(ITypeSymbol, INamedTypeSymbol, Boolean)](../../../Roslynator/SymbolExtensions/Implements/README.md) | Returns true if the type implements specified interface\. |
| [Implements(ITypeSymbol, SpecialType, Boolean)](../../../Roslynator/SymbolExtensions/Implements/README.md) | Returns true if the type implements specified interface\. |
| [ImplementsAny(ITypeSymbol, SpecialType, SpecialType, Boolean)](../../../Roslynator/SymbolExtensions/ImplementsAny/README.md) | Returns true if the type implements any of specified interfaces\. |
| [ImplementsAny(ITypeSymbol, SpecialType, SpecialType, SpecialType, Boolean)](../../../Roslynator/SymbolExtensions/ImplementsAny/README.md) | Returns true if the type implements any of specified interfaces\. |
| [InheritsFrom(ITypeSymbol, ITypeSymbol, Boolean)](../../../Roslynator/SymbolExtensions/InheritsFrom/README.md) | Returns true if the type inherits from a specified base type\. |
| [IsIEnumerableOfT(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsIEnumerableOfT/README.md) | Returns true if the type is [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\. |
| [IsIEnumerableOrIEnumerableOfT(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsIEnumerableOrIEnumerableOfT/README.md) | Returns true if the type is [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable) or [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\. |
| [IsNullableOf(ITypeSymbol, ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableOf(ITypeSymbol, SpecialType)](../../../Roslynator/SymbolExtensions/IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableType(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsNullableType/README.md) | Returns true if the type is a nullable type\. |
| [IsObject(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsObject/README.md) | Returns true if the type is [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\. |
| [IsReferenceTypeOrNullableType(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsReferenceTypeOrNullableType/README.md) | Returns true if the type is a reference type or a nullable type\. |
| [IsString(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsString/README.md) | Returns true if the type is [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)\. |
| [IsVoid(ITypeSymbol)](../../../Roslynator/SymbolExtensions/IsVoid/README.md) | Returns true if the type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [SupportsConstantValue(ITypeSymbol)](../../../Roslynator/CSharp/SymbolExtensions/SupportsConstantValue/README.md) | Returns true if the specified type can be used to declare constant value\. |
| [SupportsExplicitDeclaration(ITypeSymbol)](../../../Roslynator/SymbolExtensions/SupportsExplicitDeclaration/README.md) | Returns true if the type can be declared explicitly in a source code\. |
| [ToMinimalTypeSyntax(ITypeSymbol, SemanticModel, Int32, SymbolDisplayFormat)](../../../Roslynator/CSharp/SymbolExtensions/ToMinimalTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |
| [ToTypeSyntax(ITypeSymbol, SymbolDisplayFormat)](../../../Roslynator/CSharp/SymbolExtensions/ToTypeSyntax/README.md) | Creates a new [TypeSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.typesyntax) based on the specified type symbol\. |

