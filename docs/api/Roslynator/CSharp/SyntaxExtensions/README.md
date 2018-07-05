# SyntaxExtensions Class

Namespace: [Roslynator.CSharp](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A set of extension methods for syntax \(types derived from [CSharpSyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.csharpsyntaxnode)\)\.

```csharp
class SyntaxExtensions
```


## Methods

| Method | Summary |
| ------ | ------- |
| [Add(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean)](Add/README.md) | Creates a new list with the specified node added or inserted\. |
| [AddAttributeLists(ClassDeclarationSyntax, Boolean, AttributeListSyntax\[\])](AddAttributeLists/README.md) | Creates a new class declaration with the specified attribute lists added\. |
| [AddAttributeLists(InterfaceDeclarationSyntax, Boolean, AttributeListSyntax\[\])](AddAttributeLists/README.md) | Creates a new interface declaration with the specified attribute lists added\. |
| [AddAttributeLists(StructDeclarationSyntax, Boolean, AttributeListSyntax\[\])](AddAttributeLists/README.md) | Creates a new struct declaration with the specified attribute lists added\. |
| [AddUsings(CompilationUnitSyntax, Boolean, UsingDirectiveSyntax\[\])](AddUsings/README.md) | Creates a new [CompilationUnitSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax) with the specified using directives added\. |
| [AsCascade(IfStatementSyntax)](AsCascade/README.md) | Returns [IfStatementCascade](../IfStatementCascade/README.md) that enables to enumerate if\-else cascade\. |
| [AsChain(BinaryExpressionSyntax, TextSpan?)](AsChain/README.md) | |
| [BodyOrExpressionBody(AccessorDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns accessor body or an expression body if the body is null\. |
| [BodyOrExpressionBody(ConstructorDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns constructor body or an expression body if the body is null\. |
| [BodyOrExpressionBody(ConversionOperatorDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns conversion operator body or an expression body if the body is null\. |
| [BodyOrExpressionBody(DestructorDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns destructor body or an expression body if the body is null\. |
| [BodyOrExpressionBody(LocalFunctionStatementSyntax)](BodyOrExpressionBody/README.md) | Returns local function body or an expression body if the body is null\. |
| [BodyOrExpressionBody(MethodDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns method body or an expression body if the body is null\. |
| [BodyOrExpressionBody(OperatorDeclarationSyntax)](BodyOrExpressionBody/README.md) | Returns operator body or an expression body if the body is null\. |
| [BracesSpan(ClassDeclarationSyntax)](BracesSpan/README.md) | The absolute span of the braces, not including its leading and trailing trivia\. |
| [BracesSpan(EnumDeclarationSyntax)](BracesSpan/README.md) | The absolute span of the braces, not including its leading and trailing trivia\. |
| [BracesSpan(InterfaceDeclarationSyntax)](BracesSpan/README.md) | The absolute span of the braces, not including it leading and trailing trivia\. |
| [BracesSpan(NamespaceDeclarationSyntax)](BracesSpan/README.md) | The absolute span of the braces, not including leading and trailing trivia\. |
| [BracesSpan(StructDeclarationSyntax)](BracesSpan/README.md) | The absolute span of the braces, not including its leading and trailing trivia\. |
| [Contains(SyntaxTokenList, SyntaxKind)](Contains/README.md) | Returns true if a token of the specified kind is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [Contains(SyntaxTriviaList, SyntaxKind)](Contains/README.md) | Returns true if a trivia of the specified kind is in the [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\. |
| [Contains\<TNode>(SeparatedSyntaxList\<TNode>, SyntaxKind)](Contains-1/README.md) | Searches for a node of the specified kind and returns the zero\-based index of the first occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| [Contains\<TNode>(SyntaxList\<TNode>, SyntaxKind)](Contains-1/README.md) | Returns true if a node of the specified kind is in the [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [ContainsAny(SyntaxTokenList, SyntaxKind, SyntaxKind)](ContainsAny/README.md) | Returns true if a token of the specified kinds is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [ContainsAny(SyntaxTokenList, SyntaxKind, SyntaxKind, SyntaxKind)](ContainsAny/README.md) | Returns true if a token of the specified kinds is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [ContainsAny(SyntaxTokenList, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](ContainsAny/README.md) | Returns true if a token of the specified kinds is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [ContainsAny(SyntaxTokenList, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](ContainsAny/README.md) | Returns true if a token of the specified kinds is in the [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [ContainsDefaultLabel(SwitchSectionSyntax)](ContainsDefaultLabel/README.md) | Returns true if the specified switch section contains default switch label\. |
| [ContainsYield(LocalFunctionStatementSyntax)](ContainsYield/README.md) | Returns true if the specified local function contains yield statement\. Nested local functions are excluded\. |
| [ContainsYield(MethodDeclarationSyntax)](ContainsYield/README.md) | Returns true if the specified method contains yield statement\. Nested local functions are excluded\. |
| [DeclarationOrExpression(UsingStatementSyntax)](DeclarationOrExpression/README.md) | Returns using statement's declaration or an expression if the declaration is null\. |
| [DefaultSection(SwitchStatementSyntax)](DefaultSection/README.md) | Returns a section that contains default label, or null if the specified swtich statement does not contains section with default label\. |
| [Elements(DocumentationCommentTriviaSyntax, String)](Elements/README.md) | Gets a list of xml elements with the specified local name\. |
| [Find(SyntaxTokenList, SyntaxKind)](Find/README.md) | Searches for a token of the specified kind and returns the first occurrence within the entire [SyntaxTokenList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtokenlist)\. |
| [Find(SyntaxTriviaList, SyntaxKind)](Find/README.md) | Searches for a trivia of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [Find\<TNode>(SeparatedSyntaxList\<TNode>, SyntaxKind)](Find-1/README.md) | Searches for a node of the specified kind and returns the first occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| [Find\<TNode>(SyntaxList\<TNode>, SyntaxKind)](Find-1/README.md) | Searches for a node of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [FirstAncestor(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](FirstAncestor/README.md) | Gets the first ancestor that matches the predicate\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, Boolean)](FirstAncestor/README.md) | Gets the first ancestor of the specified kind\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, SyntaxKind, Boolean)](FirstAncestor/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean)](FirstAncestor/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestorOrSelf(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](FirstAncestorOrSelf/README.md) | Gets the first ancestor that matches the predicate\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, Boolean)](FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kind\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, SyntaxKind, Boolean)](FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean)](FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kinds\. |
| [GetDocumentationComment(MemberDeclarationSyntax)](GetDocumentationComment/README.md) | Returns documentation comment syntax that is part of the specified declaration\. |
| [GetDocumentationCommentTrivia(MemberDeclarationSyntax)](GetDocumentationCommentTrivia/README.md) | Returns documentation comment that is part of the specified declaration\. |
| [GetEndRegionDirective(RegionDirectiveTriviaSyntax)](GetEndRegionDirective/README.md) | Returns endregion directive that is related to the specified region directive\. Returns null if no matching endregion directive is found\. |
| [GetPreprocessingMessageTrivia(EndRegionDirectiveTriviaSyntax)](GetPreprocessingMessageTrivia/README.md) | Gets preprocessing message for the specified endregion directive if such message exists\. |
| [GetPreprocessingMessageTrivia(RegionDirectiveTriviaSyntax)](GetPreprocessingMessageTrivia/README.md) | Gets preprocessing message for the specified region directive if such message exists\. |
| [GetRegionDirective(EndRegionDirectiveTriviaSyntax)](GetRegionDirective/README.md) | Returns region directive that is related to the specified endregion directive\. Returns null if no matching region directive is found\. |
| [GetSingleLineDocumentationComment(MemberDeclarationSyntax)](GetSingleLineDocumentationComment/README.md) | Returns single\-line documentation comment syntax that is part of the specified declaration\. |
| [GetSingleLineDocumentationCommentTrivia(MemberDeclarationSyntax)](GetSingleLineDocumentationCommentTrivia/README.md) | Returns single\-line documentation comment that is part of the specified declaration\. |
| [Getter(AccessorListSyntax)](Getter/README.md) | Returns a get accessor contained in the specified list\. |
| [Getter(IndexerDeclarationSyntax)](Getter/README.md) | Returns a get accessor that is contained in the specified indexer declaration\. |
| [Getter(PropertyDeclarationSyntax)](Getter/README.md) | Returns property get accessor, if any\. |
| [GetTopmostIf(ElseClauseSyntax)](GetTopmostIf/README.md) | Returns topmost if statement of the if\-else cascade the specified else clause is part of\. |
| [GetTopmostIf(IfStatementSyntax)](GetTopmostIf/README.md) | Returns topmost if statement of the if\-else cascade the specified if statement is part of\. |
| [HasDocumentationComment(MemberDeclarationSyntax)](HasDocumentationComment/README.md) | Returns true if the specified declaration has a documentation comment\. |
| [HasSingleLineDocumentationComment(MemberDeclarationSyntax)](HasSingleLineDocumentationComment/README.md) | Returns true if the specified declaration has a single\-line documentation comment\. |
| [IsAutoImplemented(AccessorDeclarationSyntax)](IsAutoImplemented/README.md) | Returns true is the specified accessor is auto\-implemented accessor\. |
| [IsDescendantOf(SyntaxNode, SyntaxKind, Boolean)](IsDescendantOf/README.md) | Returns true if a node is a descendant of a node with the specified kind\. |
| [IsEmbedded(StatementSyntax, Boolean, Boolean, Boolean)](IsEmbedded/README.md) | Returns true if the specified statement is an embedded statement\. |
| [IsEmptyOrWhitespace(SyntaxTriviaList)](IsEmptyOrWhitespace/README.md) | Returns true if the list of either empty or contains only whitespace\. |
| [IsEndOfLineTrivia(SyntaxTrivia)](IsEndOfLineTrivia/README.md) | Returns true if the trivia is [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia)\. |
| [IsHexNumericLiteral(LiteralExpressionSyntax)](IsHexNumericLiteral/README.md) | Returns true if the specified literal expression is a hexadecimal numeric literal expression\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a trivia's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsKind(SyntaxTrivia, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsKind/README.md) | Returns true if a token's kind is one of the specified kinds\. |
| [IsLast(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean)](IsLast/README.md) | Returns true if the specified statement is a last statement in the list\. |
| [IsParams(ParameterSyntax)](IsParams/README.md) | Returns true if the specified parameter has "params" modifier\. |
| [IsParentKind(SyntaxNode, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is the specified kind\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxToken, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is the specified kind\. |
| [IsParentKind(SyntaxToken, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxToken, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](IsParentKind/README.md) | Returns true if a token parent's kind is one of the specified kinds\. |
| [IsSimpleIf(IfStatementSyntax)](IsSimpleIf/README.md) | Returns true if the specified if statement is a simple if statement\. Simple if statement is defined as follows: it is not a child of an else clause and it has no else clause\. |
| [IsTopmostIf(IfStatementSyntax)](IsTopmostIf/README.md) | Returns true if the specified if statement is not a child of an else clause\. |
| [IsVerbatim(InterpolatedStringExpressionSyntax)](IsVerbatim/README.md) | Returns true if the specified interpolated string is a verbatim\. |
| [IsVoid(TypeSyntax)](IsVoid/README.md) | Returns true if the type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [IsWhitespaceOrEndOfLineTrivia(SyntaxTrivia)](IsWhitespaceOrEndOfLineTrivia/README.md) | Returns true if the trivia is either [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) or [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia)\. |
| [IsWhitespaceTrivia(SyntaxTrivia)](IsWhitespaceTrivia/README.md) | Returns true if the trivia is [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia)\. |
| [IsYieldBreak(YieldStatementSyntax)](IsYieldBreak/README.md) | Returns true if the specified statement is a yield break statement\. |
| [IsYieldReturn(YieldStatementSyntax)](IsYieldReturn/README.md) | Returns true if the specified statement is a yield return statement\. |
| [LastIndexOf(SyntaxTriviaList, SyntaxKind)](LastIndexOf/README.md) | Searches for a trivia of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxTriviaList](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxtrivialist)\. |
| [LastIndexOf\<TNode>(SeparatedSyntaxList\<TNode>, SyntaxKind)](LastIndexOf-1/README.md) | Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SeparatedSyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.separatedsyntaxlist-1)\. |
| [LastIndexOf\<TNode>(SyntaxList\<TNode>, SyntaxKind)](LastIndexOf-1/README.md) | Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [NextStatement(StatementSyntax)](NextStatement/README.md) | Gets the next statement of the specified statement\. If the specified statement is not contained in the list, or if there is no next statement, then this method returns null\. |
| [ParenthesesSpan(CastExpressionSyntax)](ParenthesesSpan/README.md) | The absolute span of the parentheses, not including its leading and trailing trivia\. |
| [ParenthesesSpan(CommonForEachStatementSyntax)](ParenthesesSpan/README.md) | The absolute span of the parentheses, not including its leading and trailing trivia\. |
| [ParenthesesSpan(ForStatementSyntax)](ParenthesesSpan/README.md) | Absolute span of the parentheses, not including the leading and trailing trivia\. |
| [PreviousStatement(StatementSyntax)](PreviousStatement/README.md) | Gets the previous statement of the specified statement\. If the specified statement is not contained in the list, or if there is no previous statement, then this method returns null\. |
| [RemoveRange(SyntaxTokenList, Int32, Int32)](RemoveRange/README.md) | Creates a new list with tokens in the specified range removed\. |
| [RemoveRange(SyntaxTriviaList, Int32, Int32)](RemoveRange/README.md) | Creates a new list with trivia in the specified range removed\. |
| [RemoveRange\<TNode>(SeparatedSyntaxList\<TNode>, Int32, Int32)](RemoveRange-1/README.md) | Creates a new list with elements in the specified range removed\. |
| [RemoveRange\<TNode>(SyntaxList\<TNode>, Int32, Int32)](RemoveRange-1/README.md) | Creates a new list with elements in the specified range removed\. |
| [RemoveTrivia\<TNode>(TNode, TextSpan?)](RemoveTrivia-1/README.md) | Creates a new node with the trivia removed\. |
| [RemoveWhitespace\<TNode>(TNode, TextSpan?)](RemoveWhitespace-1/README.md) | Creates a new node with the whitespace removed\. |
| [ReplaceRange(SyntaxTokenList, Int32, Int32, IEnumerable\<SyntaxToken>)](ReplaceRange/README.md) | Creates a new list with the tokens in the specified range replaced with new tokens\. |
| [ReplaceRange(SyntaxTriviaList, Int32, Int32, IEnumerable\<SyntaxTrivia>)](ReplaceRange/README.md) | Creates a new list with the trivia in the specified range replaced with new trivia\. |
| [ReplaceRange\<TNode>(SeparatedSyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>)](ReplaceRange-1/README.md) | Creates a new list with the elements in the specified range replaced with new nodes\. |
| [ReplaceRange\<TNode>(SyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>)](ReplaceRange-1/README.md) | Creates a new list with the elements in the specified range replaced with new nodes\. |
| [ReplaceWhitespace\<TNode>(TNode, SyntaxTrivia, TextSpan?)](ReplaceWhitespace-1/README.md) | Creates a new node with the whitespace replaced\. |
| [ReturnsVoid(DelegateDeclarationSyntax)](ReturnsVoid/README.md) | Returns true the specified delegate return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [ReturnsVoid(LocalFunctionStatementSyntax)](ReturnsVoid/README.md) | Returns true if the specified local function' return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [ReturnsVoid(MethodDeclarationSyntax)](ReturnsVoid/README.md) | Returns true if the specified method return type is [Void](https://docs.microsoft.com/en-us/dotnet/api/system.void)\. |
| [Setter(AccessorListSyntax)](Setter/README.md) | Returns a set accessor contained in the specified list\. |
| [Setter(IndexerDeclarationSyntax)](Setter/README.md) | Returns a set accessor that is contained in the specified indexer declaration\. |
| [Setter(PropertyDeclarationSyntax)](Setter/README.md) | Returns property set accessor, if any\. |
| [ToSeparatedSyntaxList\<TNode>(IEnumerable\<SyntaxNodeOrToken>)](ToSeparatedSyntaxList-1/README.md) | Creates a separated list of syntax nodes from a sequence of nodes and tokens\. |
| [ToSeparatedSyntaxList\<TNode>(IEnumerable\<TNode>)](ToSeparatedSyntaxList-1/README.md) | Creates a separated list of syntax nodes from a sequence of nodes\. |
| [ToSyntaxList\<TNode>(IEnumerable\<TNode>)](ToSyntaxList-1/README.md) | Creates a list of syntax nodes from a sequence of nodes\. |
| [ToSyntaxTokenList(IEnumerable\<SyntaxToken>)](ToSyntaxTokenList/README.md) | Creates a list of syntax tokens from a sequence of tokens\. |
| [TrimLeadingTrivia(SyntaxToken)](TrimLeadingTrivia/README.md) | Removes all leading whitespace from the leading trivia and returns a new token with the new leading trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same token if there is nothing to trim\. |
| [TrimLeadingTrivia\<TNode>(TNode)](TrimLeadingTrivia-1/README.md) | Removes all leading whitespace from the leading trivia and returns a new node with the new leading trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same node if there is nothing to trim\. |
| [TrimTrailingTrivia(SyntaxToken)](TrimTrailingTrivia/README.md) | Removes all trailing whitespace from the trailing trivia and returns a new token with the new trailing trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same token if there is nothing to trim\. |
| [TrimTrailingTrivia\<TNode>(TNode)](TrimTrailingTrivia-1/README.md) | Removes all trailing whitespace from the trailing trivia and returns a new node with the new trailing trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same node if there is nothing to trim\. |
| [TrimTrivia(SyntaxToken)](TrimTrivia/README.md) | Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new token with the new trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same token if there is nothing to trim\. |
| [TrimTrivia\<TNode>(TNode)](TrimTrivia-1/README.md) | Removes all leading whitespace from the leading trivia and all trailing whitespace from the trailing trivia and returns a new node with the new trivia\. [WhitespaceTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.whitespacetrivia) and [EndOfLineTrivia](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntaxkind.endoflinetrivia) is considered to be a whitespace\. Returns the same node if there is nothing to trim\. |
| [TryGetContainingList(StatementSyntax, SyntaxList\<StatementSyntax>)](TryGetContainingList/README.md) | Gets a list the specified statement is contained in\. This method succeeds if the statement is in a block's statements or a switch section's statements\. |
| [WalkDownParentheses(ExpressionSyntax)](WalkDownParentheses/README.md) | Returns lowest expression in parentheses or self if the expression is not parenthesized\. |
| [WalkUpParentheses(ExpressionSyntax)](WalkUpParentheses/README.md) | Returns topmost parenthesized expression or self if the expression if not parenthesized\. |
| [WithMembers(ClassDeclarationSyntax, IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new [ClassDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.classdeclarationsyntax) with the members updated\. |
| [WithMembers(ClassDeclarationSyntax, MemberDeclarationSyntax)](WithMembers/README.md) | Creates a new [ClassDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.classdeclarationsyntax) with the members updated\. |
| [WithMembers(CompilationUnitSyntax, IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new [CompilationUnitSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax) with the members updated\. |
| [WithMembers(CompilationUnitSyntax, MemberDeclarationSyntax)](WithMembers/README.md) | Creates a new [CompilationUnitSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.compilationunitsyntax) with the members updated\. |
| [WithMembers(InterfaceDeclarationSyntax, IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new [InterfaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.interfacedeclarationsyntax) with the members updated\. |
| [WithMembers(InterfaceDeclarationSyntax, MemberDeclarationSyntax)](WithMembers/README.md) | Creates a new [InterfaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.interfacedeclarationsyntax) with the members updated\. |
| [WithMembers(NamespaceDeclarationSyntax, IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new [NamespaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.namespacedeclarationsyntax) with the members updated\. |
| [WithMembers(NamespaceDeclarationSyntax, MemberDeclarationSyntax)](WithMembers/README.md) | Creates a new [NamespaceDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.namespacedeclarationsyntax) with the members updated\. |
| [WithMembers(StructDeclarationSyntax, IEnumerable\<MemberDeclarationSyntax>)](WithMembers/README.md) | Creates a new [StructDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.structdeclarationsyntax) with the members updated\. |
| [WithMembers(StructDeclarationSyntax, MemberDeclarationSyntax)](WithMembers/README.md) | Creates a new [StructDeclarationSyntax](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.structdeclarationsyntax) with the members updated\. |

