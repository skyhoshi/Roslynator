// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public class ConstructorDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public ConstructorDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options,
            DocumentationResources resources) : base(symbols, directoryInfo, options, resources)
        {
        }

        public override SymbolDisplayFormat Format => FormatProvider.ConstructorFormat;

        public override MemberDocumentationParts Parts
        {
            get
            {
                return MemberDocumentationParts.Summary
                    | MemberDocumentationParts.Signature
                    | MemberDocumentationParts.Parameters
                    | MemberDocumentationParts.Attributes
                    | MemberDocumentationParts.Exceptions
                    | MemberDocumentationParts.Examples
                    | MemberDocumentationParts.Remarks
                    | MemberDocumentationParts.SeeAlso;
            }
        }

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            if (Symbols.Length == 1)
            {
                WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                WriteString(" ");
                WriteString(Resources.Constructor);
            }
            else
            {
                WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                WriteString(" ");
                WriteString(Resources.Constructors);
            }

            WriteEndHeading();
        }
    }
}
