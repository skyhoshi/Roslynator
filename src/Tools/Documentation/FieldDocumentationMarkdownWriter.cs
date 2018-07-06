// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class FieldDocumentationMarkdownWriter : MemberDocumentationWriter
    {
        public FieldDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator) : base(symbols, directoryInfo, generator)
        {
        }

        public override string CategoryName => "Field";

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + HeadingBaseLevel);
            WriteString(symbol.ToDisplayString(FormatProvider.MemberTitleFormat));
            WriteString(" ");
            WriteString(CategoryName);
            WriteEndHeading();
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
            WriteHeading(3 + HeadingBaseLevel, "Field Value");
            WriteLink(Generator.GetDocumentationInfo(fieldSymbol.Type), SymbolDisplayAdditionalOptions.None);
        }
    }
}
