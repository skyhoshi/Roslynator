// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class ConstructorDocumentationMarkdownWriter : MemberDocumentationWriter
    {
        public ConstructorDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols, directoryInfo, generator, writer)
        {
        }

        public override string CategoryName => "Constructor";

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingLevel);

            if (Symbols.Length == 1)
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                Writer.WriteString(" Constructor");
            }
            else
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                Writer.WriteString(" Constructors");
            }

            Writer.WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                Writer.WriteStartHeading(1 + HeadingLevel);
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                Writer.WriteEndHeading();
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
