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

        public SymbolDisplayFormatProvider FormatProvider => Writer.FormatProvider;

        public CompilationDocumentationInfo CompilationInfo => Writer.CompilationInfo;

        public DocumentationOptions Options => Writer.Options;

        public DocumentationResources Resources => Writer.Resources;

        protected internal int BaseHeadingLevel
        {
            get { return Writer.BaseHeadingLevel; }
            set { Writer.BaseHeadingLevel = value; }
        }

        public virtual void WriteMember(ISymbol symbol, ImmutableArray<ISymbol> symbols)
        {
            foreach (MemberDocumentationParts part in Options.EnabledAndSortedMemberParts)
            {
                switch (part)
                {
                    case MemberDocumentationParts.Namespace:
                        {
                            Writer.WriteNamespace(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Assembly:
                        {
                            Writer.WriteAssembly(symbol);
                            break;
                        }
                    case MemberDocumentationParts.Title:
                        {
                            WriteTitle(symbol, hasOverloads: symbols.Length > 1);
                            break;
                        }
                }
            }

            if (symbols.Length == 1)
            {
                WriteContent(symbol);
            }
            else
            {
                //TODO: create link for overloads
                Writer.WriteTable(
                    symbols,
                    heading: Resources.OverloadsTitle,
                    headingLevel: 2,
                    header1: Resources.GetName(symbol),
                    header2: Resources.SummaryTitle,
                    format: FormatProvider.ConstructorFormat,
                    additionalOptions: SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName,
                    addLink: false);

                foreach (ISymbol symbol2 in symbols)
                {
                    BaseHeadingLevel++;

                    Writer.WriteStartHeading(1);
                    Writer.WriteString(symbol2.ToDisplayString(Format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
                    Writer.WriteEndHeading();
                    WriteContent(symbol2);

                    BaseHeadingLevel--;
                }
            }
        }

        public virtual void WriteTitle(ISymbol symbol, bool hasOverloads)
        {
            Writer.WriteStartHeading(1);

            SymbolDisplayFormat format = (hasOverloads)
                ? FormatProvider.OverloadedMemberTitleFormat
                : FormatProvider.MemberTitleFormat;

            Writer.WriteString(symbol.ToDisplayString(format, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
            Writer.WriteSpace();
            Writer.WriteString(Resources.GetName(symbol));
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
                    Writer.WriteHeading(3, Resources.ImplementsTitle);

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
                    case MemberDocumentationParts.Definition:
                        {
                            Writer.WriteDefinition(symbol);
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

        private class ConstructorDocumentationWriter : MemberDocumentationWriter
        {
            public ConstructorDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.ConstructorFormat;

            public override void WriteTitle(ISymbol symbol, bool hasOverloads)
            {
                Writer.WriteStartHeading(1);

                if (!hasOverloads)
                {
                    Writer.WriteString(symbol.ToDisplayString(FormatProvider.ConstructorFormat));
                    Writer.WriteSpace();
                    Writer.WriteString(Resources.ConstructorTitle);
                }
                else
                {
                    Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat));
                    Writer.WriteSpace();
                    Writer.WriteString(Resources.ConstructorsTitle);
                }

                Writer.WriteEndHeading();
            }
        }

        private class EventDocumentationWriter : MemberDocumentationWriter
        {
            public EventDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.EventFormat;
        }

        private class FieldDocumentationWriter : MemberDocumentationWriter
        {
            public FieldDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.FieldFormat;

            public override void WriteReturnValue(ISymbol symbol)
            {
                var fieldSymbol = (IFieldSymbol)symbol;

                Writer.WriteHeading(3, Resources.FieldValueTitle);
                Writer.WriteLink(fieldSymbol.Type, FormatProvider.TypeFormat);
            }
        }

        private class MethodDocumentationWriter : MemberDocumentationWriter
        {
            public MethodDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.MethodFormat;

            public override void WriteReturnValue(ISymbol symbol)
            {
                var methodSymbol = (IMethodSymbol)symbol;

                Writer.WriteHeading(3, Resources.ReturnsTitle);
                Writer.WriteLink(methodSymbol.ReturnType, FormatProvider.TypeFormat);
                Writer.WriteLine();
                Writer.WriteLine();

                CompilationInfo.GetDocumentation(methodSymbol)?.WriteElementContentTo(Writer, "returns");
            }
        }

        private class OperatorDocumentationWriter : MemberDocumentationWriter
        {
            public OperatorDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.MethodFormat;

            public override void WriteReturnValue(ISymbol symbol)
            {
                var methodSymbol = (IMethodSymbol)symbol;

                Writer.WriteHeading(3, Resources.ReturnsTitle);
                Writer.WriteLink(methodSymbol.ReturnType, FormatProvider.TypeFormat);
                Writer.WriteLine();
                Writer.WriteLine();

                CompilationInfo.GetDocumentation(methodSymbol)?.WriteElementContentTo(Writer, "returns");
            }
        }

        private class PropertyDocumentationWriter : MemberDocumentationWriter
        {
            public PropertyDocumentationWriter(DocumentationWriter writer) : base(writer)
            {
            }

            public override SymbolDisplayFormat Format => FormatProvider.PropertyFormat;

            public override void WriteReturnValue(ISymbol symbol)
            {
                var propertySymbol = (IPropertySymbol)symbol;

                Writer.WriteHeading(3, Resources.PropertyValueTitle);
                Writer.WriteLink(propertySymbol.Type, FormatProvider.TypeFormat);
                Writer.WriteLine();
                Writer.WriteLine();

                string elementName = (propertySymbol.IsIndexer) ? "returns" : "value";

                CompilationInfo.GetDocumentation(propertySymbol)?.WriteElementContentTo(Writer, elementName);
            }
        }
    }
}
