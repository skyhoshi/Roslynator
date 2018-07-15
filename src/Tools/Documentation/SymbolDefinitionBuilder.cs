// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    internal static class SymbolDefinitionBuilder
    {
        private static readonly SymbolDisplayFormat _nameAndContainingNames = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes, genericsOptions: SymbolDisplayGenericsOptions.None);
        private static readonly SymbolDisplayFormat _nameAndContainingNamesAndNameSpaces = new SymbolDisplayFormat(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypesAndNamespaces, genericsOptions: SymbolDisplayGenericsOptions.None);

        public static ImmutableArray<SymbolDisplayPart> GetDisplayParts(
            ISymbol symbol,
            SymbolDisplayFormat definitionFormat,
            DocumentationOptions options)
        {
            ImmutableArray<SymbolDisplayPart> parts;

            if (symbol is INamedTypeSymbol typeSymbol)
            {
                parts = typeSymbol.ToDisplayParts(definitionFormat, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers);
            }
            else
            {
                parts = symbol.ToDisplayParts(definitionFormat);
                typeSymbol = null;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            ImmutableArray<SymbolDisplayPart>.Builder builder = null;

            AddAttributes();

            int baseListCount = 0;
            int constraintCount = parts.Count(f => f.IsKeyword("where"));

            if (typeSymbol != null)
                AddBaseTypes();

            if (constraintCount > 0)
            {
                int j = 0;
                while (j < parts.Length)
                {
                    if (parts[j].IsKeyword("where"))
                        break;

                    j++;
                }

                for (int i = parts.Length - 1; i >= j; i--)
                {
                    if (parts[i].IsKeyword("where"))
                    {
                        if (options.FormatConstraints
                            && (baseListCount > 1 || constraintCount > 1))
                        {
                            parts = parts.InsertRange(i, SymbolDisplayPartFactory.LineBreakAndIndent);
                        }
                    }
                    else if (parts[i].IsTypeName()
                        && parts[i].Symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        parts = parts.RemoveAt(i);
                        parts = parts.InsertRange(i, GetDisplayParts(namedTypeSymbol, containingNamespace));
                    }
                }
            }

            return parts;

            void AddAttributes()
            {
                using (IEnumerator<AttributeData> en = symbol
                    .GetAttributes()
                    .Where(f => !DocumentationUtility.ShouldBeExcluded(f.AttributeClass)).GetEnumerator())
                {
                    if (en.MoveNext())
                    {
                        builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

                        do
                        {
                            builder.Add(SymbolDisplayPartFactory.Punctuation("["));

                            AddDisplayParts(en.Current.AttributeClass, containingNamespace, builder);

                            builder.Add(SymbolDisplayPartFactory.Punctuation("]"));
                            builder.Add(SymbolDisplayPartFactory.LineBreak());
                        }
                        while (en.MoveNext());

                        parts = parts.InsertRange(0, builder);
                        builder.Clear();
                    }
                }
            }

            void AddBaseTypes()
            {
                INamedTypeSymbol baseType = null;

                if (typeSymbol.TypeKind.Is(TypeKind.Class, TypeKind.Interface))
                {
                    baseType = typeSymbol.BaseType;

                    if (baseType?.SpecialType == SpecialType.System_Object)
                        baseType = null;
                }

                ImmutableArray<INamedTypeSymbol> interfaces = typeSymbol.Interfaces;

                if (interfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                    interfaces = interfaces.RemoveAll(f => f.SpecialType == SpecialType.System_Collections_IEnumerable);

                baseListCount = interfaces.Length;

                if (baseType != null)
                    baseListCount++;

                if (baseListCount > 0)
                {
                    if (builder == default)
                        builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

                    int index = -1;

                    for (int i = 0; i < parts.Length; i++)
                    {
                        if (parts[i].IsKeyword("where"))
                        {
                            builder.AddRange(parts, i);
                            index = i;

                            builder.AddPunctuation(":");
                            builder.AddSpace();
                            break;
                        }
                    }

                    if (index == -1)
                    {
                        builder.AddRange(parts);

                        builder.AddSpace();
                        builder.AddPunctuation(":");
                        builder.AddSpace();
                    }

                    if (baseType != null)
                    {
                        AddDisplayParts(baseType, containingNamespace, builder);

                        if (interfaces.Any())
                        {
                            builder.AddPunctuation(",");

                            if (options.FormatBaseList)
                            {
                                builder.AddRange(SymbolDisplayPartFactory.LineBreakAndIndent);
                            }
                            else
                            {
                                builder.Add(SymbolDisplayPartFactory.Space());
                            }
                        }
                    }

                    if (interfaces.Any())
                    {
                        ImmutableArray<(INamedTypeSymbol symbol, string displayString)> sortedInterfaces = SortInterfaces(interfaces.Select(f => ((f, GetDisplayParts(f, containingNamespace).ToDisplayString()))).ToImmutableArray());

                        AddDisplayParts(sortedInterfaces[0].symbol, containingNamespace, builder);

                        for (int i = 1; i < sortedInterfaces.Length; i++)
                        {
                            builder.AddPunctuation(",");

                            if (options.FormatBaseList)
                            {
                                builder.AddRange(SymbolDisplayPartFactory.LineBreakAndIndent);
                            }
                            else
                            {
                                builder.Add(SymbolDisplayPartFactory.Space());
                            }

                            AddDisplayParts(sortedInterfaces[i].symbol, containingNamespace, builder);
                        }
                    }

                    if (index != -1)
                    {
                        if (!options.FormatConstraints
                            || (baseListCount == 1 && constraintCount == 1))
                        {
                            builder.AddSpace();
                        }

                        builder.AddRange(parts.Skip(index));
                    }

                    parts = builder.ToImmutableArray();
                }
            }
        }

        private static ImmutableArray<SymbolDisplayPart> GetDisplayParts(INamedTypeSymbol symbol, INamespaceSymbol containingNamespace)
        {
            ImmutableArray<SymbolDisplayPart>.Builder builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

            AddDisplayParts(symbol, containingNamespace, builder);

            return builder.ToImmutableArray();
        }

        private static void AddDisplayParts(INamedTypeSymbol symbol, INamespaceSymbol containingNamespace, ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            if (symbol.ContainingNamespace == containingNamespace)
            {
                builder.AddRange(symbol.ToDisplayParts(_nameAndContainingNames));
            }
            else
            {
                builder.AddRange(symbol.ToDisplayParts(_nameAndContainingNamesAndNameSpaces));
            }

            ImmutableArray<ITypeSymbol> typeArguments = symbol.TypeArguments;

            ImmutableArray<ITypeSymbol>.Enumerator en = typeArguments.GetEnumerator();

            if (en.MoveNext())
            {
                builder.AddPunctuation("<");

                while (true)
                {
                    if (en.Current.Kind == SymbolKind.NamedType)
                    {
                        AddDisplayParts((INamedTypeSymbol)en.Current, containingNamespace, builder);
                    }
                    else
                    {
                        Debug.Assert(en.Current.Kind == SymbolKind.TypeParameter, en.Current.Kind.ToString());

                        builder.Add(new SymbolDisplayPart(SymbolDisplayPartKind.TypeParameterName, en.Current, en.Current.Name));
                    }

                    if (en.MoveNext())
                    {
                        builder.AddPunctuation(",");
                        builder.AddSpace();
                    }
                    else
                    {
                        break;
                    }
                }

                builder.AddPunctuation(">");
            }
        }

        private static ImmutableArray<(INamedTypeSymbol symbol, string displayString)> SortInterfaces(
            ImmutableArray<(INamedTypeSymbol symbol, string displayString)> interfaces)
        {
            return interfaces.Sort((x, y) =>
            {
                if (x.symbol.InheritsFrom(y.symbol.OriginalDefinition, includeInterfaces: true))
                    return -1;

                if (y.symbol.InheritsFrom(x.symbol.OriginalDefinition, includeInterfaces: true))
                    return 1;

                if (interfaces.Any(f => x.symbol.InheritsFrom(f.symbol.OriginalDefinition, includeInterfaces: true)))
                {
                    if (!interfaces.Any(f => y.symbol.InheritsFrom(f.symbol.OriginalDefinition, includeInterfaces: true)))
                        return -1;
                }
                else if (interfaces.Any(f => y.symbol.InheritsFrom(f.symbol.OriginalDefinition, includeInterfaces: true)))
                {
                    return 1;
                }

                return string.Compare(x.displayString, y.displayString, StringComparison.Ordinal);
            });
        }

        private static void AddSpace(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.Space());
        }

        private static void AddPunctuation(this ImmutableArray<SymbolDisplayPart>.Builder builder, string text)
        {
            builder.Add(SymbolDisplayPartFactory.Punctuation(text));
        }
    }
}
