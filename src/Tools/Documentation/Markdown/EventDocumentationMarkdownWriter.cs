// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class EventDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public EventDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormatProvider formatProvider) : base(symbols, directoryInfo, formatProvider)
        {
        }

        public override string CategoryName => "Event";

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);
            WriteString(symbol.ToDisplayString(FormatProvider.MemberTitleFormat));
            WriteString(" ");
            WriteString(CategoryName);
            WriteEndHeading();
        }

        public override void WriteContent(ISymbol symbol)
        {
            WriteSummary(symbol);
            WriteSignature(symbol);
            WriteImplements(symbol);
            WriteAttributes(symbol);
            WriteExamples(symbol);
            WriteRemarks(symbol);
            WriteSeeAlso(symbol);
        }
    }
}
