// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

#region usings
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using Roslynator;
using Roslynator.CSharp;
using Roslynator.CSharp.Syntax;
using System.Collections.ObjectModel;
#endregion usings

#pragma warning disable RCS1018, RCS1213, CA1822

namespace Roslynator.Tests
{
    internal static class UseElementAccessInsteadOfElementAt
    {
        public static void Foo()
        {
            object x = 0;

            List<object> l = null;
            IList<object> il = null;
            IReadOnlyList<object> irl = null;
            Collection<object> c = null;
            ICollection<object> ic = null;
            IReadOnlyCollection<object> irc = null;
            IEnumerable<object> ie = null;
            object[] a = null;
            ImmutableArray<object> ia = ImmutableArray<object>.Empty;
            string s = null;
            var dic = new Dictionary<object, object>();

            x = l.ElementAt(1);

            x = il.ElementAt(1);

            x = irl.ElementAt(1);

            x = c.ElementAt(1);

            x = a.ElementAt(1);

            x = ia.ElementAt(1);

            x = s.ElementAt(1);

            //n

            x = ic.ElementAt(1);

            x = irc.ElementAt(1);

            x = ie.ElementAt(1);

            KeyValuePair<object, object> kvp = dic.ElementAt(1);

            x = l.ToList().ElementAt(1);

            x = a.ToArray().ElementAt(1);

            x = ia.ToImmutableArray().ElementAt(1);

            x = s.ToUpper().ElementAt(1);
        }
    }

    internal static class UseElementAccessInsteadOfFirst
    {
        public static void Foo()
        {
            object x = 0;

            List<object> l = null;
            IList<object> il = null;
            IReadOnlyList<object> irl = null;
            Collection<object> c = null;
            ICollection<object> ic = null;
            IReadOnlyCollection<object> irc = null;
            IEnumerable<object> ie = null;
            object[] a = null;
            ImmutableArray<object> ia = ImmutableArray<object>.Empty;
            string s = null;
            var dic = new Dictionary<object, object>();

            x = l.First();

            x = il.First();

            x = irl.First();

            x = c.First();

            x = a.First();

            x = ia.First();

            x = s.First();

            //n

            x = ic.First();

            x = irc.First();

            x = ie.First();

            KeyValuePair<object, object> kvp = dic.First();

            x = l.ToList().First();

            x = a.ToArray().First();

            x = ia.ToImmutableArray().First();

            x = s.ToUpper().First();
        }
    }
}
