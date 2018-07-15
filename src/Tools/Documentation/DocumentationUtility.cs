// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class DocumentationUtility
    {
        public static bool ShouldBeExcluded(INamedTypeSymbol attributeSymbol)
        {
            switch (attributeSymbol.MetadataName)
            {
                case "ConditionalAttribute":
                case "DebuggerBrowsableAttribute":
                case "DebuggerDisplayAttribute":
                case "DebuggerHiddenAttribute":
                case "DebuggerNonUserCodeAttribute":
                case "DebuggerStepperBoundaryAttribute":
                case "DebuggerStepThroughAttribute":
                case "DebuggerTypeProxyAttribute":
                case "DebuggerVisualizerAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics);
                case "SuppressMessageAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Diagnostics_CodeAnalysis);
                case "DefaultMemberAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Reflection);
                case "AsyncStateMachineAttribute":
                case "IteratorStateMachineAttribute":
                case "MethodImplAttribute":
                case "TypeForwardedFromAttribute":
                case "TypeForwardedToAttribute":
                    return attributeSymbol.ContainingNamespace.HasMetadataName(MetadataNames.System_Runtime_CompilerServices);
#if DEBUG
                case "CLSCompliantAttribute":
                case "FlagsAttribute":
                case "AttributeUsageAttribute":
                case "ObsoleteAttribute":
                    return false;
#endif
            }

            Debug.Fail(attributeSymbol.ToDisplayString());
            return false;
        }

        public static ImmutableArray<INamedTypeSymbol> SortInterfaces(
            ImmutableArray<INamedTypeSymbol> interfaces,
            SymbolDisplayFormat format,
            SymbolDisplayAdditionalOptions additionalOptions = SymbolDisplayAdditionalOptions.None)
        {
            return interfaces.Sort((x, y) =>
            {
                if (x.InheritsFrom(y.OriginalDefinition, includeInterfaces: true))
                    return -1;

                if (y.InheritsFrom(x.OriginalDefinition, includeInterfaces: true))
                    return 1;

                if (interfaces.Any(f => x.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                {
                    if (!interfaces.Any(f => y.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                        return -1;
                }
                else if (interfaces.Any(f => y.InheritsFrom(f.OriginalDefinition, includeInterfaces: true)))
                {
                    return 1;
                }

                return string.Compare(x.ToDisplayString(format, additionalOptions), y.ToDisplayString(format, additionalOptions), StringComparison.Ordinal);
            });
        }
    }
}
