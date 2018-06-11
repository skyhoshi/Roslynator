// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.CSharp
{
    internal class DefaultValueFormat
    {
        public DefaultValueFormat(
            ReferenceTypeDefaultValueOptions referenceTypeOptions,
            NullableTypeDefaultValueOptions nullableTypeOptions,
            ValueTypeDefaultValueOptions valueTypeOptions,
            EnumDefaultValueOptions enumOptions,
            BooleanDefaultValueOptions booleanOptions,
            CharDefaultValueOptions charOptions,
            NumericDefaultValueOptions numericOptions)
        {
            ReferenceTypeOptions = referenceTypeOptions;
            NullableTypeOptions = nullableTypeOptions;
            ValueTypeOptions = valueTypeOptions;
            EnumOptions = enumOptions;
            BooleanOptions = booleanOptions;
            CharOptions = charOptions;
            NumericOptions = numericOptions;
        }

        public ReferenceTypeDefaultValueOptions ReferenceTypeOptions { get; }
        public NullableTypeDefaultValueOptions NullableTypeOptions { get; }
        public ValueTypeDefaultValueOptions ValueTypeOptions { get; }
        public EnumDefaultValueOptions EnumOptions { get; }
        public BooleanDefaultValueOptions BooleanOptions { get; }
        public CharDefaultValueOptions CharOptions { get; }
        public NumericDefaultValueOptions NumericOptions { get; }
    }
}
