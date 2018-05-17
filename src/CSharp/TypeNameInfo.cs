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
    internal readonly struct TypeNameInfo : IEquatable<TypeNameInfo>
    {
        private static class Namespaces
        {
            public static readonly ImmutableArray<string> System_Linq = ImmutableArray.Create("System", "Linq");
            public static readonly ImmutableArray<string> System_Collections_Generic = ImmutableArray.Create("System", "Collections", "Generic");
        }

        public static TypeNameInfo System_Collections_Generic { get; } = new TypeNameInfo(null, Namespaces.System_Collections_Generic);

        public static TypeNameInfo System_Collections_Generic_ImmutableArray_1 { get; } = new TypeNameInfo("ImmutableArray`1", Namespaces.System_Collections_Generic);

        public static TypeNameInfo System_Linq_Enumerable { get; } = new TypeNameInfo("Enumerable", Namespaces.System_Linq);

        public static TypeNameInfo System_Linq_ImmutableArrayExtensions { get; } = new TypeNameInfo("ImmutableArrayExtensions", Namespaces.System_Linq);

        public TypeNameInfo(string metadataName, IEnumerable<string> namespaceNames)
            : this(metadataName, Array.Empty<string>(), namespaceNames)
        {
        }

        public TypeNameInfo(string metadataName, IEnumerable<string> containingTypeNames, IEnumerable<string> namespaceNames)
            : this(metadataName, containingTypeNames.ToImmutableArray(), namespaceNames.ToImmutableArray())
        {
        }

        public TypeNameInfo(string metadataName, ImmutableArray<string> namespaceNames)
            : this(metadataName, ImmutableArray<string>.Empty, namespaceNames)
        {
        }

        public TypeNameInfo(string metadataName, ImmutableArray<string> containingTypeNames, ImmutableArray<string> namespaceNames)
        {
            MetadataName = metadataName;
            ContainingTypeNames = containingTypeNames;
            NamespaceNames = namespaceNames;
        }

        public string MetadataName { get; }

        public ImmutableArray<string> ContainingTypeNames { get; }

        public ImmutableArray<string> NamespaceNames { get; }

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
            get
            {
                if (IsDefault)
                    return "";

                if (NamespaceNames.Any())
                {
                    if (ContainingTypeNames.Any())
                    {
                        return $"{string.Join(".", NamespaceNames)}+{string.Join(".", ContainingTypeNames)}{MetadataName}";
                    }
                    else
                    {
                        return string.Join(".", NamespaceNames) + MetadataName;
                    }
                }
                else if (ContainingTypeNames.Any())
                {
                    return string.Join(".", ContainingTypeNames) + MetadataName;
                }

                return MetadataName;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is TypeNameInfo other
                && Equals(other);
        }

        public bool Equals(TypeNameInfo other)
        {
            return string.Equals(MetadataName, other.MetadataName, StringComparison.Ordinal)
                   && ContainingTypeNames.Equals(other.ContainingTypeNames)
                   && NamespaceNames.Equals(other.NamespaceNames);
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

        public override int GetHashCode()
        {
            return Hash.Combine(EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(NamespaceNames),
                Hash.Combine(EqualityComparer<ImmutableArray<string>>.Default.GetHashCode(ContainingTypeNames),
                Hash.Create(MetadataName)));
        }

        public static bool operator ==(TypeNameInfo info1, TypeNameInfo info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(TypeNameInfo info1, TypeNameInfo info2)
        {
            return !(info1 == info2);
        }
    }
}
