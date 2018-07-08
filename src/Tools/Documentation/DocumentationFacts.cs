// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class DocumentationFacts
    {
        public static string GetName(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return "Class";
                case TypeKind.Delegate:
                    return "Delegate";
                case TypeKind.Enum:
                    return "Enum";
                case TypeKind.Interface:
                    return "Interface";
                case TypeKind.Struct:
                    return "Struct";
            }

            throw new InvalidOperationException();
        }

        public static string GetPluralName(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return "Classes";
                case TypeKind.Delegate:
                    return "Delegates";
                case TypeKind.Enum:
                    return "Enums";
                case TypeKind.Interface:
                    return "Interfaces";
                case TypeKind.Struct:
                    return "Structs";
            }

            throw new InvalidOperationException();
        }
    }
}
