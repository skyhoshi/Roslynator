// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class FieldDocumentationWriter : MemberDocumentationWriter
    {
        public FieldDocumentationWriter(DocumentationWriter writer) : base(writer)
        {
        }

        public override SymbolDisplayFormat Format => FormatProvider.FieldFormat;

        public override MemberDocumentationParts Parts
        {
            get
            {
                return MemberDocumentationParts.Summary
                    | MemberDocumentationParts.Signature
                    | MemberDocumentationParts.ReturnValue
                    | MemberDocumentationParts.Attributes
                    | MemberDocumentationParts.Examples
                    | MemberDocumentationParts.Remarks
                    | MemberDocumentationParts.SeeAlso;
            }
        }

        public override void WriteReturnValue(ISymbol symbol)
        {
            var fieldSymbol = (IFieldSymbol)symbol;

            Writer.WriteHeading(3 + BaseHeadingLevel, Resources.FieldValue);
            Writer.WriteLink(fieldSymbol.Type, SymbolDisplayAdditionalOptions.None);
        }
    }
}
