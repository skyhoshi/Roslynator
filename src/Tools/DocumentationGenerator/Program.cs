// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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
            string rootPath = null;

#if DEBUG
            rootPath = @"..\..\..\..\..\..\docs\api\";
#endif

            DocumentationSource source = DocumentationSource.CreateFromAssemblyName("Roslynator.CSharp.dll");

            var generator = new DocumentationGenerator(ImmutableArray.Create(source));

            string indexContent = generator.GenerateIndex("Roslynator API");

            FileHelper.WriteAllText(
                Path.Combine(rootPath, "README.md"),
                indexContent,
                _utf8NoBom,
                onlyIfChanges: true,
                fileMustExists: false);

            foreach (INamespaceSymbol namespaceSymbol in generator.TypeSymbols.Select(f => f.ContainingNamespace).Distinct())
            {
                string content = generator.GenerateDocument(namespaceSymbol);

                string path = generator.GetUrl(namespaceSymbol).Replace('/', '\\');

                path = rootPath + path + @"\README.md";

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                FileHelper.WriteAllText(path, content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
            }

            foreach (ITypeSymbol typeSymbol in generator.TypeSymbols)
            {
                string content = generator.GenerateDocument(typeSymbol);

                string path = generator.GetUrl(typeSymbol).Replace('/', '\\');

                path = rootPath + path + @"\README.md";

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                FileHelper.WriteAllText(path, content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
            }

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
