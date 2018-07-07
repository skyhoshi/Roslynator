// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum RootDocumentationParts
    {
        None = 0,
        Heading = 1,
        NamespaceList = 2,
        Namespaces = 4
    }
}
