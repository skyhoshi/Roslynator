// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp.SyntaxWalkers
{
    internal abstract class LocalOrParameterReferenceWalker : CSharpSyntaxNodeWalker
    {
        protected LocalOrParameterReferenceWalker()
        {
        }

        protected override void VisitType(TypeSyntax node)
        {
        }

        public override void VisitGenericName(GenericNameSyntax node)
        {
        }
    }
}
