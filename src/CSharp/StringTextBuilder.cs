// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslynator.CSharp.Syntax;

namespace Roslynator.CSharp
{
    internal class StringTextBuilder
    {
        public StringTextBuilder(StringBuilder sb = null, bool isVerbatim = false, bool isInterpolated = false)
        {
            StringBuilder = sb ?? new StringBuilder();
            IsVerbatim = isVerbatim;
            IsInterpolated = isInterpolated;
        }

        public StringBuilder StringBuilder { get; }

        public bool IsVerbatim { get; }

        public bool IsInterpolated { get; }

        public void Append(InterpolatedStringExpressionSyntax interpolatedString)
        {
            if (interpolatedString == null)
                return;

            if (!IsInterpolated)
                throw new ArgumentException("", nameof(interpolatedString));

            bool isVerbatim = interpolatedString.IsVerbatim();

            foreach (InterpolatedStringContentSyntax content in interpolatedString.Contents)
            {
                switch (content.Kind())
                {
                    case SyntaxKind.Interpolation:
                        {
                            StringBuilder.Append(content.ToFullString());
                            break;
                        }
                    case SyntaxKind.InterpolatedStringText:
                        {
                            var interpolatedText = (InterpolatedStringTextSyntax)content;

                            if (IsVerbatim == isVerbatim)
                            {
                                StringBuilder.Append(interpolatedText.TextToken.Text);
                            }
                            else
                            {
                                Append(interpolatedText.TextToken.ValueText, isVerbatim);
                            }

                            break;
                        }
                }
            }
        }

        public void Append(LiteralExpressionSyntax stringLiteral)
        {
            if (stringLiteral == null)
                return;

            if (!stringLiteral.IsKind(SyntaxKind.StringLiteralExpression))
                throw new ArgumentException("", nameof(stringLiteral));

            StringLiteralExpressionInfo literalInfo = SyntaxInfo.StringLiteralExpressionInfo(stringLiteral);
            bool isVerbatim = literalInfo.IsVerbatim;

            if (IsVerbatim == isVerbatim)
            {
                string text = literalInfo.Text;

                int length = text.Length;

                if (length == 0)
                    return;

                if (isVerbatim)
                {
                    StringBuilder.Append(text, 2, length - 3);
                }
                else
                {
                    StringBuilder.Append(text, 1, length - 2);
                }
            }
            else
            {
                Append(literalInfo.ValueText, isVerbatim);
            }
        }

        private void Append(string value, bool isVerbatim)
        {
            if (string.IsNullOrEmpty(value))
                return;

            StringBuilder sb = StringBuilder;

            for (int i = 0; i < value.Length; i++)
            {
                if (IsSpecialChar(value[i]))
                {
                    sb.Append(value, 0, i);
                    AppendSpecialChar(value[i]);

                    i++;
                    int lastIndex = i;

                    while (i < value.Length)
                    {
                        if (IsSpecialChar(value[i]))
                        {
                            sb.Append(value, lastIndex, i - lastIndex);
                            AppendSpecialChar(value[i]);

                            i++;
                            lastIndex = i;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    sb.Append(value, lastIndex, value.Length - lastIndex);
                }
            }

            bool IsSpecialChar(char ch)
            {
                switch (ch)
                {
                    case '\"':
                        {
                            return true;
                        }
                    case '\\':
                        {
                            return !IsVerbatim;
                        }
                    case '{':
                    case '}':
                        {
                            return IsInterpolated;
                        }
                    case '\r':
                    case '\n':
                        {
                            return IsVerbatim && !isVerbatim;
                        }
                    default:
                        {
                            return false;
                        }
                }
            }

            void AppendSpecialChar(char ch)
            {
                switch (ch)
                {
                    case '\"':
                        {
                            if (IsVerbatim)
                            {
                                StringBuilder.Append("\"\"");
                            }
                            else
                            {
                                StringBuilder.Append("\\\"");
                            }

                            break;
                        }
                    case '\\':
                        {
                            Debug.Assert(!IsVerbatim);

                            StringBuilder.Append(@"\\");
                            break;
                        }
                    case '{':
                        {
                            StringBuilder.Append("{{");
                            break;
                        }
                    case '}':
                        {
                            StringBuilder.Append("}}");
                            break;
                        }
                    case '\r':
                        {
                            StringBuilder.Append(@"\r");
                            break;
                        }
                    case '\n':
                        {
                            StringBuilder.Append(@"\n");
                            break;
                        }
                    default:
                        {
                            Debug.Fail(ch.ToString());
                            break;
                        }
                }
            }
        }

        public void AppendStart()
        {
            if (IsInterpolated)
                StringBuilder.Append('$');

            if (IsVerbatim)
                StringBuilder.Append('@');

            StringBuilder.Append("\"");
        }

        public void AppendEnd()
        {
            StringBuilder.Append("\"");
        }

        public override string ToString()
        {
            return StringBuilder.ToString();
        }
    }
}
