// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace Roslynator.Documentation
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public readonly struct DocumentationGeneratorResult : IEquatable<DocumentationGeneratorResult>
    {
        public DocumentationGeneratorResult(string content, string path, DocumentationKind kind)
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

        internal static DocumentationGeneratorResult Create(DocumentationWriter writer, DocumentationUriProvider uriProvider, DocumentationKind kind, SymbolDocumentationInfo symbolInfo = null)
        {
            return new DocumentationGeneratorResult(writer.ToString(), uriProvider.GetFilePath(kind, symbolInfo), kind);
        }

        public override bool Equals(object obj)
        {
            return obj is DocumentationGeneratorResult other && Equals(other);
        }

        public bool Equals(DocumentationGeneratorResult other)
        {
            return Kind == other.Kind
                && Path == other.Path
                && Content == other.Content;
        }

        public override int GetHashCode()
        {
            return Hash.Combine(StringComparer.Ordinal.GetHashCode(Content),
                Hash.Combine(StringComparer.OrdinalIgnoreCase.GetHashCode(Path), (int)Kind));
        }

        public static bool operator ==(in DocumentationGeneratorResult file1, in DocumentationGeneratorResult file2)
        {
            return file1.Equals(file2);
        }

        public static bool operator !=(in DocumentationGeneratorResult file1, in DocumentationGeneratorResult file2)
        {
            return !(file1 == file2);
        }
    }
}
