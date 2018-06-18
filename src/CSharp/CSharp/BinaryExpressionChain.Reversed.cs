// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Roslynator.CSharp
{
    internal partial struct BinaryExpressionChain
    {
        public class Reversed : IEquatable<Reversed>, IEnumerable<ExpressionSyntax>
        {
            private readonly BinaryExpressionChain _chain;

            internal Reversed(in BinaryExpressionChain chain)
            {
                _chain = chain;
            }

            public Enumerator GetEnumerator()
            {
                return new Enumerator(_chain);
            }

            IEnumerator<ExpressionSyntax> IEnumerable<ExpressionSyntax>.GetEnumerator()
            {
                if (_chain.BinaryExpression == null)
                    return Empty.Enumerator<ExpressionSyntax>();

                return new EnumeratorImpl(_chain);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                if (_chain.BinaryExpression == null)
                    return Empty.Enumerator<ExpressionSyntax>();

                return new EnumeratorImpl(_chain);
            }

            public override string ToString()
            {
                return _chain.ToString();
            }

            public override bool Equals(object obj)
            {
                return obj is Reversed other && Equals(other);
            }

            public bool Equals(Reversed other)
            {
                return _chain.Equals(other._chain);
            }

            public override int GetHashCode()
            {
                return _chain.GetHashCode();
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
            public struct Enumerator
            {
                private BinaryExpressionChain _chain;
                private ExpressionSyntax _last;
                private ExpressionSyntax _current;
                private State _state;

                internal Enumerator(in BinaryExpressionChain chain)
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
                    End = 3,
                }
            }

            private class EnumeratorImpl : IEnumerator<ExpressionSyntax>
            {
                private Enumerator _en;

                internal EnumeratorImpl(in BinaryExpressionChain chain)
                {
                    _en = new Enumerator(chain);
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
