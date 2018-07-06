// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public class DocumentationMarkdownWriter : DocumentationWriter
    {
        public DocumentationMarkdownWriter(
            ISymbol symbol,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null)
        {
            Writer = writer ?? MarkdownWriter.Create(new StringBuilder());
            Generator = generator;
            DirectoryInfo = directoryInfo;
            Symbol = symbol;
        }

        public override ISymbol Symbol { get; }

        public int HeadingBaseLevel { get; set; }

        public override SymbolDocumentationInfo DirectoryInfo { get; }

        public SymbolDisplayFormatProvider FormatProvider
        {
            get { return Generator.FormatProvider; }
        }

        protected MarkdownWriter Writer { get; }

        public DocumentationGenerator Generator { get; }

        public override void WriteTitle(ISymbol symbol)
        {
            Writer.WriteStartHeading(1 + HeadingBaseLevel);

            Writer.WriteString(symbol.ToDisplayString(FormatProvider.TitleFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName));
            Writer.WriteString(" ");
            Writer.WriteString(GetTypeName());
            Writer.WriteEndHeading();

            string GetTypeName()
            {
                switch (symbol.Kind)
                {
                    case SymbolKind.Event:
                        return "Event";
                    case SymbolKind.Field:
                        return "Field";
                    case SymbolKind.Method:
                        return "Method";
                    case SymbolKind.Namespace:
                        return "Namespace";
                    case SymbolKind.Property:
                        return "Property";
                    case SymbolKind.NamedType:
                        {
                            switch (((ITypeSymbol)symbol).TypeKind)
                            {
                                case TypeKind.Class:
                                    return "Class";
                                case TypeKind.Delegate:
                                    return "Delegate";
                                case TypeKind.Enum:
                                    return "Enum";
                                case TypeKind.Interface:
                                    return "Interface";
                                case TypeKind.Struct:
                                    return "Struct";
                            }

                            break;
                        }
                }

                throw new InvalidOperationException();
            }
        }

        public override void WriteNamespace(ISymbol symbol)
        {
            Writer.WriteString("Namespace: ");

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            Writer.WriteLink(Generator.GetDocumentationInfo(containingNamespace), DirectoryInfo, FormatProvider.NamespaceFormat, SymbolDisplayAdditionalOptions.None);
            Writer.WriteLine();
            Writer.WriteLine();
        }

        public override void WriteAssembly(ISymbol symbol)
        {
            Writer.WriteString("Assembly: ");
            Writer.WriteString(symbol.ContainingAssembly.Name);
            Writer.WriteString(".dll");
            Writer.WriteLine();
            Writer.WriteLine();
        }

        public override void WriteObsolete(ISymbol symbol)
        {
            Writer.WriteBold("WARNING: This API is now obsolete.");
            Writer.WriteLine();
            Writer.WriteLine();

            TypedConstant typedConstant = symbol.GetAttribute(MetadataNames.System_ObsoleteAttribute).ConstructorArguments.FirstOrDefault();

            if (typedConstant.Type?.SpecialType == SpecialType.System_String)
            {
                string message = typedConstant.Value?.ToString();

                if (!string.IsNullOrEmpty(message))
                    Writer.WriteString(message);

                Writer.WriteLine();
            }

            Writer.WriteLine();
        }

        public override void WriteSummary(ISymbol symbol)
        {
            WriteSection(symbol, heading: "Summary", "summary");
        }

        public override void WriteSignature(ISymbol symbol)
        {
            Writer.WriteFencedCodeBlock(symbol.ToDisplayString(FormatProvider.SignatureFormat), GetLanguageIdentifier());
        }

        public override void WriteTypeParameters(ISymbol symbol)
        {
            ImmutableArray<ITypeParameterSymbol> typeParameters = symbol.GetTypeParameters();

            if (typeParameters.Any())
                WriteTable(typeParameters, "Type Parameters", 3, "Type Parameter", "Summary", FormatProvider.TypeParameterFormat, SymbolDisplayAdditionalOptions.None);
        }

        public override void WriteParameters(ISymbol symbol)
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

        public override void WriteReturnValue(ISymbol symbol)
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

                Writer.WriteHeading(3 + HeadingBaseLevel, heading);
                Writer.WriteLink(Generator.GetDocumentationInfo(returnType), DirectoryInfo, FormatProvider.ReturnValueFormat, SymbolDisplayAdditionalOptions.None);
                Writer.WriteLine();

                string returns = Generator.GetDocumentationElement(symbol2, "returns")?.Value;

                if (returns != null)
                {
                    Writer.WriteString(returns.Trim());
                    Writer.WriteLine();
                }
            }
        }

        public override void WriteInheritance(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.TypeKind == TypeKind.Class
                && typeSymbol.IsStatic)
            {
                return;
            }

            Writer.WriteHeading(3 + HeadingBaseLevel, "Inheritance");

            using (IEnumerator<ITypeSymbol> en = typeSymbol.BaseTypesAndSelf().Reverse().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    ITypeSymbol symbol = en.Current;

                    bool isLast = !en.MoveNext();

                    WriterLinkOrText(symbol, isLast);

                    do
                    {
                        Writer.WriteString(" ");
                        Writer.WriteCharEntity('\u2192');
                        Writer.WriteString(" ");

                        symbol = en.Current;
                        isLast = !en.MoveNext();

                        WriterLinkOrText(symbol.OriginalDefinition, isLast);
                    }
                    while (!isLast);
                }
            }

            Writer.WriteLine();

            void WriterLinkOrText(ITypeSymbol symbol, bool isLast)
            {
                if (isLast)
                {
                    Writer.WriteString(symbol.ToDisplayString(FormatProvider.InheritanceFormat));
                }
                else
                {
                    Writer.WriteLink(Generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.InheritanceFormat, SymbolDisplayAdditionalOptions.None);
                }
            }
        }

        public override void WriteAttributes(ISymbol symbol)
        {
            using (IEnumerator<ITypeSymbol> en = symbol
                .GetAttributes()
                .Select(f => f.AttributeClass)
                .Where(f => !ShouldBeExcluded(f))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(3 + HeadingBaseLevel, "Attributes");

                    WriteLink(Generator.GetDocumentationInfo(en.Current), SymbolDisplayAdditionalOptions.None);

                    while (en.MoveNext())
                    {
                        Writer.WriteString(", ");

                        WriteLink(Generator.GetDocumentationInfo(en.Current), SymbolDisplayAdditionalOptions.None);
                    }
                }
            }

            Writer.WriteLine();

            bool ShouldBeExcluded(INamedTypeSymbol attributeSymbol)
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
                    case "TypeForwardedFromAttribute":
                    case "TypeForwardedToAttribute":
                    case "MethodImplAttribute":
                        return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Runtime_CompilerServices);
                    default:
                        return false;
                }
            }
        }

        public override void WriteDerived(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !typeSymbol.IsStatic)
            {
                using (IEnumerator<ITypeSymbol> en = Generator
                    .TypeSymbols
                    .Where(f => f.InheritsFrom(typeSymbol))
                    .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        Writer.WriteHeading(3 + HeadingBaseLevel, "Derived");

                        do
                        {
                            Writer.WriteStartBulletItem();
                            Writer.WriteLink(Generator.GetDocumentationInfo(en.Current), DirectoryInfo, FormatProvider.DerivedFormat, SymbolDisplayAdditionalOptions.None);
                            Writer.WriteEndBulletItem();
                        }
                        while (en.MoveNext());
                    }
                }
            }
        }

        public override void WriteImplements(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.IsStatic)
                return;

            if (typeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
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
                    Writer.WriteHeading(3 + HeadingBaseLevel, "Implements");

                    do
                    {
                        Writer.WriteStartBulletItem();
                        WriteLink(Generator.GetDocumentationInfo(en.Current), FormatProvider.ImplementsFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
                        Writer.WriteEndBulletItem();
                    }
                    while (en.MoveNext());
                }
            }
        }

        public override void WriteExceptions(ISymbol symbol)
        {
            using (IEnumerator<(XElement element, ISymbol exceptionSymbol)> en = GetExceptions().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(3 + HeadingBaseLevel, "Exceptions");

                    WriteException(en.Current.element, en.Current.exceptionSymbol);

                    while (en.MoveNext())
                        WriteException(en.Current.element, en.Current.exceptionSymbol);
                }
            }

            void WriteException(XElement element, ISymbol exceptionSymbol)
            {
                WriteLink(Generator.GetDocumentationInfo(exceptionSymbol), SymbolDisplayAdditionalOptions.None);
                Writer.WriteLine();
                Writer.WriteLine();
                WriteElementContent(element);
                Writer.WriteLine();
                Writer.WriteLine();
            }

            IEnumerable <(XElement element, ISymbol exceptionSymbol)> GetExceptions()
            {
                XElement element = Generator.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("exception"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol exceptionSymbol = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, DocumentationSource.SharedCompilation);

                            if (exceptionSymbol != null)
                                yield return (e, exceptionSymbol);
                        }
                    }
                }
            }
        }

        public override void WriteExamples(ISymbol symbol)
        {
            WriteSection(symbol, heading: "Examples", "examples");
        }

        public override void WriteRemarks(ISymbol symbol)
        {
            WriteSection(symbol, heading: "Remarks", "remarks");
        }

        public override void WriteEnumFields(IEnumerable<IFieldSymbol> fields)
        {
            using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(2 + HeadingBaseLevel, "Fields");

                    Writer.WriteStartTable(3);
                    Writer.WriteStartTableRow();
                    Writer.WriteTableCell("Name");
                    Writer.WriteTableCell("Value");
                    Writer.WriteTableCell("Summary");
                    Writer.WriteEndTableRow();
                    Writer.WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        Writer.WriteStartTableRow();
                        Writer.WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        Writer.WriteTableCell(fieldSymbol.ConstantValue.ToString());
                        Writer.WriteTableCell(Generator.GetDocumentationElement(fieldSymbol, "summary")?.Value.Trim());
                        Writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    Writer.WriteEndTable();
                }
            }
        }

        public override void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            WriteTable(constructors, "Constructors", 2, "Constructor", "Summary", FormatProvider.ConstructorFormat, SymbolDisplayAdditionalOptions.None);
        }

        public override void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, "Fields", 2, "Field", "Summary", FormatProvider.FieldFormat, SymbolDisplayAdditionalOptions.None);
        }

        public override void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, "Properties", 2, "Property", "Summary", FormatProvider.PropertyFormat, SymbolDisplayAdditionalOptions.UseItemProperty, addInheritedFrom: true);
        }

        public override void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, "Methods", 2, "Method", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.None, addInheritedFrom: true);
        }

        public override void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, "Operators", 2, "Operator", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseOperatorName);
        }

        public override void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, "Events", 2, "Event", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.None, addInheritedFrom: true);
        }

        public override void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, "Explicit Interface Implementations", 2, "Member", "Summary", FormatProvider.MethodFormat, SymbolDisplayAdditionalOptions.UseItemProperty);
        }

        public override void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
            IEnumerable<IMethodSymbol> extensionMethods = Generator
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

        public override void WriteSeeAlso(ISymbol symbol)
        {
            using (IEnumerator<ISymbol> en = GetSymbols().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(2 + HeadingBaseLevel, "See Also");
                    WriteBulletItem(en.Current);

                    while (en.MoveNext())
                        WriteBulletItem(en.Current);
                }
            }

            void WriteBulletItem(ISymbol symbol2)
            {
                Writer.WriteStartBulletItem();
                Writer.WriteLink(Generator.GetDocumentationInfo(symbol2), DirectoryInfo, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
                Writer.WriteEndBulletItem();
            }

            IEnumerable<ISymbol> GetSymbols()
            {
                XElement element = Generator.GetDocumentationElement(symbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("seealso"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol symbol2 = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, DocumentationSource.SharedCompilation);

                            if (symbol2 != null)
                                yield return symbol2;
                        }
                    }
                }
            }
        }

        private void WriteSection(ISymbol symbol, string heading, string name)
        {
            XElement element = Generator.GetDocumentationElement(symbol, name);

            if (element == null)
                return;

            if (heading != null)
            {
                Writer.WriteHeading(2 + HeadingBaseLevel, heading);
            }
            else
            {
                Writer.WriteLine();
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

                            Writer.WriteString(value);
                        }
                        else if (node is XElement e)
                        {
                            switch (XmlElementNameKindMapper.GetKindOrDefault(e.Name.LocalName))
                            {
                                case XmlElementKind.C:
                                    {
                                        string value = e.Value;
                                        value = TextUtility.ToSingleLine(value);
                                        Writer.WriteInlineCode(value);
                                        break;
                                    }
                                case XmlElementKind.Code:
                                    {
                                        if (isNested)
                                            break;

                                        string value = e.Value;
                                        value = TextUtility.RemoveLeadingTrailingNewLine(value);
                                        Writer.WriteFencedCodeBlock(value, GetLanguageIdentifier());

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
                                                        WriteList(e.Elements(), isNumbered: true);
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
                                        Writer.WriteLine();
                                        Writer.WriteLine();
                                        WriteElementContent(e);
                                        Writer.WriteLine();
                                        Writer.WriteLine();
                                        break;
                                    }
                                case XmlElementKind.ParamRef:
                                    {
                                        string parameterName = e.Attribute("name")?.Value;

                                        if (parameterName != null)
                                            Writer.WriteBold(parameterName);

                                        break;
                                    }
                                case XmlElementKind.See:
                                    {
                                        string commentId = e.Attribute("cref")?.Value;

                                        if (commentId != null)
                                        {
                                            ISymbol symbol = DocumentationCommentId.GetFirstSymbolForDeclarationId(commentId, DocumentationSource.SharedCompilation);

                                            Debug.Assert(symbol != null, commentId);

                                            if (symbol != null)
                                            {
                                                Writer.WriteLink(Generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.CrefFormat, SymbolDisplayAdditionalOptions.UseItemProperty | SymbolDisplayAdditionalOptions.UseOperatorName);
                                            }
                                            else
                                            {
                                                //TODO: documentation comment id not found
                                                Writer.WriteBold(commentId);
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.TypeParamRef:
                                    {
                                        string typeParameterName = e.Attribute("name")?.Value;

                                        if (typeParameterName != null)
                                            Writer.WriteBold(typeParameterName);

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

        private void WriteList(IEnumerable<XElement> elements, bool isNumbered = false)
        {
            int number = 1;

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
                                WriteStartItem();
                                WriteElementContent(element2, isNested: true);
                                WriteEndItem();
                            }
                        }
                        else
                        {
                            WriteStartItem();
                            WriteElementContent(element, isNested: true);
                            WriteEndItem();
                        }
                    }
                }
            }

            void WriteStartItem()
            {
                if (isNumbered)
                {
                    Writer.WriteStartOrderedItem(number);
                    number++;
                }
                else
                {
                    Writer.WriteStartBulletItem();
                }
            }

            void WriteEndItem()
            {
                if (isNumbered)
                {
                    Writer.WriteEndOrderedItem();
                }
                else
                {
                    Writer.WriteEndBulletItem();
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

                        Writer.WriteStartTable(columnCount);
                        Writer.WriteStartTableRow();

                        foreach (XElement element2 in element.Elements())
                        {
                            Writer.WriteStartTableCell();
                            WriteElementContent(element2, isNested: true);
                            Writer.WriteEndTableCell();
                        }

                        Writer.WriteEndTableRow();
                        Writer.WriteTableHeaderSeparator();

                        do
                        {
                            element = en.Current;

                            Writer.WriteStartTableRow();

                            int count = 0;
                            foreach (XElement element2 in element.Elements())
                            {
                                Writer.WriteStartTableCell();
                                WriteElementContent(element2, isNested: true);
                                Writer.WriteEndTableCell();
                                count++;

                                if (count == columnCount)
                                    break;
                            }

                            while (count < columnCount)
                            {
                                Writer.WriteTableCell(null);
                                count++;
                            }

                            Writer.WriteEndTableRow();
                        }
                        while (en.MoveNext());

                        Writer.WriteEndTable();
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
            using (IEnumerator<(ISymbol symbol, string displayString)> en = symbols
                .Select(f => (symbol: f, displayString: f.ToDisplayString(format, additionalOptions)))
                .OrderBy(f => f.displayString)
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    Writer.WriteHeading(headingLevel + HeadingBaseLevel, heading);

                    Writer.WriteStartTable(2);
                    Writer.WriteStartTableRow();
                    Writer.WriteTableCell(header1);
                    Writer.WriteTableCell(header2);
                    Writer.WriteEndTableRow();
                    Writer.WriteTableHeaderSeparator();

                    do
                    {
                        ISymbol symbol = en.Current.symbol;

                        Writer.WriteStartTableRow();
                        Writer.WriteStartTableCell();

                        SymbolDocumentationInfo info = Generator.GetDocumentationInfo(symbol);

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            Writer.WriteString(en.Current.symbol.Name);
                        }
                        else if (addLink)
                        {
                            Writer.WriteLink(info, DirectoryInfo, format, additionalOptions);
                        }
                        else
                        {
                            Writer.WriteString(en.Current.displayString);
                        }

                        Writer.WriteEndTableCell();
                        Writer.WriteStartTableCell();

                        XElement element = null;
                        switch (symbol.Kind)
                        {
                            case SymbolKind.Parameter:
                                {
                                    element = Generator
                                        .GetDocumentationElement(symbol.ContainingSymbol)?
                                        .Elements("param")
                                        .FirstOrDefault(f => f.Attribute("name")?.Value == symbol.Name);

                                    break;
                                }
                            case SymbolKind.TypeParameter:
                                {
                                    element = Generator
                                        .GetDocumentationElement(symbol.ContainingSymbol)?
                                        .Elements("typeparam")
                                        .FirstOrDefault(f => f.Attribute("name")?.Value == symbol.Name);

                                    break;
                                }
                            default:
                                {
                                    element = Generator.GetDocumentationElement(symbol, "summary");
                                    break;
                                }
                        }

                        if (element != null)
                            WriteElementContent(element, isNested: true);

                        if (addInheritedFrom
                            && Symbol != null
                            && symbol.ContainingType != Symbol)
                        {
                            Writer.WriteString(" (Inherited from ");
                            WriteLink(Generator.GetDocumentationInfo(symbol.ContainingType.OriginalDefinition), additionalOptions);
                            Writer.WriteString(")");
                        }

                        Writer.WriteEndTableCell();
                        Writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    Writer.WriteEndTable();
                }
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

                                string url = Generator.GetDocumentationInfo(symbol).GetUrl(DirectoryInfo);

                                Writer.WriteLinkOrText(symbol.Name, url);

                                break;
                            }
                        default:
                            {
                                Writer.WriteString(part.ToString());
                                break;
                            }
                    }
                }
            }
            else
            {
                string url = symbolInfo.GetUrl(DirectoryInfo);

                Writer.WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(format, additionalOptions), url);
            }
        }

        public void WriteNamespaceContent(
            IEnumerable<ITypeSymbol> typeSymbols,
            int headingLevel)
        {
            foreach (IGrouping<TypeKind, ITypeSymbol> grouping in typeSymbols
                .OrderBy(f => f.ToDisplayString(FormatProvider.TypeFormat))
                .GroupBy(f => f.TypeKind)
                .OrderBy(f => f.Key, TypeKindComparer.Instance))
            {
                TypeKind typeKind = grouping.Key;

                switch (typeKind)
                {
                    case TypeKind.Class:
                        {
                            WriteTable(grouping, "Classes", headingLevel, "Class", "Summary", FormatProvider.TypeFormat, SymbolDisplayAdditionalOptions.None);
                            break;
                        }
                    case TypeKind.Struct:
                        {
                            WriteTable(grouping, "Structs", headingLevel, "Struct", "Summary", FormatProvider.TypeFormat, SymbolDisplayAdditionalOptions.None);
                            break;
                        }
                    case TypeKind.Interface:
                        {
                            WriteTable(grouping, "Interfaces", headingLevel, "Interface", "Summary", FormatProvider.TypeFormat, SymbolDisplayAdditionalOptions.None);
                            break;
                        }
                    case TypeKind.Enum:
                        {
                            WriteTable(grouping, "Enums", headingLevel, "Enum", "Summary", FormatProvider.TypeFormat, SymbolDisplayAdditionalOptions.None);
                            break;
                        }
                    case TypeKind.Delegate:
                        {
                            WriteTable(grouping, "Delegates", headingLevel, "Delegate", "Summary", FormatProvider.TypeFormat, SymbolDisplayAdditionalOptions.None);
                            break;
                        }
                    default:
                        {
                            Debug.Fail(typeKind.ToString());
                            break;
                        }
                }
            }
        }

        public override string ToString()
        {
            return Writer.ToString();
        }

        public override void Close()
        {
            if (Writer.WriteState != WriteState.Closed)
                Writer.Close();
        }

        internal string GetLanguageIdentifier()
        {
            switch (Symbol.Language)
            {
                case LanguageNames.CSharp:
                    return LanguageIdentifiers.CSharp;
                case LanguageNames.VisualBasic:
                    return LanguageIdentifiers.VisualBasic;
                case LanguageNames.FSharp:
                    return LanguageIdentifiers.FSharp;
            }

            Debug.Assert(Symbol == null, Symbol.Language);
            return null;
        }
    }
}
