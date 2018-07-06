// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class FieldDocumentationMarkdownWriter : MemberDocumentationWriter
    {
        public FieldDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols, directoryInfo, generator, writer)
        {
        }

        public override string CategoryName => "Field";

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingBaseLevel);
            Writer.WriteString(symbol.ToDisplayString(FormatProvider.MemberTitleFormat));
            Writer.WriteString(" ");
            Writer.WriteString(CategoryName);
            Writer.WriteEndHeading();
        }

        public override void WriteContent(ISymbol symbol)
        {
            WriteSummary(symbol);
            WriteSignature(symbol);
            WriteValue((IFieldSymbol)symbol);
            WriteAttributes(symbol);
            WriteExamples(symbol);
            WriteRemarks(symbol);
            WriteSeeAlso(symbol);
        }

        private void WriteValue(IFieldSymbol fieldSymbol)
        {
            Writer.WriteHeading(3 + HeadingBaseLevel, "Field Value");
            WriteLink(Generator.GetDocumentationInfo(fieldSymbol.Type), SymbolDisplayAdditionalOptions.None);
        }
    }
}
