// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    internal sealed class MetadataNameEqualityComparer<TSymbol> : EqualityComparer<TSymbol> where TSymbol : ISymbol
    {
        private static readonly int _plusHashCode = GetHashCode("+");
        private static readonly int _dotHashCode = GetHashCode(".");

        internal MetadataNameEqualityComparer()
        {
        }

        public override bool Equals(TSymbol x, TSymbol y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (Default.Equals(x, default(TSymbol)))
                return false;

            if (Default.Equals(y, default(TSymbol)))
                return false;

            if (!StringComparer.Ordinal.Equals(x.MetadataName, y.MetadataName))
                return false;

            INamedTypeSymbol t1 = x.ContainingType;
            INamedTypeSymbol t2 = y.ContainingType;

            while (!object.ReferenceEquals(t1, t2))
            {
                if (t1 == null)
                    return false;

                if (t2 == null)
                    return false;

                if (!StringComparer.Ordinal.Equals(t1.MetadataName, t2.MetadataName))
                    return false;

                t1 = t1.ContainingType;
                t2 = t2.ContainingType;
            }

            INamespaceSymbol n1 = x.ContainingNamespace;
            INamespaceSymbol n2 = y.ContainingNamespace;

            while (!object.ReferenceEquals(n1, n2))
            {
                if (n1 == null)
                    return false;

                if (n2 == null)
                    return false;

                if (!StringComparer.Ordinal.Equals(n1.MetadataName, n2.MetadataName))
                    return false;

                n1 = n1.ContainingNamespace;
                n2 = n2.ContainingNamespace;
            }

            return true;
        }

        public override int GetHashCode(TSymbol obj)
        {
            if (Default.Equals(obj, default(TSymbol)))
                return 0;

            int hashCode = Hash.Create(GetHashCode(obj.MetadataName));

            INamedTypeSymbol t = obj.ContainingType;

            if (t != null)
            {
                hashCode = Combine(t, hashCode);

                t = t.ContainingType;

                while (t != null)
                {
                    hashCode = Hash.Combine(_plusHashCode, hashCode);

                    hashCode = Combine(t, hashCode);

                    t = t.ContainingType;
                }
            }

            INamespaceSymbol n = obj.ContainingNamespace;

            if (n != null)
            {
                hashCode = Combine(n, hashCode);

                n = n.ContainingNamespace;

                while (n != null)
                {
                    hashCode = Hash.Combine(_dotHashCode, hashCode);

                    hashCode = Combine(n, hashCode);

                    n = n.ContainingNamespace;
                }
            }

            return hashCode;
        }

        private static int Combine(ISymbol symbol, int hashCode)
        {
            return Hash.Combine(GetHashCode(symbol.MetadataName), hashCode);
        }

        private static int GetHashCode(string s)
        {
            return StringComparer.Ordinal.GetHashCode(s);
        }
    }
}
