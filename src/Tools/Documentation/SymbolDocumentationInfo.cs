// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal class SymbolDocumentationInfo
    {
        public SymbolDocumentationInfo(ISymbol symbol, string commentId, string url, string fileUrl)
        {
            Symbol = symbol;
            CommentId = commentId;
            Url = url;
            FileUrl = fileUrl;
        }

        public ISymbol Symbol { get; }

        public string CommentId { get; }

        public string Url { get; }

        public string FileUrl { get; }
    }
}
