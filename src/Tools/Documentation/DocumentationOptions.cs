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

        public DocumentationOptions(
            string fileName = "README.md",
            int maxDerivedItems = 10,
            SymbolDisplayFormatProvider formatProvider = null,
            DocumentationParts parts = DocumentationParts.Namespace | DocumentationParts.Type | DocumentationParts.Member,
            RootDocumentationParts rootParts = RootDocumentationParts.All,
            NamespaceDocumentationParts namespaceParts = NamespaceDocumentationParts.All,
            TypeDocumentationParts typeParts = TypeDocumentationParts.All,
            MemberDocumentationParts memberParts = MemberDocumentationParts.All,
            IComparer<NamespaceDocumentationParts> namespacePartComparer = null,
            IComparer<TypeDocumentationParts> typePartComparer = null,
            IComparer<MemberDocumentationParts> memberPartComparer = null)
        {
            FileName = fileName;
            MaxDerivedItems = maxDerivedItems;
            FormatProvider = formatProvider ?? SymbolDisplayFormatProvider.Default;
            Parts = parts;
            RootParts = rootParts;
            NamespaceParts = namespaceParts;
            TypeParts = typeParts;
            MemberParts = memberParts;
            NamespacePartComparer = namespacePartComparer ?? NamespaceDocumentationPartComparer.Instance;
            TypePartComparer = typePartComparer ?? TypeDocumentationPartComparer.Instance;
            MemberPartComparer = memberPartComparer ?? MemberDocumentationPartComparer.Instance;
        }

        public static DocumentationOptions Default { get; } = new DocumentationOptions();

        public string FileName { get; }

        public int MaxDerivedItems { get; }

        public SymbolDisplayFormatProvider FormatProvider { get; }

        public DocumentationParts Parts { get; }

        public RootDocumentationParts RootParts { get; }

        public NamespaceDocumentationParts NamespaceParts { get; }

        public TypeDocumentationParts TypeParts { get; }

        public MemberDocumentationParts MemberParts { get; }

        public IComparer<NamespaceDocumentationParts> NamespacePartComparer { get; }

        public IComparer<TypeDocumentationParts> TypePartComparer { get; }

        public IComparer<MemberDocumentationParts> MemberPartComparer { get; }

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

        public bool IsEnabled(RootDocumentationParts parts)
        {
            return (RootParts & parts) != 0;
        }

        public bool IsEnabled(NamespaceDocumentationParts parts)
        {
            return (NamespaceParts & parts) != 0;
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
