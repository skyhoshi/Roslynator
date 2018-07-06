// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace Roslynator.Documentation
{
    public class MethodDocumentationMarkdownWriter : MemberDocumentationWriter
    {
        public MethodDocumentationMarkdownWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols, directoryInfo, generator, writer)
        {
        }

        public override string CategoryName => "Method";

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingBaseLevel);

            SymbolDisplayFormat format = (Symbols.Length == 1) ? FormatProvider.MemberTitleFormat : FormatProvider.OverloadedMemberTitleFormat;

            Writer.WriteString(symbol.ToDisplayString(format));
            Writer.WriteString(" ");
            Writer.WriteString(CategoryName);
            Writer.WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                Writer.WriteStartHeading(1 + HeadingBaseLevel);
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.MethodFormat));
                Writer.WriteEndHeading();
            }
        }

        public override void WriteContent(ISymbol symbol)
        {
            WriteSummary(symbol);
            WriteSignature(symbol);
            WriteParameters(symbol);
            WriteTypeParameters(symbol);
            WriteValue((IMethodSymbol)symbol);
            WriteImplements(symbol);
            WriteAttributes(symbol);
            WriteExceptions(symbol);
            WriteExamples(symbol);
            WriteRemarks(symbol);
            WriteSeeAlso(symbol);
        }

        private void WriteValue(IMethodSymbol methodSymbol)
        {
            Writer.WriteHeading(3 + HeadingBaseLevel, "Returns");
            WriteLink(Generator.GetDocumentationInfo(methodSymbol.ReturnType));
            Writer.WriteLine();
            Writer.WriteLine();

            XElement element = Generator.GetDocumentationElement(methodSymbol, "returns");

            if (element != null)
            {
                WriteElementContent(element);
                Writer.WriteLine();
                Writer.WriteLine();
            }
        }
    }
}
