// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public partial class MemberDocumentationWriter
    {
        internal class MethodDocumentationWriter : MemberDocumentationWriter
        {
            public MethodDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.MethodFormat;

            public override MemberDocumentationParts Parts
            {
                get
                {
                    return MemberDocumentationParts.Summary
                        | MemberDocumentationParts.Signature
                        | MemberDocumentationParts.TypeParameters
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
                var methodSymbol = (IMethodSymbol)symbol;

                Writer.WriteHeading(3 + BaseHeadingLevel, Resources.ReturnsTitle);
                Writer.WriteLink(methodSymbol.ReturnType, FormatProvider.TypeFormat);
                Writer.WriteLine();
                Writer.WriteLine();

                XElement element = CompilationInfo.GetDocumentationElement(methodSymbol, "returns");

                if (element != null)
                {
                    Writer.WriteElementContent(element);
                    Writer.WriteLine();
                    Writer.WriteLine();
                }
            }
        }
    }
}
