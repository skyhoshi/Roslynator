# C\# Syntax Object Model

* SyntaxNode
  * CSharpSyntaxNode
    * **AccessorDeclarationSyntax**
      * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
      * Modifiers \(*SyntaxTokenList*\)
      * Keyword \(*SyntaxToken*\)
      * Body \(*BlockSyntax*\)
      * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
      * SemicolonToken \(*SyntaxToken*\)
      * SyntaxKinds:
        * SyntaxKind\.AddAccessorDeclaration
        * SyntaxKind\.GetAccessorDeclaration
        * SyntaxKind\.RemoveAccessorDeclaration
        * SyntaxKind\.SetAccessorDeclaration
        * SyntaxKind\.UnknownAccessorDeclaration

    * **AccessorListSyntax**
      * OpenBraceToken \(*SyntaxToken*\)
      * Accessors \(*SyntaxList\<AccessorDeclarationSyntax>*\)
      * CloseBraceToken \(*SyntaxToken*\)
    * **AnonymousObjectMemberDeclaratorSyntax**
      * NameEquals \(*NameEqualsSyntax*\)
      * Expression \(*ExpressionSyntax*\)
    * **ArgumentSyntax**
      * NameColon \(*NameColonSyntax*\)
      * RefOrOutKeyword \(*SyntaxToken*\)
      * Expression \(*ExpressionSyntax*\)
    * **ArrayRankSpecifierSyntax**
      * Rank \(*int*\)
      * OpenBracketToken \(*SyntaxToken*\)
      * Sizes \(*SeparatedSyntaxList\<ExpressionSyntax>*\)
      * CloseBracketToken \(*SyntaxToken*\)
    * **ArrowExpressionClauseSyntax**
      * ArrowToken \(*SyntaxToken*\)
      * Expression \(*ExpressionSyntax*\)
    * **AttributeArgumentListSyntax**
      * OpenParenToken \(*SyntaxToken*\)
      * Arguments \(*SeparatedSyntaxList\<AttributeArgumentSyntax>*\)
      * CloseParenToken \(*SyntaxToken*\)
    * **AttributeArgumentSyntax**
      * NameEquals \(*NameEqualsSyntax*\)
      * NameColon \(*NameColonSyntax*\)
      * Expression \(*ExpressionSyntax*\)
    * **AttributeListSyntax**
      * OpenBracketToken \(*SyntaxToken*\)
      * Target \(*AttributeTargetSpecifierSyntax*\)
      * Attributes \(*SeparatedSyntaxList\<AttributeSyntax>*\)
      * CloseBracketToken \(*SyntaxToken*\)
    * **AttributeSyntax**
      * Name \(*NameSyntax*\)
      * ArgumentList \(*AttributeArgumentListSyntax*\)
    * **AttributeTargetSpecifierSyntax**
      * Identifier \(*SyntaxToken*\)
      * ColonToken \(*SyntaxToken*\)
    * **BaseArgumentListSyntax**
      * Arguments \(*SeparatedSyntaxList\<ArgumentSyntax>*\)
      * **ArgumentListSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Arguments \(*SeparatedSyntaxList\<ArgumentSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **BracketedArgumentListSyntax**
        * OpenBracketToken \(*SyntaxToken*\)
        * Arguments \(*SeparatedSyntaxList\<ArgumentSyntax>*\)
        * CloseBracketToken \(*SyntaxToken*\)
    * **BaseCrefParameterListSyntax**
      * Parameters \(*SeparatedSyntaxList\<CrefParameterSyntax>*\)
      * **CrefBracketedParameterListSyntax**
        * OpenBracketToken \(*SyntaxToken*\)
        * Parameters \(*SeparatedSyntaxList\<CrefParameterSyntax>*\)
        * CloseBracketToken \(*SyntaxToken*\)
      * **CrefParameterListSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Parameters \(*SeparatedSyntaxList\<CrefParameterSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
    * **BaseListSyntax**
      * ColonToken \(*SyntaxToken*\)
      * Types \(*SeparatedSyntaxList\<BaseTypeSyntax>*\)
    * **BaseParameterListSyntax**
      * Parameters \(*SeparatedSyntaxList\<ParameterSyntax>*\)
      * **BracketedParameterListSyntax**
        * OpenBracketToken \(*SyntaxToken*\)
        * Parameters \(*SeparatedSyntaxList\<ParameterSyntax>*\)
        * CloseBracketToken \(*SyntaxToken*\)
      * **ParameterListSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Parameters \(*SeparatedSyntaxList\<ParameterSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
    * **BaseTypeSyntax**
      * Type \(*TypeSyntax*\)
      * **SimpleBaseTypeSyntax**
        * Type \(*TypeSyntax*\)
    * **CatchClauseSyntax**
      * CatchKeyword \(*SyntaxToken*\)
      * Declaration \(*CatchDeclarationSyntax*\)
      * Filter \(*CatchFilterClauseSyntax*\)
      * Block \(*BlockSyntax*\)
    * **CatchDeclarationSyntax**
      * OpenParenToken \(*SyntaxToken*\)
      * Type \(*TypeSyntax*\)
      * Identifier \(*SyntaxToken*\)
      * CloseParenToken \(*SyntaxToken*\)
    * **CatchFilterClauseSyntax**
      * WhenKeyword \(*SyntaxToken*\)
      * OpenParenToken \(*SyntaxToken*\)
      * FilterExpression \(*ExpressionSyntax*\)
      * CloseParenToken \(*SyntaxToken*\)
    * **CompilationUnitSyntax**
      * Externs \(*SyntaxList\<ExternAliasDirectiveSyntax>*\)
      * Usings \(*SyntaxList\<UsingDirectiveSyntax>*\)
      * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
      * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
      * EndOfFileToken \(*SyntaxToken*\)
    * **ConstructorInitializerSyntax**
      * ColonToken \(*SyntaxToken*\)
      * ThisOrBaseKeyword \(*SyntaxToken*\)
      * ArgumentList \(*ArgumentListSyntax*\)
      * SyntaxKinds:
        * SyntaxKind\.BaseConstructorInitializer
        * SyntaxKind\.ThisConstructorInitializer

    * **CrefParameterSyntax**
      * RefOrOutKeyword \(*SyntaxToken*\)
      * Type \(*TypeSyntax*\)
    * **CrefSyntax**
      * **MemberCrefSyntax**
        * **ConversionOperatorMemberCrefSyntax**
          * ImplicitOrExplicitKeyword \(*SyntaxToken*\)
          * OperatorKeyword \(*SyntaxToken*\)
          * Type \(*TypeSyntax*\)
          * Parameters \(*CrefParameterListSyntax*\)
        * **IndexerMemberCrefSyntax**
          * ThisKeyword \(*SyntaxToken*\)
          * Parameters \(*CrefBracketedParameterListSyntax*\)
        * **NameMemberCrefSyntax**
          * Name \(*TypeSyntax*\)
          * Parameters \(*CrefParameterListSyntax*\)
        * **OperatorMemberCrefSyntax**
          * OperatorKeyword \(*SyntaxToken*\)
          * OperatorToken \(*SyntaxToken*\)
          * Parameters \(*CrefParameterListSyntax*\)
      * **QualifiedCrefSyntax**
        * Container \(*TypeSyntax*\)
        * DotToken \(*SyntaxToken*\)
        * Member \(*MemberCrefSyntax*\)
      * **TypeCrefSyntax**
        * Type \(*TypeSyntax*\)
    * **ElseClauseSyntax**
      * ElseKeyword \(*SyntaxToken*\)
      * Statement \(*StatementSyntax*\)
    * **EqualsValueClauseSyntax**
      * EqualsToken \(*SyntaxToken*\)
      * Value \(*ExpressionSyntax*\)
    * **ExplicitInterfaceSpecifierSyntax**
      * Name \(*NameSyntax*\)
      * DotToken \(*SyntaxToken*\)
    * **ExpressionSyntax**
      * **AnonymousFunctionExpressionSyntax**
        * AsyncKeyword \(*SyntaxToken*\)
        * Body \(*CSharpSyntaxNode*\)
        * **AnonymousMethodExpressionSyntax**
          * Block \(*BlockSyntax*\)
          * AsyncKeyword \(*SyntaxToken*\)
          * DelegateKeyword \(*SyntaxToken*\)
          * ParameterList \(*ParameterListSyntax*\)
          * Body \(*CSharpSyntaxNode*\)
        * **LambdaExpressionSyntax**
          * ArrowToken \(*SyntaxToken*\)
          * **ParenthesizedLambdaExpressionSyntax**
            * AsyncKeyword \(*SyntaxToken*\)
            * ParameterList \(*ParameterListSyntax*\)
            * ArrowToken \(*SyntaxToken*\)
            * Body \(*CSharpSyntaxNode*\)
          * **SimpleLambdaExpressionSyntax**
            * AsyncKeyword \(*SyntaxToken*\)
            * Parameter \(*ParameterSyntax*\)
            * ArrowToken \(*SyntaxToken*\)
            * Body \(*CSharpSyntaxNode*\)
      * **AnonymousObjectCreationExpressionSyntax**
        * NewKeyword \(*SyntaxToken*\)
        * OpenBraceToken \(*SyntaxToken*\)
        * Initializers \(*SeparatedSyntaxList\<AnonymousObjectMemberDeclaratorSyntax>*\)
        * CloseBraceToken \(*SyntaxToken*\)
      * **ArrayCreationExpressionSyntax**
        * NewKeyword \(*SyntaxToken*\)
        * Type \(*ArrayTypeSyntax*\)
        * Initializer \(*InitializerExpressionSyntax*\)
      * **AssignmentExpressionSyntax**
        * Left \(*ExpressionSyntax*\)
        * OperatorToken \(*SyntaxToken*\)
        * Right \(*ExpressionSyntax*\)
        * SyntaxKinds:
          * SyntaxKind\.AddAssignmentExpression
          * SyntaxKind\.AndAssignmentExpression
          * SyntaxKind\.DivideAssignmentExpression
          * SyntaxKind\.ExclusiveOrAssignmentExpression
          * SyntaxKind\.LeftShiftAssignmentExpression
          * SyntaxKind\.ModuloAssignmentExpression
          * SyntaxKind\.MultiplyAssignmentExpression
          * SyntaxKind\.OrAssignmentExpression
          * SyntaxKind\.RightShiftAssignmentExpression
          * SyntaxKind\.SimpleAssignmentExpression
          * SyntaxKind\.SubtractAssignmentExpression

      * **AwaitExpressionSyntax**
        * AwaitKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **BinaryExpressionSyntax**
        * Left \(*ExpressionSyntax*\)
        * OperatorToken \(*SyntaxToken*\)
        * Right \(*ExpressionSyntax*\)
        * SyntaxKinds:
          * SyntaxKind\.AddExpression
          * SyntaxKind\.AsExpression
          * SyntaxKind\.BitwiseAndExpression
          * SyntaxKind\.BitwiseOrExpression
          * SyntaxKind\.CoalesceExpression
          * SyntaxKind\.DivideExpression
          * SyntaxKind\.EqualsExpression
          * SyntaxKind\.ExclusiveOrExpression
          * SyntaxKind\.GreaterThanExpression
          * SyntaxKind\.GreaterThanOrEqualExpression
          * SyntaxKind\.IsExpression
          * SyntaxKind\.LeftShiftExpression
          * SyntaxKind\.LessThanExpression
          * SyntaxKind\.LessThanOrEqualExpression
          * SyntaxKind\.LogicalAndExpression
          * SyntaxKind\.LogicalOrExpression
          * SyntaxKind\.ModuloExpression
          * SyntaxKind\.MultiplyExpression
          * SyntaxKind\.NotEqualsExpression
          * SyntaxKind\.RightShiftExpression
          * SyntaxKind\.SubtractExpression

      * **CastExpressionSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **ConditionalAccessExpressionSyntax**
        * Expression \(*ExpressionSyntax*\)
        * OperatorToken \(*SyntaxToken*\)
        * WhenNotNull \(*ExpressionSyntax*\)
      * **ConditionalExpressionSyntax**
        * Condition \(*ExpressionSyntax*\)
        * QuestionToken \(*SyntaxToken*\)
        * WhenTrue \(*ExpressionSyntax*\)
        * ColonToken \(*SyntaxToken*\)
        * WhenFalse \(*ExpressionSyntax*\)
      * **DeclarationExpressionSyntax**
        * Type \(*TypeSyntax*\)
        * Designation \(*VariableDesignationSyntax*\)
      * **DefaultExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **ElementAccessExpressionSyntax**
        * Expression \(*ExpressionSyntax*\)
        * ArgumentList \(*BracketedArgumentListSyntax*\)
      * **ElementBindingExpressionSyntax**
        * ArgumentList \(*BracketedArgumentListSyntax*\)
      * **CheckedExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.CheckedExpression
          * SyntaxKind\.UncheckedExpression

      * **ImplicitArrayCreationExpressionSyntax**
        * NewKeyword \(*SyntaxToken*\)
        * OpenBracketToken \(*SyntaxToken*\)
        * Commas \(*SyntaxTokenList*\)
        * CloseBracketToken \(*SyntaxToken*\)
        * Initializer \(*InitializerExpressionSyntax*\)
      * **ImplicitElementAccessSyntax**
        * ArgumentList \(*BracketedArgumentListSyntax*\)
      * **InitializerExpressionSyntax**
        * OpenBraceToken \(*SyntaxToken*\)
        * Expressions \(*SeparatedSyntaxList\<ExpressionSyntax>*\)
        * CloseBraceToken \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.ArrayInitializerExpression
          * SyntaxKind\.CollectionInitializerExpression
          * SyntaxKind\.ComplexElementInitializerExpression
          * SyntaxKind\.ObjectInitializerExpression

      * **InstanceExpressionSyntax**
        * **BaseExpressionSyntax**
          * Token \(*SyntaxToken*\)
        * **ThisExpressionSyntax**
          * Token \(*SyntaxToken*\)
      * **InterpolatedStringExpressionSyntax**
        * StringStartToken \(*SyntaxToken*\)
        * Contents \(*SyntaxList\<InterpolatedStringContentSyntax>*\)
        * StringEndToken \(*SyntaxToken*\)
      * **InvocationExpressionSyntax**
        * Expression \(*ExpressionSyntax*\)
        * ArgumentList \(*ArgumentListSyntax*\)
      * **IsPatternExpressionSyntax**
        * Expression \(*ExpressionSyntax*\)
        * IsKeyword \(*SyntaxToken*\)
        * Pattern \(*PatternSyntax*\)
      * **LiteralExpressionSyntax**
        * Token \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.ArgListExpression
          * SyntaxKind\.DefaultLiteralExpression
          * SyntaxKind\.FalseLiteralExpression
          * SyntaxKind\.CharacterLiteralExpression
          * SyntaxKind\.NullLiteralExpression
          * SyntaxKind\.NumericLiteralExpression
          * SyntaxKind\.StringLiteralExpression
          * SyntaxKind\.TrueLiteralExpression

      * **MakeRefExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **MemberAccessExpressionSyntax**
        * Expression \(*ExpressionSyntax*\)
        * OperatorToken \(*SyntaxToken*\)
        * Name \(*SimpleNameSyntax*\)
        * SyntaxKinds:
          * SyntaxKind\.PointerMemberAccessExpression
          * SyntaxKind\.SimpleMemberAccessExpression

      * **MemberBindingExpressionSyntax**
        * OperatorToken \(*SyntaxToken*\)
        * Name \(*SimpleNameSyntax*\)
      * **ObjectCreationExpressionSyntax**
        * NewKeyword \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * ArgumentList \(*ArgumentListSyntax*\)
        * Initializer \(*InitializerExpressionSyntax*\)
      * **OmittedArraySizeExpressionSyntax**
        * OmittedArraySizeExpressionToken \(*SyntaxToken*\)
      * **ParenthesizedExpressionSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **PostfixUnaryExpressionSyntax**
        * Operand \(*ExpressionSyntax*\)
        * OperatorToken \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.PostDecrementExpression
          * SyntaxKind\.PostIncrementExpression

      * **PrefixUnaryExpressionSyntax**
        * OperatorToken \(*SyntaxToken*\)
        * Operand \(*ExpressionSyntax*\)
        * SyntaxKinds:
          * SyntaxKind\.AddressOfExpression
          * SyntaxKind\.BitwiseNotExpression
          * SyntaxKind\.LogicalNotExpression
          * SyntaxKind\.PointerIndirectionExpression
          * SyntaxKind\.PreDecrementExpression
          * SyntaxKind\.PreIncrementExpression
          * SyntaxKind\.UnaryMinusExpression
          * SyntaxKind\.UnaryPlusExpression

      * **QueryExpressionSyntax**
        * FromClause \(*FromClauseSyntax*\)
        * Body \(*QueryBodySyntax*\)
      * **RefExpressionSyntax**
        * RefKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **RefTypeExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **RefValueExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * Comma \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **SizeOfExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **StackAllocArrayCreationExpressionSyntax**
        * StackAllocKeyword \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
      * **ThrowExpressionSyntax**
        * ThrowKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **TupleExpressionSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Arguments \(*SeparatedSyntaxList\<ArgumentSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **TypeOfExpressionSyntax**
        * Keyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **TypeSyntax**
        * IsVar \(*bool*\)
        * **ArrayTypeSyntax**
          * ElementType \(*TypeSyntax*\)
          * RankSpecifiers \(*SyntaxList\<ArrayRankSpecifierSyntax>*\)
        * **NameSyntax**
          * Arity \(*int*\)
          * **AliasQualifiedNameSyntax**
            * Alias \(*IdentifierNameSyntax*\)
            * ColonColonToken \(*SyntaxToken*\)
            * Name \(*SimpleNameSyntax*\)
          * **QualifiedNameSyntax**
            * Left \(*NameSyntax*\)
            * DotToken \(*SyntaxToken*\)
            * Right \(*SimpleNameSyntax*\)
          * **SimpleNameSyntax**
            * Identifier \(*SyntaxToken*\)
            * **GenericNameSyntax**
              * IsUnboundGenericName \(*bool*\)
              * Identifier \(*SyntaxToken*\)
              * TypeArgumentList \(*TypeArgumentListSyntax*\)
            * **IdentifierNameSyntax**
              * Identifier \(*SyntaxToken*\)
        * **NullableTypeSyntax**
          * ElementType \(*TypeSyntax*\)
          * QuestionToken \(*SyntaxToken*\)
        * **OmittedTypeArgumentSyntax**
          * OmittedTypeArgumentToken \(*SyntaxToken*\)
        * **PointerTypeSyntax**
          * ElementType \(*TypeSyntax*\)
          * AsteriskToken \(*SyntaxToken*\)
        * **PredefinedTypeSyntax**
          * Keyword \(*SyntaxToken*\)
        * **RefTypeSyntax**
          * RefKeyword \(*SyntaxToken*\)
          * Type \(*TypeSyntax*\)
        * **TupleTypeSyntax**
          * OpenParenToken \(*SyntaxToken*\)
          * Elements \(*SeparatedSyntaxList\<TupleElementSyntax>*\)
          * CloseParenToken \(*SyntaxToken*\)
    * **ExternAliasDirectiveSyntax**
      * ExternKeyword \(*SyntaxToken*\)
      * AliasKeyword \(*SyntaxToken*\)
      * Identifier \(*SyntaxToken*\)
      * SemicolonToken \(*SyntaxToken*\)
    * **FinallyClauseSyntax**
      * FinallyKeyword \(*SyntaxToken*\)
      * Block \(*BlockSyntax*\)
    * **InterpolatedStringContentSyntax**
      * **InterpolatedStringTextSyntax**
        * TextToken \(*SyntaxToken*\)
      * **InterpolationSyntax**
        * OpenBraceToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * AlignmentClause \(*InterpolationAlignmentClauseSyntax*\)
        * FormatClause \(*InterpolationFormatClauseSyntax*\)
        * CloseBraceToken \(*SyntaxToken*\)
    * **InterpolationAlignmentClauseSyntax**
      * CommaToken \(*SyntaxToken*\)
      * Value \(*ExpressionSyntax*\)
    * **InterpolationFormatClauseSyntax**
      * ColonToken \(*SyntaxToken*\)
      * FormatStringToken \(*SyntaxToken*\)
    * **JoinIntoClauseSyntax**
      * IntoKeyword \(*SyntaxToken*\)
      * Identifier \(*SyntaxToken*\)
    * **MemberDeclarationSyntax**
      * **BaseFieldDeclarationSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * Declaration \(*VariableDeclarationSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
        * **EventFieldDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * EventKeyword \(*SyntaxToken*\)
          * Declaration \(*VariableDeclarationSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **FieldDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * Declaration \(*VariableDeclarationSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
      * **BaseMethodDeclarationSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * ParameterList \(*ParameterListSyntax*\)
        * Body \(*BlockSyntax*\)
        * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
        * **ConstructorDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * Identifier \(*SyntaxToken*\)
          * ParameterList \(*ParameterListSyntax*\)
          * Initializer \(*ConstructorInitializerSyntax*\)
          * Body \(*BlockSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **ConversionOperatorDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * ImplicitOrExplicitKeyword \(*SyntaxToken*\)
          * OperatorKeyword \(*SyntaxToken*\)
          * Type \(*TypeSyntax*\)
          * ParameterList \(*ParameterListSyntax*\)
          * Body \(*BlockSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **DestructorDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * TildeToken \(*SyntaxToken*\)
          * Identifier \(*SyntaxToken*\)
          * ParameterList \(*ParameterListSyntax*\)
          * Body \(*BlockSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **MethodDeclarationSyntax**
          * Arity \(*int*\)
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * ReturnType \(*TypeSyntax*\)
          * ExplicitInterfaceSpecifier \(*ExplicitInterfaceSpecifierSyntax*\)
          * Identifier \(*SyntaxToken*\)
          * TypeParameterList \(*TypeParameterListSyntax*\)
          * ParameterList \(*ParameterListSyntax*\)
          * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
          * Body \(*BlockSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **OperatorDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * ReturnType \(*TypeSyntax*\)
          * OperatorKeyword \(*SyntaxToken*\)
          * OperatorToken \(*SyntaxToken*\)
          * ParameterList \(*ParameterListSyntax*\)
          * Body \(*BlockSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
      * **BasePropertyDeclarationSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * Type \(*TypeSyntax*\)
        * ExplicitInterfaceSpecifier \(*ExplicitInterfaceSpecifierSyntax*\)
        * AccessorList \(*AccessorListSyntax*\)
        * **EventDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * EventKeyword \(*SyntaxToken*\)
          * Type \(*TypeSyntax*\)
          * ExplicitInterfaceSpecifier \(*ExplicitInterfaceSpecifierSyntax*\)
          * Identifier \(*SyntaxToken*\)
          * AccessorList \(*AccessorListSyntax*\)
        * **IndexerDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * Type \(*TypeSyntax*\)
          * ExplicitInterfaceSpecifier \(*ExplicitInterfaceSpecifierSyntax*\)
          * ThisKeyword \(*SyntaxToken*\)
          * ParameterList \(*BracketedParameterListSyntax*\)
          * AccessorList \(*AccessorListSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **PropertyDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * Type \(*TypeSyntax*\)
          * ExplicitInterfaceSpecifier \(*ExplicitInterfaceSpecifierSyntax*\)
          * Identifier \(*SyntaxToken*\)
          * AccessorList \(*AccessorListSyntax*\)
          * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
          * Initializer \(*EqualsValueClauseSyntax*\)
          * SemicolonToken \(*SyntaxToken*\)
      * **BaseTypeDeclarationSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * Identifier \(*SyntaxToken*\)
        * BaseList \(*BaseListSyntax*\)
        * OpenBraceToken \(*SyntaxToken*\)
        * CloseBraceToken \(*SyntaxToken*\)
        * SemicolonToken \(*SyntaxToken*\)
        * **EnumDeclarationSyntax**
          * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
          * Modifiers \(*SyntaxTokenList*\)
          * EnumKeyword \(*SyntaxToken*\)
          * Identifier \(*SyntaxToken*\)
          * BaseList \(*BaseListSyntax*\)
          * OpenBraceToken \(*SyntaxToken*\)
          * Members \(*SeparatedSyntaxList\<EnumMemberDeclarationSyntax>*\)
          * CloseBraceToken \(*SyntaxToken*\)
          * SemicolonToken \(*SyntaxToken*\)
        * **TypeDeclarationSyntax**
          * Arity \(*int*\)
          * Keyword \(*SyntaxToken*\)
          * TypeParameterList \(*TypeParameterListSyntax*\)
          * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
          * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
          * **ClassDeclarationSyntax**
            * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
            * Modifiers \(*SyntaxTokenList*\)
            * Keyword \(*SyntaxToken*\)
            * Identifier \(*SyntaxToken*\)
            * TypeParameterList \(*TypeParameterListSyntax*\)
            * BaseList \(*BaseListSyntax*\)
            * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
            * OpenBraceToken \(*SyntaxToken*\)
            * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
            * CloseBraceToken \(*SyntaxToken*\)
            * SemicolonToken \(*SyntaxToken*\)
          * **InterfaceDeclarationSyntax**
            * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
            * Modifiers \(*SyntaxTokenList*\)
            * Keyword \(*SyntaxToken*\)
            * Identifier \(*SyntaxToken*\)
            * TypeParameterList \(*TypeParameterListSyntax*\)
            * BaseList \(*BaseListSyntax*\)
            * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
            * OpenBraceToken \(*SyntaxToken*\)
            * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
            * CloseBraceToken \(*SyntaxToken*\)
            * SemicolonToken \(*SyntaxToken*\)
          * **StructDeclarationSyntax**
            * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
            * Modifiers \(*SyntaxTokenList*\)
            * Keyword \(*SyntaxToken*\)
            * Identifier \(*SyntaxToken*\)
            * TypeParameterList \(*TypeParameterListSyntax*\)
            * BaseList \(*BaseListSyntax*\)
            * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
            * OpenBraceToken \(*SyntaxToken*\)
            * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
            * CloseBraceToken \(*SyntaxToken*\)
            * SemicolonToken \(*SyntaxToken*\)
      * **DelegateDeclarationSyntax**
        * Arity \(*int*\)
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * DelegateKeyword \(*SyntaxToken*\)
        * ReturnType \(*TypeSyntax*\)
        * Identifier \(*SyntaxToken*\)
        * TypeParameterList \(*TypeParameterListSyntax*\)
        * ParameterList \(*ParameterListSyntax*\)
        * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **EnumMemberDeclarationSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Identifier \(*SyntaxToken*\)
        * EqualsValue \(*EqualsValueClauseSyntax*\)
      * **GlobalStatementSyntax**
        * Statement \(*StatementSyntax*\)
      * **IncompleteMemberSyntax**
        * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
        * Modifiers \(*SyntaxTokenList*\)
        * Type \(*TypeSyntax*\)
      * **NamespaceDeclarationSyntax**
        * NamespaceKeyword \(*SyntaxToken*\)
        * Name \(*NameSyntax*\)
        * OpenBraceToken \(*SyntaxToken*\)
        * Externs \(*SyntaxList\<ExternAliasDirectiveSyntax>*\)
        * Usings \(*SyntaxList\<UsingDirectiveSyntax>*\)
        * Members \(*SyntaxList\<MemberDeclarationSyntax>*\)
        * CloseBraceToken \(*SyntaxToken*\)
        * SemicolonToken \(*SyntaxToken*\)
    * **NameColonSyntax**
      * Name \(*IdentifierNameSyntax*\)
      * ColonToken \(*SyntaxToken*\)
    * **NameEqualsSyntax**
      * Name \(*IdentifierNameSyntax*\)
      * EqualsToken \(*SyntaxToken*\)
    * **OrderingSyntax**
      * Expression \(*ExpressionSyntax*\)
      * AscendingOrDescendingKeyword \(*SyntaxToken*\)
      * SyntaxKinds:
        * SyntaxKind\.AscendingOrdering
        * SyntaxKind\.DescendingOrdering

    * **ParameterSyntax**
      * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
      * Modifiers \(*SyntaxTokenList*\)
      * Type \(*TypeSyntax*\)
      * Identifier \(*SyntaxToken*\)
      * Default \(*EqualsValueClauseSyntax*\)
    * **PatternSyntax**
      * **ConstantPatternSyntax**
        * Expression \(*ExpressionSyntax*\)
      * **DeclarationPatternSyntax**
        * Type \(*TypeSyntax*\)
        * Designation \(*VariableDesignationSyntax*\)
    * **QueryBodySyntax**
      * Clauses \(*SyntaxList\<QueryClauseSyntax>*\)
      * SelectOrGroup \(*SelectOrGroupClauseSyntax*\)
      * Continuation \(*QueryContinuationSyntax*\)
    * **QueryClauseSyntax**
      * **FromClauseSyntax**
        * FromKeyword \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * Identifier \(*SyntaxToken*\)
        * InKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **JoinClauseSyntax**
        * JoinKeyword \(*SyntaxToken*\)
        * Type \(*TypeSyntax*\)
        * Identifier \(*SyntaxToken*\)
        * InKeyword \(*SyntaxToken*\)
        * InExpression \(*ExpressionSyntax*\)
        * OnKeyword \(*SyntaxToken*\)
        * LeftExpression \(*ExpressionSyntax*\)
        * EqualsKeyword \(*SyntaxToken*\)
        * RightExpression \(*ExpressionSyntax*\)
        * Into \(*JoinIntoClauseSyntax*\)
      * **LetClauseSyntax**
        * LetKeyword \(*SyntaxToken*\)
        * Identifier \(*SyntaxToken*\)
        * EqualsToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
      * **OrderByClauseSyntax**
        * OrderByKeyword \(*SyntaxToken*\)
        * Orderings \(*SeparatedSyntaxList\<OrderingSyntax>*\)
      * **WhereClauseSyntax**
        * WhereKeyword \(*SyntaxToken*\)
        * Condition \(*ExpressionSyntax*\)
    * **QueryContinuationSyntax**
      * IntoKeyword \(*SyntaxToken*\)
      * Identifier \(*SyntaxToken*\)
      * Body \(*QueryBodySyntax*\)
    * **SelectOrGroupClauseSyntax**
      * **GroupClauseSyntax**
        * GroupKeyword \(*SyntaxToken*\)
        * GroupExpression \(*ExpressionSyntax*\)
        * ByKeyword \(*SyntaxToken*\)
        * ByExpression \(*ExpressionSyntax*\)
      * **SelectClauseSyntax**
        * SelectKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
    * **StatementSyntax**
      * **BlockSyntax**
        * OpenBraceToken \(*SyntaxToken*\)
        * Statements \(*SyntaxList\<StatementSyntax>*\)
        * CloseBraceToken \(*SyntaxToken*\)
      * **BreakStatementSyntax**
        * BreakKeyword \(*SyntaxToken*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **CommonForEachStatementSyntax**
        * ForEachKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * InKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
        * **ForEachStatementSyntax**
          * ForEachKeyword \(*SyntaxToken*\)
          * OpenParenToken \(*SyntaxToken*\)
          * Type \(*TypeSyntax*\)
          * Identifier \(*SyntaxToken*\)
          * InKeyword \(*SyntaxToken*\)
          * Expression \(*ExpressionSyntax*\)
          * CloseParenToken \(*SyntaxToken*\)
          * Statement \(*StatementSyntax*\)
        * **ForEachVariableStatementSyntax**
          * ForEachKeyword \(*SyntaxToken*\)
          * OpenParenToken \(*SyntaxToken*\)
          * Variable \(*ExpressionSyntax*\)
          * InKeyword \(*SyntaxToken*\)
          * Expression \(*ExpressionSyntax*\)
          * CloseParenToken \(*SyntaxToken*\)
          * Statement \(*StatementSyntax*\)
      * **ContinueStatementSyntax**
        * ContinueKeyword \(*SyntaxToken*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **DoStatementSyntax**
        * DoKeyword \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
        * WhileKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Condition \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **EmptyStatementSyntax**
        * SemicolonToken \(*SyntaxToken*\)
      * **ExpressionStatementSyntax**
        * AllowsAnyExpression \(*bool*\)
        * Expression \(*ExpressionSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **FixedStatementSyntax**
        * FixedKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Declaration \(*VariableDeclarationSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **ForStatementSyntax**
        * ForKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Declaration \(*VariableDeclarationSyntax*\)
        * Initializers \(*SeparatedSyntaxList\<ExpressionSyntax>*\)
        * FirstSemicolonToken \(*SyntaxToken*\)
        * Condition \(*ExpressionSyntax*\)
        * SecondSemicolonToken \(*SyntaxToken*\)
        * Incrementors \(*SeparatedSyntaxList\<ExpressionSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **GotoStatementSyntax**
        * GotoKeyword \(*SyntaxToken*\)
        * CaseOrDefaultKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.GotoCaseStatement
          * SyntaxKind\.GotoDefaultStatement
          * SyntaxKind\.GotoStatement

      * **CheckedStatementSyntax**
        * Keyword \(*SyntaxToken*\)
        * Block \(*BlockSyntax*\)
        * SyntaxKinds:
          * SyntaxKind\.CheckedStatement
          * SyntaxKind\.UncheckedStatement

      * **IfStatementSyntax**
        * IfKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Condition \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
        * Else \(*ElseClauseSyntax*\)
      * **LabeledStatementSyntax**
        * Identifier \(*SyntaxToken*\)
        * ColonToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **LocalDeclarationStatementSyntax**
        * IsConst \(*bool*\)
        * Modifiers \(*SyntaxTokenList*\)
        * Declaration \(*VariableDeclarationSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **LocalFunctionStatementSyntax**
        * Modifiers \(*SyntaxTokenList*\)
        * ReturnType \(*TypeSyntax*\)
        * Identifier \(*SyntaxToken*\)
        * TypeParameterList \(*TypeParameterListSyntax*\)
        * ParameterList \(*ParameterListSyntax*\)
        * ConstraintClauses \(*SyntaxList\<TypeParameterConstraintClauseSyntax>*\)
        * Body \(*BlockSyntax*\)
        * ExpressionBody \(*ArrowExpressionClauseSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **LockStatementSyntax**
        * LockKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **ReturnStatementSyntax**
        * ReturnKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **SwitchStatementSyntax**
        * SwitchKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * OpenBraceToken \(*SyntaxToken*\)
        * Sections \(*SyntaxList\<SwitchSectionSyntax>*\)
        * CloseBraceToken \(*SyntaxToken*\)
      * **ThrowStatementSyntax**
        * ThrowKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
      * **TryStatementSyntax**
        * TryKeyword \(*SyntaxToken*\)
        * Block \(*BlockSyntax*\)
        * Catches \(*SyntaxList\<CatchClauseSyntax>*\)
        * Finally \(*FinallyClauseSyntax*\)
      * **UnsafeStatementSyntax**
        * UnsafeKeyword \(*SyntaxToken*\)
        * Block \(*BlockSyntax*\)
      * **UsingStatementSyntax**
        * UsingKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Declaration \(*VariableDeclarationSyntax*\)
        * Expression \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **WhileStatementSyntax**
        * WhileKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * Condition \(*ExpressionSyntax*\)
        * CloseParenToken \(*SyntaxToken*\)
        * Statement \(*StatementSyntax*\)
      * **YieldStatementSyntax**
        * YieldKeyword \(*SyntaxToken*\)
        * ReturnOrBreakKeyword \(*SyntaxToken*\)
        * Expression \(*ExpressionSyntax*\)
        * SemicolonToken \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.YieldBreakStatement
          * SyntaxKind\.YieldReturnStatement

    * **StructuredTriviaSyntax**
      * ParentTrivia \(*SyntaxTrivia*\)
      * **DirectiveTriviaSyntax**
        * DirectiveNameToken \(*SyntaxToken*\)
        * HashToken \(*SyntaxToken*\)
        * EndOfDirectiveToken \(*SyntaxToken*\)
        * IsActive \(*bool*\)
        * **BadDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * Identifier \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **BranchingDirectiveTriviaSyntax**
          * BranchTaken \(*bool*\)
          * **ConditionalDirectiveTriviaSyntax**
            * Condition \(*ExpressionSyntax*\)
            * ConditionValue \(*bool*\)
            * **ElifDirectiveTriviaSyntax**
              * HashToken \(*SyntaxToken*\)
              * ElifKeyword \(*SyntaxToken*\)
              * Condition \(*ExpressionSyntax*\)
              * EndOfDirectiveToken \(*SyntaxToken*\)
              * IsActive \(*bool*\)
              * BranchTaken \(*bool*\)
              * ConditionValue \(*bool*\)
            * **IfDirectiveTriviaSyntax**
              * HashToken \(*SyntaxToken*\)
              * IfKeyword \(*SyntaxToken*\)
              * Condition \(*ExpressionSyntax*\)
              * EndOfDirectiveToken \(*SyntaxToken*\)
              * IsActive \(*bool*\)
              * BranchTaken \(*bool*\)
              * ConditionValue \(*bool*\)
          * **ElseDirectiveTriviaSyntax**
            * HashToken \(*SyntaxToken*\)
            * ElseKeyword \(*SyntaxToken*\)
            * EndOfDirectiveToken \(*SyntaxToken*\)
            * IsActive \(*bool*\)
            * BranchTaken \(*bool*\)
        * **DefineDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * DefineKeyword \(*SyntaxToken*\)
          * Name \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **EndIfDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * EndIfKeyword \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **EndRegionDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * EndRegionKeyword \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **ErrorDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * ErrorKeyword \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **LineDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * LineKeyword \(*SyntaxToken*\)
          * Line \(*SyntaxToken*\)
          * File \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **LoadDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * LoadKeyword \(*SyntaxToken*\)
          * File \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **PragmaChecksumDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * PragmaKeyword \(*SyntaxToken*\)
          * ChecksumKeyword \(*SyntaxToken*\)
          * File \(*SyntaxToken*\)
          * Guid \(*SyntaxToken*\)
          * Bytes \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **PragmaWarningDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * PragmaKeyword \(*SyntaxToken*\)
          * WarningKeyword \(*SyntaxToken*\)
          * DisableOrRestoreKeyword \(*SyntaxToken*\)
          * ErrorCodes \(*SeparatedSyntaxList\<ExpressionSyntax>*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **ReferenceDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * ReferenceKeyword \(*SyntaxToken*\)
          * File \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **RegionDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * RegionKeyword \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **ShebangDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * ExclamationToken \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **UndefDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * UndefKeyword \(*SyntaxToken*\)
          * Name \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
        * **WarningDirectiveTriviaSyntax**
          * HashToken \(*SyntaxToken*\)
          * WarningKeyword \(*SyntaxToken*\)
          * EndOfDirectiveToken \(*SyntaxToken*\)
          * IsActive \(*bool*\)
      * **DocumentationCommentTriviaSyntax**
        * Content \(*SyntaxList\<XmlNodeSyntax>*\)
        * EndOfComment \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.MultiLineDocumentationCommentTrivia
          * SyntaxKind\.SingleLineDocumentationCommentTrivia

      * **SkippedTokensTriviaSyntax**
        * Tokens \(*SyntaxTokenList*\)
    * **SwitchLabelSyntax**
      * Keyword \(*SyntaxToken*\)
      * ColonToken \(*SyntaxToken*\)
      * **CasePatternSwitchLabelSyntax**
        * Keyword \(*SyntaxToken*\)
        * Pattern \(*PatternSyntax*\)
        * WhenClause \(*WhenClauseSyntax*\)
        * ColonToken \(*SyntaxToken*\)
      * **CaseSwitchLabelSyntax**
        * Keyword \(*SyntaxToken*\)
        * Value \(*ExpressionSyntax*\)
        * ColonToken \(*SyntaxToken*\)
      * **DefaultSwitchLabelSyntax**
        * Keyword \(*SyntaxToken*\)
        * ColonToken \(*SyntaxToken*\)
    * **SwitchSectionSyntax**
      * Labels \(*SyntaxList\<SwitchLabelSyntax>*\)
      * Statements \(*SyntaxList\<StatementSyntax>*\)
    * **TupleElementSyntax**
      * Type \(*TypeSyntax*\)
      * Identifier \(*SyntaxToken*\)
    * **TypeArgumentListSyntax**
      * LessThanToken \(*SyntaxToken*\)
      * Arguments \(*SeparatedSyntaxList\<TypeSyntax>*\)
      * GreaterThanToken \(*SyntaxToken*\)
    * **TypeParameterConstraintClauseSyntax**
      * WhereKeyword \(*SyntaxToken*\)
      * Name \(*IdentifierNameSyntax*\)
      * ColonToken \(*SyntaxToken*\)
      * Constraints \(*SeparatedSyntaxList\<TypeParameterConstraintSyntax>*\)
    * **TypeParameterConstraintSyntax**
      * **ClassOrStructConstraintSyntax**
        * ClassOrStructKeyword \(*SyntaxToken*\)
        * SyntaxKinds:
          * SyntaxKind\.ClassConstraint
          * SyntaxKind\.StructConstraint

      * **ConstructorConstraintSyntax**
        * NewKeyword \(*SyntaxToken*\)
        * OpenParenToken \(*SyntaxToken*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **TypeConstraintSyntax**
        * Type \(*TypeSyntax*\)
    * **TypeParameterListSyntax**
      * LessThanToken \(*SyntaxToken*\)
      * Parameters \(*SeparatedSyntaxList\<TypeParameterSyntax>*\)
      * GreaterThanToken \(*SyntaxToken*\)
    * **TypeParameterSyntax**
      * AttributeLists \(*SyntaxList\<AttributeListSyntax>*\)
      * VarianceKeyword \(*SyntaxToken*\)
      * Identifier \(*SyntaxToken*\)
    * **UsingDirectiveSyntax**
      * UsingKeyword \(*SyntaxToken*\)
      * StaticKeyword \(*SyntaxToken*\)
      * Alias \(*NameEqualsSyntax*\)
      * Name \(*NameSyntax*\)
      * SemicolonToken \(*SyntaxToken*\)
    * **VariableDeclarationSyntax**
      * Type \(*TypeSyntax*\)
      * Variables \(*SeparatedSyntaxList\<VariableDeclaratorSyntax>*\)
    * **VariableDeclaratorSyntax**
      * Identifier \(*SyntaxToken*\)
      * ArgumentList \(*BracketedArgumentListSyntax*\)
      * Initializer \(*EqualsValueClauseSyntax*\)
    * **VariableDesignationSyntax**
      * **DiscardDesignationSyntax**
        * UnderscoreToken \(*SyntaxToken*\)
      * **ParenthesizedVariableDesignationSyntax**
        * OpenParenToken \(*SyntaxToken*\)
        * Variables \(*SeparatedSyntaxList\<VariableDesignationSyntax>*\)
        * CloseParenToken \(*SyntaxToken*\)
      * **SingleVariableDesignationSyntax**
        * Identifier \(*SyntaxToken*\)
    * **WhenClauseSyntax**
      * WhenKeyword \(*SyntaxToken*\)
      * Condition \(*ExpressionSyntax*\)
    * **XmlAttributeSyntax**
      * Name \(*XmlNameSyntax*\)
      * EqualsToken \(*SyntaxToken*\)
      * StartQuoteToken \(*SyntaxToken*\)
      * EndQuoteToken \(*SyntaxToken*\)
      * **XmlCrefAttributeSyntax**
        * Name \(*XmlNameSyntax*\)
        * EqualsToken \(*SyntaxToken*\)
        * StartQuoteToken \(*SyntaxToken*\)
        * Cref \(*CrefSyntax*\)
        * EndQuoteToken \(*SyntaxToken*\)
      * **XmlNameAttributeSyntax**
        * Name \(*XmlNameSyntax*\)
        * EqualsToken \(*SyntaxToken*\)
        * StartQuoteToken \(*SyntaxToken*\)
        * Identifier \(*IdentifierNameSyntax*\)
        * EndQuoteToken \(*SyntaxToken*\)
      * **XmlTextAttributeSyntax**
        * Name \(*XmlNameSyntax*\)
        * EqualsToken \(*SyntaxToken*\)
        * StartQuoteToken \(*SyntaxToken*\)
        * TextTokens \(*SyntaxTokenList*\)
        * EndQuoteToken \(*SyntaxToken*\)
    * **XmlElementEndTagSyntax**
      * LessThanSlashToken \(*SyntaxToken*\)
      * Name \(*XmlNameSyntax*\)
      * GreaterThanToken \(*SyntaxToken*\)
    * **XmlElementStartTagSyntax**
      * LessThanToken \(*SyntaxToken*\)
      * Name \(*XmlNameSyntax*\)
      * Attributes \(*SyntaxList\<XmlAttributeSyntax>*\)
      * GreaterThanToken \(*SyntaxToken*\)
    * **XmlNameSyntax**
      * Prefix \(*XmlPrefixSyntax*\)
      * LocalName \(*SyntaxToken*\)
    * **XmlNodeSyntax**
      * **XmlCDataSectionSyntax**
        * StartCDataToken \(*SyntaxToken*\)
        * TextTokens \(*SyntaxTokenList*\)
        * EndCDataToken \(*SyntaxToken*\)
      * **XmlCommentSyntax**
        * LessThanExclamationMinusMinusToken \(*SyntaxToken*\)
        * TextTokens \(*SyntaxTokenList*\)
        * MinusMinusGreaterThanToken \(*SyntaxToken*\)
      * **XmlElementSyntax**
        * StartTag \(*XmlElementStartTagSyntax*\)
        * Content \(*SyntaxList\<XmlNodeSyntax>*\)
        * EndTag \(*XmlElementEndTagSyntax*\)
      * **XmlEmptyElementSyntax**
        * LessThanToken \(*SyntaxToken*\)
        * Name \(*XmlNameSyntax*\)
        * Attributes \(*SyntaxList\<XmlAttributeSyntax>*\)
        * SlashGreaterThanToken \(*SyntaxToken*\)
      * **XmlProcessingInstructionSyntax**
        * StartProcessingInstructionToken \(*SyntaxToken*\)
        * Name \(*XmlNameSyntax*\)
        * TextTokens \(*SyntaxTokenList*\)
        * EndProcessingInstructionToken \(*SyntaxToken*\)
      * **XmlTextSyntax**
        * TextTokens \(*SyntaxTokenList*\)
    * **XmlPrefixSyntax**
      * Prefix \(*SyntaxToken*\)
      * ColonToken \(*SyntaxToken*\)

*\(Generated with [DotMarkdown](http://github.com/JosefPihrt/DotMarkdown)\)*