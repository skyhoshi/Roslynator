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
            DocumentationOptions options,
            DocumentationResources resources) : base(symbols[0], directoryInfo, options, resources)
        {
            Symbols = symbols;
        }

        public ImmutableArray<SymbolDocumentationInfo> Symbols { get; }

        public abstract SymbolDisplayFormat Format { get; }

        public abstract MemberDocumentationParts Parts { get; }

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
                //TODO: create link for overloads
                WriteTable(Symbols.Select(f => f.Symbol), "Overloads", 2, KindName, "Summary", FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLink: false);

                foreach (SymbolDocumentationInfo symbolInfo in Symbols)
                {
                    ISymbol symbol = symbolInfo.Symbol;

                    if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                        WriteObsolete(symbol);

                    BaseHeadingLevel++;

                    WriteStartHeading(1 + BaseHeadingLevel);
                    WriteString(symbol.ToDisplayString(Format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
                    WriteEndHeading();

                    WriteContent(symbol);
                    BaseHeadingLevel--;
                }
            }
        }

        public override void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            SymbolDisplayFormat format = (Symbols.Length == 1)
                ? FormatProvider.MemberTitleFormat
                : FormatProvider.OverloadedMemberTitleFormat;

            WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
            WriteString(" ");
            WriteString(KindName);
            WriteEndHeading();
        }

        public override void WriteReturnValue(ISymbol symbol)
        {
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

                    WriteStartBulletList();

                    do
                    {
                        WriteStartBulletItem();
                        WriteLink(Compilation.GetSymbolInfo(en.Current), FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        WriteEndBulletItem();
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        public void WriteContent(ISymbol symbol)
        {
            if ((Parts & MemberDocumentationParts.Summary) != 0)
                WriteSummary(symbol);

            if ((Parts & MemberDocumentationParts.Signature) != 0)
                WriteSignature(symbol);

            if ((Parts & MemberDocumentationParts.TypeParameters) != 0)
                WriteTypeParameters(symbol);

            if ((Parts & MemberDocumentationParts.Parameters) != 0)
                WriteParameters(symbol);

            if ((Parts & MemberDocumentationParts.ReturnValue) != 0)
                WriteReturnValue(symbol);

            if ((Parts & MemberDocumentationParts.Implements) != 0)
                WriteImplements(symbol);

            if ((Parts & MemberDocumentationParts.Attributes) != 0)
                WriteAttributes(symbol);

            if ((Parts & MemberDocumentationParts.Exceptions) != 0)
                WriteExceptions(symbol);

            if ((Parts & MemberDocumentationParts.Examples) != 0)
                WriteExamples(symbol);

            if ((Parts & MemberDocumentationParts.Remarks) != 0)
                WriteRemarks(symbol);

            if ((Parts & MemberDocumentationParts.SeeAlso) != 0)
                WriteSeeAlso(symbol);
        }

        public static MemberDocumentationMarkdownWriter Create(
            ImmutableArray<SymbolDocumentationInfo> symbols,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options,
            DocumentationResources resources)
        {
            ISymbol symbol = symbols[0].Symbol;

            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return new EventDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                    }
                case SymbolKind.Field:
                    {
                        return new FieldDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return new ConstructorDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                                }
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Conversion:
                                {
                                    return new OperatorDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                                }
                        }

                        return new MethodDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                    }
                case SymbolKind.Property:
                    {
                        return new PropertyDocumentationMarkdownWriter(symbols, directoryInfo, options, resources);
                    }
            }

            throw new InvalidOperationException();
        }
    }
}
