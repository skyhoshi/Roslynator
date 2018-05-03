// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Roslynator.CSharp.Analysis.ReplaceEnumeratorWithForEach
{
    internal static class ReplaceEnumeratorWithForEachWalkerCache
    {
        [ThreadStatic]
        private static ReplaceEnumeratorWithForEachWalker _cachedInstance;

        public static ReplaceEnumeratorWithForEachWalker GetInstance()
        {
            ReplaceEnumeratorWithForEachWalker walker = _cachedInstance;

            if (walker != null)
            {
                _cachedInstance = null;
                walker.Clear();
                return walker;
            }

            return new ReplaceEnumeratorWithForEachWalker();
        }

        public static void Free(ReplaceEnumeratorWithForEachWalker walker)
        {
            walker.Clear();
            _cachedInstance = walker;
        }

        public static bool GetIsFixableAndFree(ReplaceEnumeratorWithForEachWalker walker)
        {
            bool? isFixable = walker.IsFixable;

            Free(walker);

            return isFixable == true;
        }
    }
}

