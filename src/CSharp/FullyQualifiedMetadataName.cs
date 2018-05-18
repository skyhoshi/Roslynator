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
        public FullyQualifiedMetadataName(string metadataName, IEnumerable<string> namespaceNames)
            : this(metadataName, Array.Empty<string>(), namespaceNames)
        {
        }

        public FullyQualifiedMetadataName(string metadataName, IEnumerable<string> containingTypeNames, IEnumerable<string> namespaceNames)
            : this(metadataName, containingTypeNames.ToImmutableArray(), namespaceNames.ToImmutableArray())
        {
        }

        public FullyQualifiedMetadataName(string metadataName, ImmutableArray<string> namespaceNames)
            : this(metadataName, ImmutableArray<string>.Empty, namespaceNames)
        {
        }

        public FullyQualifiedMetadataName(string metadataName, ImmutableArray<string> containingTypeNames, ImmutableArray<string> namespaceNames)
        {
            if (metadataName == null
                && containingTypeNames.Any())
            {
                throw new ArgumentException(metadataName, nameof(metadataName));
            }

            MetadataName = metadataName;
            ContainingTypeNames = containingTypeNames;
            NamespaceNames = namespaceNames;
        }

        public string MetadataName { get; }

        public ImmutableArray<string> ContainingTypeNames { get; }

        public ImmutableArray<string> NamespaceNames { get; }

        public string Namespace
        {
            get { return (!IsDefault) ? string.Join(".", NamespaceNames) : ""; }
        }

        public bool IsNamespace
        {
            get
            {
                return !NamespaceNames.IsDefault
                    && NamespaceNames.Any()
                    && MetadataName == null;
            }
        }

        public bool IsDefault
        {
            get
            {
                return MetadataName == null
                    && ContainingTypeNames.IsDefault
                    && NamespaceNames.IsDefault;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            if (IsDefault)
                return "";

            if (NamespaceNames.Any())
            {
                if (ContainingTypeNames.Any())
                {
                    return $"{Namespace}+{string.Join("+", ContainingTypeNames)}+{MetadataName}";
                }
                else
                {
                    return $"{Namespace}.{MetadataName}";
                }
            }
            else if (ContainingTypeNames.Any())
            {
                return $"{string.Join("+", ContainingTypeNames)}+{MetadataName}";
            }

            return MetadataName;
        }

        public override bool Equals(object obj)
        {
            return obj is FullyQualifiedMetadataName other
                && Equals(other);
        }

        public bool Equals(ISymbol symbol)
        {
            if (symbol == null)
                return false;

            if (MetadataName?.Equals(symbol.MetadataName, StringComparison.Ordinal) == false)
                return false;

            INamedTypeSymbol containingType = symbol.ContainingType;

            for (int i = ContainingTypeNames.Length - 1; i >= 0; i--)
            {
                if (containingType == null)
                    return false;

                if (!string.Equals(containingType.MetadataName, ContainingTypeNames[i], StringComparison.Ordinal))
                    return false;

                containingType = containingType.ContainingType;
            }

            if (containingType != null)
                return false;

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            for (int i = NamespaceNames.Length - 1; i >= 0; i--)
            {
                if (containingNamespace?.IsGlobalNamespace != false)
                    return false;

                if (!string.Equals(containingNamespace.Name, NamespaceNames[i], StringComparison.Ordinal))
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

            if (!string.Equals(MetadataName, other.MetadataName, StringComparison.Ordinal))
                return false;

            if (!ContainingTypeNames.SequenceEqual(other.ContainingTypeNames, StringComparer.Ordinal))
                return false;

            if (!NamespaceNames.SequenceEqual(other.NamespaceNames, StringComparer.Ordinal))
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            if (IsDefault)
                return 0;

            return Hash.Combine(Hash.CombineValues(NamespaceNames, StringComparer.Ordinal),
                Hash.Combine(Hash.CombineValues(ContainingTypeNames, StringComparer.Ordinal),
                Hash.Create(MetadataName)));
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
