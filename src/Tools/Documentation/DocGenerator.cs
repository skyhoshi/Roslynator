// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using DotMarkdown;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.Documentation
{
    public class DocGenerator
    {
        public void GenerateDocumentationFiles(string name, IEnumerable<string> assemblyNames)
        {
            IEnumerable<PortableExecutableReference> references = assemblyNames
                .Select(f => RuntimeMetadataReference.CreateFromAssemblyName(f))
                .ToArray();

            IEnumerable<PortableExecutableReference> allReferences = RuntimeMetadataReference
                .TrustedPlatformAssemblies
                .Keys
                .Except(assemblyNames)
                .Select(f => RuntimeMetadataReference.CreateFromAssemblyName(f))
                .Concat(references);

            CSharpCompilation compilation = CSharpCompilation.Create(
                "Temp",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: allReferences,
                options: default(CSharpCompilationOptions));

            foreach (PortableExecutableReference reference in references)
            {
                var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

                foreach (ITypeSymbol typeSymbol in assemblySymbol.GetPubliclyVisibleTypes())
                {
                    GenerateTypeFile(typeSymbol);
                }
            }
        }

        public void GenerateTypeFile(ITypeSymbol typeSymbol)
        {
            var sb = new StringBuilder();
            using (MarkdownWriter writer = MarkdownWriter.Create(sb))
            {
                WriteTitle(writer, typeSymbol);
                WriteContainingNamespace(writer, typeSymbol);
                WriteContainingAssemblies(writer, typeSymbol);
                WriteSummary(writer, typeSymbol);

                ImmutableArray<ISymbol> members = typeSymbol.GetMembers();

                Debug.WriteLine(sb.ToString());
            }
        }

        public virtual void WriteTitle(MarkdownWriter writer, ITypeSymbol typeSymbol)
        {
            writer.WriteStartHeading(1);
            writer.WriteString(typeSymbol.ToDisplayString());
            writer.WriteString(" ");
            writer.WriteString(GetTypeName());
            writer.WriteEndHeading();

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

        public virtual void WriteContainingNamespace(MarkdownWriter writer, ITypeSymbol typeSymbol)
        {
            writer.WriteString("Namespace: ");
            writer.WriteString(typeSymbol.ContainingNamespace.ToDisplayString());
            writer.WriteLine();
        }

        public virtual void WriteContainingAssemblies(MarkdownWriter writer, ITypeSymbol typeSymbol)
        {
            writer.WriteString("Assembly: ");
            writer.WriteString(typeSymbol.ContainingAssembly.ToDisplayString());
            writer.WriteLine();
        }

        public virtual void WriteSummary(MarkdownWriter writer, ITypeSymbol typeSymbol)
        {
            DocumentationCommentInfo info = DocumentationCommentInfo.Create(typeSymbol);

            string summary = info.Summary;
            if (summary != null)
            {
                writer.WriteString(summary);
                writer.WriteLine();
            }
        }
    }
}
