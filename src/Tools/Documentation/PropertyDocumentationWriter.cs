// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class PropertyDocumentationWriter : MemberDocumentationWriter
    {
        public PropertyDocumentationWriter(DocumentationWriter writer) : base(writer)
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
            var propertySymbol = (IPropertySymbol)symbol;

            Writer.WriteHeading(3 + BaseHeadingLevel, Resources.PropertyValueTitle);
            Writer.WriteLink(propertySymbol.Type, SymbolDisplayAdditionalOptions.None);
            Writer.WriteLine();
            Writer.WriteLine();

            string elementName = (propertySymbol.IsIndexer) ? "returns" : "value";

            XElement element = CompilationInfo.GetDocumentationElement(propertySymbol, elementName);

            if (element != null)
            {
                Writer.WriteElementContent(element);
                Writer.WriteLine();
                Writer.WriteLine();
            }
        }
    }
}
