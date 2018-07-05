// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class ConstructorsDocumentationMarkdownWriter : ConstructorDocumentationMarkdownWriter
    {
        public ConstructorsDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols, directoryInfo, generator, writer)
        {
        }

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingLevel);
            Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
            Writer.WriteString(" Constructors");
            Writer.WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingLevel);
            Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
            Writer.WriteEndHeading();
        }
    }
}
