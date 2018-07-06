// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class MarkdownWriterExtensions
    {
        public static void WriteLink(
            this MarkdownWriter writer,
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions)
        {
            string url = symbolInfo.GetUrl(directoryInfo);

            writer.WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(format, additionalOptions), url);
        }

        public static void WriteTableCell(this MarkdownWriter writer, string text)
        {
            writer.WriteStartTableCell();
            writer.WriteString(text);
            writer.WriteEndTableCell();
        }
    }
}
