// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation.Markdown
{
    public abstract class MemberDocumentationMarkdownWriter : DocumentationMarkdownWriter
    {
        protected MemberDocumentationMarkdownWriter(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormatProvider formatProvider) : base(symbols[0], directoryInfo, formatProvider)
        {
            Symbols = symbols;
        }

        public ImmutableArray<SymbolDocumentationInfo> Symbols { get; }

        public abstract string CategoryName { get; }

        public abstract void WriteContent(ISymbol symbol);

        public virtual void WriteMemberTitle(ISymbol symbol)
        {
        }

        public virtual void WriteMember()
        {
            WriteTitle(Symbol);
            WriteNamespace(Symbol);
            WriteAssembly(Symbol);

            if (Symbols.Length == 1)
            {
                if (Symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                    WriteObsolete(Symbol);

                WriteContent(Symbol);
            }
            else
            {
                WriteTable(Symbols.Select(f => f.Symbol), "Overloads", 2, CategoryName, "Summary", FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLink: false);

                foreach (SymbolDocumentationInfo symbolInfo in Symbols)
                {
                    ISymbol symbol = symbolInfo.Symbol;

                    if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                        WriteObsolete(symbol);

                    BaseHeadingLevel++;
                    WriteMemberTitle(symbol);
                    WriteContent(symbol);
                    BaseHeadingLevel--;
                }
            }
        }

        public virtual void WriteImplements(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = symbol.FindImplementedInterfaceMembers()
                .OrderBy(f => f.ToDisplayString(FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3 + BaseHeadingLevel, "Implements");

                    do
                    {
                        WriteStartBulletItem();
                        WriteLink(Compilation.GetDocumentationInfo(en.Current), FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        WriteEndBulletItem();
                    }
                    while (en.MoveNext());
                }
            }
        }

        public static MemberDocumentationMarkdownWriter Create(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormatProvider formatProvider)
        {
            ISymbol symbol = symbols[0].Symbol;

            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return new EventDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                    }
                case SymbolKind.Field:
                    {
                        return new FieldDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return new ConstructorDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                                }
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Conversion:
                                {
                                    return new OperatorDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                                }
                        }

                        return new MethodDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                    }
                case SymbolKind.Property:
                    {
                        return new PropertyDocumentationMarkdownWriter(symbols, directoryInfo, formatProvider);
                    }
            }

            throw new InvalidOperationException();
        }
    }
}
