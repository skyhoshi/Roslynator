// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class DocumentationUtility
    {
        public static bool IsHiddenAttribute(INamedTypeSymbol attributeSymbol)
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
                case "FlagsAttribute":
                case "AttributeUsageAttribute":
                case "ObsoleteAttribute":
                    return false;
#endif
            }

            Debug.Fail(attributeSymbol.ToDisplayString());
            return false;
        }
    }
}
