﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable CA2217, RCS1157

namespace Roslynator.Documentation
{
    [Flags]
    public enum NamespaceDocumentationParts
    {
        None = 0,
        Heading = 1,
        Classes = 2,
        Structs = 4,
        Interfaces = 8,
        Enums = 16,
        Delegates = 32,
        All = int.MaxValue
    }
}
