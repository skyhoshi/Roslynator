// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class ConstructorDocumentationWriter : MemberDocumentationWriter
    {
        public ConstructorDocumentationWriter(DocumentationWriter writer) : base(writer)
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

        public override void WriteTitle(ISymbol symbol, bool hasOverloads)
        {
            Writer.WriteStartHeading(1 + BaseHeadingLevel);

            if (!hasOverloads)
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                Writer.WriteSpace();
                Writer.WriteString(Resources.Constructor);
            }
            else
            {
                Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                Writer.WriteSpace();
                Writer.WriteString(Resources.Constructors);
            }

            Writer.WriteEndHeading();
        }
    }
}
