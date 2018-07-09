// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public class FieldDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public FieldDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options) : base(symbols, directoryInfo, options)
        {
        }

        public override string KindName => "Field";

        public override SymbolDisplayFormat Format => FormatProvider.FieldFormat;

        public override MemberDocumentationParts Parts
        {
            get
            {
                return MemberDocumentationParts.Summary
                    | MemberDocumentationParts.Signature
                    | MemberDocumentationParts.ReturnValue
                    | MemberDocumentationParts.Attributes
                    | MemberDocumentationParts.Examples
                    | MemberDocumentationParts.Remarks
                    | MemberDocumentationParts.SeeAlso;
            }
        }

        public override void WriteReturnValue(ISymbol symbol)
        {
            var fieldSymbol = (IFieldSymbol)Symbol;

            WriteHeading(3 + BaseHeadingLevel, "Field Value");
            WriteLink(Compilation.GetSymbolInfo(fieldSymbol.Type), SymbolDisplayAdditionalOptions.None);
        }
    }
}
