// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp
{
    /// <summary>
    /// Enables to enumerate expressions of a binary expression and expressions of nested binary expressions of the same kind as parent binary expression.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public readonly partial struct ExpressionChain : IEquatable<ExpressionChain>, IEnumerable<ExpressionSyntax>
    {
        internal ExpressionChain(BinaryExpressionSyntax binaryExpression)
        {
            BinaryExpression = binaryExpression;
            OriginalSpan = binaryExpression?.FullSpan ?? default;
        }

        internal ExpressionChain(BinaryExpressionSyntax binaryExpression, TextSpan span)
        {
            BinaryExpression = binaryExpression;
            OriginalSpan = span;
        }

        /// <summary>
        /// The binary expression.
        /// </summary>
        public BinaryExpressionSyntax BinaryExpression { get; }

        /// <summary>
        /// The span that was passed to the constructor or a full span of the binary expression.
        /// </summary>
        internal TextSpan OriginalSpan { get; }

        /// <summary>
        /// The absolute span of expressions in characters, not including its leading and trailing trivia.
        /// </summary>
        public TextSpan Span
        {
            get
            {
                Enumerator en = GetEnumerator();

                if (en.MoveNext())
                {
                    int end = en.Current.Span.End;

                    int start = en.Current.SpanStart;

                    while (en.MoveNext())
                        start = en.Current.SpanStart;

                    return TextSpan.FromBounds(start, end);
                }

                return default;
            }
        }

        /// <summary>
        /// The absolute span of expressions in characters, including its leading and trailing trivia.
        /// </summary>
        public TextSpan FullSpan
        {
            get
            {
                Enumerator en = GetEnumerator();

                if (en.MoveNext())
                {
                    int end = en.Current.FullSpan.End;

                    int start = en.Current.FullSpan.Start;

                    while (en.MoveNext())
                        start = en.Current.FullSpan.Start;

                    return TextSpan.FromBounds(start, end);
                }

                return default;
            }
        }

        internal int Count
        {
            get
            {
                int count = 0;

                Enumerator en = GetEnumerator();

                while (en.MoveNext())
                    count++;

                return count;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return BinaryExpression?.ToString(Span) ?? "Uninitialized"; }
        }

        /// <summary>
        /// Returns a chain which contains all expressions of <see cref="ExpressionChain"/> in reversed order
        /// </summary>
        /// <returns></returns>
        public Reversed Reverse()
        {
            return new Reversed(this);
        }

        /// <summary>
        /// Returns true if this chain contains any expression.
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return BinaryExpression != null && GetEnumerator().MoveNext();
        }

        internal ExpressionSyntax First()
        {
            Enumerator en = GetEnumerator();

            return (en.MoveNext()) ? en.Current : throw new InvalidOperationException();
        }

        /// <summary>
        /// Gets the enumerator for the expressions.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<ExpressionSyntax> IEnumerable<ExpressionSyntax>.GetEnumerator()
        {
            if (BinaryExpression != null)
                return new EnumeratorImpl(this);

            return Empty.Enumerator<ExpressionSyntax>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (BinaryExpression != null)
                return new EnumeratorImpl(this);

            return Empty.Enumerator<ExpressionSyntax>();
        }

        /// <summary>
        /// Returns the string representation of the expressions, not including its leading and trailing trivia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return BinaryExpression?.ToString(Span) ?? "";
        }

        /// <summary>
        /// Determines whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
        public override bool Equals(object obj)
        {
            return obj is ExpressionChain other && Equals(other);
        }

        /// <summary>
        /// Determines whether this instance is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(ExpressionChain other)
        {
            return EqualityComparer<BinaryExpressionSyntax>.Default.Equals(BinaryExpression, other.BinaryExpression);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return EqualityComparer<BinaryExpressionSyntax>.Default.GetHashCode(BinaryExpression);
        }

        public static bool operator ==(in ExpressionChain info1, in ExpressionChain info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(in ExpressionChain info1, in ExpressionChain info2)
        {
            return !(info1 == info2);
        }

        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
        public struct Enumerator
        {
            private ExpressionChain _chain;
            private ExpressionSyntax _last;
            private ExpressionSyntax _current;
            private State _state;

            internal Enumerator(in ExpressionChain chain)
            {
                _chain = chain;
                _last = null;
                _current = null;
                _state = State.Start;
            }

            private TextSpan Span
            {
                get { return _chain.OriginalSpan; }
            }

            public bool MoveNext()
            {
                switch (_state)
                {
                    case State.Start:
                        {
                            if (_chain.BinaryExpression == null)
                                return false;

                            BinaryExpressionSyntax binaryExpression = _chain.BinaryExpression;
                            ExpressionSyntax left = null;
                            ExpressionSyntax right = binaryExpression.Right;

                            if (IsInSpan(right.Span))
                            {
                                _last = right;
                            }
                            else
                            {
                                while (true)
                                {
                                    left = binaryExpression.Left;

                                    if (left.RawKind == binaryExpression.RawKind)
                                    {
                                        binaryExpression = (BinaryExpressionSyntax)left;
                                        right = binaryExpression.Right;

                                        if (IsInSpan(right.Span))
                                        {
                                            _last = right;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (IsInSpan(left.Span))
                                        {
                                            _last = left;
                                            _current = left;
                                            _state = State.Left;
                                            return true;
                                        }

                                        return false;
                                    }
                                }
                            }

                            ExpressionSyntax first = _last;
                            var state = State.Right;

                            while (true)
                            {
                                left = binaryExpression.Left;

                                if (left.RawKind == binaryExpression.RawKind)
                                {
                                    binaryExpression = (BinaryExpressionSyntax)left;
                                    right = binaryExpression.Right;

                                    if (IsInSpan(right.Span))
                                    {
                                        first = right;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    if (IsInSpan(left.Span))
                                    {
                                        first = left;
                                        state = State.Left;
                                    }

                                    break;
                                }
                            }

                            _current = first;
                            _state = state;
                            return true;
                        }
                    case State.Right:
                        {
                            if (_current == _last)
                            {
                                _current = null;
                                _last = null;
                                _state = State.End;
                                return false;
                            }

                            _current = ((BinaryExpressionSyntax)_current.Parent.Parent).Right;
                            return true;
                        }
                    case State.Left:
                        {
                            if (_current == _last)
                            {
                                _current = null;
                                _last = null;
                                _state = State.End;
                                return false;
                            }

                            _current = ((BinaryExpressionSyntax)_current.Parent).Right;
                            _state = State.Right;
                            return true;
                        }
                    case State.End:
                        {
                            return false;
                        }
                    default:
                        {
                            throw new InvalidOperationException();
                        }
                }
            }

            public ExpressionSyntax Current
            {
                get { return _current ?? throw new InvalidOperationException(); }
            }

            public void Reset()
            {
                _current = null;
                _last = null;
                _state = State.Start;
            }

            private bool IsInSpan(TextSpan span)
            {
                return Span.OverlapsWith(span)
                    || (span.Length == 0 && Span.IntersectsWith(span));
            }

            public override bool Equals(object obj) => throw new NotSupportedException();

            public override int GetHashCode() => throw new NotSupportedException();

            private enum State
            {
                Start = 0,
                Left = 1,
                Right = 2,
                End = 3,
            }
        }

        private class EnumeratorImpl : IEnumerator<ExpressionSyntax>
        {
            private Enumerator _en;

            internal EnumeratorImpl(in ExpressionChain chain)
            {
                _en = new Enumerator(chain);
            }

            public ExpressionSyntax Current => _en.Current;

            object IEnumerator.Current => _en.Current;

            public bool MoveNext() => _en.MoveNext();

            public void Reset() => _en.Reset();

            public void Dispose()
            {
            }
        }
    }
}
