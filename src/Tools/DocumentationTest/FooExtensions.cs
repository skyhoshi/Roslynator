// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable CA1051, CA1822, RCS1101, RCS1163, RCS1175

namespace Roslynator.Documentation.Test
{
    /// <summary>
    /// fx
    /// </summary>
    public static class FooExtensions
    {
        /// <summary>
        /// x
        /// </summary>
        /// <param name="foo"></param>
        internal static void ExtensionMethod(this Foo foo)
        {
        }

        /// <summary>
        /// xt
        /// </summary>
        /// <typeparam name="T"T></typeparam>
        /// <param name="foo">f</param>
        internal static void ExtensionMethod<T>(this T foo) where T : Foo
        {
        }

        internal static void ExtensionMethod<T, T2>(this T foo) where T : T2 where T2 : Foo
        {
        }
    }
}
