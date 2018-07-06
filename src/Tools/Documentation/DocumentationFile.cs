// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Roslynator.Documentation
{
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
    public readonly struct DocumentationFile
    {
        public DocumentationFile(string content, string directoryPath, DocumentationKind kind)
        {
            Content = content;
            DirectoryPath = directoryPath;
            Kind = kind;
        }

        public string Content { get; }

        public string DirectoryPath { get; }

        public DocumentationKind Kind { get; }

        internal static DocumentationFile Create(string content, SymbolDocumentationInfo info, DocumentationKind kind)
        {
            return new DocumentationFile(content, string.Join(@"\", info.Names.Reverse()), kind);
        }
    }
}
