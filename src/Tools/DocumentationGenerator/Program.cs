// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Roslynator.Utilities;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        [SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "<Pending>")]
        private static void Main(string[] args)
        {
            GenerateDocumentation(@"..\..\..\..\..\..\docs\api\", "Roslynator API", "Roslynator.CSharp.dll");
            GenerateDocumentation(@"..\..\..\..\..\..\docs\apitest\", "Foo API", "Roslynator.Documentation.DocTest.dll");

            //Console.WriteLine("OK");
            //Console.ReadKey();
        }

        private static void GenerateDocumentation(string rootPath, string name, params string[] assemblyNames)
        {
            ImmutableArray<DocumentationSource> sources = assemblyNames
                .Select(DocumentationSource.CreateFromAssemblyName)
                .ToImmutableArray();

            var generator = new DocumentationGenerator(sources);

            string indexContent = generator.GenerateRootDocument(name);

            FileHelper.WriteAllText(Path.Combine(rootPath, "README.md"), indexContent, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);

            foreach (INamespaceSymbol namespaceSymbol in generator.TypeSymbols.Select(f => f.ContainingNamespace).Distinct())
            {
                string content = generator.GenerateNamespaceDocument(namespaceSymbol);

                WriteFile(rootPath, content, generator.GetDocumentationInfo(namespaceSymbol));
            }

            foreach (ITypeSymbol typeSymbol in generator.TypeSymbols)
            {
                string content = generator.GenerateTypeDocument(typeSymbol);

                SymbolDocumentationInfo info = generator.GetDocumentationInfo(typeSymbol);

                WriteFile(rootPath, content, info);

                if (typeSymbol.BaseType?.SpecialType == SpecialType.System_Enum)
                    continue;

                content = generator.GenerateMemberDocument(info.GetConstructors());

                if (content != null)
                    WriteFile(rootPath, content, generator.GetDocumentationInfo(info.GetConstructors().First()));
            }
        }

        private static void WriteFile(string rootPath, string content, SymbolDocumentationInfo info)
        {
            string path = string.Join(@"\", info.Names.Reverse());

            path = rootPath + path + @"\README.md";

            Directory.CreateDirectory(Path.GetDirectoryName(path));
            FileHelper.WriteAllText(path, content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
        }
    }
}
