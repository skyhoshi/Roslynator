// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal class SymbolDocumentationInfo
    {
        public SymbolDocumentationInfo(ISymbol symbol, string commentId, string documentId, string filePath)
        {
            Symbol = symbol;
            CommentId = commentId;
            DocumentId = documentId;
            FilePath = filePath;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public string DocumentId { get; }

        public string FilePath { get; }
    }
}
