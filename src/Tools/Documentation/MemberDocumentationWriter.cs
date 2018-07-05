// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
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

        public abstract void WriteContent(ISymbol symbol);

        public abstract void WriteMemberTitle(ISymbol symbol);

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
                WriteTable(Symbols, "Overloads", 2, "Constructor", "Summary", FormatProvider.ConstructorFormat);

                foreach (IMethodSymbol symbol in Symbols)
                {
                    if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                        WriteObsolete(symbol);

                    HeadingLevel++;
                    WriteMemberTitle(symbol);
                    WriteContent(symbol);
                    HeadingLevel--;
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
                    break;
                case SymbolKind.Field:
                    break;
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        if (methodSymbol.MethodKind == MethodKind.Constructor)
                        {
                            if (symbols.Length > 1)
                            {
                                return new ConstructorsDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                            }
                            else
                            {
                                return new ConstructorDocumentationMarkdownWriter(symbols, directoryInfo, generator);
                            }
                        }

                        break;
                    }
                case SymbolKind.Property:
                    break;
            }

            return null;
        }
    }
}
