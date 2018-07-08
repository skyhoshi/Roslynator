# Types Extended by Roslynator API

## [Microsoft.CodeAnalysis](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis) Namespace

### Classes

| Class | Summary |
| ----- | ------- |
| [SemanticModel](Microsoft/CodeAnalysis/SemanticModel/README.md) | Allows asking semantic questions about a tree of syntax nodes in a Compilation\. Typically, an instance is obtained by a call to GetBinding on a Compilation or Compilation\. |
| [SyntaxNode](Microsoft/CodeAnalysis/SyntaxNode/README.md) | Represents a non\-terminal node in the syntax tree\. This is the language agnostic equivalent of **T:Microsoft\.CodeAnalysis\.CSharp\.SyntaxNode** and **T:Microsoft\.CodeAnalysis\.VisualBasic\.SyntaxNode**\. |
| [SyntaxTree](Microsoft/CodeAnalysis/SyntaxTree/README.md) | The parsed representation of a source document\. |

### Structs

| Struct | Summary |
| ------ | ------- |
| [FileLinePositionSpan](Microsoft/CodeAnalysis/FileLinePositionSpan/README.md) | Represents a span of text in a source code file in terms of file name, line number, and offset within line\. However, the file is actually whatever was passed in when asked to parse; there may not really be a file\. |
| [SeparatedSyntaxList\<TNode>](Microsoft/CodeAnalysis/SeparatedSyntaxList-1/README.md) | |
| [SyntaxList\<TNode>](Microsoft/CodeAnalysis/SyntaxList-1/README.md) | A list of [SyntaxNode](Microsoft/CodeAnalysis/SyntaxNode/README.md)\. |
| [SyntaxNodeOrToken](Microsoft/CodeAnalysis/SyntaxNodeOrToken/README.md) | A wrapper for either a syntax node \([SyntaxNode](Microsoft/CodeAnalysis/SyntaxNode/README.md)\) or a syntax token \([SyntaxToken](Microsoft/CodeAnalysis/SyntaxToken/README.md)\)\. |
| [SyntaxToken](Microsoft/CodeAnalysis/SyntaxToken/README.md) | Represents a token in the syntax tree\. This is the language agnostic equivalent of **T:Microsoft\.CodeAnalysis\.CSharp\.SyntaxToken** and **T:Microsoft\.CodeAnalysis\.VisualBasic\.SyntaxToken**\. |
| [SyntaxTokenList](Microsoft/CodeAnalysis/SyntaxTokenList/README.md) | Represents a read\-only list of [SyntaxToken](Microsoft/CodeAnalysis/SyntaxToken/README.md)\. |
| [SyntaxTrivia](Microsoft/CodeAnalysis/SyntaxTrivia/README.md) | Represents a trivia in the syntax tree\. This is the language agnostic equivalent of **T:Microsoft\.CodeAnalysis\.CSharp\.SyntaxTrivia** and **T:Microsoft\.CodeAnalysis\.VisualBasic\.SyntaxTrivia**\. |
| [SyntaxTriviaList](Microsoft/CodeAnalysis/SyntaxTriviaList/README.md) | Represents a read\-only list of [SyntaxTrivia](Microsoft/CodeAnalysis/SyntaxTrivia/README.md)\. |

### Interfaces

| Interface | Summary |
| --------- | ------- |
| [IFieldSymbol](Microsoft/CodeAnalysis/IFieldSymbol/README.md) | Represents a field in a class, struct or enum\. |
| [IMethodSymbol](Microsoft/CodeAnalysis/IMethodSymbol/README.md) | Represents a method or method\-like symbol \(including constructor, destructor, operator, or property/event accessor\)\. |
| [INamedTypeSymbol](Microsoft/CodeAnalysis/INamedTypeSymbol/README.md) | Represents a type other than an array, a pointer, a type parameter\. |
| [INamespaceOrTypeSymbol](Microsoft/CodeAnalysis/INamespaceOrTypeSymbol/README.md) | Represents either a namespace or a type\. |
| [INamespaceSymbol](Microsoft/CodeAnalysis/INamespaceSymbol/README.md) | Represents a namespace\. |
| [IParameterSymbol](Microsoft/CodeAnalysis/IParameterSymbol/README.md) | Represents a parameter of a method or property\. |
| [ISymbol](Microsoft/CodeAnalysis/ISymbol/README.md) | Represents a symbol \(namespace, class, method, parameter, etc\.\) exposed by the compiler\. |
| [ITypeSymbol](Microsoft/CodeAnalysis/ITypeSymbol/README.md) | Represents a type\. |

### Enums

| Enum | Summary |
| ---- | ------- |
| [Accessibility](Microsoft/CodeAnalysis/Accessibility/README.md) | Enumeration for common accessibility combinations\. |
| [MethodKind](Microsoft/CodeAnalysis/MethodKind/README.md) | Enumeration for possible kinds of method symbols\. |
| [SpecialType](Microsoft/CodeAnalysis/SpecialType/README.md) | Specifies the Ids of special runtime types\. |
| [TypeKind](Microsoft/CodeAnalysis/TypeKind/README.md) | Enumeration for possible kinds of type symbols\. |

## [Microsoft.CodeAnalysis.CSharp](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp) Namespace

### Enums

| Enum | Summary |
| ---- | ------- |
| [SyntaxKind](Microsoft/CodeAnalysis/CSharp/SyntaxKind/README.md) | |

## [Microsoft.CodeAnalysis.CSharp.Syntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax) Namespace

### Classes

| Class | Summary |
| ----- | ------- |
| [AccessorDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/AccessorDeclarationSyntax/README.md) | |
| [AccessorListSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/AccessorListSyntax/README.md) | |
| [BinaryExpressionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/BinaryExpressionSyntax/README.md) | Class which represents an expression that has a binary operator\. |
| [CastExpressionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/CastExpressionSyntax/README.md) | Class which represents the syntax node for cast expression\. |
| [ClassDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ClassDeclarationSyntax/README.md) | Class type declaration syntax\. |
| [CommonForEachStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/CommonForEachStatementSyntax/README.md) | |
| [CompilationUnitSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/CompilationUnitSyntax/README.md) | |
| [ConstructorDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ConstructorDeclarationSyntax/README.md) | Constructor declaration syntax\. |
| [ConversionOperatorDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ConversionOperatorDeclarationSyntax/README.md) | Conversion operator declaration syntax\. |
| [DelegateDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/DelegateDeclarationSyntax/README.md) | Delegate declaration syntax\. |
| [DestructorDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/DestructorDeclarationSyntax/README.md) | Destructor declaration syntax\. |
| [DocumentationCommentTriviaSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/DocumentationCommentTriviaSyntax/README.md) | |
| [ElseClauseSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ElseClauseSyntax/README.md) | Represents an else statement syntax\. |
| [EndRegionDirectiveTriviaSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/EndRegionDirectiveTriviaSyntax/README.md) | |
| [EnumDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/EnumDeclarationSyntax/README.md) | Enum type declaration syntax\. |
| [ExpressionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ExpressionSyntax/README.md) | Provides the base class from which the classes that represent expression syntax nodes are derived\. This is an abstract class\. |
| [ForStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ForStatementSyntax/README.md) | |
| [IfStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/IfStatementSyntax/README.md) | Represents an if statement syntax\. |
| [IndexerDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/IndexerDeclarationSyntax/README.md) | |
| [InterfaceDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/InterfaceDeclarationSyntax/README.md) | Interface type declaration syntax\. |
| [InterpolatedStringExpressionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/InterpolatedStringExpressionSyntax/README.md) | |
| [LiteralExpressionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/LiteralExpressionSyntax/README.md) | Class which represents the syntax node for a literal expression\. |
| [LocalFunctionStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/LocalFunctionStatementSyntax/README.md) | |
| [MemberDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/MemberDeclarationSyntax/README.md) | Member declaration syntax\. |
| [MethodDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/MethodDeclarationSyntax/README.md) | Method declaration syntax\. |
| [NamespaceDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/NamespaceDeclarationSyntax/README.md) | |
| [OperatorDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/OperatorDeclarationSyntax/README.md) | Operator declaration syntax\. |
| [ParameterSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/ParameterSyntax/README.md) | Parameter syntax\. |
| [PropertyDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/PropertyDeclarationSyntax/README.md) | |
| [RegionDirectiveTriviaSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/RegionDirectiveTriviaSyntax/README.md) | |
| [StatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/StatementSyntax/README.md) | Represents the base class for all statements syntax classes\. |
| [StructDeclarationSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/StructDeclarationSyntax/README.md) | Struct type declaration syntax\. |
| [SwitchSectionSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/SwitchSectionSyntax/README.md) | Represents a switch section syntax of a switch statement\. |
| [SwitchStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/SwitchStatementSyntax/README.md) | Represents a switch statement syntax\. |
| [TypeSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/TypeSyntax/README.md) | Provides the base class from which the classes that represent type syntax nodes are derived\. This is an abstract class\. |
| [UsingStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/UsingStatementSyntax/README.md) | |
| [YieldStatementSyntax](Microsoft/CodeAnalysis/CSharp/Syntax/YieldStatementSyntax/README.md) | |

## [Microsoft.CodeAnalysis.Diagnostics](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.diagnostics) Namespace

### Structs

| Struct | Summary |
| ------ | ------- |
| [SymbolAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SymbolAnalysisContext/README.md) | Context for a symbol action\. A symbol action can use a [SymbolAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SymbolAnalysisContext/README.md) to report [Diagnostic](Microsoft/CodeAnalysis/Diagnostic/README.md)s about an [ISymbol](Microsoft/CodeAnalysis/ISymbol/README.md)\. |
| [SyntaxNodeAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SyntaxNodeAnalysisContext/README.md) | Context for a syntax node action\. A syntax node action can use a [SyntaxNodeAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SyntaxNodeAnalysisContext/README.md) to report [Diagnostic](Microsoft/CodeAnalysis/Diagnostic/README.md)s for a [SyntaxNode](Microsoft/CodeAnalysis/SyntaxNode/README.md)\. |
| [SyntaxTreeAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SyntaxTreeAnalysisContext/README.md) | Context for a syntax tree action\. A syntax tree action can use a [SyntaxTreeAnalysisContext](Microsoft/CodeAnalysis/Diagnostics/SyntaxTreeAnalysisContext/README.md) to report [Diagnostic](Microsoft/CodeAnalysis/Diagnostic/README.md)s about a [SyntaxTree](Microsoft/CodeAnalysis/SyntaxTree/README.md) for a code document\. |

## [System.Collections.Generic](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic) Namespace

### Interfaces

| Interface | Summary |
| --------- | ------- |
| [IEnumerable\<T>](System/Collections/Generic/IEnumerable-1/README.md) | |

