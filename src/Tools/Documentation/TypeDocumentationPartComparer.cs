// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Diagnostics;

namespace Roslynator.Documentation
{
    internal sealed class TypeDocumentationPartComparer : IComparer<TypeDocumentationParts>
    {
        private TypeDocumentationPartComparer()
        {
        }

        public static TypeDocumentationPartComparer Instance { get; } = new TypeDocumentationPartComparer();

        public int Compare(TypeDocumentationParts x, TypeDocumentationParts y)
        {
            return GetRank(x).CompareTo(GetRank(y));
        }

        private static int GetRank(TypeDocumentationParts part)
        {
            switch (part)
            {
                case TypeDocumentationParts.Title:
                    return 1;
                case TypeDocumentationParts.Namespace:
                    return 2;
                case TypeDocumentationParts.Assembly:
                    return 3;
                case TypeDocumentationParts.Obsolete:
                    return 4;
                case TypeDocumentationParts.Summary:
                    return 5;
                case TypeDocumentationParts.Definition:
                    return 6;
                case TypeDocumentationParts.TypeParameters:
                    return 7;
                case TypeDocumentationParts.Parameters:
                    return 8;
                case TypeDocumentationParts.ReturnValue:
                    return 9;
                case TypeDocumentationParts.Inheritance:
                    return 10;
                case TypeDocumentationParts.Attributes:
                    return 11;
                case TypeDocumentationParts.Derived:
                    return 12;
                case TypeDocumentationParts.Implements:
                    return 13;
                case TypeDocumentationParts.Examples:
                    return 14;
                case TypeDocumentationParts.Remarks:
                    return 15;
                case TypeDocumentationParts.Constructors:
                    return 16;
                case TypeDocumentationParts.Fields:
                    return 17;
                case TypeDocumentationParts.Properties:
                    return 18;
                case TypeDocumentationParts.Methods:
                    return 19;
                case TypeDocumentationParts.Operators:
                    return 20;
                case TypeDocumentationParts.Events:
                    return 21;
                case TypeDocumentationParts.ExplicitInterfaceImplementations:
                    return 22;
                case TypeDocumentationParts.ExtensionMethods:
                    return 23;
                case TypeDocumentationParts.SeeAlso:
                    return 24;
            }

            Debug.Fail(part.ToString());

            return 0;
        }
    }
}
