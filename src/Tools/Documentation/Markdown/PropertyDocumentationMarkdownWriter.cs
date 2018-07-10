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
            DocumentationOptions options,
            DocumentationResources resources) : base(symbols, directoryInfo, options, resources)
        {
        }

        public override SymbolDisplayFormat Format => FormatProvider.PropertyFormat;

        public override MemberDocumentationParts Parts
        {
            get
            {
                return MemberDocumentationParts.Summary
                    | MemberDocumentationParts.Signature
                    | MemberDocumentationParts.Parameters
                    | MemberDocumentationParts.ReturnValue
                    | MemberDocumentationParts.Implements
                    | MemberDocumentationParts.Attributes
                    | MemberDocumentationParts.Exceptions
                    | MemberDocumentationParts.Examples
                    | MemberDocumentationParts.Remarks
                    | MemberDocumentationParts.SeeAlso;
            }
        }

        public override void WriteReturnValue(ISymbol symbol)
        {
            var propertySymbol = (IPropertySymbol)Symbol;

            WriteHeading(3 + BaseHeadingLevel, Resources.PropertyValue);
            WriteLink(propertySymbol.Type, SymbolDisplayAdditionalOptions.None);
            WriteLine();
            WriteLine();

            string elementName = (propertySymbol.IsIndexer) ? "returns" : "value";

            XElement element = CompilationInfo.GetDocumentationElement(propertySymbol, elementName);

            if (element != null)
            {
                WriteElementContent(element);
                WriteLine();
                WriteLine();
            }
        }
    }
}
