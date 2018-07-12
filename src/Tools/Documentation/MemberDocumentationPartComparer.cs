// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Roslynator.Documentation
{
    internal sealed class MemberDocumentationPartComparer : IComparer<MemberDocumentationParts>
    {
        private MemberDocumentationPartComparer()
        {
        }

        public static MemberDocumentationPartComparer Instance { get; } = new MemberDocumentationPartComparer();

        public int Compare(MemberDocumentationParts x, MemberDocumentationParts y)
        {
            return GetRank(x).CompareTo(GetRank(y));
        }

        private static int GetRank(MemberDocumentationParts part)
        {
            switch (part)
            {
                case MemberDocumentationParts.Overloads:
                    return 1;
                case MemberDocumentationParts.Title:
                    return 2;
                case MemberDocumentationParts.Namespace:
                    return 3;
                case MemberDocumentationParts.Assembly:
                    return 4;
                case MemberDocumentationParts.Obsolete:
                    return 5;
                case MemberDocumentationParts.Summary:
                    return 6;
                case MemberDocumentationParts.Definition:
                    return 7;
                case MemberDocumentationParts.TypeParameters:
                    return 8;
                case MemberDocumentationParts.Parameters:
                    return 9;
                case MemberDocumentationParts.ReturnValue:
                    return 10;
                case MemberDocumentationParts.Implements:
                    return 11;
                case MemberDocumentationParts.Attributes:
                    return 12;
                case MemberDocumentationParts.Exceptions:
                    return 13;
                case MemberDocumentationParts.Examples:
                    return 14;
                case MemberDocumentationParts.Remarks:
                    return 15;
                case MemberDocumentationParts.SeeAlso:
                    return 16;
            }

            Debug.Fail(part.ToString());

            return 0;
        }
    }
}
