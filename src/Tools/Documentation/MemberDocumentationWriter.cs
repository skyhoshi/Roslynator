// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class MemberDocumentationWriter
    {
        protected MemberDocumentationWriter(DocumentationWriter writer)
        {
            Writer = writer;
        }

        public DocumentationWriter Writer { get; }

        public abstract SymbolDisplayFormat Format { get; }

        public abstract MemberDocumentationParts Parts { get; }

        public SymbolDisplayFormatProvider FormatProvider => Writer.FormatProvider;

        public CompilationDocumentationInfo CompilationInfo => Writer.CompilationInfo;

        public DocumentationOptions Options => Writer.Options;

        public DocumentationResources Resources => Writer.Resources;

        public string KindName => Writer.KindName;

        public int BaseHeadingLevel
        {
            get { return Writer.BaseHeadingLevel; }
            set { Writer.BaseHeadingLevel = value; }
        }

        public virtual void WriteMember(ISymbol Symbol, ImmutableArray<ISymbol> Symbols)
        {
            foreach (MemberDocumentationParts part in Options.EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Namespace:
                        {
                            Writer.WriteNamespace(Symbol);
                            break;
                        }
                    case MemberDocumentationParts.Assembly:
                        {
                            Writer.WriteAssembly(Symbol);
                            break;
                        }
                    case MemberDocumentationParts.Title:
                        {
                            WriteTitle(Symbol, hasOverloads: Symbols.Length > 1);
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
                Writer.WriteTable(Symbols, Resources.OverloadsTitle, 2, KindName, Resources.SummaryTitle, FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLocalLink: false);

                foreach (ISymbol symbol2 in Symbols)
                {
                    BaseHeadingLevel++;

                    Writer.WriteStartHeading(1 + BaseHeadingLevel);
                    Writer.WriteString(symbol2.ToDisplayString(Format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
                    Writer.WriteEndHeading();
                    WriteContent(symbol2);

                    BaseHeadingLevel--;
                }
            }
        }

        public virtual void WriteTitle(ISymbol symbol, bool hasOverloads)
        {
            Writer.WriteStartHeading(1 + BaseHeadingLevel);

            SymbolDisplayFormat format = (hasOverloads)
                ? FormatProvider.OverloadedMemberTitleFormat
                : FormatProvider.MemberTitleFormat;

            Writer.WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
            Writer.WriteSpace();
            Writer.WriteString(KindName);
            Writer.WriteEndHeading();
        }

        public virtual void WriteImplements(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = symbol.FindImplementedInterfaceMembers()
                .OrderBy(f => f.ToDisplayString(FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(3 + BaseHeadingLevel, Resources.ImplementsTitle);

                    Writer.WriteStartBulletList();

                    do
                    {
                        Writer.WriteStartBulletItem();
                        Writer.WriteLink(en.Current, FormatProvider.MemberImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        Writer.WriteEndBulletItem();
                    }
                    while (en.MoveNext());

                    Writer.WriteEndBulletList();
                }
            }
        }

        public virtual void WriteReturnValue(ISymbol symbol)
        {
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
                                Writer.WriteObsolete(symbol);

                            break;
                        }
                    case MemberDocumentationParts.Summary:
                        {
                            Writer.WriteSummary(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Signature:
                        {
                            Writer.WriteSignature(symbol);
                            break;
                        }
                    case MemberDocumentationParts.TypeParameters:
                        {
                            Writer.WriteTypeParameters(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Parameters:
                        {
                            Writer.WriteParameters(symbol);
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
                            Writer.WriteAttributes(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Exceptions:
                        {
                            Writer.WriteExceptions(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Examples:
                        {
                            Writer.WriteExamples(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Remarks:
                        {
                            Writer.WriteRemarks(symbol);
                            break;
                        }
                    case MemberDocumentationParts.SeeAlso:
                        {
                            Writer.WriteSeeAlso(symbol);
                            break;
                        }
                }
            }
        }

        public static MemberDocumentationWriter Create(ISymbol symbol, DocumentationWriter writer)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    {
                        return new EventDocumentationWriter(writer);
                    }
                case SymbolKind.Field:
                    {
                        return new FieldDocumentationWriter(writer);
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                {
                                    return new ConstructorDocumentationWriter(writer);
                                }
                            case MethodKind.UserDefinedOperator:
                            case MethodKind.Conversion:
                                {
                                    return new OperatorDocumentationWriter(writer);
                                }
                        }

                        return new MethodDocumentationWriter(writer);
                    }
                case SymbolKind.Property:
                    {
                        return new PropertyDocumentationWriter(writer);
                    }
            }

            throw new InvalidOperationException();
        }
    }
}
