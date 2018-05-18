// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;

namespace Roslynator
{
    internal static class FullyQualifiedMetadataNames
    {
        public static FullyQualifiedMetadataName System_Collections_Generic { get; } = new FullyQualifiedMetadataName(null, Namespaces.System_Collections_Generic);
        public static FullyQualifiedMetadataName System_Collections_Generic_List_1 { get; } = new FullyQualifiedMetadataName("List`1", Namespaces.System_Collections_Generic);
        public static FullyQualifiedMetadataName System_Collections_Immutable_ImmutableArray_1 { get; } = new FullyQualifiedMetadataName("ImmutableArray`1", Namespaces.System_Collections_Immutable);
        public static FullyQualifiedMetadataName System_Func_2 { get; } = new FullyQualifiedMetadataName("Func`2", Namespaces.System);
        public static FullyQualifiedMetadataName System_Func_3 { get; } = new FullyQualifiedMetadataName("Func`3", Namespaces.System);
        public static FullyQualifiedMetadataName System_Linq_Enumerable { get; } = new FullyQualifiedMetadataName("Enumerable", Namespaces.System_Linq);
        public static FullyQualifiedMetadataName System_Linq_ImmutableArrayExtensions { get; } = new FullyQualifiedMetadataName("ImmutableArrayExtensions", Namespaces.System_Linq);

        private static class Namespaces
        {
            public static readonly ImmutableArray<string> System = ImmutableArray.Create("System");
            public static readonly ImmutableArray<string> System_Linq = ImmutableArray.Create("System", "Linq");
            public static readonly ImmutableArray<string> System_Collections_Generic = ImmutableArray.Create("System", "Collections", "Generic");
            public static readonly ImmutableArray<string> System_Collections_Immutable = ImmutableArray.Create("System", "Collections", "Immutable");
        }
    }
}
