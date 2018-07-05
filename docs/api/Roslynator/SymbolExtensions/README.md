# SymbolExtensions Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A set of extension methods for [ISymbol](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.isymbol) and its derived types\.

```csharp
class SymbolExtensions
```


## Methods

| Method | Summary |
| ------ | ------- |
| [BaseTypes(ITypeSymbol)](BaseTypes/README.md) | Gets a list of base types of this type\. |
| [BaseTypesAndSelf(ITypeSymbol)](BaseTypesAndSelf/README.md) | Gets a list of base types of this type \(including this type\)\. |
| [ContainsMember\<TSymbol>(ITypeSymbol, Func\<TSymbol, Boolean>)](ContainsMember-1/README.md) | Returns true if the type contains member that matches the conditions defined by the specified predicate, if any\. |
| [ContainsMember\<TSymbol>(ITypeSymbol, String, Func\<TSymbol, Boolean>)](ContainsMember-1/README.md) | Returns true if the type contains member that has the specified name and matches the conditions defined by the specified predicate, if any\. |
| [EqualsOrInheritsFrom(ITypeSymbol, ITypeSymbol, Boolean)](EqualsOrInheritsFrom/README.md) | Returns true if the type is equal or inherits from a specified base type\. |
| [FindMember\<TSymbol>(ITypeSymbol, Func\<TSymbol, Boolean>)](FindMember-1/README.md) | Searches for a member that matches the conditions defined by the specified predicate, if any, and returns the first occurrence within the type's members\. |
| [FindMember\<TSymbol>(ITypeSymbol, String, Func\<TSymbol, Boolean>)](FindMember-1/README.md) | Searches for a member that has the specified name and matches the conditions defined by the specified predicate, if any, and returns the first occurrence within the type's members\. |
| [GetAttribute(ISymbol, INamedTypeSymbol)](GetAttribute/README.md) | Returns the attribute for the symbol that matches the specified attribute class, or null if the symbol does not have the specified attribute\. |
| [HasAttribute(ISymbol, INamedTypeSymbol)](HasAttribute/README.md) | Returns true if the symbol has the specified attribute\. |
| [HasAttribute(ITypeSymbol, INamedTypeSymbol, Boolean)](HasAttribute/README.md) | Returns true if the type symbol has the specified attribute\. |
| [HasConstantValue(IFieldSymbol, Boolean)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Byte)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Decimal)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Double)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Char)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Int16)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Int32)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Int64)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, SByte)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, Single)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, String)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, UInt16)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, UInt32)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [HasConstantValue(IFieldSymbol, UInt64)](HasConstantValue/README.md) | Get a value indicating whether the field symbol has specified constant value\. |
| [Implements(ITypeSymbol, INamedTypeSymbol, Boolean)](Implements/README.md) | Returns true if the type implements specified interface\. |
| [Implements(ITypeSymbol, SpecialType, Boolean)](Implements/README.md) | Returns true if the type implements specified interface\. |
| [ImplementsAny(ITypeSymbol, SpecialType, SpecialType, Boolean)](ImplementsAny/README.md) | Returns true if the type implements any of specified interfaces\. |
| [ImplementsAny(ITypeSymbol, SpecialType, SpecialType, SpecialType, Boolean)](ImplementsAny/README.md) | Returns true if the type implements any of specified interfaces\. |
| [ImplementsInterfaceMember(ISymbol, Boolean)](ImplementsInterfaceMember/README.md) | Returns true if the the symbol implements any interface member\. |
| [ImplementsInterfaceMember(ISymbol, INamedTypeSymbol, Boolean)](ImplementsInterfaceMember/README.md) | Returns true if the symbol implements any member of the specified interface\. |
| [ImplementsInterfaceMember\<TSymbol>(ISymbol, Boolean)](ImplementsInterfaceMember-1/README.md) | Returns true if the symbol implements any interface member\. |
| [ImplementsInterfaceMember\<TSymbol>(ISymbol, INamedTypeSymbol, Boolean)](ImplementsInterfaceMember-1/README.md) | Returns true if the symbol implements any member of the specified interface\. |
| [InheritsFrom(ITypeSymbol, ITypeSymbol, Boolean)](InheritsFrom/README.md) | Returns true if the type inherits from a specified base type\. |
| [IsAsyncMethod(ISymbol)](IsAsyncMethod/README.md) | Returns true if the symbol is an async method\. |
| [IsErrorType(ISymbol)](IsErrorType/README.md) | Returns true if the symbol represents an error\. |
| [IsIEnumerableOfT(ITypeSymbol)](IsIEnumerableOfT/README.md) | Returns true if the type is [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\. |
| [IsIEnumerableOrIEnumerableOfT(ITypeSymbol)](IsIEnumerableOrIEnumerableOfT/README.md) | Returns true if the type is [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable) or [IEnumerable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)\. |
| [IsKind(ISymbol, SymbolKind, SymbolKind)](IsKind/README.md) | Returns true if the symbol is one of the specified kinds\. |
| [IsKind(ISymbol, SymbolKind, SymbolKind, SymbolKind)](IsKind/README.md) | Returns true if the symbol is one of the specified kinds\. |
| [IsKind(ISymbol, SymbolKind, SymbolKind, SymbolKind, SymbolKind)](IsKind/README.md) | Returns true if the symbol is one of the specified kinds\. |
| [IsKind(ISymbol, SymbolKind, SymbolKind, SymbolKind, SymbolKind, SymbolKind)](IsKind/README.md) | Returns true if the symbol is one of the specified kinds\. |
| [IsNullableOf(INamedTypeSymbol, ITypeSymbol)](IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableOf(INamedTypeSymbol, SpecialType)](IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableOf(ITypeSymbol, ITypeSymbol)](IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableOf(ITypeSymbol, SpecialType)](IsNullableOf/README.md) | Returns true if the type is [Nullable\<T>](https://docs.microsoft.com/en-us/dotnet/api/system.nullable-1) and it has specified type argument\. |
| [IsNullableType(ITypeSymbol)](IsNullableType/README.md) | Returns true if the type is a nullable type\. |
| [IsObject(ITypeSymbol)](IsObject/README.md) | Returns true if the type is [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object)\. |
| [IsOrdinaryExtensionMethod(IMethodSymbol)](IsOrdinaryExtensionMethod/README.md) | Returns true if this method is an ordinary extension method \(i\.e\. "this" parameter has not been removed\)\. |
| [IsParameterArrayOf(IParameterSymbol, SpecialType)](IsParameterArrayOf/README.md) | Returns true if the parameter was declared as a parameter array that has a specified element type\. |
| [IsParameterArrayOf(IParameterSymbol, SpecialType, SpecialType)](IsParameterArrayOf/README.md) | Returns true if the parameter was declared as a parameter array that has one of specified element types\. |
| [IsParameterArrayOf(IParameterSymbol, SpecialType, SpecialType, SpecialType)](IsParameterArrayOf/README.md) | Returns true if the parameter was declared as a parameter array that has one of specified element types\. |
| [IsPubliclyVisible(ISymbol)](IsPubliclyVisible/README.md) | Return true if the specified symbol is publicly visible\. |
| [IsReducedExtensionMethod(IMethodSymbol)](IsReducedExtensionMethod/README.md) | Returns true if this method is a reduced extension method\. |
| [IsReferenceTypeOrNullableType(ITypeSymbol)](IsReferenceTypeOrNullableType/README.md) | Returns true if the type is a reference type or a nullable type\. |
| [IsRefOrOut(IParameterSymbol)](IsRefOrOut/README.md) | Returns true if the parameter was declared as "ref" or "out" parameter\. |
| [IsString(ITypeSymbol)](IsString/README.md) | Returns true if the type is [String](https://docs.microsoft.com/en-us/dotnet/api/system.string)\. |
| [IsVoid(ITypeSymbol)](IsVoid/README.md) | Returns true if the type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [ReducedFromOrSelf(IMethodSymbol)](ReducedFromOrSelf/README.md) | If this method is a reduced extension method, returns the definition of extension method from which this was reduced\. Otherwise, returns this symbol\. |
| [SupportsExplicitDeclaration(ITypeSymbol)](SupportsExplicitDeclaration/README.md) | Returns true if the type can be declared explicitly in a source code\. |

