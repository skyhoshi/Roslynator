# Roslynator API

## [Roslynator](Roslynator/README.md) Namespace

### Classes

| Class| Summary|
| --- | --- |
| [DiagnosticsExtensions](Roslynator/DiagnosticsExtensions/README.md) | A set of extension methods for ,  and \. |
| [EnumExtensions](Roslynator/EnumExtensions/README.md) | A set of extension methods for enumerations\. |
| [FileLinePositionSpanExtensions](Roslynator/FileLinePositionSpanExtensions/README.md) | A set of extension methods for \. |
| [NameGenerator](Roslynator/NameGenerator/README.md) | Provides methods to obtain an unique identifier\. |
| [Selection\<T>](Roslynator/Selection-1/README.md) | Represents consecutive sequence of selected items in a collection\. |
| [SemanticModelExtensions](Roslynator/SemanticModelExtensions/README.md) | A set of extension methods for a \. |
| [SeparatedSyntaxListSelection\<TNode>](Roslynator/SeparatedSyntaxListSelection-1/README.md) | Represents selected nodes in a \. |
| [SymbolExtensions](Roslynator/SymbolExtensions/README.md) | A set of extension methods for  and its derived types\. |
| [SyntaxExtensions](Roslynator/SyntaxExtensions/README.md) | A set of extension method for a syntax\. |
| [SyntaxListSelection\<TNode>](Roslynator/SyntaxListSelection-1/README.md) | Represents selected nodes in a \. |
| [SyntaxTreeExtensions](Roslynator/SyntaxTreeExtensions/README.md) | A set of extension methods for \. |

### Structs

| Struct| Summary|
| --- | --- |
| [ExtensionMethodSymbolInfo](Roslynator/ExtensionMethodSymbolInfo/README.md) | Represents an extension method symbol\. |
| [SeparatedSyntaxListSelection\<TNode>.Enumerator](Roslynator/SeparatedSyntaxListSelection-1/Enumerator/README.md) | |
| [SyntaxListSelection\<TNode>.Enumerator](Roslynator/SyntaxListSelection-1/Enumerator/README.md) | |

## [Roslynator.CSharp](Roslynator/CSharp/README.md) Namespace

### Classes

| Class| Summary|
| --- | --- |
| [CSharpExtensions](Roslynator/CSharp/CSharpExtensions/README.md) | A set of extension methods for a \. |
| [CSharpFactory](Roslynator/CSharp/CSharpFactory/README.md) | A factory for syntax nodes, tokens and trivia\. This class is built on top of  members\. |
| [CSharpFacts](Roslynator/CSharp/CSharpFacts/README.md) | |
| [EnumExtensions](Roslynator/CSharp/EnumExtensions/README.md) | A set of extension methods for enumerations\. |
| [MemberDeclarationListSelection](Roslynator/CSharp/MemberDeclarationListSelection/README.md) | Represents selected member declarations in a \. |
| [ModifierList](Roslynator/CSharp/ModifierList/README.md) | A set of static methods that allows manipulation with modifiers\. |
| [ModifierList\<TNode>](Roslynator/CSharp/ModifierList-1/README.md) | Represents a list of modifiers\. |
| [Modifiers](Roslynator/CSharp/Modifiers/README.md) | Serves as a factory for a modifier list\. |
| [StatementListSelection](Roslynator/CSharp/StatementListSelection/README.md) | Represents selected statements in a \. |
| [SymbolExtensions](Roslynator/CSharp/SymbolExtensions/README.md) | A set of static methods for  and derived types\. |
| [SyntaxAccessibility](Roslynator/CSharp/SyntaxAccessibility/README.md) | A set of static methods that are related to C\# accessibility\. |
| [SyntaxExtensions](Roslynator/CSharp/SyntaxExtensions/README.md) | A set of extension methods for syntax \(types derived from \)\. |
| [SyntaxInfo](Roslynator/CSharp/SyntaxInfo/README.md) | Serves as a factory for types in Roslynator\.CSharp\.Syntax namespace\. |

### Structs

| Struct| Summary|
| --- | --- |
| [IfStatementCascade](Roslynator/CSharp/IfStatementCascade/README.md) | Enables to enumerate if statement cascade\. |
| [IfStatementCascade.Enumerator](Roslynator/CSharp/IfStatementCascade/Enumerator/README.md) | |
| [IfStatementOrElseClause](Roslynator/CSharp/IfStatementOrElseClause/README.md) | A wrapper for either an  or an \. |

### Enums

| Enum| Summary|
| --- | --- |
| [CommentKinds](Roslynator/CSharp/CommentKinds/README.md) | Specifies C\# comments\. |
| [ModifierKinds](Roslynator/CSharp/ModifierKinds/README.md) | Specifies C\# modifier\. |
| [NullCheckStyles](Roslynator/CSharp/NullCheckStyles/README.md) | Specifies a null check\. |
| [PreprocessorDirectiveKinds](Roslynator/CSharp/PreprocessorDirectiveKinds/README.md) | Specifies C\# preprocessor directives\. |

## [Roslynator.CSharp.Syntax](Roslynator/CSharp/Syntax/README.md) Namespace

### Structs

| Struct| Summary|
| --- | --- |
| [AsExpressionInfo](Roslynator/CSharp/Syntax/AsExpressionInfo/README.md) | Provides information about "as" expression\. |
| [AssignmentExpressionInfo](Roslynator/CSharp/Syntax/AssignmentExpressionInfo/README.md) | Provides information about simple assignment expression\. |
| [BinaryExpressionInfo](Roslynator/CSharp/Syntax/BinaryExpressionInfo/README.md) | Provides information about binary expression\. |
| [ConditionalExpressionInfo](Roslynator/CSharp/Syntax/ConditionalExpressionInfo/README.md) | Provides information about conditional expression\. |
| [GenericInfo](Roslynator/CSharp/Syntax/GenericInfo/README.md) | Provides information about generic syntax \(class, struct, interface, delegate, method or local function\)\. |
| [IsExpressionInfo](Roslynator/CSharp/Syntax/IsExpressionInfo/README.md) | Provides information about "is" expression\. |
| [LocalDeclarationStatementInfo](Roslynator/CSharp/Syntax/LocalDeclarationStatementInfo/README.md) | Provides information about local declaration statement\. |
| [MemberDeclarationListInfo](Roslynator/CSharp/Syntax/MemberDeclarationListInfo/README.md) | Provides information about a list of member declaration list\. |
| [ModifierListInfo](Roslynator/CSharp/Syntax/ModifierListInfo/README.md) | Provides information about modifier list\. |
| [NullCheckExpressionInfo](Roslynator/CSharp/Syntax/NullCheckExpressionInfo/README.md) | Provides information about a null check expression\. |
| [RegionInfo](Roslynator/CSharp/Syntax/RegionInfo/README.md) | Provides information about a region\. |
| [SimpleAssignmentExpressionInfo](Roslynator/CSharp/Syntax/SimpleAssignmentExpressionInfo/README.md) | Provides information about simple assignment expression\. |
| [SimpleAssignmentStatementInfo](Roslynator/CSharp/Syntax/SimpleAssignmentStatementInfo/README.md) | Provides information about a simple assignment expression in an expression statement\. |
| [SimpleIfElseInfo](Roslynator/CSharp/Syntax/SimpleIfElseInfo/README.md) | Provides information about a simple if\-else\. Simple if\-else is defined as follows: it is not a child of an else clause and it has an else clause and the else clause does not continue with another if statement\. |
| [SimpleIfStatementInfo](Roslynator/CSharp/Syntax/SimpleIfStatementInfo/README.md) | Provides information about a simple if statement\. Simple if statement is defined as follows: it is not a child of an else clause and it has no else clause\. |
| [SimpleMemberInvocationExpressionInfo](Roslynator/CSharp/Syntax/SimpleMemberInvocationExpressionInfo/README.md) | Provides information about invocation expression\. |
| [SimpleMemberInvocationStatementInfo](Roslynator/CSharp/Syntax/SimpleMemberInvocationStatementInfo/README.md) | Provides information about invocation expression in an expression statement\. |
| [SingleLocalDeclarationStatementInfo](Roslynator/CSharp/Syntax/SingleLocalDeclarationStatementInfo/README.md) | Provides information about a local declaration statement with a single variable\. |
| [SingleParameterLambdaExpressionInfo](Roslynator/CSharp/Syntax/SingleParameterLambdaExpressionInfo/README.md) | Provides information about a lambda expression with a single parameter\. |
| [StatementListInfo](Roslynator/CSharp/Syntax/StatementListInfo/README.md) | Provides information about a list of statements\. |
| [StringConcatenationExpressionInfo](Roslynator/CSharp/Syntax/StringConcatenationExpressionInfo/README.md) | Provides information about string concatenation, i\.e\. a binary expression that binds to string '\+' operator\. |
| [StringLiteralExpressionInfo](Roslynator/CSharp/Syntax/StringLiteralExpressionInfo/README.md) | Provides information about string literal expression\. |
| [UsingDirectiveListInfo](Roslynator/CSharp/Syntax/UsingDirectiveListInfo/README.md) | Provides information about a list of using directives\. |
| [XmlElementInfo](Roslynator/CSharp/Syntax/XmlElementInfo/README.md) | Provides information about a \. |

## [Roslynator.Text](Roslynator/Text/README.md) Namespace

### Classes

| Class| Summary|
| --- | --- |
| [TextLineCollectionSelection](Roslynator/Text/TextLineCollectionSelection/README.md) | Represents selected lines in a \. |

### Structs

| Struct| Summary|
| --- | --- |
| [TextLineCollectionSelection.Enumerator](Roslynator/Text/TextLineCollectionSelection/Enumerator/README.md) | |

