// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal readonly struct FullyQualifiedMetadataName : IEquatable<FullyQualifiedMetadataName>
    {
        public FullyQualifiedMetadataName(string name, IEnumerable<string> namespaces)
            : this(name, Array.Empty<string>(), namespaces)
        {
        }

        public FullyQualifiedMetadataName(string name, IEnumerable<string> containingTypes, IEnumerable<string> namespaces)
            : this(name, containingTypes.ToImmutableArray(), namespaces.ToImmutableArray())
        {
        }

        public FullyQualifiedMetadataName(string name, ImmutableArray<string> namespaces)
            : this(name, ImmutableArray<string>.Empty, namespaces)
        {
        }

        public FullyQualifiedMetadataName(string name, ImmutableArray<string> containingTypes, ImmutableArray<string> namespaces)
        {
            if (name == null
                && containingTypes.Any())
            {
                throw new ArgumentException(name, nameof(name));
            }

            Name = name;
            ContainingTypes = containingTypes;
            Namespaces = namespaces;
        }

        public string Name { get; }

        public ImmutableArray<string> ContainingTypes { get; }

        public ImmutableArray<string> Namespaces { get; }

        public string Namespace
        {
            get { return (!IsDefault) ? string.Join(".", Namespaces) : ""; }
        }

        public bool IsNamespace
        {
            get
            {
                return !Namespaces.IsDefault
                    && Namespaces.Any()
                    && Name == null;
            }
        }

        public bool IsDefault
        {
            get
            {
                return Name == null
                    && ContainingTypes.IsDefault
                    && Namespaces.IsDefault;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return ToString(SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces);
        }

        public string ToString(SymbolDisplayTypeQualificationStyle typeQualificationStyle)
        {
            if (IsDefault)
                return "";

            if (Namespaces.Any()
                && typeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces)
            {
                if (ContainingTypes.Any())
                {
                    return $"{Namespace}+{string.Join("+", ContainingTypes)}+{Name}";
                }
                else
                {
                    return $"{Namespace}.{Name}";
                }
            }
            else if (ContainingTypes.Any()
                && typeQualificationStyle == SymbolDisplayTypeQualificationStyle.NameAndContainingTypes)
            {
                return $"{string.Join("+", ContainingTypes)}+{Name}";
            }

            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is FullyQualifiedMetadataName other
                && Equals(other);
        }

        internal bool Equals(ISymbol symbol)
        {
            if (symbol == null)
                return false;

            if (Name?.Equals(symbol.MetadataName, StringComparison.Ordinal) == false)
                return false;

            INamedTypeSymbol containingType = symbol.ContainingType;

            for (int i = ContainingTypes.Length - 1; i >= 0; i--)
            {
                if (containingType == null)
                    return false;

                if (!string.Equals(containingType.MetadataName, ContainingTypes[i], StringComparison.Ordinal))
                    return false;

                containingType = containingType.ContainingType;
            }

            if (containingType != null)
                return false;

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            for (int i = Namespaces.Length - 1; i >= 0; i--)
            {
                if (containingNamespace?.IsGlobalNamespace != false)
                    return false;

                if (!string.Equals(containingNamespace.Name, Namespaces[i], StringComparison.Ordinal))
                    return false;

                containingNamespace = containingNamespace.ContainingNamespace;
            }

            return containingNamespace?.IsGlobalNamespace == true;
        }

        public bool Equals(FullyQualifiedMetadataName other)
        {
            if (IsDefault)
                return other.IsDefault;

            if (other.IsDefault)
                return false;

            if (!string.Equals(Name, other.Name, StringComparison.Ordinal))
                return false;

            if (!ContainingTypes.SequenceEqual(other.ContainingTypes, StringComparer.Ordinal))
                return false;

            if (!Namespaces.SequenceEqual(other.Namespaces, StringComparer.Ordinal))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            if (IsDefault)
                return 0;

            return Hash.Combine(Hash.CombineValues(Namespaces, StringComparer.Ordinal),
                Hash.Combine(Hash.CombineValues(ContainingTypes, StringComparer.Ordinal),
                Hash.Create(Name)));
        }

        public static bool operator ==(FullyQualifiedMetadataName info1, FullyQualifiedMetadataName info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(FullyQualifiedMetadataName info1, FullyQualifiedMetadataName info2)
        {
            return !(info1 == info2);
        }
    }
}
