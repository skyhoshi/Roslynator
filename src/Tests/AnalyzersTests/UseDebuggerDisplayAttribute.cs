// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#pragma warning disable CA1034, CA1052, RCS1016, RCS1024, RCS1101, RCS1138

using System;
using System.Diagnostics;

namespace Roslynator.CSharp.Analyzers.Tests
{
    public static class UseDebuggerDisplayAttribute
    {
        /// <summary></summary>
        public class Foo
        {
            [Obsolete]
            public class Foo2
            {
            }

            /// <summary></summary>
            [Obsolete]
            protected internal class FooProtectedInternal
            {
            }

            protected class FooProtected
            {
            }
        }

        protected internal class FooProtectedInternal
        {
            public class Foo
            {
            }

            protected internal class FooProtectedInternal2
            {
            }

            protected class FooProtected
            {
            }
        }

        protected class FooProtected
        {
            public class Foo
            {
            }

            protected internal class FooProtectedInternal
            {
            }

            protected class FooProtected2
            {
            }
        }

        /// <summary></summary>
        public struct FooStruct
        {
        }

        [Obsolete]
        public struct FooStruct2
        {
        }

        /// <summary></summary>
        [Obsolete]
        public struct FooStruct3
        {
        }

        //n

        public static class FooStatic
        {
        }

        public class Foo3 : Foo2
        {
        }

        [DebuggerDisplay("")]
        public class Foo2
        {
        }

        [DebuggerDisplay("")]
        protected internal class FooProtectedInternal2
        {
        }

        [DebuggerDisplay("")]
        protected class FooProtected2
        {
        }

        internal class FooInternal
        {
            public class Foo
            {
            }

            protected internal class FooProtectedInternal
            {
            }

            protected class FooProtected
            {
            }

            internal class FooInternal2
            {
            }

            private class FooPrivate
            {
            }
        }

        private class FooPrivate
        {
            public class Foo
            {
            }

            protected internal class FooProtectedInternal
            {
            }

            protected class FooProtected
            {
            }

            internal class FooInternal
            {
            }

            private class FooPrivate2
            {
            }
        }

        public interface IFoo
        {
        }
    }
}
