// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace Roslynator
{
    internal static class RuntimeMetadataReference
    {
        private static MetadataReference _corLibReference;
        private static ImmutableDictionary<string, string> _trustedPlatformAssemblyPaths;

        public static MetadataReference CorLibReference
        {
            get { return _corLibReference ?? (_corLibReference = MetadataReference.CreateFromFile(typeof(object).Assembly.Location)); }
        }

        public static ImmutableDictionary<string, string> TrustedPlatformAssemblyPaths
        {
            get { return _trustedPlatformAssemblyPaths ?? (_trustedPlatformAssemblyPaths = CreateTrustedPlatformAssemblies()); }
        }

        private static ImmutableDictionary<string, string> CreateTrustedPlatformAssemblies()
        {
            return AppContext
                .GetData("TRUSTED_PLATFORM_ASSEMBLIES")
                .ToString()
                .Split(';')
                .ToImmutableDictionary(Path.GetFileName);
        }

        public static PortableExecutableReference CreateFromAssemblyName(string assemblyName)
        {
            string path = TrustedPlatformAssemblyPaths[assemblyName];

            return MetadataReference.CreateFromFile(path);
        }
    }
}
