// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
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

            if (options.Environment != "github")
                return;

           Dictionary<string, PortableExecutableReference> references = File.ReadAllLines(options.AssemblyReferencesPath)
                .ToDictionary(f => Path.GetFileName(f), f => MetadataReference.CreateFromFile(f));

            CSharpCompilation compilation = CSharpCompilation.Create(
                "",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: references.Values,
                options: default(CSharpCompilationOptions));

            var compilationInfo = new CompilationDocumentationInfo(
                compilation,
                options.AssemblyNames.Select(f =>
                {
                    PortableExecutableReference reference = references[f];

                    var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

                    return new AssemblyDocumentationInfo(assemblySymbol, reference);
                }));

            var parts = DocumentationParts.None;

            if (!options.DocumentationParts.Any())
            {
                parts = DocumentationOptions.Default.Parts;
            }
            else
            {
                foreach (string partAsString in options.DocumentationParts)
                {
                    if (Enum.TryParse(typeof(DocumentationParts), partAsString, ignoreCase: true, out object enumParseResult))
                    {
                        parts |= ((DocumentationParts)enumParseResult);
                    }
                    else
                    {
                        return;
                    }
                }
            }

            var documentationOptions = new DocumentationOptions(
                parts: parts,
                formatBaseList: options.FormatBaseList,
                formatConstraints: options.FormatConstraints);

            var generator = new MarkdownDocumentationGenerator(compilationInfo, DocumentationUriProvider.GitHub, documentationOptions);

            string directoryPath = options.OutputDirectory;

            Directory.Delete(directoryPath, recursive: true);

            foreach (DocumentationFile documentationFile in generator.GenerateFiles(
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
    }
}
