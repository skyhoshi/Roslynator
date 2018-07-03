// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Roslynator.Documentation
{
    /// <summary>
    /// a &#x2192; &gt; b <see cref="Foo"/>
    /// </summary>
    /// <remarks>
    /// bla <c>null</c>
    /// <code>
    /// string s = null;
    /// </code>
    /// bla2
    /// <para>
    /// paratext
    /// </para>
    /// <list type="bullet">
    /// <item>itemtext</item>
    /// </list>
    /// </remarks>
    public class Foo
    {
        /// <summary>
        /// s <paramref name="value"/> <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">p</param>
        /// <PARAM name="value2">p</PARAM>
        /// <permission cref="Foo">ppp</permission>
        public void Bar<T>(string value, string value2)
        {
            Bar<T>(value, value2);
        }
    }
}
