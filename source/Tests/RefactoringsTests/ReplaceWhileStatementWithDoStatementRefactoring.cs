﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Pihrtsoft.CodeAnalysis.CSharp.Refactorings.Tests
{
    internal class ReplaceWhileStatementWithDoStatementRefactoring
    {
        public void Do()
        {
            bool condition = false;

            while (condition)
            {

            }
        }
    }
}
