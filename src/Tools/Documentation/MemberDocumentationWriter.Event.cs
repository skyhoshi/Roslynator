// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public partial class MemberDocumentationWriter
    {
        internal class EventDocumentationWriter : MemberDocumentationWriter
        {
            public EventDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.EventFormat;

            public override MemberDocumentationParts Parts
            {
                get
                {
                    return MemberDocumentationParts.Summary
                        | MemberDocumentationParts.Signature
                        | MemberDocumentationParts.Implements
                        | MemberDocumentationParts.Attributes
                        | MemberDocumentationParts.Examples
                        | MemberDocumentationParts.Remarks
                        | MemberDocumentationParts.SeeAlso;
                }
            }
        }
    }
}
