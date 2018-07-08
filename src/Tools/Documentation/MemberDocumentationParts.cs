// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

#pragma warning disable CA2217, RCS1157

namespace Roslynator.Documentation
{
    //TODO: Security
    [Flags]
    public enum MemberDocumentationParts
    {
        None = 0,
        Overloads = 1,
        Title = 2,
        Namespace = 4,
        Assembly = 8,
        Obsolete = 16,
        Summary = 32,
        Signature = 64,
        TypeParameters = 128,
        Parameters = 256,
        ReturnValue = 512,
        Implements = 1024,
        Attributes = 2048,
        Exceptions = 4096,
        Examples = 8192,
        Remarks = 16384,
        SeeAlso = 32768,
        All = int.MaxValue
    }
}
