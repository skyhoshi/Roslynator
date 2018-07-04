# SyntaxExtensions Class

Namespace: [Roslynator](../README.md)

Assembly: Roslynator\.CSharp\.dll

## Summary

A set of extension method for a syntax\.

## Methods

| Method | Summary |
| ------ | ------- |
| [All(SyntaxTokenList, Func\<SyntaxToken, Boolean>)](All/README.md) | Returns true if all tokens in a  matches the predicate\. |
| [All(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>)](All/README.md) | Returns true if all trivia in a  matches the predicate\. |
| [All\<TNode>(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>)](All-1/README.md) | Returns true if all nodes in a list matches the predicate\. |
| [All\<TNode>(SyntaxList\<TNode>, Func\<TNode, Boolean>)](All-1/README.md) | Returns true if all nodes in a list matches the predicate\. |
| [Any(SyntaxTokenList, Func\<SyntaxToken, Boolean>)](Any/README.md) | Returns true if any token in a  matches the predicate\. |
| [Any(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>)](Any/README.md) | Returns true if any trivia in a  matches the predicate\. |
| [Any\<TNode>(SeparatedSyntaxList\<TNode>, Func\<TNode, Boolean>)](Any-1/README.md) | Returns true if any node in a list matches the predicate\. |
| [Any\<TNode>(SyntaxList\<TNode>, Func\<TNode, Boolean>)](Any-1/README.md) | Returns true if any node in a list matches the predicate\. |
| [AppendToLeadingTrivia(SyntaxToken, IEnumerable\<SyntaxTrivia>)](AppendToLeadingTrivia/README.md) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| [AppendToLeadingTrivia(SyntaxToken, SyntaxTrivia)](AppendToLeadingTrivia/README.md) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| [AppendToLeadingTrivia\<TNode>(TNode, IEnumerable\<SyntaxTrivia>)](AppendToLeadingTrivia-1/README.md) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| [AppendToLeadingTrivia\<TNode>(TNode, SyntaxTrivia)](AppendToLeadingTrivia-1/README.md) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is added at the end of the leading trivia\. |
| [AppendToTrailingTrivia(SyntaxToken, IEnumerable\<SyntaxTrivia>)](AppendToTrailingTrivia/README.md) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| [AppendToTrailingTrivia(SyntaxToken, SyntaxTrivia)](AppendToTrailingTrivia/README.md) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| [AppendToTrailingTrivia\<TNode>(TNode, IEnumerable\<SyntaxTrivia>)](AppendToTrailingTrivia-1/README.md) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| [AppendToTrailingTrivia\<TNode>(TNode, SyntaxTrivia)](AppendToTrailingTrivia-1/README.md) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is added at the end of the trailing trivia\. |
| [Contains(SyntaxTokenList, SyntaxToken)](Contains/README.md) | Returns true if the specified token is in the \. |
| [Contains\<TNode>(SeparatedSyntaxList\<TNode>, TNode)](Contains-1/README.md) | Returns true if the specified node is in the \. |
| [Contains\<TNode>(SyntaxList\<TNode>, TNode)](Contains-1/README.md) | Returns true if the specified node is in the \. |
| [ContainsDirectives(SyntaxNode, TextSpan)](ContainsDirectives/README.md) | Returns true if the node contains any preprocessor directives inside the specified span\. |
| [DescendantTrivia\<TNode>(SyntaxList\<TNode>, Func\<SyntaxNode, Boolean>, Boolean)](DescendantTrivia-1/README.md) | Get a list of all the trivia associated with the nodes in the list\. |
| [DescendantTrivia\<TNode>(SyntaxList\<TNode>, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](DescendantTrivia-1/README.md) | Get a list of all the trivia associated with the nodes in the list\. |
| [FirstAncestor\<TNode>(SyntaxNode, Func\<TNode, Boolean>, Boolean)](FirstAncestor-1/README.md) | Returns the first node of type  that matches the predicate\. |
| [FirstDescendant\<TNode>(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](FirstDescendant-1/README.md) | Searches a list of descendant nodes in prefix document order and returns first descendant of type \. |
| [FirstDescendant\<TNode>(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](FirstDescendant-1/README.md) | Searches a list of descendant nodes in prefix document order and returns first descendant of type \. |
| [FirstDescendantOrSelf\<TNode>(SyntaxNode, Func\<SyntaxNode, Boolean>, Boolean)](FirstDescendantOrSelf-1/README.md) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type \. |
| [FirstDescendantOrSelf\<TNode>(SyntaxNode, TextSpan, Func\<SyntaxNode, Boolean>, Boolean)](FirstDescendantOrSelf-1/README.md) | Searches a list of descendant nodes \(including this node\) in prefix document order and returns first descendant of type \. |
| [GetLeadingAndTrailingTrivia(SyntaxNode)](GetLeadingAndTrailingTrivia/README.md) | Returns leading and trailing trivia of the specified node in a single list\. |
| [IndexOf(SyntaxTokenList, Func\<SyntaxToken, Boolean>)](IndexOf/README.md) | Searches for a token that matches the predicate and returns the zero\-based index of the first occurrence within the entire \. |
| [IndexOf(SyntaxTriviaList, Func\<SyntaxTrivia, Boolean>)](IndexOf/README.md) | Searches for a trivia that matches the predicate and returns the zero\-based index of the first occurrence within the entire \. |
| [IsFirst\<TNode>(SeparatedSyntaxList\<TNode>, TNode)](IsFirst-1/README.md) | Returns true if the specified node is a first node in the list\. |
| [IsFirst\<TNode>(SyntaxList\<TNode>, TNode)](IsFirst-1/README.md) | Returns true if the specified node is a first node in the list\. |
| [IsLast\<TNode>(SeparatedSyntaxList\<TNode>, TNode)](IsLast-1/README.md) | Returns true if the specified node is a last node in the list\. |
| [IsLast\<TNode>(SyntaxList\<TNode>, TNode)](IsLast-1/README.md) | Returns true if the specified node is a last node in the list\. |
| [LeadingAndTrailingTrivia(SyntaxToken)](LeadingAndTrailingTrivia/README.md) | Returns leading and trailing trivia of the specified node in a single list\. |
| [PrependToLeadingTrivia(SyntaxToken, IEnumerable\<SyntaxTrivia>)](PrependToLeadingTrivia/README.md) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| [PrependToLeadingTrivia(SyntaxToken, SyntaxTrivia)](PrependToLeadingTrivia/README.md) | Creates a new token from this token with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| [PrependToLeadingTrivia\<TNode>(TNode, IEnumerable\<SyntaxTrivia>)](PrependToLeadingTrivia-1/README.md) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| [PrependToLeadingTrivia\<TNode>(TNode, SyntaxTrivia)](PrependToLeadingTrivia-1/README.md) | Creates a new node from this node with the leading trivia replaced with a new trivia where the specified trivia is inserted at the begining of the leading trivia\. |
| [PrependToTrailingTrivia(SyntaxToken, IEnumerable\<SyntaxTrivia>)](PrependToTrailingTrivia/README.md) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| [PrependToTrailingTrivia(SyntaxToken, SyntaxTrivia)](PrependToTrailingTrivia/README.md) | Creates a new token from this token with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| [PrependToTrailingTrivia\<TNode>(TNode, IEnumerable\<SyntaxTrivia>)](PrependToTrailingTrivia-1/README.md) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| [PrependToTrailingTrivia\<TNode>(TNode, SyntaxTrivia)](PrependToTrailingTrivia-1/README.md) | Creates a new node from this node with the trailing trivia replaced with a new trivia where the specified trivia is inserted at the begining of the trailing trivia\. |
| [ReplaceAt(SyntaxTokenList, Int32, SyntaxToken)](ReplaceAt/README.md) | Creates a new  with a token at the specified index replaced with a new token\. |
| [ReplaceAt(SyntaxTriviaList, Int32, SyntaxTrivia)](ReplaceAt/README.md) | Creates a new  with a trivia at the specified index replaced with new trivia\. |
| [ReplaceAt\<TNode>(SeparatedSyntaxList\<TNode>, Int32, TNode)](ReplaceAt-1/README.md) | Creates a new list with a node at the specified index replaced with a new node\. |
| [ReplaceAt\<TNode>(SyntaxList\<TNode>, Int32, TNode)](ReplaceAt-1/README.md) | Creates a new list with the node at the specified index replaced with a new node\. |
| [SpanContainsDirectives(SyntaxNode)](SpanContainsDirectives/README.md) | Returns true if the node's span contains any preprocessor directives\. |
| [TryGetContainingList(SyntaxTrivia, SyntaxTriviaList, Boolean, Boolean)](TryGetContainingList/README.md) | Gets a  the specified trivia is contained in\. |
| [WithoutLeadingTrivia(SyntaxNodeOrToken)](WithoutLeadingTrivia/README.md) | Creates a new  with the leading trivia removed\. |
| [WithoutLeadingTrivia(SyntaxToken)](WithoutLeadingTrivia/README.md) | Creates a new token from this token with the leading trivia removed\. |
| [WithoutTrailingTrivia(SyntaxNodeOrToken)](WithoutTrailingTrivia/README.md) | Creates a new  with the trailing trivia removed\. |
| [WithoutTrailingTrivia(SyntaxToken)](WithoutTrailingTrivia/README.md) | Creates a new token from this token with the trailing trivia removed\. |
| [WithoutTrivia(SyntaxNodeOrToken)](WithoutTrivia/README.md) | Creates a new  from this node without leading and trailing trivia\. |
| [WithTriviaFrom(SyntaxToken, SyntaxNode)](WithTriviaFrom/README.md) | Creates a new token from this token with both the leading and trailing trivia of the specified node\. |
| [WithTriviaFrom\<TNode>(SeparatedSyntaxList\<TNode>, SyntaxNode)](WithTriviaFrom-1/README.md) | Creates a new separated list with both leading and trailing trivia of the specified node\. If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\. |
| [WithTriviaFrom\<TNode>(SyntaxList\<TNode>, SyntaxNode)](WithTriviaFrom-1/README.md) | Creates a new list with both leading and trailing trivia of the specified node\. If the list contains more than one item, first item is updated with leading trivia and last item is updated with trailing trivia\. |
| [WithTriviaFrom\<TNode>(TNode, SyntaxToken)](WithTriviaFrom-1/README.md) | Creates a new node from this node with both the leading and trailing trivia of the specified token\. |

