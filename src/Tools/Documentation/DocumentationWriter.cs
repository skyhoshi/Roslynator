// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public abstract class DocumentationWriter : IDisposable
    {
        private bool _disposed;

        protected DocumentationWriter(
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            DocumentationOptions options = null,
            DocumentationResources resources = null)
        {
            SymbolInfo = symbolInfo;
            DirectoryInfo = directoryInfo;
            Options = options ?? DocumentationOptions.Default;
            Resources = resources ?? DocumentationResources.Default;
        }

        public SymbolDocumentationInfo DirectoryInfo { get; }

        public SymbolDocumentationInfo SymbolInfo { get; }

        public ISymbol Symbol => SymbolInfo.Symbol;

        public CompilationDocumentationInfo CompilationInfo => SymbolInfo.CompilationInfo;

        internal bool CanCreateTypeLocalUrl { get; set; } = true;

        internal bool CanCreateMemberLocalUrl { get; set; } = true;

        protected internal int BaseHeadingLevel { get; set; }

        public SymbolDisplayFormatProvider FormatProvider => Options.FormatProvider;

        public DocumentationOptions Options { get; }

        internal string FileName => Options.FileName;

        public DocumentationResources Resources { get; }

        internal SymbolDocumentationInfo GetSymbolInfo(ISymbol symbol)
        {
            return CompilationInfo.GetSymbolInfo(symbol);
        }

        public abstract void WriteStartBold();

        public abstract void WriteEndBold();

        public virtual void WriteBold(string text)
        {
            WriteStartBold();
            WriteString(text);
            WriteEndBold();
        }

        public abstract void WriteStartItalic();

        public abstract void WriteEndItalic();

        public virtual void WriteItalic(string text)
        {
            WriteStartItalic();
            WriteString(text);
            WriteEndItalic();
        }

        public abstract void WriteStartStrikethrough();

        public abstract void WriteEndStrikethrough();

        public virtual void WriteStrikethrough(string text)
        {
            WriteStartStrikethrough();
            WriteString(text);
            WriteEndStrikethrough();
        }

        public abstract void WriteInlineCode(string text);

        public abstract void WriteStartHeading(int level);

        public abstract void WriteEndHeading();

        public virtual void WriteHeading1(string text)
        {
            WriteHeading(1, text);
        }

        public virtual void WriteHeading2(string text)
        {
            WriteHeading(2, text);
        }

        public virtual void WriteHeading3(string text)
        {
            WriteHeading(3, text);
        }

        public virtual void WriteHeading4(string text)
        {
            WriteHeading(4, text);
        }

        public virtual void WriteHeading5(string text)
        {
            WriteHeading(5, text);
        }

        public virtual void WriteHeading6(string text)
        {
            WriteHeading(6, text);
        }

        public virtual void WriteHeading(int level, string text)
        {
            WriteStartHeading(level);
            WriteString(text);
            WriteEndHeading();
        }

        public abstract void WriteStartBulletList();

        public abstract void WriteEndBulletList();

        public abstract void WriteStartBulletItem();

        public abstract void WriteEndBulletItem();

        public virtual void WriteBulletItem(string text)
        {
            WriteStartBulletItem();
            WriteString(text);
            WriteEndBulletItem();
        }

        public abstract void WriteStartOrderedList();

        public abstract void WriteEndOrderedList();

        public abstract void WriteStartOrderedItem(int number);

        public abstract void WriteEndOrderedItem();

        public virtual void WriteOrderedItem(int number, string text)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number), number, "Item number must be greater than or equal to 0.");

            WriteStartOrderedItem(number);
            WriteString(text);
            WriteEndOrderedItem();
        }

        public abstract void WriteImage(string text, string url, string title = null);

        public abstract void WriteLink(string text, string url, string title = null);

        public void WriteLinkOrText(string text, string url = null, string title = null)
        {
            if (!string.IsNullOrEmpty(url))
            {
                WriteLink(text, url, title);
            }
            else
            {
                WriteString(text);
            }
        }

        public abstract void WriteCodeBlock(string text, string language = null);

        public abstract void WriteStartBlockQuote();

        public abstract void WriteEndBlockQuote();

        public virtual void WriteBlockQuote(string text)
        {
            WriteStartBlockQuote();
            WriteString(text);
            WriteEndBlockQuote();
        }

        public abstract void WriteHorizontalRule();

        public abstract void WriteStartTable(int columnCount);

        public abstract void WriteEndTable();

        public abstract void WriteStartTableRow();

        public abstract void WriteEndTableRow();

        public abstract void WriteStartTableCell();

        public abstract void WriteEndTableCell();

        public abstract void WriteTableHeaderSeparator();

        public abstract void WriteCharEntity(char value);

        public abstract void WriteEntityRef(string name);

        public abstract void WriteComment(string text);

        public abstract void Flush();

        public abstract void WriteString(string text);

        public abstract void WriteRaw(string data);

        public abstract void WriteLine();

        public virtual void WriteValue(bool value)
        {
            WriteString((value) ? Resources.TrueValue : Resources.FalseValue);
        }

        public virtual void WriteValue(int value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(long value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(float value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(double value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public virtual void WriteValue(decimal value)
        {
            WriteString(value.ToString(null, CultureInfo.InvariantCulture));
        }

        public void WriteSpace()
        {
            WriteString(" ");
        }

        public void WriteSymbol(ISymbol symbol, SymbolDisplayFormat format = null, SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None)
        {
            WriteString(symbol.ToDisplayString(format, additionalOptions));
        }

        public abstract string GetLanguageIdentifier(string language);

        public void WriteTableCell(string text)
        {
            WriteStartTableCell();
            WriteString(text);
            WriteEndTableCell();
        }

        public virtual void WriteTitle(ISymbol symbol)
        {
            WriteHeading(1, symbol, FormatProvider.TitleFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName, addLink: false);
        }

        public virtual void WriteNamespace(ISymbol symbol)
        {
            WriteString(Resources.NamespaceTitle);
            WriteString(Resources.Colon);
            WriteSpace();
            WriteLink(symbol.ContainingNamespace, FormatProvider.NamespaceFormat);
            WriteLine();
            WriteLine();
        }

        public virtual void WriteAssembly(ISymbol symbol)
        {
            WriteString(Resources.AssemblyTitle);
            WriteString(Resources.Colon);
            WriteSpace();
            WriteString(symbol.ContainingAssembly.Name);
            WriteString(Resources.Dot);
            WriteString(Resources.DllExtension);
            WriteLine();
            WriteLine();
        }

        public virtual void WriteObsolete(ISymbol symbol)
        {
            WriteBold(Resources.ObsoleteWarning);
            WriteLine();
            WriteLine();

            TypedConstant typedConstant = symbol.GetAttribute(MetadataNames.System_ObsoleteAttribute).ConstructorArguments.FirstOrDefault();

            if (typedConstant.Type?.SpecialType == SpecialType.System_String)
            {
                string message = typedConstant.Value?.ToString();

                if (!string.IsNullOrEmpty(message))
                    WriteString(message);

                WriteLine();
            }

            WriteLine();
        }

        public virtual void WriteSummary(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.SummaryTitle, "summary");
        }

        public virtual void WriteDefinition(ISymbol symbol)
        {
            ImmutableArray<SymbolDisplayPart> parts;

            var typeSymbol = symbol as ITypeSymbol;

            if (typeSymbol != null)
            {
                parts = typeSymbol.ToDisplayParts(FormatProvider.DefinitionFormat, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers);
            }
            else
            {
                parts = symbol.ToDisplayParts(FormatProvider.DefinitionFormat);
            }

            SymbolDisplayFormat format = FormatProvider.DefinitionTypeFormat;

            ImmutableArray<SymbolDisplayPart>.Builder builder = default;

            using (IEnumerator<AttributeData> en = symbol
                .GetAttributes()
                .Where(f => !DocumentationUtility.IsHiddenAttribute(f.AttributeClass)).GetEnumerator())
            {
                if (en.MoveNext())
                {
                    builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

                    do
                    {
                        builder.Add(SymbolDisplayPartFactory.Punctuation("["));
                        builder.AddRange(en.Current.AttributeClass.ToDisplayParts(format));
                        builder.Add(SymbolDisplayPartFactory.Punctuation("]"));
                        builder.Add(SymbolDisplayPartFactory.LineBreak());
                    }
                    while (en.MoveNext());

                    parts = parts.InsertRange(0, builder);
                    builder.Clear();
                }
            }

            if (typeSymbol != null)
                AddBaseTypes();

            if (Options.FormatConstraints)
            {
                for (int i = parts.Length - 1; i >= 0; i--)
                {
                    if (parts[i].IsKeyword("where"))
                        parts = parts.InsertRange(i, SymbolDisplayPartFactory.LineBreakAndIndent);
                }
            }

            WriteCodeBlock(parts.ToDisplayString(), GetLanguageIdentifier(symbol.Language));

            void AddBaseTypes()
            {
                INamedTypeSymbol baseType = null;

                if (typeSymbol.TypeKind.Is(TypeKind.Class, TypeKind.Interface))
                {
                    baseType = typeSymbol.BaseType;

                    if (baseType?.SpecialType == SpecialType.System_Object)
                        baseType = null;
                }

                ImmutableArray<INamedTypeSymbol> interfaces = typeSymbol.Interfaces;

                if (interfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                    interfaces = interfaces.RemoveAll(f => f.SpecialType == SpecialType.System_Collections_IEnumerable);

                int baseListCount = interfaces.Length;

                if (baseType != null)
                    baseListCount++;

                if (baseListCount > 0)
                {
                    if (builder == default)
                        builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

                    int index = -1;

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].IsKeyword("where"))
                        {
                            builder.AddRange(parts, i);
                            index = i;

                            AddPunctuation(":");
                            AddSpaceOrNewLine((bool)this.Options.FormatBaseList);
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        builder.AddRange(parts);

                        AddSpace();
                        AddPunctuation(":");
                        AddSpaceOrNewLine((bool)this.Options.FormatBaseList);
                    }

                    if (baseType != null)
                    {
                        builder.AddRange(baseType.ToDisplayParts(format));

                        if (interfaces.Any())
                        {
                            AddPunctuation(",");
                            AddSpaceOrNewLine((bool)this.Options.FormatBaseList);
                        }
                    }

                    interfaces = interfaces.Sort((x, y) =>
                    {
                        if (x.InheritsFrom(y.OriginalDefinition, includeInterfaces: true))
                            return -1;

                        if (y.InheritsFrom(x.OriginalDefinition, includeInterfaces: true))
                            return 1;

                        if (interfaces.Any(f => x.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                        {
                            if (!interfaces.Any(f => y.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                                return -1;
                        }
                        else if (interfaces.Any(f => y.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                        {
                            return 1;
                        }

                        return string.Compare(x.ToDisplayString(format), y.ToDisplayString(format), StringComparison.Ordinal);
                    });

                    if (interfaces.Any())
                        builder.AddRange(interfaces[0].ToDisplayParts(format));

                    for (int i = 1; i < interfaces.Length; i++)
                    {
                        AddPunctuation(",");
                        AddSpaceOrNewLine((bool)this.Options.FormatBaseList);
                        builder.AddRange(interfaces[i].ToDisplayParts(format));
                    }

                    if (index != -1)
                        builder.AddRange(parts.Skip(index));

                    parts = builder.ToImmutableArray();
                }

                void AddSpace()
                {
                    builder.Add(SymbolDisplayPartFactory.Space());
                }

                void AddPunctuation(string text)
                {
                    builder.Add(SymbolDisplayPartFactory.Punctuation(text));
                }

                void AddSpaceOrNewLine(bool newLine)
                {
                    if (newLine)
                    {
                        builder.AddRange(SymbolDisplayPartFactory.LineBreakAndIndent);
                    }
                    else
                    {
                        builder.Add(SymbolDisplayPartFactory.Space());
                    }
                }
            }
        }

        public virtual void WriteTypeParameters(ISymbol symbol)
        {
            ImmutableArray<ITypeParameterSymbol> typeParameters = symbol.GetTypeParameters();

            if (typeParameters.Any())
                WriteTable(typeParameters, Resources.TypeParametersTitle, 3, Resources.TypeParameterTitle, Resources.SummaryTitle, FormatProvider.TypeParameterFormat);
        }

        public virtual void WriteParameters(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        WriteTable(
                            methodSymbol.Parameters,
                            Resources.ParametersTitle,
                            3,
                            Resources.ParameterTitle,
                            Resources.SummaryTitle,
                            FormatProvider.ParameterFormat);

                        break;
                    }
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)symbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                        {
                            WriteTable(
                                methodSymbol.Parameters,
                                Resources.ParametersTitle,
                                3,
                                Resources.ParameterTitle,
                                Resources.SummaryTitle,
                                FormatProvider.ParameterFormat);
                        }

                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        WriteTable(
                            propertySymbol.Parameters,
                            Resources.ParametersTitle,
                            3,
                            Resources.ParameterTitle,
                            Resources.SummaryTitle,
                            FormatProvider.ParameterFormat);

                        break;
                    }
            }
        }

        public virtual void WriteReturnValue(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)symbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                            WriteReturnValue(Resources.ReturnValueTitle, methodSymbol.ReturnType);

                        break;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        WriteReturnValue(Resources.ReturnsTitle, methodSymbol.ReturnType);
                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        WriteReturnValue(Resources.PropertyValueTitle, propertySymbol.Type);
                        break;
                    }
            }

            void WriteReturnValue(string heading, ITypeSymbol returnType)
            {
                if (returnType.SpecialType == SpecialType.System_Void)
                    return;

                WriteHeading(3 + BaseHeadingLevel, heading);
                WriteLink(returnType, FormatProvider.ReturnValueFormat);
                WriteLine();

                XElement returns = CompilationInfo.GetDocumentationElement(symbol, "returns");

                if (returns != null)
                    WriteElementContent(returns);
            }
        }

        public virtual void WriteInheritance(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind == TypeKind.Interface)
                return;

            if (typeKind == TypeKind.Class
                && typeSymbol.IsStatic)
            {
                return;
            }

            WriteHeading(3 + BaseHeadingLevel, Resources.InheritanceTitle);

            using (IEnumerator<ITypeSymbol> en = typeSymbol.BaseTypesAndSelf().Reverse().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    ITypeSymbol symbol = en.Current;

                    bool isLast = !en.MoveNext();

                    WriterLinkOrText(symbol, isLast);

                    do
                    {
                        WriteSpace();
                        WriteCharEntity(Resources.InheritanceChar);
                        WriteSpace();

                        symbol = en.Current;
                        isLast = !en.MoveNext();

                        WriterLinkOrText(symbol.OriginalDefinition, isLast);
                    }
                    while (!isLast);
                }
            }

            WriteLine();

            void WriterLinkOrText(ITypeSymbol symbol, bool isLast)
            {
                if (isLast)
                {
                    WriteSymbol(symbol, FormatProvider.InheritanceFormat);
                }
                else
                {
                    WriteLink(symbol, FormatProvider.InheritanceFormat);
                }
            }
        }

        public virtual void WriteAttributes(ISymbol symbol)
        {
            using (IEnumerator<ITypeSymbol> en = symbol
                .GetAttributes()
                .Select(f => f.AttributeClass)
                .Where(f => !DocumentationUtility.IsHiddenAttribute(f))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3 + BaseHeadingLevel, Resources.AttributesTitle);

                    bool isNext = false;

                    do
                    {
                        WriteLink(en.Current, FormatProvider.TypeFormat);

                        isNext = en.MoveNext();

                        if (isNext)
                        {
                            WriteString(Resources.Comma);
                            WriteSpace();
                        }
                    }
                    while (isNext);
                }
            }

            WriteLine();
        }

        public virtual void WriteDerived(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !typeSymbol.IsStatic)
            {
                using (IEnumerator<INamedTypeSymbol> en = CompilationInfo
                    .DerivedTypes(typeSymbol)
                    .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        WriteHeading(3 + BaseHeadingLevel, Resources.DerivedTitle);

                        int count = 0;

                        WriteStartBulletList();

                        do
                        {
                            WriteBulletItemLink(en.Current, FormatProvider.DerivedFormat);

                            count++;

                            if (count == Options.MaxDerivedItems)
                            {
                                if (en.MoveNext())
                                    WriteBulletItem(Resources.Ellipsis);

                                break;
                            }
                        }
                        while (en.MoveNext());

                        WriteEndBulletList();
                    }
                }
            }
        }

        public virtual void WriteImplements(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.IsStatic)
                return;

            if (typeSymbol.TypeKind.Is(TypeKind.Enum, TypeKind.Delegate))
                return;

            IEnumerable<INamedTypeSymbol> allInterfaces = typeSymbol.AllInterfaces;

            if (allInterfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                allInterfaces = allInterfaces.Where(f => f.SpecialType != SpecialType.System_Collections_IEnumerable);
            }

            using (IEnumerator<INamedTypeSymbol> en = allInterfaces
                .OrderBy(f => f.ToDisplayString(FormatProvider.ImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3 + BaseHeadingLevel, Resources.ImplementsTitle);

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItemLink(en.Current, FormatProvider.ImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        public virtual void WriteExceptions(ISymbol symbol)
        {
            using (IEnumerator<(XElement element, ISymbol exceptionSymbol)> en = GetExceptions().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3 + BaseHeadingLevel, Resources.ExceptionsTitle);

                    do
                    {
                        XElement element = en.Current.element;
                        ISymbol exceptionSymbol = en.Current.exceptionSymbol;

                        WriteLink(exceptionSymbol, FormatProvider.TypeFormat);
                        WriteLine();
                        WriteLine();
                        WriteElementContent(element);
                        WriteLine();
                        WriteLine();
                    }
                    while (en.MoveNext());
                }
            }

            IEnumerable<(XElement element, ISymbol exceptionSymbol)> GetExceptions()
            {
                XElement element = CompilationInfo.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("exception"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol exceptionSymbol = CompilationInfo.GetFirstSymbolForReferenceId(commentId);

                            if (exceptionSymbol != null)
                                yield return (e, exceptionSymbol);
                        }
                    }
                }
            }
        }

        public virtual void WriteExamples(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.ExamplesTitle, "examples");
        }

        public virtual void WriteRemarks(ISymbol symbol)
        {
            WriteSection(symbol, heading: Resources.RemarksTitle, "remarks");
        }

        public virtual void WriteEnumFields(IEnumerable<IFieldSymbol> fields)
        {
            using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(2 + BaseHeadingLevel, Resources.FieldsTitle);

                    WriteStartTable(3);
                    WriteStartTableRow();
                    WriteTableCell(Resources.NameTitle);
                    WriteTableCell(Resources.ValueTitle);
                    WriteTableCell(Resources.SummaryTitle);
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        WriteStartTableRow();
                        WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        WriteTableCell(fieldSymbol.ConstantValue.ToString());
                        WriteTableCell(CompilationInfo.GetDocumentationElement(fieldSymbol, "summary")?.Value.Trim());
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        public virtual void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            WriteTable(constructors, Resources.ConstructorsTitle, 2, Resources.ConstructorTitle, Resources.SummaryTitle, FormatProvider.ConstructorFormat);
        }

        public virtual void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, Resources.FieldsTitle, 2, Resources.FieldTitle, Resources.SummaryTitle, FormatProvider.FieldFormat);
        }

        public virtual void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, Resources.PropertiesTitle, 2, Resources.PropertyTitle, Resources.SummaryTitle, FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty, addInheritedFrom: true);
        }

        public virtual void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, Resources.MethodsTitle, 2, Resources.MethodTitle, Resources.SummaryTitle, FormatProvider.MethodFormat, addInheritedFrom: true);
        }

        public virtual void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, Resources.OperatorsTitle, 2, Resources.OperatorTitle, Resources.SummaryTitle, FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseOperatorName);
        }

        public virtual void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, Resources.EventsTitle, 2, Resources.EventTitle, Resources.SummaryTitle, FormatProvider.MethodFormat, addInheritedFrom: true);
        }

        public virtual void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, Resources.ExplicitInterfaceImplementationsTitle, 2, Resources.MemberTitle, Resources.SummaryTitle, FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
        }

        public virtual void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
            WriteTable(
                CompilationInfo.GetExtensionMethods(typeSymbol),
                Resources.ExtensionMethodsTitle,
                2,
                Resources.MethodTitle,
                Resources.SummaryTitle,
                FormatProvider.MethodFormat);
        }

        public virtual void WriteSeeAlso(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = GetSymbols().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(2 + BaseHeadingLevel, Resources.SeeAlsoTitle);

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItemLink(en.Current, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }

            IEnumerable<ISymbol> GetSymbols()
            {
                XElement element = CompilationInfo.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("seealso"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol symbol2 = CompilationInfo.GetFirstSymbolForReferenceId(commentId);

                            if (symbol2 != null)
                                yield return symbol2;
                        }
                    }
                }
            }
        }

        private void WriteSection(ISymbol symbol, string heading, string elementName)
        {
            XElement element = CompilationInfo.GetDocumentationElement(symbol, elementName);

            if (element == null)
                return;

            if (heading != null)
            {
                WriteHeading(2 + BaseHeadingLevel, heading);
            }
            else
            {
                WriteLine();
            }

            WriteElementContent(element);
        }

        protected internal void WriteElementContent(XElement element, bool isNested = false)
        {
            using (IEnumerator<XNode> en = element.Nodes().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    XNode node = null;

                    bool isFirst = true;
                    bool isLast = false;

                    do
                    {
                        node = en.Current;

                        isLast = !en.MoveNext();

                        if (node is XText t)
                        {
                            string value = t.Value;
                            value = TextUtility.RemoveLeadingTrailingNewLine(value, isFirst, isLast);

                            if (isNested)
                                value = TextUtility.ToSingleLine(value);

                            WriteString(value);
                        }
                        else if (node is XElement e)
                        {
                            switch (XmlElementNameKindMapper.GetKindOrDefault(e.Name.LocalName))
                            {
                                case XmlElementKind.C:
                                    {
                                        string value = e.Value;
                                        value = TextUtility.ToSingleLine(value);
                                        WriteInlineCode(value);
                                        break;
                                    }
                                case XmlElementKind.Code:
                                    {
                                        if (isNested)
                                            break;

                                        string value = e.Value;
                                        value = TextUtility.RemoveLeadingTrailingNewLine(value);
                                        WriteCodeBlock(value, GetLanguageIdentifier(Symbol.Language));

                                        break;
                                    }
                                case XmlElementKind.List:
                                    {
                                        if (isNested)
                                            break;

                                        string type = e.Attribute("type")?.Value;

                                        if (!string.IsNullOrEmpty(type))
                                        {
                                            switch (type)
                                            {
                                                case "bullet":
                                                    {
                                                        WriteList(e.Elements());
                                                        break;
                                                    }
                                                case "number":
                                                    {
                                                        WriteList(e.Elements(), isOrdered: true);
                                                        break;
                                                    }
                                                case "table":
                                                    {
                                                        WriteTable(e.Elements());
                                                        break;
                                                    }
                                                default:
                                                    {
                                                        Debug.Fail(type);
                                                        break;
                                                    }
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.Para:
                                    {
                                        WriteLine();
                                        WriteLine();
                                        WriteElementContent(e);
                                        WriteLine();
                                        WriteLine();
                                        break;
                                    }
                                case XmlElementKind.ParamRef:
                                    {
                                        string parameterName = e.Attribute("name")?.Value;

                                        if (parameterName != null)
                                            WriteBold(parameterName);

                                        break;
                                    }
                                case XmlElementKind.See:
                                    {
                                        string commentId = e.Attribute("cref")?.Value;

                                        if (commentId != null)
                                        {
                                            ISymbol symbol = CompilationInfo.GetFirstSymbolForDeclarationId(commentId);

                                            //XTODO: repair roslyn documentation
                                            Debug.Assert(symbol != null
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxNode"
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxToken"
                                                || commentId == "T:Microsoft.CodeAnalysis.CSharp.SyntaxTrivia"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxNode"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxToken"
                                                || commentId == "T:Microsoft.CodeAnalysis.VisualBasic.SyntaxTrivia", commentId);

                                            if (symbol != null)
                                            {
                                                WriteLink(symbol, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
                                            }
                                            else
                                            {
                                                WriteBold(TextUtility.RemovePrefixFromDocumentationCommentId(commentId));
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.TypeParamRef:
                                    {
                                        string typeParameterName = e.Attribute("name")?.Value;

                                        if (typeParameterName != null)
                                            WriteBold(typeParameterName);

                                        break;
                                    }
                                case XmlElementKind.Example:
                                case XmlElementKind.Exception:
                                case XmlElementKind.Exclude:
                                case XmlElementKind.Include:
                                case XmlElementKind.InheritDoc:
                                case XmlElementKind.Param:
                                case XmlElementKind.Permission:
                                case XmlElementKind.Remarks:
                                case XmlElementKind.Returns:
                                case XmlElementKind.SeeAlso:
                                case XmlElementKind.Summary:
                                case XmlElementKind.TypeParam:
                                case XmlElementKind.Value:
                                    {
                                        break;
                                    }
                                default:
                                    {
                                        Debug.Fail(e.Name.LocalName);
                                        break;
                                    }
                            }
                        }
                        else
                        {
                            Debug.Fail(node.NodeType.ToString());
                        }

                        isFirst = false;
                    }
                    while (!isLast);
                }
            }
        }

        private void WriteList(IEnumerable<XElement> elements, bool isOrdered = false)
        {
            int number = 1;

            using (IEnumerator<XElement> en = Iterator().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (isOrdered)
                    {
                        WriteStartOrderedList();
                    }
                    else
                    {
                        WriteStartBulletList();
                    }

                    do
                    {
                        WriteStartItem();
                        WriteElementContent(en.Current, isNested: true);
                        WriteEndItem();
                    }
                    while (en.MoveNext());

                    if (isOrdered)
                    {
                        WriteEndOrderedList();
                    }
                    else
                    {
                        WriteEndBulletList();
                    }
                }
            }

            IEnumerable<XElement> Iterator()
            {
                foreach (XElement element in elements)
                {
                    if (element.Name.LocalName == "item")
                    {
                        using (IEnumerator<XElement> en = element.Elements().GetEnumerator())
                        {
                            if (en.MoveNext())
                            {
                                XElement element2 = en.Current;

                                if (element2.Name.LocalName == "description")
                                {
                                    yield return element2;
                                }
                            }
                            else
                            {
                                yield return element;
                            }
                        }
                    }
                }
            }

            void WriteStartItem()
            {
                if (isOrdered)
                {
                    WriteStartOrderedItem(number);
                    number++;
                }
                else
                {
                    WriteStartBulletItem();
                }
            }

            void WriteEndItem()
            {
                if (isOrdered)
                {
                    WriteEndOrderedItem();
                }
                else
                {
                    WriteEndBulletItem();
                }
            }
        }

        private void WriteTable(IEnumerable<XElement> elements)
        {
            using (IEnumerator<XElement> en = elements.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    XElement element = en.Current;

                    string name = element.Name.LocalName;

                    if (name == "listheader"
                        && en.MoveNext())
                    {
                        int columnCount = element.Elements().Count();

                        WriteStartTable(columnCount);
                        WriteStartTableRow();

                        foreach (XElement element2 in element.Elements())
                        {
                            WriteStartTableCell();
                            WriteElementContent(element2, isNested: true);
                            WriteEndTableCell();
                        }

                        WriteEndTableRow();
                        WriteTableHeaderSeparator();

                        do
                        {
                            element = en.Current;

                            WriteStartTableRow();

                            int count = 0;
                            foreach (XElement element2 in element.Elements())
                            {
                                WriteStartTableCell();
                                WriteElementContent(element2, isNested: true);
                                WriteEndTableCell();
                                count++;

                                if (count == columnCount)
                                    break;
                            }

                            while (count < columnCount)
                            {
                                WriteTableCell(null);
                                count++;
                            }

                            WriteEndTableRow();
                        }
                        while (en.MoveNext());

                        WriteEndTable();
                    }
                }
            }
        }

        internal void WriteTable(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool addLink = true,
            bool addInheritedFrom = false)
        {
            using (IEnumerator<ISymbol> en = symbols
                .OrderBy(f => f.ToDisplayString(format, additionalOptions))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (heading != null)
                        WriteHeading(headingLevel + BaseHeadingLevel, heading);

                    WriteStartTable(2);
                    WriteStartTableRow();
                    WriteTableCell(header1);
                    WriteTableCell(header2);
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        ISymbol symbol = en.Current;

                        WriteStartTableRow();
                        WriteStartTableCell();

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            WriteString(symbol.Name);
                        }
                        else if (addLink)
                        {
                            WriteLink(symbol, format, additionalOptions);
                        }
                        else
                        {
                            WriteString(symbol.ToDisplayString(format, additionalOptions));
                        }

                        WriteEndTableCell();
                        WriteStartTableCell();

                        XElement element = FindElement(symbol);

                        if (element != null)
                            WriteElementContent(element, isNested: true);

                        if (addInheritedFrom
                            && Symbol != null
                            && symbol.ContainingType != Symbol)
                        {
                            WriteSpace();
                            WriteString(Resources.OpenParenthesis);
                            WriteString(Resources.InheritedFrom);
                            WriteSpace();
                            WriteLink(symbol.ContainingType.OriginalDefinition, FormatProvider.TypeFormat, additionalOptions);
                            WriteString(Resources.CloseParenthesis);
                        }

                        WriteEndTableCell();
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }

            XElement FindElement(ISymbol symbol)
            {
                switch (symbol.Kind)
                {
                    case SymbolKind.Parameter:
                        return FindElementWithName(symbol, "param");
                    case SymbolKind.TypeParameter:
                        return FindElementWithName(symbol, "typeparam");
                    default:
                        return CompilationInfo.GetDocumentationElement(symbol, "summary");
                }
            }

            XElement FindElementWithName(ISymbol symbol, string name)
            {
                XElement element = CompilationInfo.GetDocumentationElement(symbol.ContainingSymbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements(name))
                    {
                        if (e.Attribute("name")?.Value == symbol.Name)
                            return e;
                    }
                }

                return null;
            }
        }

        internal void WriteList(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool canCreateExternalUrl = true)
        {
            using (IEnumerator<ISymbol> en = symbols
                .OrderBy(f => f.ToDisplayString(format, additionalOptions))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (heading != null)
                        WriteHeading(headingLevel + BaseHeadingLevel, heading);

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItemLink(en.Current, format, canCreateExternalUrl: canCreateExternalUrl);
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        internal void WriteHeading(
            int level,
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool addLink = true)
        {
            WriteStartHeading(level + BaseHeadingLevel);

            if (addLink)
            {
                WriteLink(symbol, format, additionalOptions);
            }
            else
            {
                WriteSymbol(symbol, format, additionalOptions);
            }

            WriteSpace();
            WriteString(Resources.GetName(symbol));

            WriteEndHeading();
        }

        internal void WriteBulletItemLink(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool canCreateExternalUrl = true)
        {
            WriteStartBulletItem();
            WriteLink(symbol, format, additionalOptions: additionalOptions, canCreateExternalUrl: canCreateExternalUrl);
            WriteEndBulletItem();
        }

        public void WriteLink(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool canCreateExternalUrl = true)
        {
            WriteLink(GetSymbolInfo(symbol), format, additionalOptions, canCreateExternalUrl: canCreateExternalUrl);
        }

        public void WriteLink(
            SymbolDocumentationInfo symbolInfo,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool canCreateExternalUrl = true)
        {
            ISymbol symbol = symbolInfo.Symbol;

            if ((symbol as INamedTypeSymbol)?
                .TypeArguments
                .Any(f => f.Kind != SymbolKind.TypeParameter) == true)
            {
                foreach (SymbolDisplayPart part in symbol.ToDisplayParts(format, additionalOptions))
                {
                    if (part.IsTypeNameOrMemberName())
                    {
                        string url = GetUrl(GetSymbolInfo(part.Symbol), DirectoryInfo, canCreateExternalUrl);

                        WriteLinkOrText(part.Symbol.Name, url);
                    }
                    else
                    {
                        WriteString(part.ToString());
                    }
                }
            }
            else
            {
                string url = GetUrl(symbolInfo, DirectoryInfo, canCreateExternalUrl);

                WriteLinkOrText(symbol.ToDisplayString(format, additionalOptions), url);
            }
        }

        private string GetUrl(
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo = null,
            bool canCreateExternalUrl = true)
        {
            SymbolKind kind = symbolInfo.Symbol.Kind;

            switch (kind)
            {
                case SymbolKind.NamedType:
                    {
                        if (!CanCreateTypeLocalUrl)
                            return null;

                        break;
                    }
                case SymbolKind.Namespace:
                    {
                        break;
                    }
                case SymbolKind.Event:
                case SymbolKind.Field:
                case SymbolKind.Method:
                case SymbolKind.Property:
                    {
                        if (!CanCreateMemberLocalUrl)
                            return null;

                        break;
                    }
                case SymbolKind.Parameter:
                case SymbolKind.TypeParameter:
                    {
                        return null;
                    }
                default:
                    {
                        Debug.Fail(kind.ToString());
                        return null;
                    }
            }

            return UrlProvider.CreateUrl(FileName, symbolInfo, directoryInfo, canCreateExternalUrl: canCreateExternalUrl);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Close();

                _disposed = true;
            }
        }

        public virtual void Close()
        {
        }
    }
}
