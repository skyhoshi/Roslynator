// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using Roslynator.Utilities;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static readonly UTF8Encoding _utf8NoBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);

        [SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "<Pending>")]
        private static void Main(string[] args)
        {
            GenerateDocumentation(@"..\..\..\..\..\..\docs\api", "Roslynator API", "Roslynator.CSharp.dll");
            GenerateDocumentation(@"..\..\..\..\..\..\docs\apitest", "Foo API", "Roslynator.Documentation.DocTest.dll");
        }

        private static void GenerateDocumentation(string directoryPath, string heading, params string[] assemblyNames)
        {
            const string fileName = "README.md";

            ImmutableArray<AssemblyDocumentationInfo> assemblies = assemblyNames
                .Select(AssemblyDocumentationInfo.CreateFromAssemblyName)
                .ToImmutableArray();

            var compilation = new CompilationDocumentationInfo(DefaultCompilation.Instance, assemblies);

            var generator = new DocumentationGenerator(compilation, fileName);

            WriteFile(directoryPath + @"\_ObjectModel.md", generator.GenerateObjectModel("Roslynator Object Model"));
            WriteFile(directoryPath + @"\_External.md", generator.GenerateExtendedTypesFile("Types Extended by Roslynator API").Content);

            foreach (DocumentationFile documentationFile in generator.GenerateExtendedTypeFiles())
                WriteFile(documentationFile, fileName, directoryPath);

            foreach (DocumentationFile documentationFile in generator.GenerateFiles(heading))
                WriteFile(documentationFile, fileName, directoryPath);
        }

        private static void WriteFile(string path, string content)
        {
            if (!string.IsNullOrEmpty(content))
                FileHelper.WriteAllText(path, content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
        }

        private static void WriteFile(in DocumentationFile documentationFile, string fileName, string directoryPath)
        {
            string path = directoryPath;

            if (documentationFile.DirectoryPath != null)
                path = directoryPath + @"\" + string.Join(@"\", documentationFile.DirectoryPath);

            path += @"\" + fileName;

            Directory.CreateDirectory(Path.GetDirectoryName(path));

            FileHelper.WriteAllText(path, documentationFile.Content, _utf8NoBom, onlyIfChanges: true, fileMustExists: false);
        }
    }
}
