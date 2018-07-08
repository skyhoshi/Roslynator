// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public class PropertyDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public PropertyDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options) : base(symbols, directoryInfo, options)
        {
        }

        public override string CategoryName => "Property";

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            SymbolDisplayFormat format = (Symbols.Length == 1) ? FormatProvider.MemberTitleFormat : FormatProvider.OverloadedMemberTitleFormat;

            WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalOptions.UseItemProperty));
            WriteString(" ");
            WriteString(CategoryName);
            WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                WriteStartHeading(1 + BaseHeadingLevel);
                WriteString(symbol.ToDisplayString(FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty));
                WriteEndHeading();
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
            WriteHeading(3 + BaseHeadingLevel, "Property Value");
            WriteLink(Compilation.GetSymbolInfo(propertySymbol.Type), SymbolDisplayAdditionalOptions.None);
            WriteLine();
            WriteLine();

            string elementName = (propertySymbol.IsIndexer) ? "returns" : "value";

            XElement element = Compilation.GetDocumentationElement(propertySymbol, elementName);

            if (element != null)
            {
                WriteElementContent(element);
                WriteLine();
                WriteLine();
            }
        }
    }
}
