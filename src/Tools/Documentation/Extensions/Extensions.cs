// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class Extensions
    {
        public static bool IsPunctuation(this SymbolDisplayPart part)
        {
            return part.Kind == SymbolDisplayPartKind.Punctuation;
        }

        public static bool IsSpace(this SymbolDisplayPart part)
        {
            return part.Kind == SymbolDisplayPartKind.Space;
        }

        public static bool IsKeyword(this SymbolDisplayPart part, string text)
        {
            return part.Kind == SymbolDisplayPartKind.Keyword
                && part.ToString() == text;
        }

        internal static bool IsName(this SymbolDisplayPart part)
        {
            switch (part.Kind)
            {
                case SymbolDisplayPartKind.AliasName:
                case SymbolDisplayPartKind.AssemblyName:
                case SymbolDisplayPartKind.ClassName:
                case SymbolDisplayPartKind.DelegateName:
                case SymbolDisplayPartKind.EnumName:
                case SymbolDisplayPartKind.ErrorTypeName:
                case SymbolDisplayPartKind.EventName:
                case SymbolDisplayPartKind.FieldName:
                case SymbolDisplayPartKind.InterfaceName:
                case SymbolDisplayPartKind.LabelName:
                case SymbolDisplayPartKind.LocalName:
                case SymbolDisplayPartKind.MethodName:
                case SymbolDisplayPartKind.ModuleName:
                case SymbolDisplayPartKind.NamespaceName:
                case SymbolDisplayPartKind.ParameterName:
                case SymbolDisplayPartKind.PropertyName:
                case SymbolDisplayPartKind.StructName:
                case SymbolDisplayPartKind.TypeParameterName:
                case SymbolDisplayPartKind.RangeVariableName:
                    return true;
                default:
                    return false;
            }
        }

        public static SymbolDisplayPart WithText(this SymbolDisplayPart part, string text)
        {
            return new SymbolDisplayPart(part.Kind, part.Symbol, text);
        }

        public static string ToDisplayString(this ISymbol symbol, SymbolDisplayFormat format, SymbolDisplayAdditionalOptions additionalOptions)
        {
            return symbol.ToDisplayParts(format, additionalOptions).ToDisplayString();
        }

        public static ImmutableArray<SymbolDisplayPart> ToDisplayParts(this ISymbol symbol, SymbolDisplayFormat format, SymbolDisplayAdditionalOptions additionalOptions)
        {
            if (additionalOptions == SymbolDisplayAdditionalOptions.None)
                return symbol.ToDisplayParts(format);

            ImmutableArray<SymbolDisplayPart> parts = symbol.ToDisplayParts(format);
            int length = parts.Length;

            for (int i = 0; i < length; i++)
            {
                SymbolDisplayPart part = parts[i];

                switch (part.Kind)
                {
                    case SymbolDisplayPartKind.Keyword:
                        {
                            switch (part.ToString())
                            {
                                case "this":
                                    {
                                        if ((additionalOptions & SymbolDisplayAdditionalOptions.UseItemProperty) != 0
                                            && (symbol as IPropertySymbol)?.IsIndexer == true)
                                        {
                                            parts = parts.Replace(part, new SymbolDisplayPart(SymbolDisplayPartKind.PropertyName, part.Symbol, "Item"));
                                        }

                                        break;
                                    }
                                case "operator":
                                    {
                                        if ((additionalOptions & SymbolDisplayAdditionalOptions.UseOperatorName) != 0
                                            && symbol is IMethodSymbol methodSymbol
                                            && methodSymbol.MethodKind == MethodKind.UserDefinedOperator)
                                        {
                                            string name = methodSymbol.Name;

                                            Debug.Assert(name.StartsWith("op_", StringComparison.Ordinal), name);

                                            if (name.StartsWith("op_", StringComparison.Ordinal)
                                                && i < length - 2
                                                && parts[i + 1].IsSpace()
                                                && parts[i + 2].Kind == SymbolDisplayPartKind.MethodName)
                                            {
                                                parts = parts.Replace(parts[i + 2], new SymbolDisplayPart(SymbolDisplayPartKind.MethodName, parts[i + 2].Symbol, name.Substring(3)));
                                                parts = parts.RemoveRange(i, 2);
                                                length -= 2;
                                            }
                                        }

                                        break;
                                    }
                                case "implicit":
                                case "explicit":
                                    {
                                        if ((additionalOptions & SymbolDisplayAdditionalOptions.UseOperatorName) != 0
                                            && symbol is IMethodSymbol methodSymbol
                                            && methodSymbol.MethodKind == MethodKind.Conversion)
                                        {
                                            string name = methodSymbol.Name;

                                            Debug.Assert(name.StartsWith("op_", StringComparison.Ordinal), name);

                                            if (name.StartsWith("op_", StringComparison.Ordinal)
                                                && i < length - 2
                                                && parts[i + 1].IsSpace()
                                                && parts[i + 2].IsKeyword("operator"))
                                            {
                                                List<SymbolDisplayPart> list = parts.ToList();

                                                list[i + 2] = new SymbolDisplayPart(SymbolDisplayPartKind.MethodName, list[i + 4].Symbol, name.Substring(3));
                                                list.RemoveRange(i, 2);
                                                length -= 2;

                                                if (i == length - 3
                                                    && list[i + 1].IsSpace()
                                                    && list[i + 2].IsName())
                                                {
                                                    list.RemoveRange(i + 1, 2);
                                                    length -= 2;
                                                }
                                                else if (i < length - 5
                                                    && list[i + 1].IsSpace()
                                                    && list[i + 2].IsName()
                                                    && list[i + 3].IsPunctuation()
                                                    && list[i + 4].IsName()
                                                    && list[i + 5].IsPunctuation())
                                                {
                                                    list.Insert(i + 5, list[i + 2]);
                                                    list.Insert(i + 5, new SymbolDisplayPart(SymbolDisplayPartKind.Text, null, " to "));
                                                    list.RemoveRange(i + 1, 2);
                                                    length -= 5;
                                                }

                                                parts = list.ToImmutableArray();
                                            }
                                        }

                                        break;
                                    }
                            }

                            break;
                        }
                }
            }

            return parts;
        }
    }
}
