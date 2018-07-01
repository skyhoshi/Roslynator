// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal sealed class TypeKindComparer : IComparer<TypeKind>
    {
        public static TypeKindComparer Instance { get; } = new TypeKindComparer();

        private TypeKindComparer()
        {
        }

        public int Compare(TypeKind x, TypeKind y)
        {
            return GetRank(x).CompareTo(GetRank(y));
        }

        private static int GetRank(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return 1;
                case TypeKind.Struct:
                    return 2;
                case TypeKind.Interface:
                    return 3;
                case TypeKind.Enum:
                    return 4;
                case TypeKind.Delegate:
                    return 5;
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
