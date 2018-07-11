// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public static class UrlProvider
    {
        public static string CreateUrl(
            string fileName,
            SymbolDocumentationInfo symbolInfo,
            SymbolDocumentationInfo directoryInfo = null,
            bool createLocalUrlForExternalType = false)
        {
            if (symbolInfo.IsExternal
                && !createLocalUrlForExternalType)
            {
                return CreateExternalUrl(symbolInfo);
            }

            if (directoryInfo == null)
                return CreateFullUrl(fileName, symbolInfo.NameAndBaseNamesAndNamespaceNames, "/");

            if (symbolInfo == directoryInfo)
                return "./" + fileName;

            int count = 0;

            ImmutableArray<ISymbol> symbols = symbolInfo.SymbolAndBaseTypesAndNamespaces;

            int i = symbols.Length - 1;
            int j = directoryInfo.SymbolAndBaseTypesAndNamespaces.Length - 1;

            while (i >= 0
                && j >= 0
                && symbols[i] == directoryInfo.SymbolAndBaseTypesAndNamespaces[j])
            {
                count++;
                i--;
                j--;
            }

            int diff = directoryInfo.SymbolAndBaseTypesAndNamespaces.Length - count;

            StringBuilder sb = StringBuilderCache.GetInstance();

            if (diff > 0)
            {
                sb.Append("..");
                diff--;

                while (diff > 0)
                {
                    sb.Append("/..");
                    diff--;
                }
            }

            ImmutableArray<string> names = symbolInfo.NameAndBaseNamesAndNamespaceNames;

            i = names.Length - 1 - count;

            if (i >= 0)
            {
                if (sb.Length > 0)
                    sb.Append("/");

                sb.Append(names[i]);
                i--;

                while (i >= 0)
                {
                    sb.Append("/");
                    sb.Append(names[i]);
                    i--;
                }
            }

            sb.Append("/");
            sb.Append(fileName);

            return StringBuilderCache.GetStringAndFree(sb);
        }

        internal static string CreateFullUrl(string fileName, ImmutableArray<string> names, string separator)
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

        public static string CreateExternalUrl(SymbolDocumentationInfo symbolInfo)
        {
            if (symbolInfo.SymbolAndBaseTypesAndNamespaces.LastOrDefault()?.Kind == SymbolKind.Namespace)
            {
                ImmutableArray<string> Names = symbolInfo.NameAndBaseNamesAndNamespaceNames;

                switch (Names.Last())
                {
                    case "System":
                    case "Microsoft":
                        {
                            const string baseUrl = "https://docs.microsoft.com/en-us/dotnet/api/";

                            int capacity = baseUrl.Length;

                            foreach (string name in Names)
                                capacity += name.Length;

                            capacity += Names.Length - 1;

                            StringBuilder sb = StringBuilderCache.GetInstance(capacity);

                            sb.Append(baseUrl);

                            sb.Append(Names.Last().ToLowerInvariant());

                            for (int i = Names.Length - 2; i >= 0; i--)
                            {
                                sb.Append(".");
                                sb.Append(Names[i].ToLowerInvariant());
                            }

                            return StringBuilderCache.GetStringAndFree(sb);
                        }
                }
            }

            return null;
        }
    }
}
