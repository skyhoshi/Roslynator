// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using DotMarkdown;
using DotMarkdown.Linq;
using Microsoft.CodeAnalysis;
using static DotMarkdown.Linq.MFactory;

namespace Roslynator.Documentation
{
    public class MarkdownDocumentationWriter : DocumentationWriter
    {
        private readonly MarkdownWriter _writer;
        private readonly XmlDocumentation _xmlDocumentation;
        private readonly SymbolDisplayFormatProvider _formatProvider;

        public MarkdownDocumentationWriter(
            XmlDocumentation xmlDocumentation,
            SymbolDisplayFormatProvider formatProvider = null)
        {
            _xmlDocumentation = xmlDocumentation;
            _formatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
            _writer = MarkdownWriter.Create(new StringBuilder());
        }

        public override void WriteTitle(ITypeSymbol typeSymbol)
        {
            _writer.WriteStartHeading(1);
            _writer.WriteString(typeSymbol.ToDisplayString(_formatProvider.TitleFormat));
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
            _writer.WriteString(typeSymbol.ContainingNamespace.ToDisplayString(_formatProvider.NamespaceFormat));
            _writer.WriteLine();
        }

        public override void WriteAssembly(ITypeSymbol typeSymbol)
        {
            _writer.WriteString("Assembly: ");
            _writer.WriteString(Path.ChangeExtension(typeSymbol.ContainingAssembly.Name, "dll"));
            _writer.WriteLine();
        }

        public override void WriteSummary(ITypeSymbol typeSymbol)
        {
            WriteChapter(typeSymbol, heading: null, "summary");
        }

        public override void WriteTypeParameters(ITypeSymbol typeSymbol)
        {
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                ImmutableArray<ITypeParameterSymbol> typeParameters = namedTypeSymbol.TypeParameters;

                WriteTable(typeParameters, "Type Parameters", 4, "Type Parameter", "Summary", _formatProvider.TypeParameterFormat);
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

                    WriteTable(parameters, "Parameters", 4, "Parameter", "Summary", _formatProvider.ParameterFormat);
                }
            }
        }

        public override void WriteReturnValue(ITypeSymbol typeSymbol)
        {
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                if (methodSymbol != null)
                {
                    _writer.WriteHeading3("Return Value");
                    _writer.WriteString(methodSymbol.ReturnType.ToDisplayString(_formatProvider.ReturnValueFormat));
                    _writer.WriteLine();

                    string returns = _xmlDocumentation.GetElementValue(methodSymbol.GetDocumentationCommentId(), "returns");

                    if (returns != null)
                    {
                        _writer.WriteString(returns.Trim());
                        _writer.WriteLine();
                    }
                }
            }
        }

        public override void WriteInheritance(ITypeSymbol typeSymbol)
        {
            _writer.WriteHeading4("Inheritance");

            MBulletItem item = BulletItem(typeSymbol.ToDisplayString(_formatProvider.InheritanceFormat));

            typeSymbol = typeSymbol.BaseType;

            while (typeSymbol != null)
            {
                item = BulletItem(typeSymbol.ToDisplayString(_formatProvider.InheritanceFormat), item);

                typeSymbol = typeSymbol.BaseType;
            }

            item.WriteTo(_writer);
        }

        public override void WriteAttributes(ITypeSymbol typeSymbol)
        {
            ImmutableArray<AttributeData> attributes = typeSymbol.GetAttributes();

            if (attributes.Any())
            {
                _writer.WriteHeading4("Attributes");

                _writer.WriteString(string.Join(", ", attributes.Select(f => f.AttributeClass.ToDisplayString(_formatProvider.AttributeFormat))));
                _writer.WriteLine();
            }
        }

        public override void WriteDerived(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteImplements(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteExamples(ITypeSymbol typeSymbol)
        {
            WriteChapter(typeSymbol, heading: "Examples", "examples");
        }

        public override void WriteRemarks(ITypeSymbol typeSymbol)
        {
            WriteChapter(typeSymbol, heading: "Remarks", "remarks");
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
                        _writer.WriteString(fieldSymbol.ToDisplayString(_formatProvider.FieldFormat));
                        _writer.WriteEndTableCell();
                        _writer.WriteStartTableCell();
                        _writer.WriteString(fieldSymbol.ConstantValue.ToString());
                        _writer.WriteEndTableCell();
                        _writer.WriteStartTableCell();
                        _writer.WriteString(_xmlDocumentation.GetSummary(fieldSymbol.GetDocumentationCommentId())?.Trim());
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
            WriteTable(constructors, "Constructors", 2, "Constructor", "Summary", _formatProvider.ConstructorFormat);
        }

        public override void WriteFields(IEnumerable<IFieldSymbol> fields)
        {
            WriteTable(fields, "Fields", 2, "Field", "Summary", _formatProvider.FieldFormat);
        }

        public override void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
            WriteTable(properties, "Properties", 2, "Property", "Summary", _formatProvider.PropertyFormat);
        }

        public override void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
            WriteTable(methods, "Methods", 2, "Method", "Summary", _formatProvider.MethodFormat);
        }

        public override void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
            WriteTable(operators, "Operators", 2, "Operator", "Summary", _formatProvider.MethodFormat);
        }

        public override void WriteEvents(IEnumerable<IEventSymbol> events)
        {
            WriteTable(events, "Events", 2, "Event", "Summary", _formatProvider.MethodFormat);
        }

        public override void WriteExplicitInterfaceImplementations(IEnumerable<IMethodSymbol> explicitInterfaceImplementations)
        {
            WriteTable(explicitInterfaceImplementations, "Explicit Interface Implementations", 2, "Member", "Summary", _formatProvider.MethodFormat);
        }

        public override void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteSeeAlso(ITypeSymbol typeSymbol)
        {
        }

        private void WriteChapter(ITypeSymbol typeSymbol, string heading, string name)
        {
            string text = _xmlDocumentation.GetElementValue(typeSymbol.GetDocumentationCommentId(), name);

            if (text != null)
            {
                if (heading != null)
                {
                    _writer.WriteHeading2(heading);
                }
                else
                {
                    _writer.WriteLine();
                }

                _writer.WriteString(text.Trim());
                _writer.WriteLine();
            }
        }

        private void WriteTable(
            IEnumerable<ISymbol> properties,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format)
        {
            using (IEnumerator<ISymbol> en = properties.GetEnumerator())
            {
                if (en.MoveNext())
                {
                    _writer.WriteHeading(headingLevel, heading);

                    WriteTableHeader(header1, header2);

                    do
                    {
                        WriteTableRow(en.Current, format);
                    }
                    while (en.MoveNext());

                    _writer.WriteEndTable();
                }
            }
        }

        private void WriteTableHeader(string header1, string header2)
        {
            _writer.WriteStartTable(2);
            _writer.WriteStartTableRow();
            _writer.WriteStartTableCell();
            _writer.WriteString(header1);
            _writer.WriteEndTableCell();
            _writer.WriteStartTableCell();
            _writer.WriteString(header2);
            _writer.WriteEndTableCell();
            _writer.WriteEndTableRow();
            _writer.WriteTableHeaderSeparator();
        }

        private void WriteTableRow(ISymbol symbol, SymbolDisplayFormat format)
        {
            _writer.WriteStartTableRow();
            _writer.WriteStartTableCell();
            _writer.WriteString(symbol.ToDisplayString(format));
            _writer.WriteEndTableCell();
            _writer.WriteStartTableCell();
            _writer.WriteString(_xmlDocumentation.GetSummary(symbol.GetDocumentationCommentId())?.Trim());
            _writer.WriteEndTableCell();
            _writer.WriteEndTableRow();
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
