# Roslynator API

## Roslynator Namespace

### Classes

| Class| Summary|
| --- | --- |
| [DiagnosticsExtensions](Roslynator.DiagnosticsExtensions.md) | A set of extension methods for ,  and \. |
| [EnumExtensions](Roslynator.EnumExtensions.md) | A set of extension methods for enumerations\. |
| [FileLinePositionSpanExtensions](Roslynator.FileLinePositionSpanExtensions.md) | A set of extension methods for \. |
| [NameGenerator](Roslynator.NameGenerator.md) | Provides methods to obtain an unique identifier\. |
| [Selection\<T>](Roslynator.Selection-1.md) | Represents consecutive sequence of selected items in a collection\. |
| [SemanticModelExtensions](Roslynator.SemanticModelExtensions.md) | A set of extension methods for a \. |
| [SeparatedSyntaxListSelection\<TNode>](Roslynator.SeparatedSyntaxListSelection-1.md) | Represents selected nodes in a \. |
| [SymbolExtensions](Roslynator.SymbolExtensions.md) | A set of extension methods for  and its derived types\. |
| [SyntaxExtensions](Roslynator.SyntaxExtensions.md) | A set of extension method for a syntax\. |
| [SyntaxListSelection\<TNode>](Roslynator.SyntaxListSelection-1.md) | Represents selected nodes in a \. |
| [SyntaxTreeExtensions](Roslynator.SyntaxTreeExtensions.md) | A set of extension methods for \. |

### Structs

| Struct| Summary|
| --- | --- |
| [ExtensionMethodSymbolInfo](Roslynator.ExtensionMethodSymbolInfo.md) | Represents an extension method symbol\. |
| [SeparatedSyntaxListSelection\<TNode>.Enumerator](Roslynator.SeparatedSyntaxListSelection-1.Enumerator.md) | |
| [SyntaxListSelection\<TNode>.Enumerator](Roslynator.SyntaxListSelection-1.Enumerator.md) | |

## Roslynator\.CSharp Namespace

### Classes

| Class| Summary|
| --- | --- |
| [CSharpExtensions](Roslynator.CSharp.CSharpExtensions.md) | A set of extension methods for a \. |
| [CSharpFactory](Roslynator.CSharp.CSharpFactory.md) | A factory for syntax nodes, tokens and trivia\. This class is built on top of  members\. |
| [CSharpFacts](Roslynator.CSharp.CSharpFacts.md) | |
| [EnumExtensions](Roslynator.CSharp.EnumExtensions.md) | A set of extension methods for enumerations\. |
| [MemberDeclarationListSelection](Roslynator.CSharp.MemberDeclarationListSelection.md) | Represents selected member declarations in a \. |
| [ModifierList](Roslynator.CSharp.ModifierList.md) | A set of static methods that allows manipulation with modifiers\. |
| [ModifierList\<TNode>](Roslynator.CSharp.ModifierList-1.md) | Represents a list of modifiers\. |
| [Modifiers](Roslynator.CSharp.Modifiers.md) | Serves as a factory for a modifier list\. |
| [StatementListSelection](Roslynator.CSharp.StatementListSelection.md) | Represents selected statements in a \. |
| [SymbolExtensions](Roslynator.CSharp.SymbolExtensions.md) | A set of static methods for  and derived types\. |
| [SyntaxAccessibility](Roslynator.CSharp.SyntaxAccessibility.md) | A set of static methods that are related to C\# accessibility\. |
| [SyntaxExtensions](Roslynator.CSharp.SyntaxExtensions.md) | A set of extension methods for syntax \(types derived from \)\. |
| [SyntaxInfo](Roslynator.CSharp.SyntaxInfo.md) | Serves as a factory for types in Roslynator\.CSharp\.Syntax namespace\. |

### Structs

| Struct| Summary|
| --- | --- |
| [IfStatementCascade](Roslynator.CSharp.IfStatementCascade.md) | Enables to enumerate if statement cascade\. |
| [IfStatementCascade.Enumerator](Roslynator.CSharp.IfStatementCascade.Enumerator.md) | |
| [IfStatementOrElseClause](Roslynator.CSharp.IfStatementOrElseClause.md) | A wrapper for either an  or an \. |

### Enums

| Enum| Summary|
| --- | --- |
| [CommentKinds](Roslynator.CSharp.CommentKinds.md) | Specifies C\# comments\. |
| [ModifierKinds](Roslynator.CSharp.ModifierKinds.md) | Specifies C\# modifier\. |
| [NullCheckStyles](Roslynator.CSharp.NullCheckStyles.md) | Specifies a null check\. |
| [PreprocessorDirectiveKinds](Roslynator.CSharp.PreprocessorDirectiveKinds.md) | Specifies C\# preprocessor directives\. |

## Roslynator\.CSharp\.Syntax Namespace

### Structs

| Struct| Summary|
| --- | --- |
| [AsExpressionInfo](Roslynator.CSharp.Syntax.AsExpressionInfo.md) | Provides information about "as" expression\. |
| [AssignmentExpressionInfo](Roslynator.CSharp.Syntax.AssignmentExpressionInfo.md) | Provides information about simple assignment expression\. |
| [BinaryExpressionInfo](Roslynator.CSharp.Syntax.BinaryExpressionInfo.md) | Provides information about binary expression\. |
| [ConditionalExpressionInfo](Roslynator.CSharp.Syntax.ConditionalExpressionInfo.md) | Provides information about conditional expression\. |
| [GenericInfo](Roslynator.CSharp.Syntax.GenericInfo.md) | Provides information about generic syntax \(class, struct, interface, delegate, method or local function\)\. |
| [IsExpressionInfo](Roslynator.CSharp.Syntax.IsExpressionInfo.md) | Provides information about "is" expression\. |
| [LocalDeclarationStatementInfo](Roslynator.CSharp.Syntax.LocalDeclarationStatementInfo.md) | Provides information about local declaration statement\. |
| [MemberDeclarationListInfo](Roslynator.CSharp.Syntax.MemberDeclarationListInfo.md) | Provides information about a list of member declaration list\. |
| [ModifierListInfo](Roslynator.CSharp.Syntax.ModifierListInfo.md) | Provides information about modifier list\. |
| [NullCheckExpressionInfo](Roslynator.CSharp.Syntax.NullCheckExpressionInfo.md) | Provides information about a null check expression\. |
| [RegionInfo](Roslynator.CSharp.Syntax.RegionInfo.md) | Provides information about a region\. |
| [SimpleAssignmentExpressionInfo](Roslynator.CSharp.Syntax.SimpleAssignmentExpressionInfo.md) | Provides information about simple assignment expression\. |
| [SimpleAssignmentStatementInfo](Roslynator.CSharp.Syntax.SimpleAssignmentStatementInfo.md) | Provides information about a simple assignment expression in an expression statement\. |
| [SimpleIfElseInfo](Roslynator.CSharp.Syntax.SimpleIfElseInfo.md) | Provides information about a simple if\-else\.
            Simple if\-else is defined as follows: it is not a child of an else clause and it has an else clause and the else clause does not continue with another if statement\. |
| [SimpleIfStatementInfo](Roslynator.CSharp.Syntax.SimpleIfStatementInfo.md) | Provides information about a simple if statement\.
            Simple if statement is defined as follows: it is not a child of an else clause and it has no else clause\. |
| [SimpleMemberInvocationExpressionInfo](Roslynator.CSharp.Syntax.SimpleMemberInvocationExpressionInfo.md) | Provides information about invocation expression\. |
| [SimpleMemberInvocationStatementInfo](Roslynator.CSharp.Syntax.SimpleMemberInvocationStatementInfo.md) | Provides information about invocation expression in an expression statement\. |
| [SingleLocalDeclarationStatementInfo](Roslynator.CSharp.Syntax.SingleLocalDeclarationStatementInfo.md) | Provides information about a local declaration statement with a single variable\. |
| [SingleParameterLambdaExpressionInfo](Roslynator.CSharp.Syntax.SingleParameterLambdaExpressionInfo.md) | Provides information about a lambda expression with a single parameter\. |
| [StatementListInfo](Roslynator.CSharp.Syntax.StatementListInfo.md) | Provides information about a list of statements\. |
| [StringConcatenationExpressionInfo](Roslynator.CSharp.Syntax.StringConcatenationExpressionInfo.md) | Provides information about string concatenation, i\.e\. a binary expression that binds to string '\+' operator\. |
| [StringLiteralExpressionInfo](Roslynator.CSharp.Syntax.StringLiteralExpressionInfo.md) | Provides information about string literal expression\. |
| [UsingDirectiveListInfo](Roslynator.CSharp.Syntax.UsingDirectiveListInfo.md) | Provides information about a list of using directives\. |
| [XmlElementInfo](Roslynator.CSharp.Syntax.XmlElementInfo.md) | Provides information about a \. |

## Roslynator\.Text Namespace

### Classes

| Class| Summary|
| --- | --- |
| [TextLineCollectionSelection](Roslynator.Text.TextLineCollectionSelection.md) | Represents selected lines in a \. |

### Structs

| Struct| Summary|
| --- | --- |
| [TextLineCollectionSelection.Enumerator](Roslynator.Text.TextLineCollectionSelection.Enumerator.md) | |

