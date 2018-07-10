// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Roslynator.Documentation
{
    [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public readonly struct DocumentationFile
    {
        public DocumentationFile(string content, string path, DocumentationKind kind)
        {
            Content = content;
            Path = path;
            Kind = kind;
        }

        public string Content { get; }

        public string Path { get; }

        public DocumentationKind Kind { get; }

        internal bool HasContent
        {
            get { return !string.IsNullOrWhiteSpace(Content); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{Kind} {Path} {Content}"; }
        }

        internal static DocumentationFile Create(DocumentationWriter writer, SymbolDocumentationInfo info, string fileName, DocumentationKind kind)
        {
            return new DocumentationFile(writer.ToString(), info.GetPath(fileName), kind);
        }
    }
}
