// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class AssemblyDocumentationInfo
    {
        public AssemblyDocumentationInfo(IAssemblySymbol assemblySymbol, PortableExecutableReference reference)
        {
            AssemblySymbol = assemblySymbol;
            Reference = reference;
        }

        public string FilePath => Reference.FilePath;

        public IAssemblySymbol AssemblySymbol { get; }

        public PortableExecutableReference Reference { get; }

        public XmlDocumentation GetXmlDocumentation()
        {
            string xmlDocPath = Path.ChangeExtension(Reference.FilePath, "xml");

            if (!File.Exists(xmlDocPath))
                throw new FileNotFoundException("Unable to find xml documentation file.", xmlDocPath);

            return XmlDocumentation.Load(xmlDocPath);
        }
    }
}
