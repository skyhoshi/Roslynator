﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Pihrtsoft.CodeAnalysis.CSharp.Refactorings.Tests
{
    internal class AddBracesRefactoring
    {
        public void Do()
        {
            string value = null;

            if (value == null)
                value = Initialize();
        }

        private string Initialize()
        {
            return null;
        }
    }
}
