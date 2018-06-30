// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class SymbolDisplayFormatProvider
    {
        public static SymbolDisplayFormatProvider Default { get; } = new DefaultSymbolDisplayFormatProvider();

        public abstract SymbolDisplayFormat TitleFormat { get; }

        public abstract SymbolDisplayFormat NamespaceFormat { get; }

        private class DefaultSymbolDisplayFormatProvider : SymbolDisplayFormatProvider
        {
            public override SymbolDisplayFormat TitleFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat NamespaceFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces; }
            }
        }
    }
}
