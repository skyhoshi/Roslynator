// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

#pragma warning disable CS0618, CS1591, RCS1079 

namespace Roslynator.Documentation.Test
{
    [Obsolete]
    public class FooDic<TKey, TValue> :
        IEnumerable<TValue>,
        IReadOnlyList<KeyValuePair<TKey, TValue>>
        where TKey : Foo
        where TValue : Foo
    {
        KeyValuePair<TKey, TValue> IReadOnlyList<KeyValuePair<TKey, TValue>>.this[int index] => throw new NotImplementedException();

        int IReadOnlyCollection<KeyValuePair<TKey, TValue>>.Count => throw new NotImplementedException();

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
