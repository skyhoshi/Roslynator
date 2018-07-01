// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.Documentation
{
    public class DocumentationSource
    {
        private static Compilation _sharedCompilation;

        private readonly PortableExecutableReference _reference;

        public DocumentationSource(PortableExecutableReference metadataReference)
        {
            _reference = metadataReference;
        }

        internal static Compilation SharedCompilation
        {
            get
            {
                if (_sharedCompilation == null)
                {
                    IEnumerable<PortableExecutableReference> references = RuntimeMetadataReference
                        .TrustedPlatformAssemblies
                        .Select(f => MetadataReference.CreateFromFile(f.Value));

                    _sharedCompilation = CSharpCompilation.Create(
                        "Temp",
                        syntaxTrees: default(IEnumerable<SyntaxTree>),
                        references: references,
                        options: default(CSharpCompilationOptions));
                }

                return _sharedCompilation;
            }
        }

        public string FilePath
        {
            get { return _reference.FilePath; }
        }

        public IAssemblySymbol AssemblySymbol
        {
            get { return (IAssemblySymbol)SharedCompilation.GetAssemblyOrModuleSymbol(_reference); }
        }

        public XmlDocumentation GetXmlDocumentation()
        {
            string xmlDocPath = Path.ChangeExtension(_reference.FilePath, "xml");

            if (!File.Exists(xmlDocPath))
                throw new FileNotFoundException();

            return XmlDocumentation.Load(xmlDocPath);
        }

        public static DocumentationSource CreateFromAssemblyName(string assemblyName)
        {
            string path = RuntimeMetadataReference.TrustedPlatformAssemblies[assemblyName];

            return Create(path);
        }

        public static DocumentationSource Create(string path)
        {
            foreach (MetadataReference metadataReference in SharedCompilation.ExternalReferences)
            {
                var portableExecutableReference = (PortableExecutableReference)metadataReference;

                if (portableExecutableReference.FilePath == path)
                {
                    return new DocumentationSource(portableExecutableReference);
                }
            }

            throw new InvalidOperationException();
        }
    }
}
