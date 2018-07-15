// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.IO;

namespace Roslynator
{
    internal static class TrustedPlatformAssemblies
    {
        private static ImmutableDictionary<string, string> _trustedPlatformAssemblyPaths;

        public static ImmutableDictionary<string, string> Paths
        {
            get { return _trustedPlatformAssemblyPaths ?? (_trustedPlatformAssemblyPaths = CreateTrustedPlatformAssemblyPaths()); }
        }

        private static ImmutableDictionary<string, string> CreateTrustedPlatformAssemblyPaths()
        {
            return AppContext
                .GetData("TRUSTED_PLATFORM_ASSEMBLIES")
                .ToString()
                .Split(';')
                .ToImmutableDictionary(Path.GetFileName, StringComparer.OrdinalIgnoreCase);
        }
    }
}
