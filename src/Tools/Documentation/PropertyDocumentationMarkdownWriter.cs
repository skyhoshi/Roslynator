// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace Roslynator.Documentation
{
    public class PropertyDocumentationMarkdownWriter : MemberDocumentationWriter
    {
        public PropertyDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols, directoryInfo, generator, writer)
        {
        }

        public override string CategoryName => "Property";

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingLevel);

            if (Symbols.Length == 1)
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.PropertyFormat));
                Writer.WriteString(" Property");
            }
            else
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                Writer.WriteString(" Properties");
            }

            Writer.WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                Writer.WriteStartHeading(1 + HeadingLevel);
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.PropertyFormat));
                Writer.WriteEndHeading();
            }
        }

        public override void WriteContent(ISymbol symbol)
        {
            WriteSummary(symbol);
            WriteSignature(symbol);
            WriteParameters(symbol);
            WriteValue((IPropertySymbol)symbol);
            WriteImplements(symbol);
            WriteAttributes(symbol);
            WriteExceptions(symbol);
            WriteExamples(symbol);
            WriteRemarks(symbol);
            WriteSeeAlso(symbol);
        }

        private void WriteValue(IPropertySymbol propertySymbol)
        {
            Writer.WriteHeading(4 + HeadingLevel, "Property Value");
            WriteLink(Generator.GetDocumentationInfo(propertySymbol.Type));
            Writer.WriteLine();
            Writer.WriteLine();

            string elementName = (propertySymbol.IsIndexer) ? "returns" : "value";

            XElement element = Generator.GetDocumentationElement(propertySymbol, elementName);

            if (element != null)
            {
                WriteElementContent(element);
                Writer.WriteLine();
                Writer.WriteLine();
            }
        }
    }
}
