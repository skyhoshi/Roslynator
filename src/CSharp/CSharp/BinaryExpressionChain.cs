// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp
{
    //TODO: make public
    /// <summary>
    /// Enables to enumerate nested binary expressions of the same kind.
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    internal readonly struct BinaryExpressionChain : IEquatable<BinaryExpressionChain>, IEnumerable<ExpressionSyntax>
    {
        internal BinaryExpressionChain(BinaryExpressionSyntax binaryExpression)
        {
            BinaryExpression = binaryExpression;
        }

        /// <summary>
        /// The binary expression.
        /// </summary>
        public BinaryExpressionSyntax BinaryExpression { get; }

        private int Count
        {
            get
            {
                int count = 0;
                foreach (ExpressionSyntax expression in this)
                    count++;

                return count;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebuggerDisplay
        {
            get { return (BinaryExpression != null) ? $"Count = {Count} {BinaryExpression}" : "Uninitialized"; }
        }

        /// <summary>
        /// Gets the enumerator for the binary expression.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator()
        {
            return new Enumerator(BinaryExpression);
        }

        IEnumerator<ExpressionSyntax> IEnumerable<ExpressionSyntax>.GetEnumerator()
        {
            if (BinaryExpression != null)
                return new EnumeratorImpl(BinaryExpression);

            return Empty.Enumerator<ExpressionSyntax>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (BinaryExpression != null)
                return new EnumeratorImpl(BinaryExpression);

            return Empty.Enumerator<ExpressionSyntax>();
        }

        /// <summary>
        /// Returns the string representation of the underlying syntax, not including its leading and trailing trivia.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return BinaryExpression?.ToString() ?? "";
        }

        /// <summary>
        /// Determines whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance. </param>
        /// <returns>true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false. </returns>
        public override bool Equals(object obj)
        {
            return obj is BinaryExpressionChain other && Equals(other);
        }

        /// <summary>
        /// Determines whether this instance is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.</returns>
        public bool Equals(BinaryExpressionChain other)
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

        public static bool operator ==(in BinaryExpressionChain info1, in BinaryExpressionChain info2)
        {
            return info1.Equals(info2);
        }

        public static bool operator !=(in BinaryExpressionChain info1, in BinaryExpressionChain info2)
        {
            return !(info1 == info2);
        }

        [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
        [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
        [SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
        [SuppressMessage("Usage", "RCS1224:Use DebuggerDisplay attribute for publicly visible type.", Justification = "<Pending>")]
        public struct Enumerator
        {
            private readonly BinaryExpressionSyntax _binaryExpression;
            private ExpressionSyntax _current;
            private State _state;

            internal Enumerator(BinaryExpressionSyntax binaryExpression)
            {
                _binaryExpression = binaryExpression;
                _current = null;
                _state = State.Start;
            }

            public bool MoveNext()
            {
                switch (_state)
                {
                    case State.Start:
                        {
                            if (_binaryExpression == null)
                                return false;

                            _current = _binaryExpression.Right;

                            Debug.Assert(_current != null, "BinaryExpressionSyntax.Right is null.");

                            _state = State.Right;
                            return true;
                        }
                    case State.Right:
                        {
                            var binaryExpression = (BinaryExpressionSyntax)_current.Parent;

                            ExpressionSyntax left = binaryExpression.Left;

                            if (left.IsKind(binaryExpression.Kind()))
                            {
                                binaryExpression = (BinaryExpressionSyntax)left;
                                _current = binaryExpression.Right;
                                _state = State.Right;

                                Debug.Assert(_current != null, "BinaryExpressionSyntax.Right is null.");
                            }
                            else
                            {
                                _current = left;
                                _state = State.Left;

                                Debug.Assert(_current != null, "BinaryExpressionSyntax.Left is null.");
                            }

                            return true;
                        }
                    case State.Left:
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
                _state = State.Start;
            }

            public override bool Equals(object obj)
            {
                throw new NotSupportedException();
            }

            public override int GetHashCode()
            {
                throw new NotSupportedException();
            }

            private enum State
            {
                Start = 0,
                Left = 1,
                Right = 2
            }
        }

        private class EnumeratorImpl : IEnumerator<ExpressionSyntax>
        {
            private Enumerator _en;

            internal EnumeratorImpl(BinaryExpressionSyntax binaryExpression)
            {
                _en = new Enumerator(binaryExpression);
            }

            public ExpressionSyntax Current
            {
                get { return _en.Current; }
            }

            object IEnumerator.Current
            {
                get { return _en.Current; }
            }

            public bool MoveNext()
            {
                return _en.MoveNext();
            }

            public void Reset()
            {
                _en.Reset();
            }

            public void Dispose()
            {
            }
        }
    }
}
