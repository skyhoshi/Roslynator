// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.Documentation
{
    [Flags]
    public enum DocumentationParts
    {
        None = 0,
        Namespace = 1,
        Type = 2,
        Member = 4,
        ExtendedExternalTypes = 8,
        ObjectModel = 16,
        All = Namespace | Type | Member | ExtendedExternalTypes | ObjectModel,
    }
}
