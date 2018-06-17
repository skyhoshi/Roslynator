// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp
{
    internal partial struct BinaryExpressionChain
    {
        public class Reversed : IEquatable<Reversed>, IEnumerable<ExpressionSyntax>
        {
            private readonly BinaryExpressionChain _binaryExpressionChain;

            internal Reversed(in BinaryExpressionChain binaryExpressionChain)
            {
                _binaryExpressionChain = binaryExpressionChain;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_binaryExpressionChain);
            }

            IEnumerator<ExpressionSyntax> IEnumerable<ExpressionSyntax>.GetEnumerator()
            {
                if (_binaryExpressionChain.BinaryExpression != null)
                    return new EnumeratorImpl(_binaryExpressionChain);

                return Empty.Enumerator<ExpressionSyntax>();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                if (_binaryExpressionChain.BinaryExpression != null)
                    return new EnumeratorImpl(_binaryExpressionChain);

                return Empty.Enumerator<ExpressionSyntax>();
            }

            public override string ToString()
            {
                return _binaryExpressionChain.ToString();
            }

            public override bool Equals(object obj)
            {
                return obj is Reversed other && Equals(other);
            }

            public bool Equals(Reversed other)
            {
                return _binaryExpressionChain.Equals(other._binaryExpressionChain);
            }

            public override int GetHashCode()
            {
                return _binaryExpressionChain.GetHashCode();
            }

            public static bool operator ==(in Reversed reversed1, in Reversed reversed2)
            {
                return reversed1.Equals(reversed2);
            }

            public static bool operator !=(in Reversed reversed1, in Reversed reversed2)
            {
                return !(reversed1 == reversed2);
            }

            [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
            [SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "<Pending>")]
            [SuppressMessage("Usage", "CA2231:Overload operator equals on overriding value type Equals", Justification = "<Pending>")]
            [SuppressMessage("Usage", "RCS1224:Use DebuggerDisplay attribute for publicly visible type.", Justification = "<Pending>")]
            public struct Enumerator
            {
                private BinaryExpressionChain _binaryExpressionChain;
                private ExpressionSyntax _current;
                private State _state;

                internal Enumerator(in BinaryExpressionChain binaryExpressionChain)
                {
                    _binaryExpressionChain = binaryExpressionChain;
                    _current = null;
                    _state = State.Start;
                }

                private BinaryExpressionSyntax BinaryExpression
                {
                    get { return _binaryExpressionChain.BinaryExpression; }
                }

                private TextSpan Span
                {
                    get { return _binaryExpressionChain.Span; }
                }

                public bool MoveNext()
                {
                    switch (_state)
                    {
                        case State.Start:
                            {
                                if (BinaryExpression == null)
                                    return false;

                                ExpressionSyntax right = BinaryExpression.Right;

                                Debug.Assert(right != null, "BinaryExpressionSyntax.Right is null.");

                                if (IsInSpan(right.Span))
                                {
                                    _current = right;
                                    _state = State.Right;
                                    return true;
                                }

                                BinaryExpressionSyntax binaryExpression = BinaryExpression;

                                while (true)
                                {
                                    ExpressionSyntax left = binaryExpression.Left;

                                    Debug.Assert(left != null, "BinaryExpressionSyntax.Left is null.");

                                    if (left.IsKind(binaryExpression.Kind()))
                                    {
                                        binaryExpression = (BinaryExpressionSyntax)left;
                                        right = binaryExpression.Right;

                                        Debug.Assert(right != null, "BinaryExpressionSyntax.Right is null.");

                                        if (IsInSpan(right.Span))
                                        {
                                            _current = right;
                                            _state = State.Right;
                                            return true;
                                        }
                                    }
                                    else
                                    {
                                        _state = State.Left;

                                        if (IsInSpan(left.Span))
                                        {
                                            _current = left;
                                            return true;
                                        }

                                        return false;
                                    }
                                }
                            }
                        case State.Right:
                            {
                                var binaryExpression = (BinaryExpressionSyntax)_current.Parent;

                                ExpressionSyntax left = binaryExpression.Left;

                                Debug.Assert(left != null, "BinaryExpressionSyntax.Left is null.");

                                if (left.IsKind(binaryExpression.Kind()))
                                {
                                    binaryExpression = (BinaryExpressionSyntax)left;

                                    ExpressionSyntax right = binaryExpression.Right;

                                    Debug.Assert(right != null, "BinaryExpressionSyntax.Right is null.");

                                    if (IsInSpan(right.Span))
                                    {
                                        _current = right;
                                        _state = State.Right;
                                        return true;
                                    }
                                }

                                _state = State.Left;

                                if (IsInSpan(left.Span))
                                {
                                    _current = left;
                                    return true;
                                }
                                else
                                {
                                    _current = null;
                                    return false;
                                }
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

                private bool IsInSpan(TextSpan span)
                {
                    return Span.OverlapsWith(span)
                        || (span.Length == 0 && Span.IntersectsWith(span));
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
                    Right = 2,
                }
            }

            private class EnumeratorImpl : IEnumerator<ExpressionSyntax>
            {
                private Enumerator _en;

                internal EnumeratorImpl(in BinaryExpressionChain binaryExpressionChain)
                {
                    _en = new Enumerator(binaryExpressionChain);
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
}
