﻿// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.Text
{
    /// <summary>
    /// Represents selected lines in a <see cref="TextLineCollection"/>.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class TextLineCollectionSelection : Selection<TextLine>
    {
        private TextLineCollectionSelection(TextLineCollection lines, TextSpan span, in SelectionResult result)
            : this(lines, span, result.FirstIndex, result.LastIndex)
        {
        }

        protected TextLineCollectionSelection(TextLineCollection lines, TextSpan span, int firstIndex, int lastIndex)
            : base(span, firstIndex, lastIndex)
        {
            UnderlyingLines = lines;
        }

        /// <summary>
        /// Gets an underlying collection that contains selected lines.
        /// </summary>
        public TextLineCollection UnderlyingLines { get; }

        /// <summary>
        /// Gets an underlying collection that contains selected lines.
        /// </summary>
        protected override IReadOnlyList<TextLine> Items => UnderlyingLines;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return $"Count = {Count} FirstIndex = {FirstIndex} LastIndex = {LastIndex}"; }
        }

        /// <summary>
        /// Creates a new <see cref="TextLineCollectionSelection"/> based on the specified list and span.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static TextLineCollectionSelection Create(TextLineCollection lines, TextSpan span)
        {
            if (lines == null)
                throw new ArgumentNullException(nameof(lines));

            SelectionResult result = SelectionResult.Create(lines, span);

            return new TextLineCollectionSelection(lines, span, result);
        }

        /// <summary>
        /// Creates a new <see cref="TextLineCollectionSelection"/> based on the specified list and span.
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="span"></param>
        /// <param name="selectedLines"></param>
        /// <returns>True if the specified span contains at least one line; otherwise, false.</returns>
        public static bool TryCreate(TextLineCollection lines, TextSpan span, out TextLineCollectionSelection selectedLines)
        {
            selectedLines = Create(lines, span, 1, int.MaxValue);
            return selectedLines != null;
        }

        internal static bool TryCreate(TextLineCollection lines, TextSpan span, int minCount, out TextLineCollectionSelection selectedLines)
        {
            selectedLines = Create(lines, span, minCount, int.MaxValue);
            return selectedLines != null;
        }

        internal static bool TryCreate(TextLineCollection lines, TextSpan span, int minCount, int maxCount, out TextLineCollectionSelection selectedLines)
        {
            selectedLines = Create(lines, span, minCount, maxCount);
            return selectedLines != null;
        }

        private static TextLineCollectionSelection Create(TextLineCollection lines, TextSpan span, int minCount, int maxCount)
        {
            if (lines == null)
                return null;

            SelectionResult result = SelectionResult.Create(lines, span, minCount, maxCount);

            if (!result.Success)
                return null;

            return new TextLineCollectionSelection(lines, span, result);
        }

        /// <summary>
        /// Returns an enumerator that iterates through selected items.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        protected override IEnumerator<TextLine> GetEnumeratorCore()
        {
            return new EnumeratorImpl(this);
        }

        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
        public struct Enumerator
        {
            private readonly TextLineCollectionSelection _selection;
            private int _index;

            internal Enumerator(TextLineCollectionSelection selection)
            {
                _selection = selection;
                _index = -1;
            }

            public bool MoveNext()
            {
                if (_index == -1)
                {
                    _index = _selection.FirstIndex;
                    return true;
                }
                else
                {
                    int newIndex = _index + 1;
                    if (newIndex <= _selection.LastIndex)
                    {
                        _index = newIndex;
                        return true;
                    }
                }

                return false;
            }

            public TextLine Current
            {
                get { return _selection.UnderlyingLines[_index]; }
            }

            public void Reset()
            {
                _index = -1;
            }

            public override bool Equals(object obj)
            {
                throw new NotSupportedException();
            }

            public override int GetHashCode()
            {
                throw new NotSupportedException();
            }
        }

        private class EnumeratorImpl : IEnumerator<TextLine>
        {
            private Enumerator _en;

            internal EnumeratorImpl(TextLineCollectionSelection selection)
            {
                _en = new Enumerator(selection);
            }

            public bool MoveNext()
            {
                return _en.MoveNext();
            }

            public TextLine Current
            {
                get { return _en.Current; }
            }

            object IEnumerator.Current
            {
                get { return _en.Current; }
            }

            void IEnumerator.Reset()
            {
                _en.Reset();
            }

            void IDisposable.Dispose()
            {
            }
        }
    }
}
