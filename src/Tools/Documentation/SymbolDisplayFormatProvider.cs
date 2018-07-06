// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class SymbolDisplayFormatProvider
    {
        public static SymbolDisplayFormatProvider Default { get; } = new DefaultSymbolDisplayFormatProvider();

        public abstract SymbolDisplayFormat TitleFormat { get; }

        public abstract SymbolDisplayFormat MemberTitleFormat { get; }

        public abstract SymbolDisplayFormat NamespaceFormat { get; }

        public abstract SymbolDisplayFormat TypeFormat { get; }

        public abstract SymbolDisplayFormat SignatureFormat { get; }

        public abstract SymbolDisplayFormat TypeParameterFormat { get; }

        public abstract SymbolDisplayFormat ParameterFormat { get; }

        public abstract SymbolDisplayFormat ReturnValueFormat { get; }

        public abstract SymbolDisplayFormat InheritanceFormat { get; }

        public abstract SymbolDisplayFormat DerivedFormat { get; }

        public abstract SymbolDisplayFormat ImplementsFormat { get; }

        public abstract SymbolDisplayFormat MemberImplementsFormat { get; }

        public abstract SymbolDisplayFormat AttributeFormat { get; }

        public abstract SymbolDisplayFormat ConstructorFormat { get; }

        public abstract SymbolDisplayFormat FieldFormat { get; }

        public abstract SymbolDisplayFormat PropertyFormat { get; }

        public abstract SymbolDisplayFormat MethodFormat { get; }

        public abstract SymbolDisplayFormat EventFormat { get; }

        public abstract SymbolDisplayFormat CrefFormat { get; }

        private class DefaultSymbolDisplayFormatProvider : SymbolDisplayFormatProvider
        {
            public override SymbolDisplayFormat TitleFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat MemberTitleFormat
            {
                get { return SymbolDisplayFormats.MemberTitle; }
            }

            public override SymbolDisplayFormat NamespaceFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces; }
            }

            public override SymbolDisplayFormat TypeFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat SignatureFormat
            {
                get { return SymbolDisplayFormats.FullSignature; }
            }

            public override SymbolDisplayFormat TypeParameterFormat
            {
                get { return SymbolDisplayFormats.Default; }
            }

            public override SymbolDisplayFormat ParameterFormat
            {
                get { return SymbolDisplayFormats.Default; }
            }

            public override SymbolDisplayFormat ReturnValueFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat InheritanceFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat DerivedFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypesAndNamespaces; }
            }

            public override SymbolDisplayFormat ImplementsFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat MemberImplementsFormat
            {
                get { return SymbolDisplayFormats.MemberImplements; }
            }

            public override SymbolDisplayFormat AttributeFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }

            public override SymbolDisplayFormat ConstructorFormat
            {
                get { return SymbolDisplayFormats.SimpleSignature; }
            }

            public override SymbolDisplayFormat FieldFormat
            {
                get { return SymbolDisplayFormats.SimpleSignature; }
            }

            public override SymbolDisplayFormat PropertyFormat
            {
                get { return SymbolDisplayFormats.SimpleSignature; }
            }

            public override SymbolDisplayFormat MethodFormat
            {
                get { return SymbolDisplayFormats.SimpleSignature; }
            }

            public override SymbolDisplayFormat EventFormat
            {
                get { return SymbolDisplayFormats.SimpleSignature; }
            }

            public override SymbolDisplayFormat CrefFormat
            {
                get { return SymbolDisplayFormats.TypeNameAndContainingTypes; }
            }
        }
    }
}
