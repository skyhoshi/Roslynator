// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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
            DocumentationUriProvider uriProvider,
            DocumentationOptions options = null,
            DocumentationResources resources = null)
        {
            SymbolInfo = symbolInfo;
            DirectoryInfo = directoryInfo;
            UriProvider = uriProvider;
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

        public DocumentationResources Resources { get; }

        public DocumentationUriProvider UriProvider { get; }

        internal SymbolDocumentationInfo GetSymbolInfo(ISymbol symbol)
        {
            return CompilationInfo.GetSymbolInfo(symbol);
        }

        public abstract void WriteStartDocument();

        public abstract void WriteEndDocument();

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
            ImmutableArray<SymbolDisplayPart> parts = SymbolDefinitionBuilder.GetDisplayParts(
                symbol,
                FormatProvider.DefinitionFormat,
                ShouldBeHidden,
                Options.FormatBaseList,
                Options.FormatConstraints);

            WriteCodeBlock(parts.ToDisplayString(), GetLanguageIdentifier(symbol.Language));
        }

        public virtual void WriteTypeParameters(ISymbol symbol)
        {
            ImmutableArray<ITypeParameterSymbol> typeParameters = symbol.GetTypeParameters();

            if (typeParameters.Any())
                WriteTable(typeParameters, Resources.TypeParametersTitle, 3, Resources.NameTitle, Resources.SummaryTitle, FormatProvider.TypeParameterFormat);
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
                            Resources.NameTitle,
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

                WriteHeading(3, heading);
                WriteLink(returnType, FormatProvider.ReturnValueFormat);
                WriteLine();

                CompilationInfo.GetDocumentation(symbol)?.WriteElementContentTo(this, "returns");
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

            WriteHeading(3, Resources.InheritanceTitle);

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
            ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

            if (!attributes.Any())
                return;

            IEnumerable<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> symbolsAttributes = attributes
                .Where(f => !ShouldBeHidden(f.AttributeClass))
                .Select(f => ((symbol, attributeClass: f.AttributeClass)));

            if (symbol is ITypeSymbol typeSymbol)
            {
                List<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> inheritedAttributes = GetInheritedAttributes();

                if (inheritedAttributes != null)
                    symbolsAttributes = symbolsAttributes.Concat(inheritedAttributes);
            }

            using (IEnumerator<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> en = symbolsAttributes
                .OrderBy(f => f.attributeSymbol.ToDisplayString(FormatProvider.TypeFormat))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3, Resources.AttributesTitle);

                    while (true)
                    {
                        WriteLink(en.Current.attributeSymbol, FormatProvider.TypeFormat);

                        if (symbol != en.Current.symbol)
                        {
                            WriteInheritedFrom(en.Current.symbol.OriginalDefinition, FormatProvider.TypeFormat);
                        }

                        if (en.MoveNext())
                        {
                            WriteString(Resources.Comma);
                            WriteSpace();
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            WriteLine();

            List<(ISymbol symbol, INamedTypeSymbol attributeSymbol)> GetInheritedAttributes()
            {
                if (typeSymbol == null)
                    return null;

                List<(ISymbol typeSymbol, INamedTypeSymbol attributeSymbol)> inheritedAttributes = null;

                INamedTypeSymbol baseType = typeSymbol.BaseType;

                while (baseType != null
                    && baseType.SpecialType != SpecialType.System_Object)
                {
                    foreach (AttributeData attribute in baseType.GetAttributes())
                    {
                        AttributeData attributeUsage = attribute.AttributeClass.GetAttribute(MetadataNames.System_AttributeUsageAttribute);

                        if (attributeUsage != null)
                        {
                            TypedConstant typedConstant = attributeUsage.NamedArguments.FirstOrDefault(f => f.Key == "Inherited").Value;

                            if (typedConstant.Type?.SpecialType == SpecialType.System_Boolean
                                && (!(bool)typedConstant.Value))
                            {
                                continue;
                            }
                        }

                        if (ShouldBeHidden(attribute.AttributeClass))
                            continue;

                        if (AttributesContains(attribute))
                            continue;

                        if (inheritedAttributes == null)
                        {
                            inheritedAttributes = new List<(ISymbol typeSymbol, INamedTypeSymbol attributeSymbol)>();
                        }
                        else if (InheritedAttributesContains(attribute))
                        {
                            continue;
                        }

                        inheritedAttributes.Add((baseType, attribute.AttributeClass));
                    }

                    baseType = baseType.BaseType;
                }

                return inheritedAttributes;

                bool AttributesContains(AttributeData attribute)
                {
                    foreach (AttributeData f in attributes)
                    {
                        if (f.AttributeClass == attribute.AttributeClass)
                            return true;
                    }

                    return false;
                }

                bool InheritedAttributesContains(AttributeData attribute)
                {
                    foreach ((ISymbol typeSymbol, INamedTypeSymbol attributeSymbol) f in inheritedAttributes)
                    {
                        if (f.attributeSymbol == attribute.AttributeClass)
                            return true;
                    }

                    return false;
                }
            }
        }

        public virtual void WriteDerived(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !typeSymbol.IsStatic)
            {
                using (IEnumerator<INamedTypeSymbol> en = CompilationInfo
                    .GetDerivedTypes(typeSymbol)
                    .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        WriteHeading(3, Resources.DerivedTitle);

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

            ImmutableArray<INamedTypeSymbol> allInterfaces = typeSymbol.AllInterfaces;

            if (allInterfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
            {
                allInterfaces = allInterfaces.RemoveAll(f => f.SpecialType == SpecialType.System_Collections_IEnumerable);
            }

            const SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.UseItemProperty;
            SymbolDisplayFormat format = FormatProvider.ImplementsFormat;

            allInterfaces = SortInterfaces(allInterfaces);

            ImmutableArray<INamedTypeSymbol>.Enumerator en = allInterfaces.GetEnumerator();

            if (en.MoveNext())
            {
                WriteHeading(3, Resources.ImplementsTitle);

                WriteStartBulletList();

                do
                {
                    WriteBulletItemLink(en.Current, format, additionalOptions);
                }
                while (en.MoveNext());

                WriteEndBulletList();
            }

            ImmutableArray<INamedTypeSymbol> SortInterfaces(ImmutableArray<INamedTypeSymbol> interfaces)
            {
                return interfaces.Sort((x, y) =>
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

                    return string.Compare(x.ToDisplayString(format, additionalOptions), y.ToDisplayString(format, additionalOptions), StringComparison.Ordinal);
                });
            }
        }

        public virtual void WriteExceptions(ISymbol symbol)
        {
            CompilationInfo.GetDocumentation(symbol)?.WriteExceptionsTo(this);
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
                    INamedTypeSymbol enumSymbol = en.Current.ContainingType;

                    bool hasCombinedValue = false;

                    ImmutableArray<EnumFieldSymbolInfo> fieldInfos = default;

                    if (enumSymbol.HasAttribute(MetadataNames.System_FlagsAttribute))
                    {
                        fieldInfos = EnumFieldSymbolInfo.CreateRange(en.Current.ContainingType);

                        if (HasCombinedValue(fieldInfos))
                            hasCombinedValue = true;
                    }

                    WriteHeading(2, Resources.FieldsTitle);

                    WriteStartTable((hasCombinedValue) ? 4 : 3);
                    WriteStartTableRow();
                    WriteTableCell(Resources.NameTitle);
                    WriteTableCell(Resources.ValueTitle);

                    if (hasCombinedValue)
                        WriteTableCell(Resources.CombinationOfTitle);

                    WriteTableCell(Resources.SummaryTitle);
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        WriteStartTableRow();
                        WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        WriteTableCell(fieldSymbol.ConstantValue.ToString());

                        if (hasCombinedValue)
                        {
                            WriteStartTableCell();

                            var fieldInfo = new EnumFieldSymbolInfo(en.Current);

                            List<EnumFieldSymbolInfo> values = fieldInfo.Decompose(fieldInfos);

                            if (values != null)
                            {
                                values.Sort((f, g) =>
                                {
                                    if (f.IsComposite())
                                    {
                                        if (g.IsComposite())
                                        {
                                            return ((IComparable)f.Value).CompareTo((IComparable)g.Value);
                                        }
                                        else
                                        {
                                            return -1;
                                        }
                                    }
                                    else if (g.IsComposite())
                                    {
                                        return 1;
                                    }

                                    return ((IComparable)f.Value).CompareTo((IComparable)g.Value);
                                });

                                WriteString(values[0].Name);

                                for (int i = 1; i < values.Count; i++)
                                {
                                    WriteString(" | ");
                                    WriteString(values[i].Name);
                                }
                            }

                            WriteEndTableCell();
                        }

                        SymbolXmlDocumentation xmlDocumentation = CompilationInfo.GetDocumentation(fieldSymbol);

                        if (xmlDocumentation != null)
                        {
                            WriteStartTableCell();
                            xmlDocumentation.WriteElementContentTo(this, "summary", inlineOnly: true);
                            WriteEndTableCell();
                        }

                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }

            bool HasCombinedValue(ImmutableArray<EnumFieldSymbolInfo> fieldInfos)
            {
                using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        do
                        {
                            var fieldInfo = new EnumFieldSymbolInfo(en.Current);

                            List<EnumFieldSymbolInfo> values = fieldInfo.Decompose(fieldInfos);

                            if (values != null)
                            {
                                return true;
                            }
                        }
                        while (en.MoveNext());
                    }
                }

                return false;
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
            WriteTable(properties, Resources.PropertiesTitle, 2, Resources.PropertyTitle, Resources.SummaryTitle, FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
        }

        public virtual void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, Resources.MethodsTitle, 2, Resources.MethodTitle, Resources.SummaryTitle, FormatProvider.MethodFormat);
        }

        public virtual void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, Resources.OperatorsTitle, 2, Resources.OperatorTitle, Resources.SummaryTitle, FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseOperatorName);
        }

        public virtual void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, Resources.EventsTitle, 2, Resources.EventTitle, Resources.SummaryTitle, FormatProvider.MethodFormat);
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
            CompilationInfo.GetDocumentation(symbol)?.WriteSeeAlsoTo(this);
        }

        private void WriteSection(ISymbol symbol, string heading, string elementName)
        {
            CompilationInfo.GetDocumentation(symbol)?.WriteSectionTo(this, heading, elementName);
        }

        internal void WriteTable(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None,
            bool addLink = true)
        {
            using (IEnumerator<ISymbol> en = symbols
                .OrderBy(f => f.ToDisplayString(format, additionalOptions))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    if (heading != null)
                        WriteHeading(headingLevel, heading);

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

                        bool isInherited = !symbol.IsStatic
                            && symbol.IsKind(SymbolKind.Event, SymbolKind.Method, SymbolKind.Property)
                            && symbol.ContainingType?.TypeKind == TypeKind.Class
                            && symbol.ContainingType != Symbol
                            && Symbol.Kind == SymbolKind.NamedType;

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            CompilationInfo.GetDocumentation(symbol.ContainingSymbol)?.WriteParamContentTo(this, symbol.Name);
                        }
                        else
                        {
                            ISymbol symbol2 = (isInherited) ? symbol.OriginalDefinition : symbol;
                            CompilationInfo.GetDocumentation(symbol2)?.WriteElementContentTo(this, "summary", inlineOnly: true);
                        }

                        if (isInherited)
                            WriteInheritedFrom(symbol.ContainingType.OriginalDefinition, FormatProvider.TypeFormat, additionalOptions);

                        WriteEndTableCell();
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        private void WriteInheritedFrom(ISymbol symbol, SymbolDisplayFormat format, SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None)
        {
            WriteSpace();
            WriteString(Resources.OpenParenthesis);
            WriteString(Resources.InheritedFrom);
            WriteSpace();
            WriteLink(symbol, format, additionalOptions);
            WriteString(Resources.CloseParenthesis);
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
                        WriteHeading(headingLevel, heading);

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
            WriteStartHeading(level);

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

            if (symbolInfo.IsExternal
                && canCreateExternalUrl)
            {
                return UriProvider.GetExternalUrl(symbolInfo).Url;
            }

            return UriProvider.GetLocalUrl(symbolInfo, directoryInfo).Url;
        }

        public virtual bool ShouldBeHidden(ISymbol symbol)
        {
            switch (symbol.MetadataName)
            {
                case "ConditionalAttribute":
                case "DebuggerBrowsableAttribute":
                case "DebuggerDisplayAttribute":
                case "DebuggerHiddenAttribute":
                case "DebuggerNonUserCodeAttribute":
                case "DebuggerStepperBoundaryAttribute":
                case "DebuggerStepThroughAttribute":
                case "DebuggerTypeProxyAttribute":
                case "DebuggerVisualizerAttribute":
                    return symbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics);
                case "SuppressMessageAttribute":
                    return symbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics_CodeAnalysis);
                case "DefaultMemberAttribute":
                    return symbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Reflection);
                case "AsyncStateMachineAttribute":
                case "IteratorStateMachineAttribute":
                case "MethodImplAttribute":
                case "TypeForwardedFromAttribute":
                case "TypeForwardedToAttribute":
                    return symbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Runtime_CompilerServices);
#if DEBUG
                case "CLSCompliantAttribute":
                case "FlagsAttribute":
                case "AttributeUsageAttribute":
                case "ObsoleteAttribute":
                    return false;
#endif
            }

            Debug.Fail(symbol.ToDisplayString());
            return false;
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
