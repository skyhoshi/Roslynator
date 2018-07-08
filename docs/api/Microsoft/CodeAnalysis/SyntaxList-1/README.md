# [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1) Struct Extensions

| Extension Method | Summary |
| ---------------- | ------- |
| [Add(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/Add/README.md) | Creates a new list with the specified node added or inserted\. |
| [All\<TNode>(SyntaxList\<TNode>, Func\<TNode, Boolean>)](../../../Roslynator/SyntaxExtensions/All-1/README.md) | Returns true if all nodes in a list matches the predicate\. |
| [Any\<TNode>(SyntaxList\<TNode>, Func\<TNode, Boolean>)](../../../Roslynator/SyntaxExtensions/Any-1/README.md) | Returns true if any node in a list matches the predicate\. |
| [Contains\<TNode>(SyntaxList\<TNode>, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/Contains-1/README.md) | Returns true if a node of the specified kind is in the [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [Contains\<TNode>(SyntaxList\<TNode>, TNode)](../../../Roslynator/SyntaxExtensions/Contains-1/README.md) | Returns true if the specified node is in the [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [DescendantTrivia\<TNode>(SyntaxList\<TNode>, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/DescendantTrivia-1/README.md) | Get a list of all the trivia associated with the nodes in the list\. |
| [DescendantTrivia\<TNode>(SyntaxList\<TNode>, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](../../../Roslynator/SyntaxExtensions/DescendantTrivia-1/README.md) | Get a list of all the trivia associated with the nodes in the list\. |
| [Find\<TNode>(SyntaxList\<TNode>, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/Find-1/README.md) | Searches for a node of the specified kind and returns the first occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [IsFirst\<TNode>(SyntaxList\<TNode>, TNode)](../../../Roslynator/SyntaxExtensions/IsFirst-1/README.md) | Returns true if the specified node is a first node in the list\. |
| [IsLast(SyntaxList\<StatementSyntax>, StatementSyntax, Boolean)](../../../Roslynator/CSharp/SyntaxExtensions/IsLast/README.md) | Returns true if the specified statement is a last statement in the list\. |
| [IsLast\<TNode>(SyntaxList\<TNode>, TNode)](../../../Roslynator/SyntaxExtensions/IsLast-1/README.md) | Returns true if the specified node is a last node in the list\. |
| [LastIndexOf\<TNode>(SyntaxList\<TNode>, SyntaxKind)](../../../Roslynator/CSharp/SyntaxExtensions/LastIndexOf-1/README.md) | Searches for a node of the specified kind and returns the zero\-based index of the last occurrence within the entire [SyntaxList\<TNode>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.syntaxlist-1)\. |
| [RemoveRange\<TNode>(SyntaxList\<TNode>, Int32, Int32)](../../../Roslynator/CSharp/SyntaxExtensions/RemoveRange-1/README.md) | Creates a new list with elements in the specified range removed\. |
| [ReplaceAt\<TNode>(SyntaxList\<TNode>, Int32, TNode)](../../../Roslynator/SyntaxExtensions/ReplaceAt-1/README.md) | Creates a new list with the node at the specified index replaced with a new node\. |
| [ReplaceRange\<TNode>(SyntaxList\<TNode>, Int32, Int32, IEnumerable\<TNode>)](../../../Roslynator/CSharp/SyntaxExtensions/ReplaceRange-1/README.md) | Creates a new list with the elements in the specified range replaced with new nodes\. |
| [WithTriviaFrom\<TNode>(SyntaxList\<TNode>, SyntaxNode)](../../../Roslynator/SyntaxExtensions/WithTriviaFrom-1/README.md) | Creates a new list with both leading and trailing trivia of the specified node\. If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\. |

