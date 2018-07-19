// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CommandLine;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslynator.Documentation.Markdown;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        private static void Main(string[] args)
        {
            ParserResult<CommandLineOptions> result = Parser.Default.ParseArguments<CommandLineOptions>(args);

            if (!(result is Parsed<CommandLineOptions> parsed))
                return;

            CommandLineOptions options = parsed.Value;

            if (options.MaxDerivedItems < -1)
            {
                Console.WriteLine("Maximum number of derived items must be equal or greater than 0.");
            }

            if (!string.Equals(options.Environment, "github", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Unknown environment value '{options.Environment}'.");
                return;
            }

            IEnumerable<string> referencePaths = GetReferencePaths(options.AssemblyReferences);

            if (referencePaths == null)
                return;

            List<PortableExecutableReference> references = referencePaths
                .Select(f => MetadataReference.CreateFromFile(f))
                .ToList();

            CSharpCompilation compilation = CSharpCompilation.Create(
                "",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: references,
                options: default(CSharpCompilationOptions));

            var compilationInfo = new CompilationDocumentationInfo(
                compilation,
                options.Assemblies.Select(f =>
                {
                    if (!TryGetReference(references, f, out PortableExecutableReference reference))
                        Console.WriteLine($"Assembly '{f}' not found in the list of assembly references.");

                    var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

                    return new AssemblyDocumentationInfo(assemblySymbol, reference);
                }));

            if (!TryGetDocumentationParts(options.DocumentationParts, out DocumentationParts parts))
                return;

            if (!TryGetNamespaceDocumentationParts(options.NamespaceParts, out NamespaceDocumentationParts namespaceParts))
                return;

            if (!TryGetTypeDocumentationParts(options.TypeParts, out TypeDocumentationParts typeParts))
                return;

            if (!TryGetMemberDocumentationParts(options.MemberParts, out MemberDocumentationParts memberParts))
                return;

            var documentationOptions = new DocumentationOptions(
                parts: parts,
                namespaceParts: namespaceParts,
                typeParts: typeParts,
                memberParts: memberParts,
                formatBaseList: options.FormatBaseList,
                formatConstraints: options.FormatConstraints,
                maxDerivedItems: (options.MaxDerivedItems == -1) ? DocumentationOptions.Default.MaxDerivedItems : options.MaxDerivedItems);

            var generator = new MarkdownDocumentationGenerator(compilationInfo, DocumentationUriProvider.GitHub, documentationOptions);

            string directoryPath = options.OutputDirectory;

            //Directory.Delete(directoryPath, recursive: true);

            foreach (DocumentationGeneratorResult documentationFile in generator.Generate(
                heading: options.Heading,
                objectModelHeading: options.ObjectModelHeading,
                extendedExternalTypesHeading: options.ExtendedExternalTypeHeading))
            {
                string path = Path.Combine(directoryPath, documentationFile.Path);

                Console.WriteLine($"saving '{path}'");

                //Directory.CreateDirectory(Path.GetDirectoryName(path));
                //File.WriteAllText(path, documentationFile.Content, _utf8NoBom);
            }
        }

        private static IEnumerable<string> GetReferencePaths(string assemblyReferences)
        {
            if (assemblyReferences.Contains(";"))
            {
                return assemblyReferences.Split(";", StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                string path = assemblyReferences;

                if (!File.Exists(path))
                {
                    Console.WriteLine($"File not found '{path}'.");
                    return null;
                }

                string extension = Path.GetExtension(path);

                if (string.Equals(extension, ".dll", StringComparison.OrdinalIgnoreCase))
                {
                    return assemblyReferences.Split(";", StringSplitOptions.RemoveEmptyEntries);
                }
                else
                {
                    return File.ReadLines(assemblyReferences).Where(f => !string.IsNullOrWhiteSpace(f));
                }
            }
        }

        private static bool TryGetDocumentationParts(IEnumerable<string> values, out DocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.Parts;
                return true;
            }

            parts = DocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(typeof(DocumentationParts), value, ignoreCase: true, out object result))
                {
                    parts |= ((DocumentationParts)result);
                }
                else
                {
                    Console.WriteLine($"Unknown documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetNamespaceDocumentationParts(IEnumerable<string> values, out NamespaceDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.NamespaceParts;
                return true;
            }

            parts = NamespaceDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(typeof(NamespaceDocumentationParts), value, ignoreCase: true, out object result))
                {
                    parts |= ((NamespaceDocumentationParts)result);
                }
                else
                {
                    Console.WriteLine($"Unknown namespace documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetTypeDocumentationParts(IEnumerable<string> values, out TypeDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.TypeParts;
                return true;
            }

            parts = TypeDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(typeof(TypeDocumentationParts), value, ignoreCase: true, out object result))
                {
                    parts |= ((TypeDocumentationParts)result);
                }
                else
                {
                    Console.WriteLine($"Unknown type documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetMemberDocumentationParts(IEnumerable<string> values, out MemberDocumentationParts parts)
        {
            if (!values.Any())
            {
                parts = DocumentationOptions.Default.MemberParts;
                return true;
            }

            parts = MemberDocumentationParts.None;

            foreach (string value in values)
            {
                if (Enum.TryParse(typeof(MemberDocumentationParts), value, ignoreCase: true, out object result))
                {
                    parts |= ((MemberDocumentationParts)result);
                }
                else
                {
                    Console.WriteLine($"Unknown member documentation part '{value}'.");
                    return false;
                }
            }

            return true;
        }

        private static bool TryGetReference(List<PortableExecutableReference> references, string path, out PortableExecutableReference reference)
        {
            if (path.Contains(Path.DirectorySeparatorChar))
            {
                foreach (PortableExecutableReference r in references)
                {
                    if (r.FilePath == path)
                    {
                        reference = r;
                        return true;
                    }
                }
            }
            else
            {
                foreach (PortableExecutableReference r in references)
                {
                    string filePath = r.FilePath;

                    int index = filePath.LastIndexOf(Path.DirectorySeparatorChar);

                    if (string.Compare(filePath, index + 1, path, 0, path.Length, StringComparison.Ordinal) == 0)
                    {
                        reference = r;
                        return true;
                    }
                }
            }

            reference = null;
            return false;
        }
    }
}
