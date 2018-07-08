# [SyntaxNode](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxnode) Class Extensions

| Extension Method | Summary |
| ---------------- | ------- |
| [ContainsDirectives(SyntaxNode, TextSpan)](../../../Roslynator/SyntaxExtensions/ContainsDirectives/README.md) | Returns true if the node contains any preprocessor directives inside the specified span\. |
| [FirstAncestor(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestor/README.md) | Gets the first ancestor that matches the predicate\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestor/README.md) | Gets the first ancestor of the specified kind\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestor/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestor(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestor/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestor\<TNode>(SyntaxNode, Func\<TNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/FirstAncestor-1/README.md) | Returns the first node of type **TNode** that matches the predicate\. |
| [FirstAncestorOrSelf(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestorOrSelf/README.md) | Gets the first ancestor that matches the predicate\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kind\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstAncestorOrSelf(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/FirstAncestorOrSelf/README.md) | Gets the first ancestor of the specified kinds\. |
| [FirstDescendant\<TNode>(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/FirstDescendant-1/README.md) | Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\. |
| [FirstDescendant\<TNode>(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/FirstDescendant-1/README.md) | Searches a list of descendant nodes in prefix document order and returns first descendant of type **TNode**\. |
| [FirstDescendantOrSelf\<TNode>(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/FirstDescendantOrSelf-1/README.md) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\. |
| [FirstDescendantOrSelf\<TNode>(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/FirstDescendantOrSelf-1/README.md) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type **TNode**\. |
| [GetLeadingAndTrailingTrivia(SyntaxNode)](../../../Roslynator/SyntaxExtensions/GetLeadingAndTrailingTrivia/README.md) | Returns leading and trailing trivia of the specified node in a single list\. |
| [IsDescendantOf(SyntaxNode, SyntaxKind, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/IsDescendantOf/README.md) | Returns true if a node is a descendant of a node with the specified kind\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsKind/README.md) | Returns true if a node's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is the specified kind\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [IsParentKind(SyntaxNode, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/IsParentKind/README.md) | Returns true if a node parent's kind is one of the specified kinds\. |
| [SpanContainsDirectives(SyntaxNode)](../../../Roslynator/SyntaxExtensions/SpanContainsDirectives/README.md) | Returns true if the node's span contains any preprocessor directives\. |

