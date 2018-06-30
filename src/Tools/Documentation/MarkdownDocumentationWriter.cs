// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DotMarkdown;
using Microsoft.CodeAnalysis;

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

        public override void WriteAssemblies(ITypeSymbol typeSymbol)
        {
            _writer.WriteString("Assembly: ");
            _writer.WriteString(Path.ChangeExtension(typeSymbol.ContainingAssembly.Name, "dll"));
            _writer.WriteLine();
        }

        public override void WriteSummary(ITypeSymbol typeSymbol)
        {
            string summary = _xmlDocumentation.GetSummary(typeSymbol.GetDocumentationCommentId());

            if (summary != null)
            {
                _writer.WriteLine();
                _writer.WriteString(summary.Trim());
                _writer.WriteLine();
            }
        }

        public override void WriteTypeParameters(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteInheritance(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteDerived(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteImplements(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteExamples(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteRemarks(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteConstructors(IEnumerable<IMethodSymbol> constructors)
        {
        }

        public override void WriteConstructor(IMethodSymbol constructor)
        {
        }

        public override void WriteProperties(IEnumerable<IPropertySymbol> properties)
        {
        }

        public override void WriteProperty(IPropertySymbol propertySymbol)
        {
        }

        public override void WriteMethods(IEnumerable<IMethodSymbol> methods)
        {
        }

        public override void WriteMethod(IMethodSymbol methods)
        {
        }

        public override void WriteExplicitInterfaceImplementations(IEnumerable<IMethodSymbol> explicitInterfaceImplementations)
        {
        }

        public override void WriteOperators(IEnumerable<IMethodSymbol> operators)
        {
        }

        public override void WriteExtensionMethods(ITypeSymbol typeSymbol)
        {
        }

        public override void WriteSeeAlso(ITypeSymbol typeSymbol)
        {
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
