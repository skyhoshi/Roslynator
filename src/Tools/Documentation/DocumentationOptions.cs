// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;
using System;

namespace Roslynator.Documentation
{
    public class DocumentationOptions
    {
        private ImmutableArray<NamespaceDocumentationParts> _enabledAndSortedNamespaceParts;
        private ImmutableArray<TypeDocumentationParts> _enabledAndSortedTypeParts;
        private ImmutableArray<MemberDocumentationParts> _enabledAndSortedMemberParts;

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public string FileName { get; } = "README.md";

        public SymbolDisplayFormatProvider FormatProvider { get; } = SymbolDisplayFormatProvider.Default;

        public int MaxDerivedItems { get; } = 10;

        public RootDocumentationParts RootParts { get; } = RootDocumentationParts.All;

        public NamespaceDocumentationParts NamespaceParts { get; } = NamespaceDocumentationParts.All;

        public TypeDocumentationParts TypeParts { get; } = TypeDocumentationParts.All;

        public MemberDocumentationParts MemberParts { get; } = MemberDocumentationParts.All;

        public IComparer<NamespaceDocumentationParts> NamespacePartComparer { get; } = NamespaceDocumentationPartComparer.Instance;

        public IComparer<TypeDocumentationParts> TypePartComparer { get; } = TypeDocumentationPartComparer.Instance;

        public IComparer<MemberDocumentationParts> MemberPartComparer { get; } = MemberDocumentationPartComparer.Instance;

        internal ImmutableArray<NamespaceDocumentationParts> EnabledAndSortedNamespaceParts
        {
            get
            {
                if (_enabledAndSortedNamespaceParts.IsDefault)
                {
                    _enabledAndSortedNamespaceParts = Enum.GetValues(typeof(NamespaceDocumentationParts))
                        .Cast<NamespaceDocumentationParts>()
                        .Where(f => f != NamespaceDocumentationParts.None
                            && f != NamespaceDocumentationParts.All
                            && IsEnabled(f))
                        .OrderBy(f => f, NamespacePartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedNamespaceParts;
            }
        }

        internal ImmutableArray<TypeDocumentationParts> EnabledAndSortedTypeParts
        {
            get
            {
                if (_enabledAndSortedTypeParts.IsDefault)
                {
                    _enabledAndSortedTypeParts = Enum.GetValues(typeof(TypeDocumentationParts))
                        .Cast<TypeDocumentationParts>()
                        .Where(f => f != TypeDocumentationParts.None
                            && f != TypeDocumentationParts.All
                            && IsEnabled(f))
                        .OrderBy(f => f, TypePartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedTypeParts;
            }
        }

        internal ImmutableArray<MemberDocumentationParts> EnabledAndSortedMemberParts
        {
            get
            {
                if (_enabledAndSortedMemberParts.IsDefault)
                {
                    _enabledAndSortedMemberParts = Enum.GetValues(typeof(MemberDocumentationParts))
                        .Cast<MemberDocumentationParts>()
                        .Where(f => f != MemberDocumentationParts.None
                            && f != MemberDocumentationParts.All
                            && IsEnabled(f))
                        .OrderBy(f => f, MemberPartComparer)
                        .ToImmutableArray();
                }

                return _enabledAndSortedMemberParts;
            }
        }

        public bool IsEnabled(NamespaceDocumentationParts parts)
        {
            return (NamespaceParts & parts) != 0;
        }

        public bool IsEnabled(RootDocumentationParts parts)
        {
            return (RootParts & parts) != 0;
        }

        public bool IsEnabled(TypeDocumentationParts parts)
        {
            return (TypeParts & parts) != 0;
        }

        public bool IsEnabled(MemberDocumentationParts parts)
        {
            return (MemberParts & parts) != 0;
        }

        internal bool IsNamespacePartEnabled(TypeKind typeKind)
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
