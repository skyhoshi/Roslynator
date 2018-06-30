// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var g = new DocGenerator();

            g.GenerateDocumentationFiles("Roslynator API", new string[] { "Roslynator.CSharp.dll" });

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
