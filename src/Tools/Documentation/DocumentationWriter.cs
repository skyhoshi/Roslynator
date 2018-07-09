// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
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
            DocumentationOptions options = null)
        {
            SymbolInfo = symbolInfo;
            DirectoryInfo = directoryInfo;
            Options = options ?? DocumentationOptions.Default;
        }

        public SymbolDocumentationInfo DirectoryInfo { get; }

        public SymbolDocumentationInfo SymbolInfo { get; }

        public ISymbol Symbol
        {
            get { return SymbolInfo.Symbol; }
        }

        internal bool CanCreateExternalLink { get; set; } = true;

        protected internal int BaseHeadingLevel { get; set; }

        public SymbolDisplayFormatProvider FormatProvider
        {
            get { return Options.FormatProvider; }
        }

        public CompilationDocumentationInfo Compilation
        {
            get { return SymbolInfo.Compilation; }
        }

        public DocumentationOptions Options { get; }

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
            WriteString((value) ? "true" : "false");
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

        public abstract string GetLanguageIdentifier(string language);

        public void WriteTableCell(string text)
        {
            WriteStartTableCell();
            WriteString(text);
            WriteEndTableCell();
        }

        public void WriteLink(
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions)
        {
            string url = symbolInfo.GetUrl(directoryInfo, useExternalLink: CanCreateExternalLink);

            WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(format, additionalOptions), url);
        }

        public void WriteNamespace(INamespaceSymbol namespaceSymbol, IEnumerable<ITypeSymbol> typeSymbols)
        {
            WriteStartHeading(2);

            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(namespaceSymbol);

            WriteLink(namespaceSymbol.ToDisplayString(FormatProvider.NamespaceFormat), string.Join("/", info.Names.Reverse()) + "/README.md");
            WriteString(" Namespace");
            WriteEndHeading();
            WriteNamespaceContentAsTable(typeSymbols, 3);
        }

        public virtual void WriteTitle(ISymbol symbol)
        {
            WriteStartHeading(1 + BaseHeadingLevel);

            WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
            WriteString(" ");
            WriteString(symbol.GetName());
            WriteEndHeading();
        }

        public virtual void WriteNamespace(ISymbol symbol)
        {
            WriteString("Namespace: ");

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            WriteLink(Compilation.GetSymbolInfo(containingNamespace), DirectoryInfo, FormatProvider.NamespaceFormat, SymbolDisplayAdditionalOptions.None);
            WriteLine();
            WriteLine();
        }

        public virtual void WriteAssembly(ISymbol symbol)
        {
            WriteString("Assembly: ");
            WriteString(symbol.ContainingAssembly.Name);
            WriteString(".dll");
            WriteLine();
            WriteLine();
        }

        public virtual void WriteObsolete(ISymbol symbol)
        {
            WriteBold("WARNING: This API is now obsolete.");
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
            WriteSection(symbol, heading: "Summary", "summary");
        }

        public virtual void WriteSignature(ISymbol symbol)
        {
            string s = null;

            if (symbol is ITypeSymbol typeSymbol)
            {
                const SymbolDisplayTypeDeclarationOptions typeDeclarationOptions = SymbolDisplayTypeDeclarationOptions.IncludeAccessibility
                    | SymbolDisplayTypeDeclarationOptions.IncludeModifiers
                    | SymbolDisplayTypeDeclarationOptions.IncludeBaseTypes;

                s = typeSymbol.ToDisplayString(FormatProvider.SignatureFormat, typeDeclarationOptions);
            }
            else
            {
                s = symbol.ToDisplayString(FormatProvider.SignatureFormat);
            }

            using (IEnumerator<AttributeData> en = symbol
                .GetAttributes()
                .Where(f => !ShouldBeExcluded(f.AttributeClass)).GetEnumerator())
            {
                if (en.MoveNext())
                {
                    var sb = new StringBuilder(s.Length);

                    do
                    {
                        sb.Append("[");
                        sb.Append(en.Current.AttributeClass.ToDisplayString(FormatProvider.TypeFormat));
                        sb.Append("]");
                        sb.AppendLine();
                    }
                    while (en.MoveNext());

                    sb.Append(s);

                    s = sb.ToString();
                }
            }

            WriteCodeBlock(s, GetLanguageIdentifier(symbol.Language));
        }

        public virtual void WriteTypeParameters(ISymbol symbol)
        {
            ImmutableArray<ITypeParameterSymbol> typeParameters = symbol.GetTypeParameters();

            if (typeParameters.Any())
                WriteTable(typeParameters, "Type Parameters", 3, "Type Parameter", "Summary", FormatProvider.TypeParameterFormat, SymbolDisplayAdditionalOptions.None);
        }

        public virtual void WriteParameters(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

                        WriteTable(parameters, "Parameters", 3, "Parameter", "Summary", FormatProvider.ParameterFormat, SymbolDisplayAdditionalOptions.None);
                        break;
                    }
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)symbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                        {
                            ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

                            WriteTable(parameters, "Parameters", 3, "Parameter", "Summary", FormatProvider.ParameterFormat, SymbolDisplayAdditionalOptions.None);
                        }

                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        ImmutableArray<IParameterSymbol> parameters = propertySymbol.Parameters;

                        WriteTable(parameters, "Parameters", 3, "Parameter", "Summary", FormatProvider.ParameterFormat, SymbolDisplayAdditionalOptions.None);
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
                            WriteReturnValue("Return Value", symbol, methodSymbol.ReturnType);

                        break;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        WriteReturnValue("Returns", symbol, methodSymbol.ReturnType);
                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)symbol;

                        WriteReturnValue("Property Value", symbol, propertySymbol.Type);
                        break;
                    }
            }

            void WriteReturnValue(string heading, ISymbol symbol2, ITypeSymbol returnType)
            {
                if (returnType.SpecialType == SpecialType.System_Void)
                    return;

                WriteHeading(3 + BaseHeadingLevel, heading);
                WriteLink(Compilation.GetSymbolInfo(returnType), DirectoryInfo, FormatProvider.ReturnValueFormat, SymbolDisplayAdditionalOptions.None);
                WriteLine();

                string returns = Compilation.GetDocumentationElement(symbol2, "returns")?.Value;

                if (returns != null)
                {
                    WriteString(returns.Trim());
                    WriteLine();
                }
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

            WriteHeading(3 + BaseHeadingLevel, "Inheritance");

            using (IEnumerator<ITypeSymbol> en = typeSymbol.BaseTypesAndSelf().Reverse().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    ITypeSymbol symbol = en.Current;

                    bool isLast = !en.MoveNext();

                    WriterLinkOrText(symbol, isLast);

                    do
                    {
                        WriteString(" ");
                        WriteCharEntity('\u2192');
                        WriteString(" ");

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
                    WriteString(symbol.ToDisplayString(FormatProvider.InheritanceFormat));
                }
                else
                {
                    WriteLink(Compilation.GetSymbolInfo(symbol), DirectoryInfo, FormatProvider.InheritanceFormat, SymbolDisplayAdditionalOptions.None);
                }
            }
        }

        public virtual void WriteAttributes(ISymbol symbol)
        {
            using (IEnumerator<ITypeSymbol> en = symbol
                .GetAttributes()
                .Select(f => f.AttributeClass)
                .Where(f => !ShouldBeExcluded(f))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(3 + BaseHeadingLevel, "Attributes");

                    WriteLink(Compilation.GetSymbolInfo(en.Current), SymbolDisplayAdditionalOptions.None);

                    while (en.MoveNext())
                    {
                        WriteString(", ");

                        WriteLink(Compilation.GetSymbolInfo(en.Current), SymbolDisplayAdditionalOptions.None);
                    }
                }
            }

            WriteLine();
        }

        private static bool ShouldBeExcluded(INamedTypeSymbol attributeSymbol)
        {
            switch (attributeSymbol.MetadataName)
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
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics);
                case "SuppressMessageAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics_CodeAnalysis);
                case "DefaultMemberAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Reflection);
                case "AsyncStateMachineAttribute":
                case "IteratorStateMachineAttribute":
                case "MethodImplAttribute":
                case "TypeForwardedFromAttribute":
                case "TypeForwardedToAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Runtime_CompilerServices);
#if DEBUG
                case "FlagsAttribute":
                case "ObsoleteAttribute":
                    return false;
#endif
            }

            Debug.Fail(attributeSymbol.ToDisplayString());
            return false;
        }

        public virtual void WriteDerived(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !typeSymbol.IsStatic)
            {
                using (IEnumerator<INamedTypeSymbol> en = Compilation
                    .TypeSymbols
                    .Where(f => f.InheritsFrom(typeSymbol))
                    .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        WriteHeading(3 + BaseHeadingLevel, "Derived");

                        int count = 0;

                        WriteStartBulletList();

                        do
                        {
                            WriteStartBulletItem();
                            WriteLink(Compilation.GetSymbolInfo(en.Current), DirectoryInfo, FormatProvider.DerivedFormat, SymbolDisplayAdditionalOptions.None);
                            WriteEndBulletItem();

                            count++;

                            if (count == Options.MaxDerivedItems)
                            {
                                if (en.MoveNext())
                                    WriteBulletItem("...");

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

            if (typeSymbol.TypeKind == TypeKind.Enum)
                return;

            IEnumerable<INamedTypeSymbol> allInterfaces = (typeSymbol.BaseType?.SpecialType.Is(SpecialType.System_Delegate, SpecialType.System_MulticastDelegate) == true)
                ? typeSymbol.Interfaces
                : typeSymbol.AllInterfaces;

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
                    WriteHeading(3 + BaseHeadingLevel, "Implements");

                    WriteStartBulletList();

                    do
                    {
                        WriteStartBulletItem();
                        WriteLink(Compilation.GetSymbolInfo(en.Current), FormatProvider.ImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        WriteEndBulletItem();
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
                    WriteHeading(3 + BaseHeadingLevel, "Exceptions");

                    WriteException(en.Current.element, en.Current.exceptionSymbol);

                    while (en.MoveNext())
                        WriteException(en.Current.element, en.Current.exceptionSymbol);
                }
            }

            void WriteException(XElement element, ISymbol exceptionSymbol)
            {
                WriteLink(Compilation.GetSymbolInfo(exceptionSymbol), SymbolDisplayAdditionalOptions.None);
                WriteLine();
                WriteLine();
                WriteElementContent(element);
                WriteLine();
                WriteLine();
            }

            IEnumerable<(XElement element, ISymbol exceptionSymbol)> GetExceptions()
            {
                XElement element = Compilation.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("exception"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol exceptionSymbol = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, Compilation.Compilation);

                            if (exceptionSymbol != null)
                                yield return (e, exceptionSymbol);
                        }
                    }
                }
            }
        }

        public virtual void WriteExamples(ISymbol symbol)
        {
            WriteSection(symbol, heading: "Examples", "examples");
        }

        public virtual void WriteRemarks(ISymbol symbol)
        {
            WriteSection(symbol, heading: "Remarks", "remarks");
        }

        public virtual void WriteEnumFields(IEnumerable<IFieldSymbol> fields)
        {
            using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(2 + BaseHeadingLevel, "Fields");

                    WriteStartTable(3);
                    WriteStartTableRow();
                    WriteTableCell("Name");
                    WriteTableCell("Value");
                    WriteTableCell("Summary");
                    WriteEndTableRow();
                    WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        WriteStartTableRow();
                        WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        WriteTableCell(fieldSymbol.ConstantValue.ToString());
                        WriteTableCell(Compilation.GetDocumentationElement(fieldSymbol, "summary")?.Value.Trim());
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        public virtual void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            WriteTable(constructors, "Constructors", 2, "Constructor", "Summary", FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.None);
        }

        public virtual void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, "Fields", 2, "Field", "Summary", FormatProvider.FieldFormat, SymbolDisplayAdditionalOptions.None);
        }

        public virtual void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, "Properties", 2, "Property", "Summary", FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty, addInheritedFrom: true);
        }

        public virtual void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, "Methods", 2, "Method", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.None, addInheritedFrom: true);
        }

        public virtual void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, "Operators", 2, "Operator", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseOperatorName);
        }

        public virtual void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, "Events", 2, "Event", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.None, addInheritedFrom: true);
        }

        public virtual void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, "Explicit Interface Implementations", 2, "Member", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
        }

        public virtual void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
            IEnumerable<IMethodSymbol> extensionMethods = Compilation
                .ExtensionMethodSymbols
                .Where(f => f.Parameters[0].Type.OriginalDefinition == typeSymbol);

            WriteTable(
                extensionMethods,
                "Extension Methods",
                2,
                "Method",
                "Summary",
                FormatProvider.MethodFormat,
                SymbolDisplayAdditionalOptions.None);
        }

        public virtual void WriteSeeAlso(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = GetSymbols().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    WriteHeading(2 + BaseHeadingLevel, "See Also");

                    WriteStartBulletList();

                    do
                    {
                        WriteBulletItem(en.Current);
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }

            void WriteBulletItem(ISymbol symbol2)
            {
                WriteStartBulletItem();
                WriteLink(Compilation.GetSymbolInfo(symbol2), DirectoryInfo, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
                WriteEndBulletItem();
            }

            IEnumerable<ISymbol> GetSymbols()
            {
                XElement element = Compilation.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("seealso"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol symbol2 = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, Compilation.Compilation);

                            if (symbol2 != null)
                                yield return symbol2;
                        }
                    }
                }
            }
        }

        private void WriteSection(ISymbol symbol, string heading, string name)
        {
            XElement element = Compilation.GetDocumentationElement(symbol, name);

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

        protected void WriteElementContent(XElement element, bool isNested = false)
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
                                            ISymbol symbol = DocumentationCommentId.GetFirstSymbolForDeclarationId(commentId, Compilation.Compilation);

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
                                                WriteLink(Compilation.GetSymbolInfo(symbol), DirectoryInfo, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
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
            SymbolDisplayAdditionalOptions additionalOptions,
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

                        WriteLinkOrText(symbol, format, additionalOptions, addLink);

                        WriteEndTableCell();
                        WriteStartTableCell();

                        XElement element = null;
                        switch (symbol.Kind)
                        {
                            case SymbolKind.Parameter:
                                {
                                    element = Compilation
                                        .GetDocumentationElement(symbol.ContainingSymbol)?
                                        .Elements("param")
                                        .FirstOrDefault(f => f.Attribute("name")?.Value == symbol.Name);

                                    break;
                                }
                            case SymbolKind.TypeParameter:
                                {
                                    element = Compilation
                                        .GetDocumentationElement(symbol.ContainingSymbol)?
                                        .Elements("typeparam")
                                        .FirstOrDefault(f => f.Attribute("name")?.Value == symbol.Name);

                                    break;
                                }
                            default:
                                {
                                    element = Compilation.GetDocumentationElement(symbol, "summary");
                                    break;
                                }
                        }

                        if (element != null)
                            WriteElementContent(element, isNested: true);

                        if (addInheritedFrom
                            && Symbol != null
                            && symbol.ContainingType != Symbol)
                        {
                            WriteString(" (Inherited from ");
                            WriteLink(Compilation.GetSymbolInfo(symbol.ContainingType.OriginalDefinition), additionalOptions);
                            WriteString(")");
                        }

                        WriteEndTableCell();
                        WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    WriteEndTable();
                }
            }
        }

        internal void WriteList(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions,
            bool addLink = true)
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
                        WriteStartBulletItem();
                        WriteLinkOrText(en.Current, format, additionalOptions, addLink);
                        WriteEndBulletItem();
                    }
                    while (en.MoveNext());

                    WriteEndBulletList();
                }
            }
        }

        private void WriteLinkOrText(
            ISymbol symbol,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions,
            bool addLink)
        {
            SymbolDocumentationInfo info = Compilation.GetSymbolInfo(symbol);

            if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
            {
                WriteString(symbol.Name);
            }
            else if (addLink)
            {
                WriteLink(info, DirectoryInfo, format, additionalOptions);
            }
            else
            {
                WriteString(symbol.ToDisplayString(format, additionalOptions));
            }
        }

        public void WriteLink(SymbolDocumentationInfo symbolInfo, SymbolDisplayFormat format = null)
        {
            WriteLink(symbolInfo, format ?? SymbolDisplayFormats.TypeNameAndContainingTypes, SymbolDisplayAdditionalOptions.None);
        }

        public void WriteLink(SymbolDocumentationInfo symbolInfo, SymbolDisplayAdditionalOptions additionalOptions)
        {
            WriteLink(symbolInfo, SymbolDisplayFormats.TypeNameAndContainingTypes, additionalOptions);
        }

        public void WriteLink(SymbolDocumentationInfo symbolInfo, SymbolDisplayFormat format, SymbolDisplayAdditionalOptions additionalOptions)
        {
            if (symbolInfo.Symbol is INamedTypeSymbol namedTypeSymbol
                && namedTypeSymbol.TypeArguments.Any(f => f.Kind != SymbolKind.TypeParameter))
            {
                var sb = new StringBuilder();

                foreach (SymbolDisplayPart part in symbolInfo
                    .Symbol
                    .ToDisplayParts(format, additionalOptions))
                {
                    switch (part.Kind)
                    {
                        case SymbolDisplayPartKind.ClassName:
                        case SymbolDisplayPartKind.DelegateName:
                        case SymbolDisplayPartKind.EnumName:
                        case SymbolDisplayPartKind.EventName:
                        case SymbolDisplayPartKind.FieldName:
                        case SymbolDisplayPartKind.InterfaceName:
                        case SymbolDisplayPartKind.MethodName:
                        case SymbolDisplayPartKind.PropertyName:
                        case SymbolDisplayPartKind.StructName:
                            {
                                ISymbol symbol = part.Symbol;

                                string url = Compilation.GetSymbolInfo(symbol).GetUrl(DirectoryInfo);

                                WriteLinkOrText(symbol.Name, url);

                                break;
                            }
                        default:
                            {
                                WriteString(part.ToString());
                                break;
                            }
                    }
                }
            }
            else
            {
                string url = symbolInfo.GetUrl(DirectoryInfo);

                WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(format, additionalOptions), url);
            }
        }

        public void WriteNamespaceContentAsTable(
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .OrderBy(f => f.ToDisplayString(format))
                .GroupBy(f => f.TypeKind)
                .Where(f => Options.IsNamespacePartEnabled(f.Key))
                .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), NamespaceDocumentationPartComparer.Instance))
            {
                TypeKind typeKind = grouping.Key;

                WriteTable(
                    grouping,
                    DocumentationFacts.GetPluralName(typeKind),
                    headingLevel,
                    DocumentationFacts.GetName(typeKind),
                    "Summary",
                    format,
                    SymbolDisplayAdditionalOptions.None);
            }
        }

        public void WriteNamespaceContentAsList(
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel,
            bool addLink = true)
        {
            SymbolDisplayFormat format = FormatProvider.TypeFormat;

            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .OrderBy(f => f.ToDisplayString(format))
                .GroupBy(f => f.TypeKind)
                .Where(f => Options.IsNamespacePartEnabled(f.Key))
                .OrderBy(f => f.Key.ToNamespaceDocumentationPart(), NamespaceDocumentationPartComparer.Instance))
            {
                TypeKind typeKind = grouping.Key;

                WriteList(
                    grouping,
                    DocumentationFacts.GetPluralName(typeKind),
                    headingLevel,
                    format,
                    SymbolDisplayAdditionalOptions.None,
                    addLink: addLink);
            }
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
