// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public class ConstructorDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public ConstructorDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options) : base(symbols, directoryInfo, options)
        {
        }

        public override string CategoryName => "Constructor";

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            if (Symbols.Length == 1)
            {
                WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                WriteString(" Constructor");
            }
            else
            {
                WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                WriteString(" Constructors");
            }

            WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                WriteStartHeading(1 + BaseHeadingLevel);
                WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                WriteEndHeading();
            }
        }

        public override void WriteContent(ISymbol symbol)
        {
            WriteSummary(symbol);
            WriteSignature(symbol);
            WriteParameters(symbol);
            WriteAttributes(symbol);
            WriteExceptions(symbol);
            WriteExamples(symbol);
            WriteRemarks(symbol);
            WriteSeeAlso(symbol);
        }
    }
}
