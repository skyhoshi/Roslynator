// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public class OperatorDocumentationMarkdownWriter : MemberDocumentationMarkdownWriter
    {
        public OperatorDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options,
            DocumentationResources resources) : base(symbols, directoryInfo, options, resources)
        {
        }

        public override SymbolDisplayFormat Format => FormatProvider.MethodFormat;

        public override MemberDocumentationParts Parts
        {
            get
            {
                return MemberDocumentationParts.Summary
                    | MemberDocumentationParts.Signature
                    | MemberDocumentationParts.Parameters
                    | MemberDocumentationParts.ReturnValue
                    | MemberDocumentationParts.Attributes
                    | MemberDocumentationParts.Exceptions
                    | MemberDocumentationParts.Examples
                    | MemberDocumentationParts.Remarks
                    | MemberDocumentationParts.SeeAlso;
            }
        }

        public override void WriteReturnValue(ISymbol symbol)
        {
            var methodSymbol = (IMethodSymbol)symbol;

            WriteHeading(3 + BaseHeadingLevel, Resources.Returns);
            WriteLink(methodSymbol.ReturnType, SymbolDisplayAdditionalOptions.None);
            WriteLine();
            WriteLine();

            XElement element = CompilationInfo.GetDocumentationElement(methodSymbol, "returns");

            if (element != null)
            {
                WriteElementContent(element);
                WriteLine();
                WriteLine();
            }
        }
    }
}
