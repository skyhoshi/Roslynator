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
            foreach (MemberDocumentationParts part in Options.EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Namespace:
                        {
                            WriteNamespace(Symbol);
                            break;
                        }
                    case MemberDocumentationParts.Assembly:
                        {
                            WriteAssembly(Symbol);
                            break;
                        }
                    case MemberDocumentationParts.Title:
                        {
                            WriteTitle(Symbol);
                            break;
                        }
                }
            }

            if (Symbols.Length == 1)
            {
                WriteContent(Symbol);
            }
            else
            {
                //TODO: create link for overloads
                WriteTable(Symbols.Select(f => f.Symbol), Resources.Overloads, 2, KindName, Resources.Summary, FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLink: false);

                foreach (SymbolDocumentationInfo symbolInfo in Symbols)
                {
                    ISymbol symbol = symbolInfo.Symbol;

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
                    WriteHeading(3 + BaseHeadingLevel, Resources.Implements);

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
            foreach (MemberDocumentationParts part in Options.EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Obsolete:
                        {
                            if (symbol.HasAttribute(MetadataNames.System_ObsoleteAttribute))
                                WriteObsolete(symbol);

                            break;
                        }
                    case MemberDocumentationParts.Summary:
                        {
                            WriteSummary(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Signature:
                        {
                            WriteSignature(symbol);
                            break;
                        }
                    case MemberDocumentationParts.TypeParameters:
                        {
                            WriteTypeParameters(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Parameters:
                        {
                            WriteParameters(symbol);
                            break;
                        }
                    case MemberDocumentationParts.ReturnValue:
                        {
                            WriteReturnValue(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Implements:
                        {
                            WriteImplements(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Attributes:
                        {
                            WriteAttributes(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Exceptions:
                        {
                            WriteExceptions(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Examples:
                        {
                            WriteExamples(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Remarks:
                        {
                            WriteRemarks(symbol);
                            break;
                        }
                    case MemberDocumentationParts.SeeAlso:
                        {
                            WriteSeeAlso(symbol);
                            break;
                        }
                }
            }
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
