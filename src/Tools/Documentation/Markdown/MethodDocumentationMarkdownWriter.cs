// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using System.Xml.Linq;

namespace Roslynator.Documentation.Markdown
{
    public class MethodDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public MethodDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormatProvider formatProvider) : base(symbols, directoryInfo, formatProvider)
        {
        }

        public override string CategoryName => "Method";

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            SymbolDisplayFormat format = (Symbols.Length == 1) ? FormatProvider.MemberTitleFormat : FormatProvider.OverloadedMemberTitleFormat;

            WriteString(symbol.ToDisplayString(format));
            WriteString(" ");
            WriteString(CategoryName);
            WriteEndHeading();
        }

        public override void WriteMemberTitle(ISymbol symbol)
        {
            if (Symbols.Length > 1)
            {
                WriteStartHeading(1 + BaseHeadingLevel);
                WriteString(symbol.ToDisplayString(FormatProvider.MethodFormat));
                WriteEndHeading();
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
            WriteHeading(3 + BaseHeadingLevel, "Returns");
            WriteLink(Compilation.GetDocumentationInfo(methodSymbol.ReturnType), SymbolDisplayAdditionalOptions.None);
            WriteLine();
            WriteLine();

            XElement element = Compilation.GetDocumentationElement(methodSymbol, "returns");

            if (element != null)
            {
                WriteElementContent(element);
                WriteLine();
                WriteLine();
            }
        }
    }
}
