// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Roslynator.Documentation
{
    internal sealed class NamespaceDocumentationPartComparer : IComparer<NamespaceDocumentationParts>
    {
        private NamespaceDocumentationPartComparer()
        {
        }

        public static NamespaceDocumentationPartComparer Instance { get; } = new NamespaceDocumentationPartComparer();

        public int Compare(NamespaceDocumentationParts x, NamespaceDocumentationParts y)
        {
            return GetRank(x).CompareTo(GetRank(y));
        }

        private static int GetRank(NamespaceDocumentationParts part)
        {
            switch (part)
            {
                case NamespaceDocumentationParts.Heading:
                    return 1;
                case NamespaceDocumentationParts.Classes:
                    return 2;
                case NamespaceDocumentationParts.Structs:
                    return 3;
                case NamespaceDocumentationParts.Interfaces:
                    return 4;
                case NamespaceDocumentationParts.Enums:
                    return 5;
                case NamespaceDocumentationParts.Delegates:
                    return 6;
            }

            Debug.Fail(part.ToString());

            return 0;
        }
    }
}
