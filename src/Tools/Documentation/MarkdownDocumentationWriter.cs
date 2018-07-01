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
        private readonly DocumentationGenerator _generator;
        private readonly MarkdownWriter _writer;

        public MarkdownDocumentationWriter(DocumentationGenerator generator)
        {
            _generator = generator;
            _writer = MarkdownWriter.Create(new StringBuilder());
        }

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
            _writer.WriteString(typeSymbol.ContainingNamespace.ToDisplayString(FormatProvider.NamespaceFormat));
            _writer.WriteLine();
            _writer.WriteLine();
        }

        public override void WriteAssembly(ITypeSymbol typeSymbol)
        {
            _writer.WriteString("Assembly: ");
            _writer.WriteString(Path.ChangeExtension(typeSymbol.ContainingAssembly.Name, "dll"));
            _writer.WriteLine();
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
            if (typeSymbol is INamedTypeSymbol namedTypeSymbol)
            {
                IMethodSymbol methodSymbol = namedTypeSymbol.DelegateInvokeMethod;

                if (methodSymbol != null)
                {
                    _writer.WriteHeading3("Return Value");
                    _writer.WriteString(methodSymbol.ReturnType.ToDisplayString(FormatProvider.ReturnValueFormat));
                    _writer.WriteLine();

                    string returns = _generator.GetDocumentationElement(methodSymbol, "returns")?.Value;

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

            MBulletItem item = BulletItem(typeSymbol.ToDisplayString(FormatProvider.InheritanceFormat));

            typeSymbol = typeSymbol.BaseType;

            while (typeSymbol != null)
            {
                item = BulletItem(typeSymbol.ToDisplayString(FormatProvider.InheritanceFormat), item);

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

                _writer.WriteString(string.Join(", ", attributes.Select(f => f.AttributeClass.ToDisplayString(FormatProvider.AttributeFormat))));
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
        }

        public override void WriteSeeAlso(ITypeSymbol typeSymbol)
        {
        }

        private void WriteChapter(ITypeSymbol typeSymbol, string heading, string name)
        {
            string text = _generator.GetDocumentationElement(typeSymbol, name)?.Value;

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
            IEnumerable<ISymbol> symbols,
            string heading,
            int headingLevel,
            string header1,
            string header2,
            SymbolDisplayFormat format)
        {
            _generator.WriteTable(_writer, symbols, heading, headingLevel, header1, header2, format);
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
