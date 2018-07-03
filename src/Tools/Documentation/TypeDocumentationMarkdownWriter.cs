// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using Roslynator.CSharp;

namespace Roslynator.Documentation
{
    public class TypeDocumentationMarkdownWriter : TypeDocumentationWriter
    {
        private readonly DocumentationGenerator _generator;
        private readonly MarkdownWriter _writer;

        public TypeDocumentationMarkdownWriter(ITypeSymbol typeSymbol, SymbolDocumentationInfo directoryInfo, DocumentationGenerator generator)
        {
            _writer = MarkdownWriter.Create(new StringBuilder());
            _generator = generator;
            DirectoryInfo = directoryInfo;
            TypeSymbol = typeSymbol;
        }

        public override ITypeSymbol TypeSymbol { get; }

        public override SymbolDocumentationInfo DirectoryInfo { get; }

        private SymbolDisplayFormatProvider FormatProvider
        {
            get { return _generator.FormatProvider; }
        }

        public override void WriteTitle(ITypeSymbol typeSymbol)
        {
            _writer.WriteStartHeading(1);
            _writer.WriteString(typeSymbol.ToDisplayString(FormatProvider.TitleFormat));
            _writer.WriteString(" ");
            _writer.WriteString(GetTypeName());
            _writer.WriteEndHeading();

            string GetTypeName()
            {
                switch (typeSymbol.TypeKind)
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
                    default:
                        throw new ArgumentException("", nameof(typeSymbol));
                }
            }
        }

        public override void WriteNamespace(ITypeSymbol typeSymbol)
        {
            _writer.WriteString("Namespace: ");

            INamespaceSymbol containingNamespace = typeSymbol.ContainingNamespace;

            _writer.WriteLink(_generator.GetDocumentationInfo(containingNamespace), DirectoryInfo, FormatProvider.NamespaceFormat);
            _writer.WriteLine();
            _writer.WriteLine();
        }

        public override void WriteAssembly(ITypeSymbol typeSymbol)
        {
            _writer.WriteString("Assembly: ");
            _writer.WriteString(typeSymbol.ContainingAssembly.Name);
            _writer.WriteString(".dll");
            _writer.WriteLine();
            _writer.WriteLine();
        }

        public override void WriteObsolete(ITypeSymbol typeSymbol)
        {
            _writer.WriteBold("WARNING: This API is now obsolete.");
            _writer.WriteLine();
            _writer.WriteLine();

            TypedConstant typedConstant = typeSymbol.GetAttribute(MetadataNames.System_ObsoleteAttribute).ConstructorArguments.FirstOrDefault();

            if (typedConstant.Type?.SpecialType == SpecialType.System_String)
            {
                string message = typedConstant.Value?.ToString();

                if (!string.IsNullOrEmpty(message))
                    _writer.WriteString(message);

                _writer.WriteLine();
            }

            _writer.WriteLine();
        }

        public override void WriteSummary(ITypeSymbol typeSymbol)
        {
            WriteSection(typeSymbol, heading: "Summary", "summary");
        }

        public override void WriteTypeParameters(ITypeSymbol typeSymbol)
        {
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                ImmutableArray<ITypeParameterSymbol> typeParameters = namedTypeSymbol.TypeParameters;

                WriteTable(typeParameters, "Type Parameters", 4, "Type Parameter", "Summary", FormatProvider.TypeParameterFormat);
            }
        }

        public override void WriteParameters(ITypeSymbol typeSymbol)
        {
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                if (methodSymbol != null)
                {
                    ImmutableArray<IParameterSymbol> parameters = methodSymbol.Parameters;

                    WriteTable(parameters, "Parameters", 4, "Parameter", "Summary", FormatProvider.ParameterFormat);
                }
            }
        }

        public override void WriteReturnValue(ITypeSymbol typeSymbol)
        {
            switch (typeSymbol.Kind)
            {
                case SymbolKind.NamedType:
                    {
                        var namedTypeSymbol = (INamedTypeSymbol)typeSymbol;

                        IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                        if (methodSymbol != null)
                            WriteReturnValue("Return Value", typeSymbol, methodSymbol.ReturnType);

                        break;
                    }
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)typeSymbol;

                        WriteReturnValue("Returns", typeSymbol, methodSymbol.ReturnType);
                        break;
                    }
                case SymbolKind.Property:
                    {
                        var propertySymbol = (IPropertySymbol)typeSymbol;

                        WriteReturnValue("Property Value", typeSymbol, propertySymbol.Type);
                        break;
                    }
            }

            void WriteReturnValue(string heading, ISymbol symbol, ITypeSymbol returnType)
            {
                if (returnType.SpecialType == SpecialType.System_Void)
                    return;

                _writer.WriteHeading3(heading);
                _writer.WriteLink(_generator.GetDocumentationInfo(returnType), DirectoryInfo, FormatProvider.ReturnValueFormat);
                _writer.WriteLine();

                string returns = _generator.GetDocumentationElement(symbol, "returns")?.Value;

                if (returns != null)
                {
                    _writer.WriteString(returns.Trim());
                    _writer.WriteLine();
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

            _writer.WriteHeading4("Inheritance");

            using (IEnumerator<ITypeSymbol> en = typeSymbol.BaseTypesAndSelf().Reverse().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    ITypeSymbol symbol = en.Current;

                    bool isLast = !en.MoveNext();

                    WriterLinkOrText(symbol, isLast);

                    do
                    {
                        _writer.WriteString(" ");
                        _writer.WriteCharEntity('\u2192');
                        _writer.WriteString(" ");

                        symbol = en.Current;
                        isLast = !en.MoveNext();

                        WriterLinkOrText(symbol.OriginalDefinition, isLast);
                    }
                    while (!isLast);
                }
            }

            _writer.WriteLine();

            void WriterLinkOrText(ISymbol symbol, bool isLast)
            {
                if (isLast)
                {
                    _writer.WriteString(symbol.ToDisplayString(FormatProvider.InheritanceFormat));
                }
                else
                {
                    _writer.WriteLink(_generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.InheritanceFormat);
                }
            }
        }

        public override void WriteAttributes(ITypeSymbol typeSymbol)
        {
            using (IEnumerator<ITypeSymbol> en = typeSymbol
                .GetAttributes()
                .Select(f => f.AttributeClass)
                .Where(f => !ShouldBeExcluded(f))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading4("Attributes");

                    WriterLink(en.Current);

                    while (en.MoveNext())
                    {
                        _writer.WriteString(", ");

                        WriterLink(en.Current);
                    }
                }
            }

            _writer.WriteLine();

            void WriterLink(ISymbol symbol)
            {
                _writer.WriteLink(_generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.AttributeFormat);
            }

            bool ShouldBeExcluded(INamedTypeSymbol attributeSymbol)
            {
                switch (attributeSymbol.MetadataName)
                {
                    case "DebuggerDisplayAttribute":
                    case "DebuggerTypeProxyAttribute":
                        {
                            return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics);
                        }
                    case "DefaultMemberAttribute":
                        {
                            return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Reflection);
                        }
                    case "TypeForwardedFromAttribute":
                    case "TypeForwardedToAttribute":
                        {
                            return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Runtime_CompilerServices);
                        }
                    default:
                        {
                            return false;
                        }
                }
            }
        }

        public override void WriteDerived(ITypeSymbol typeSymbol)
        {
            TypeKind typeKind = typeSymbol.TypeKind;

            if (typeKind.Is(TypeKind.Class, TypeKind.Interface)
                && !typeSymbol.IsStatic)
            {
                using (IEnumerator<ITypeSymbol> en = _generator
                    .TypeSymbols
                    .Where(f => f.InheritsFrom(typeSymbol))
                    .OrderBy(f => f.ToDisplayString(FormatProvider.DerivedFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        _writer.WriteHeading4("Derived");

                        do
                        {
                            _writer.WriteStartBulletItem();
                            _writer.WriteLink(_generator.GetDocumentationInfo(en.Current), DirectoryInfo, FormatProvider.DerivedFormat);
                            _writer.WriteEndBulletItem();
                        }
                        while (en.MoveNext());
                    }
                }
            }
        }

        public override void WriteImplements(ITypeSymbol typeSymbol)
        {
            if (typeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
                return;

            if (!typeSymbol.IsStatic)
            {
                IEnumerable<INamedTypeSymbol> allInterfaces = typeSymbol.AllInterfaces;

                if (allInterfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                {
                    allInterfaces = allInterfaces.Where(f => f.SpecialType != SpecialType.System_Collections_IEnumerable);
                }

                using (IEnumerator<INamedTypeSymbol> en = allInterfaces
                    .OrderBy(f => f.ToDisplayString(FormatProvider.ImplementsFormat))
                    .GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        _writer.WriteHeading4("Implements");

                        do
                        {
                            _writer.WriteStartBulletItem();
                            _writer.WriteLink(_generator.GetDocumentationInfo(en.Current), DirectoryInfo, FormatProvider.ImplementsFormat);
                            _writer.WriteEndBulletItem();
                        }
                        while (en.MoveNext());
                    }
                }
            }
        }

        public override void WriteExamples(ITypeSymbol typeSymbol)
        {
            WriteSection(typeSymbol, heading: "Examples", "examples");
        }

        public override void WriteRemarks(ITypeSymbol typeSymbol)
        {
            WriteSection(typeSymbol, heading: "Remarks", "remarks");
        }

        public override void WriteEnumFields(IEnumerable<IFieldSymbol> fields)
        {
            using (IEnumerator<IFieldSymbol> en = fields.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading(2, "Fields");

                    _writer.WriteStartTable(3);
                    _writer.WriteStartTableRow();
                    _writer.WriteStartTableCell();
                    _writer.WriteString("Name");
                    _writer.WriteEndTableCell();
                    _writer.WriteStartTableCell();
                    _writer.WriteString("Value");
                    _writer.WriteEndTableCell();
                    _writer.WriteStartTableCell();
                    _writer.WriteString("Summary");
                    _writer.WriteEndTableCell();
                    _writer.WriteEndTableRow();
                    _writer.WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        _writer.WriteStartTableRow();
                        _writer.WriteStartTableCell();
                        _writer.WriteString(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        _writer.WriteEndTableCell();
                        _writer.WriteStartTableCell();
                        _writer.WriteString(fieldSymbol.ConstantValue.ToString());
                        _writer.WriteEndTableCell();
                        _writer.WriteStartTableCell();
                        _writer.WriteString(_generator.GetDocumentationElement(fieldSymbol, "summary")?.Value.Trim());
                        _writer.WriteEndTableCell();
                        _writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    _writer.WriteEndTable();
                }
            }
        }

        public override void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            if (TypeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
                return;

            WriteTable(constructors, "Constructors", 2, "Constructor", "Summary", FormatProvider.ConstructorFormat);
        }

        public override void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, "Fields", 2, "Field", "Summary", FormatProvider.FieldFormat);
        }

        public override void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, "Properties", 2, "Property", "Summary", FormatProvider.PropertyFormat);
        }

        public override void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, "Methods", 2, "Method", "Summary", FormatProvider.MethodFormat);
        }

        public override void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, "Operators", 2, "Operator", "Summary", FormatProvider.MethodFormat);
        }

        public override void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, "Events", 2, "Event", "Summary", FormatProvider.MethodFormat);
        }

        public override void WriteExplicitInterfaceImplementations(IEnumerable<IMethodSymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, "Explicit Interface Implementations", 2, "Member", "Summary", FormatProvider.MethodFormat);
        }

        public override void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
            IEnumerable<IMethodSymbol> extensionMethods = _generator
                .ExtensionMethodSymbols
                .Where(f => f.Parameters[0].Type.OriginalDefinition == typeSymbol);

            WriteTable(
                extensionMethods,
                "Extension Methods",
                2,
                "Method",
                "Summary",
                FormatProvider.MethodFormat);
        }

        //TODO: WriteSeeAlso
        public override void WriteSeeAlso(ITypeSymbol typeSymbol)
        {
        }

        private void WriteSection(ITypeSymbol typeSymbol, string heading, string name)
        {
            XElement element = _generator.GetDocumentationElement(typeSymbol, name);

            if (element == null)
                return;

            if (heading != null)
            {
                _writer.WriteHeading2(heading);
            }
            else
            {
                _writer.WriteLine();
            }

            WriteElementContent(element);
        }

        private void WriteElementContent(XElement element)
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

                            _writer.WriteString(value);
                        }
                        else if (node is XElement e)
                        {
                            switch (XmlElementNameKindMapper.GetKindOrDefault(e.Name.LocalName))
                            {
                                case XmlElementKind.C:
                                    {
                                        _writer.WriteInlineCode(e.Value);
                                        break;
                                    }
                                case XmlElementKind.Code:
                                    {
                                        string value = e.Value;
                                        value = TextUtility.RemoveLeadingTrailingNewLine(value);
                                        _writer.WriteFencedCodeBlock(value);
                                        break;
                                    }
                                case XmlElementKind.List:
                                    {
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
                                        //TODO: write paragraph
                                        break;
                                    }
                                case XmlElementKind.ParamRef:
                                    {
                                        string parameterName = e.Attribute("name")?.Value;

                                        if (parameterName != null)
                                            _writer.WriteBold(parameterName);

                                        break;
                                    }
                                case XmlElementKind.See:
                                    {
                                        string commentId = e.Attribute("cref")?.Value;

                                        if (commentId != null)
                                        {
                                            ISymbol symbol = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, DocumentationSource.SharedCompilation);

                                            Debug.Assert(symbol != null, commentId);

                                            if (symbol != null)
                                            {
                                                _writer.WriteLink(_generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.CrefFormat);
                                            }
                                            else
                                            {
                                                //TODO: documentation comment id not found
                                                _writer.WriteBold(commentId);
                                            }
                                        }

                                        break;
                                    }
                                case XmlElementKind.TypeParamRef:
                                    {
                                        string typeParameterName = e.Attribute("name")?.Value;

                                        if (typeParameterName != null)
                                            _writer.WriteBold(typeParameterName);

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

                            switch (element2.Name.LocalName)
                            {
                                case "term":
                                    {
                                        if (en.MoveNext())
                                        {
                                            XElement element3 = en.Current;

                                            if (element3.Name.LocalName == "description")
                                            {
                                                WriteStartItem();
                                                _writer.WriteBold(element2.Value);
                                                _writer.WriteString(" - ");
                                                _writer.WriteString(element3.Value);
                                                WriteEndItem();
                                            }
                                        }

                                        break;
                                    }
                                case "description":
                                    {
                                        WriteStartItem();
                                        _writer.WriteString(element2.Value);
                                        WriteEndItem();
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            void WriteStartItem()
            {
                if (isNumbered)
                {
                    _writer.WriteStartOrderedItem(number);
                    number++;
                }
                else
                {
                    _writer.WriteStartBulletItem();
                }
            }

            void WriteEndItem()
            {
                if (isNumbered)
                {
                    _writer.WriteEndOrderedItem();
                }
                else
                {
                    _writer.WriteEndBulletItem();
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

                        _writer.WriteStartTable(columnCount);
                        _writer.WriteStartTableRow();

                        foreach (XElement element2 in element.Elements())
                        {
                            _writer.WriteStartTableCell();
                            _writer.WriteString(element2.Value);
                            _writer.WriteEndTableCell();
                        }

                        _writer.WriteEndTableRow();
                        _writer.WriteTableHeaderSeparator();

                        do
                        {
                            element = en.Current;

                            _writer.WriteStartTableRow();

                            int count = 0;
                            foreach (XElement element2 in element.Elements())
                            {
                                _writer.WriteStartTableCell();
                                _writer.WriteString(element2.Value);
                                _writer.WriteEndTableCell();
                                count++;

                                if (count == columnCount)
                                    break;
                            }

                            while (count < columnCount)
                            {
                                _writer.WriteStartTableCell();
                                _writer.WriteEndTableCell();
                                count++;
                            }

                            _writer.WriteEndTableRow();
                        }
                        while (en.MoveNext());
                    }
                }
            }
        }

        private void WriteTable(
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format)
        {
            _generator.WriteTable(_writer, symbols, heading, headingLevel, header1, header2, format, DirectoryInfo);
        }

        public override string ToString()
        {
            return _writer.ToString();
        }

        public override void Close()
        {
            if (_writer.WriteState != DotMarkdown.WriteState.Closed)
                _writer.Close();
        }
    }
}
