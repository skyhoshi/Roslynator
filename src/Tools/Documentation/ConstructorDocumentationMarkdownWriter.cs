// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public class ConstructorDocumentationMarkdownWriter : DocumentationMarkdownWriter
    {
        public ConstructorDocumentationMarkdownWriter(
            ISymbol symbol,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbol, directoryInfo, generator, writer)
        {
        }

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1);
            Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
            Writer.WriteString(" Constructor");
            Writer.WriteEndHeading();
        }
    }
}
