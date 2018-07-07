// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Text;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationMarkdownWriter : DocumentationWriter
    {
        public DocumentationMarkdownWriter(
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormatProvider formatProvider) : base(symbolInfo, directoryInfo, formatProvider)
        {
            Writer = MarkdownWriter.Create(new StringBuilder());
        }

        private MarkdownWriter Writer { get; }

        public override void WriteStartBold() => Writer.WriteStartBold();

        public override void WriteEndBold() => Writer.WriteEndBold();

        public override void WriteStartItalic() => Writer.WriteStartItalic();

        public override void WriteEndItalic() => Writer.WriteEndItalic();

        public override void WriteStartStrikethrough() => Writer.WriteStartStrikethrough();

        public override void WriteEndStrikethrough() => Writer.WriteEndStrikethrough();

        public override void WriteInlineCode(string text) => Writer.WriteInlineCode(text);

        public override void WriteStartHeading(int level) => Writer.WriteStartHeading(level);

        public override void WriteEndHeading() => Writer.WriteEndHeading();

        public override void WriteStartBulletItem() => Writer.WriteStartBulletItem();

        public override void WriteEndBulletItem() => Writer.WriteEndBulletItem();

        public override void WriteStartOrderedItem(int number) => Writer.WriteStartOrderedItem(number);

        public override void WriteEndOrderedItem() => Writer.WriteEndOrderedItem();

        public override void WriteImage(string text, string url, string title = null) => Writer.WriteImage(text, url, title);

        public override void WriteLink(string text, string url, string title = null) => Writer.WriteLink(text, url, title);

        public override void WriteCodeBlock(string text, string language = null) => Writer.WriteFencedCodeBlock(text, language);

        public override void WriteStartBlockQuote() => Writer.WriteStartBlockQuote();

        public override void WriteEndBlockQuote() => Writer.WriteEndBlockQuote();

        public override void WriteHorizontalRule() => Writer.WriteHorizontalRule();

        public override void WriteStartTable(int columnCount) => Writer.WriteStartTable(columnCount);

        public override void WriteEndTable() => Writer.WriteEndTable();

        public override void WriteStartTableRow() => Writer.WriteStartTableRow();

        public override void WriteEndTableRow() => Writer.WriteEndTableRow();

        public override void WriteStartTableCell() => Writer.WriteStartTableCell();

        public override void WriteEndTableCell() => Writer.WriteEndTableCell();

        public override void WriteTableHeaderSeparator() => Writer.WriteTableHeaderSeparator();

        public override void WriteCharEntity(char value) => Writer.WriteCharEntity(value);

        public override void WriteEntityRef(string name) => Writer.WriteEntityRef(name);

        public override void WriteComment(string text) => Writer.WriteComment(text);

        public override void Flush() => Writer.Flush();

        public override void WriteString(string text) => Writer.WriteString(text);

        public override void WriteRaw(string data) => Writer.WriteRaw(data);

        public override void WriteLine() => Writer.WriteLine();

        public override string ToString()
        {
            return Writer.ToString();
        }

        public override void Close()
        {
            if (Writer.WriteState != WriteState.Closed)
                Writer.Close();
        }

        public override string GetLanguageIdentifier(string language)
        {
            switch (language)
            {
                case LanguageNames.CSharp:
                    return LanguageIdentifiers.CSharp;
                case LanguageNames.VisualBasic:
                    return LanguageIdentifiers.VisualBasic;
                case LanguageNames.FSharp:
                    return LanguageIdentifiers.FSharp;
            }

            Debug.Assert(Symbol == null, Symbol.Language);
            return null;
        }
    }
}
