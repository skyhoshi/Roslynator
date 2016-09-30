﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Pihrtsoft.CodeAnalysis.CSharp.Analyzers.Tests
{
    public static class AvoidInterpolatedStringWithNoInterpolation
    {
        public static void MethodName()
        {
            string s = $"";
            string s2 = $@"";
        }
    }
}
