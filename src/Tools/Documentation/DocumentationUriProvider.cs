// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Roslynator.Documentation
{
    public abstract class DocumentationUriProvider
    {
        protected DocumentationUriProvider(IEnumerable<ExternalDocumentationUrlProvider> externalProviders = null)
        {
            ExternalProviders = (externalProviders != null)
                ? ImmutableArray.CreateRange(externalProviders)
                : ImmutableArray<ExternalDocumentationUrlProvider>.Empty;
        }

        public static DocumentationUriProvider GitHub { get; } = new GitHubDocumentationUriProvider(ImmutableArray.Create(ExternalDocumentationUrlProvider.MicrosoftDocs));

        public ImmutableArray<ExternalDocumentationUrlProvider> ExternalProviders { get; }

        public abstract string GetFilePath(DocumentationKind kind, SymbolDocumentationInfo symbolInfo);

        public abstract DocumentationUrlInfo GetLocalUrl(SymbolDocumentationInfo symbolInfo, SymbolDocumentationInfo directoryInfo);

        public DocumentationUrlInfo GetExternalUrl(SymbolDocumentationInfo symbolInfo)
        {
            foreach (ExternalDocumentationUrlProvider provider in ExternalProviders)
            {
                DocumentationUrlInfo urlInfo = provider.CreateUrl(symbolInfo);

                if (urlInfo.Url != null)
                    return urlInfo;
            }

            return default;
        }

        internal static string GetFullUri(string fileName, ImmutableArray<string> names, char separator)
        {
            int capacity = fileName.Length + 1;

            foreach (string name in names)
                capacity += name.Length;

            capacity += names.Length - 1;

            StringBuilder sb = StringBuilderCache.GetInstance(capacity);

            sb.Append(names.Last());

            for (int i = names.Length - 2; i >= 0; i--)
            {
                sb.Append(separator);
                sb.Append(names[i]);
            }

            sb.Append(separator);
            sb.Append(fileName);

            return StringBuilderCache.GetStringAndFree(sb);
        }
    }
}
