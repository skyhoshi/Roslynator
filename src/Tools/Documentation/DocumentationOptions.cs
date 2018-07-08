// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public class DocumentationOptions
    {
        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public SymbolDisplayFormatProvider FormatProvider { get; } = SymbolDisplayFormatProvider.Default;

        public int MaxDerivedItems { get; } = 10;

        public RootDocumentationParts RootParts { get; } = RootDocumentationParts.All;

        public NamespaceDocumentationParts NamespaceParts { get; } = NamespaceDocumentationParts.All;

        public TypeDocumentationParts TypeParts { get; } = TypeDocumentationParts.All;

        public MemberDocumentationParts MemberParts { get; } = MemberDocumentationParts.All;

        public IComparer<NamespaceDocumentationParts> NamespacePartComparer { get; } = NamespaceDocumentationPartComparer.Instance;

        public bool IsEnabled(NamespaceDocumentationParts parts)
        {
            return (NamespaceParts & parts) != 0;
        }

        internal bool IsNamespacePartEnabled(TypeKind  typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return IsEnabled(NamespaceDocumentationParts.Classes);
                case TypeKind.Delegate:
                    return IsEnabled(NamespaceDocumentationParts.Delegates);
                case TypeKind.Enum:
                    return IsEnabled(NamespaceDocumentationParts.Enums);
                case TypeKind.Interface:
                    return IsEnabled(NamespaceDocumentationParts.Interfaces);
                case TypeKind.Struct:
                    return IsEnabled(NamespaceDocumentationParts.Structs);
                default:
                    return false;
            }
        }
    }
}
