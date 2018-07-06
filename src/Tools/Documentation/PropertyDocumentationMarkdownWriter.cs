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
            Writer.WriteStartHeading(1 + HeadingBaseLevel);

            SymbolDisplayFormat format = (Symbols.Length == 1) ? FormatProvider.MemberTitleFormat : FormatProvider.OverloadedMemberTitleFormat;

            Writer.WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalOptions.UseItemProperty));
            Writer.WriteString(" ");
            Writer.WriteString(CategoryName);
            Writer.WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                Writer.WriteStartHeading(1 + HeadingBaseLevel);
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty));
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
            Writer.WriteHeading(3 + HeadingBaseLevel, "Property Value");
            WriteLink(Generator.GetDocumentationInfo(propertySymbol.Type), SymbolDisplayAdditionalOptions.None);
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
