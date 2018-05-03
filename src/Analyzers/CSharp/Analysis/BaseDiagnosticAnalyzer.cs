// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;

namespace Roslynator.CSharp.Analysis
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public abstract class BaseDiagnosticAnalyzer : DiagnosticAnalyzer
    {
        protected BaseDiagnosticAnalyzer()
        {
        }

        //TODO: make extension method
        internal IEnumerable<string> SupportedDiagnosticIds
        {
            get { return SupportedDiagnostics.Select(f => f.Id); }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"{{{string.Join(", ", SupportedDiagnosticIds)}}}"; }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        }
    }
}
