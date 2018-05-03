// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using Roslynator.Tests.CSharp;

namespace Roslynator.CSharp.CodeFixes.Tests
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class AbstractCSharpCompilerCodeFixVerifier : CSharpCompilerCodeFixVerifier
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{DiagnosticId} {FixProvider.GetType().Name}"; }
        }
    }
}
