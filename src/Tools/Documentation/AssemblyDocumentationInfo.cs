// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class AssemblyDocumentationInfo
    {
        private readonly PortableExecutableReference _reference;

        public AssemblyDocumentationInfo(PortableExecutableReference metadataReference)
        {
            _reference = metadataReference;
        }

        public string FilePath
        {
            get { return _reference.FilePath; }
        }

        public IAssemblySymbol AssemblySymbol
        {
            get { return (IAssemblySymbol)DefaultCompilation.Instance.GetAssemblyOrModuleSymbol(_reference); }
        }

        public XmlDocumentation GetXmlDocumentation()
        {
            string xmlDocPath = Path.ChangeExtension(_reference.FilePath, "xml");

            if (!File.Exists(xmlDocPath))
                throw new FileNotFoundException("Unable to find xml documentation file.", xmlDocPath);

            return XmlDocumentation.Load(xmlDocPath);
        }

        public static AssemblyDocumentationInfo CreateFromAssemblyName(string assemblyName)
        {
            string path = RuntimeMetadataReference.TrustedPlatformAssemblyPaths[assemblyName];

            return Create(path);
        }

        public static AssemblyDocumentationInfo Create(string path)
        {
            foreach (MetadataReference metadataReference in DefaultCompilation.Instance.ExternalReferences)
            {
                var portableExecutableReference = (PortableExecutableReference)metadataReference;

                if (portableExecutableReference.FilePath == path)
                {
                    return new AssemblyDocumentationInfo(portableExecutableReference);
                }
            }

            throw new InvalidOperationException();
        }
    }
}
