// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.Documentation
{
    internal sealed class DefaultCompilation
    {
        private static Compilation _instance;

        private DefaultCompilation()
        {
        }

        public static Compilation Instance
        {
            get
            {
                if (_instance == null)
                {
                    IEnumerable<PortableExecutableReference> references = RuntimeMetadataReference
                        .TrustedPlatformAssemblyPaths
                        .Select(f => MetadataReference.CreateFromFile(f.Value));

                    _instance = CSharpCompilation.Create(
                        "Temp",
                        syntaxTrees: default(IEnumerable<SyntaxTree>),
                        references: references,
                        options: default(CSharpCompilationOptions));
                }

                return _instance;
            }
        }
    }
}
