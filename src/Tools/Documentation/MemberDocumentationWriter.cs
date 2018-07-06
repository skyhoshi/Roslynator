// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class MemberDocumentationWriter : DocumentationMarkdownWriter
    {
        protected MemberDocumentationWriter(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null) : base(symbols[0], directoryInfo, generator, writer)
        {
            Symbols = symbols;
        }

        public ImmutableArray<ISymbol> Symbols { get; }

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
                WriteTable(Symbols, "Overloads", 2, CategoryName, "Summary", FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLink: false);

                foreach (ISymbol symbol in Symbols)
                {
                    if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                        WriteObsolete(symbol);

                    HeadingBaseLevel++;
                    WriteMemberTitle(symbol);
                    WriteContent(symbol);
                    HeadingBaseLevel--;
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
                    Writer.WriteHeading(3 + HeadingBaseLevel, "Implements");

                    do
                    {
                        Writer.WriteStartBulletItem();
                        WriteLink(Generator.GetDocumentationInfo(en.Current), FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        Writer.WriteEndBulletItem();
                    }
                    while (en.MoveNext());
                }
            }
        }

        public static MemberDocumentationWriter Create(
            ImmutableArray<ISymbol> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator)
        {
            ISymbol symbol = symbols[0];

            switch (symbols[0].Kind)
            {
                case SymbolKind.Event:
                    {
                        return new EventDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                    }
                case SymbolKind.Field:
                    {
                        return new FieldDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return new ConstructorDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                                }
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Conversion:
                                {
                                    return new OperatorDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                                }
                        }

                        return new MethodDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                    }
                case SymbolKind.Property:
                    {
                        return new PropertyDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                    }
            }

            throw new InvalidOperationException();
        }
    }
}
