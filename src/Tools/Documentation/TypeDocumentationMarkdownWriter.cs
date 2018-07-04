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
    public class TypeDocumentationMarkdownWriter : DocumentationWriter
    {
        private readonly DocumentationGenerator _generator;
        private readonly MarkdownWriter _writer;

        public TypeDocumentationMarkdownWriter(
            ISymbol symbol,
            SymbolDocumentationInfo directoryInfo,
            DocumentationGenerator generator,
            MarkdownWriter writer = null)
        {
            _writer = writer ?? MarkdownWriter.Create(new StringBuilder());
            _generator = generator;
            DirectoryInfo = directoryInfo;
            Symbol = symbol;
        }

        public override ISymbol Symbol { get; }

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
                WriteLink(_generator.GetDocumentationInfo(symbol));
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
                .OrderBy(f => f.ToDisplayString(FormatProvider.ImplementsFormat))
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading4("Implements");

                    do
                    {
                        _writer.WriteStartBulletItem();
                        WriteLink(_generator.GetDocumentationInfo(en.Current));
                        _writer.WriteEndBulletItem();
                    }
                    while (en.MoveNext());
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
                    _writer.WriteTableCell("Name");
                    _writer.WriteTableCell("Value");
                    _writer.WriteTableCell("Summary");
                    _writer.WriteEndTableRow();
                    _writer.WriteTableHeaderSeparator();

                    do
                    {
                        IFieldSymbol fieldSymbol = en.Current;

                        _writer.WriteStartTableRow();
                        _writer.WriteTableCell(fieldSymbol.ToDisplayString(FormatProvider.FieldFormat));
                        _writer.WriteTableCell(fieldSymbol.ConstantValue.ToString());
                        _writer.WriteTableCell(_generator.GetDocumentationElement(fieldSymbol, "summary")?.Value.Trim());
                        _writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    _writer.WriteEndTable();
                }
            }
        }

        public override void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
            WriteTable(constructors, "Constructors", 2, "Constructor", "Summary", FormatProvider.ConstructorFormat);
        }

        public override void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, "Fields", 2, "Field", "Summary", FormatProvider.FieldFormat);
        }

        public override void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, "Properties", 2, "Property", "Summary", FormatProvider.PropertyFormat, addInheritedFrom: true);
        }

        public override void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, "Methods", 2, "Method", "Summary", FormatProvider.MethodFormat, addInheritedFrom: true);
        }

        public override void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, "Operators", 2, "Operator", "Summary", FormatProvider.MethodFormat);
        }

        public override void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, "Events", 2, "Event", "Summary", FormatProvider.MethodFormat, addInheritedFrom: true);
        }

        public override void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations)
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

        public override void WriteSeeAlso(ITypeSymbol typeSymbol)
        {
            using (IEnumerator<ISymbol> en = GetSymbols().GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading2("See Also");
                    WriteBulletItem(en.Current);

                    while (en.MoveNext())
                        WriteBulletItem(en.Current);
                }
            }

            void WriteBulletItem(ISymbol symbol)
            {
                _writer.WriteStartBulletItem();
                _writer.WriteLink(_generator.GetDocumentationInfo(symbol), DirectoryInfo, FormatProvider.CrefFormat);
                _writer.WriteEndBulletItem();
            }

            IEnumerable<ISymbol> GetSymbols()
            {
                XElement element = _generator.GetDocumentationElement(typeSymbol);

                if (element != null)
                {
                    foreach (XElement e in element.Elements("seealso"))
                    {
                        string commentId = e.Attribute("cref")?.Value;

                        if (commentId != null)
                        {
                            ISymbol symbol = DocumentationCommentId.GetFirstSymbolForReferenceId(commentId, DocumentationSource.SharedCompilation);

                            if (symbol != null)
                                yield return symbol;
                        }
                    }
                }
            }
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

        private void WriteElementContent(XElement element, bool isNested = false)
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

                            _writer.WriteString(value);
                        }
                        else if (node is XElement e)
                        {
                            switch (XmlElementNameKindMapper.GetKindOrDefault(e.Name.LocalName))
                            {
                                case XmlElementKind.C:
                                    {
                                        string value = e.Value;
                                        value = TextUtility.ToSingleLine(value);
                                        _writer.WriteInlineCode(value);
                                        break;
                                    }
                                case XmlElementKind.Code:
                                    {
                                        if (isNested)
                                            break;

                                        string value = e.Value;
                                        value = TextUtility.RemoveLeadingTrailingNewLine(value);
                                        _writer.WriteFencedCodeBlock(value, GetLanguageIdentifier());

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
                                        _writer.WriteLine();
                                        _writer.WriteLine();
                                        WriteElementContent(e);
                                        _writer.WriteLine();
                                        _writer.WriteLine();
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
                                            ISymbol symbol = DocumentationCommentId.GetFirstSymbolForDeclarationId(commentId, DocumentationSource.SharedCompilation);

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

            string GetLanguageIdentifier()
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

                Debug.Fail(Symbol.Language);
                return null;
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
                            WriteElementContent(element2, isNested: true);
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
                                WriteElementContent(element2, isNested: true);
                                _writer.WriteEndTableCell();
                                count++;

                                if (count == columnCount)
                                    break;
                            }

                            while (count < columnCount)
                            {
                                _writer.WriteTableCell(null);
                                count++;
                            }

                            _writer.WriteEndTableRow();
                        }
                        while (en.MoveNext());

                        _writer.WriteEndTable();
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
            SymbolDisplayFormat format,
            bool addInheritedFrom = false)
        {
            using (IEnumerator<(ISymbol symbol, string displayString)> en = symbols
                .Select(f => (symbol: f, displayString: f.ToDisplayString(format)))
                .OrderBy(f => f.displayString)
                .GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading(headingLevel, heading);

                    _writer.WriteStartTable(2);
                    _writer.WriteStartTableRow();
                    _writer.WriteTableCell(header1);
                    _writer.WriteTableCell(header2);
                    _writer.WriteEndTableRow();
                    _writer.WriteTableHeaderSeparator();

                    do
                    {
                        ISymbol symbol = en.Current.symbol;

                        _writer.WriteStartTableRow();
                        _writer.WriteStartTableCell();

                        SymbolDocumentationInfo info = _generator.GetDocumentationInfo(symbol);

                        if (symbol.IsKind(SymbolKind.Parameter, SymbolKind.TypeParameter))
                        {
                            _writer.WriteString(en.Current.displayString);
                        }
                        else
                        {
                            _writer.WriteLink(info, DirectoryInfo, format);
                        }

                        _writer.WriteEndTableCell();
                        _writer.WriteStartTableCell();

                        XElement element = _generator.GetDocumentationElement(symbol, "summary");

                        if (element != null)
                            WriteElementContent(element, isNested: true);

                        if (addInheritedFrom
                            && Symbol != null
                            && symbol.ContainingType != Symbol)
                        {
                            _writer.WriteString(" (Inherited from ");
                            WriteLink(_generator.GetDocumentationInfo(symbol.ContainingType));
                            _writer.WriteString(")");
                        }

                        _writer.WriteEndTableCell();
                        _writer.WriteEndTableRow();
                    }
                    while (en.MoveNext());

                    _writer.WriteEndTable();
                }
            }
        }

        public void WriteLink(SymbolDocumentationInfo symbolInfo)
        {
            if (symbolInfo.Symbol is INamedTypeSymbol namedTypeSymbol
                && namedTypeSymbol.TypeArguments.Any(f => f.Kind != SymbolKind.TypeParameter))
            {
                var sb = new StringBuilder();

                foreach (SymbolDisplayPart part in symbolInfo
                    .Symbol
                    .ToDisplayParts(SymbolDisplayFormats.TypeNameAndContainingTypes))
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

                                string url = _generator.GetDocumentationInfo(symbol).GetUrl(DirectoryInfo);

                                _writer.WriteLinkOrText(symbol.Name, url);

                                break;
                            }
                        default:
                            {
                                _writer.WriteString(part.ToString());
                                break;
                            }
                    }
                }
            }
            else
            {
                string url = symbolInfo.GetUrl(DirectoryInfo);
                _writer.WriteLinkOrText(symbolInfo.Symbol.ToDisplayString(SymbolDisplayFormats.TypeNameAndContainingTypes), url);
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
                            WriteTable(grouping, "Classes", headingLevel, "Class", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Struct:
                        {
                            WriteTable(grouping, "Structs", headingLevel, "Struct", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Interface:
                        {
                            WriteTable(grouping, "Interfaces", headingLevel, "Interface", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Enum:
                        {
                            WriteTable(grouping, "Enums", headingLevel, "Enum", "Summary", FormatProvider.TypeFormat);
                            break;
                        }
                    case TypeKind.Delegate:
                        {
                            WriteTable(grouping, "Delegates", headingLevel, "Delegate", "Summary", FormatProvider.TypeFormat);
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
            return _writer.ToString();
        }

        public override void Close()
        {
            if (_writer.WriteState != WriteState.Closed)
                _writer.Close();
        }
    }
}
