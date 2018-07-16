// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
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
            SymbolDisplayFormat format,
            bool formatBaseList = false,
            bool formatConstraints = false)
        {
            ImmutableArray<SymbolDisplayPart> parts;

            if (symbol is INamedTypeSymbol typeSymbol)
            {
                parts = typeSymbol.ToDisplayParts(format, SymbolDisplayTypeDeclarationOptions.IncludeAccessibility | SymbolDisplayTypeDeclarationOptions.IncludeModifiers);
            }
            else
            {
                parts = symbol.ToDisplayParts(format);
                typeSymbol = null;
            }

            ImmutableArray<AttributeData> attributes = symbol.GetAttributes();

            bool hasAttributes = attributes.Any(f => !DocumentationUtility.ShouldBeExcluded(f.AttributeClass));

            int baseListCount = 0;
            INamedTypeSymbol baseType = null;
            ImmutableArray<INamedTypeSymbol> interfaces = default;

            if (typeSymbol != null)
            {
                if (typeSymbol.TypeKind.Is(TypeKind.Class, TypeKind.Interface))
                {
                    baseType = typeSymbol.BaseType;

                    if (baseType?.SpecialType == SpecialType.System_Object)
                        baseType = null;
                }

                interfaces = typeSymbol.Interfaces;

                if (interfaces.Any(f => f.OriginalDefinition.SpecialType == SpecialType.System_Collections_Generic_IEnumerable_T))
                    interfaces = interfaces.RemoveAll(f => f.SpecialType == SpecialType.System_Collections_IEnumerable);

                baseListCount = interfaces.Length;

                if (baseType != null)
                    baseListCount++;
            }

            int constraintCount = 0;
            int whereIndex = -1;

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].IsKeyword("where"))
                {
                    if (whereIndex == -1)
                        whereIndex = i;

                    constraintCount++;
                }
            }

            if (!hasAttributes
                && baseListCount == 0
                && constraintCount == 0)
            {
                return parts;
            }

            INamespaceSymbol containingNamespace = symbol.ContainingNamespace;

            ImmutableArray<SymbolDisplayPart>.Builder builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>(parts.Length);

            foreach (AttributeData attribute in attributes)
            {
                if (DocumentationUtility.ShouldBeExcluded(attribute.AttributeClass))
                    continue;

                builder.AddPunctuation("[");
                builder.AddDisplayParts(attribute.AttributeClass, containingNamespace);
                builder.AddPunctuation("]");
                builder.AddLineBreak();
            }

            if (baseListCount == 0
                && constraintCount == 0)
            {
                builder.AddRange(parts);
                return builder.ToImmutableArray();
            }

            if (baseListCount > 0)
            {
                if (whereIndex != -1)
                {
                    builder.AddRange(parts, whereIndex);
                }
                else
                {
                    builder.AddRange(parts);
                    builder.AddSpace();
                }

                builder.AddPunctuation(":");
                builder.AddSpace();

                if (baseType != null)
                {
                    builder.AddDisplayParts(baseType, containingNamespace);

                    if (interfaces.Any())
                    {
                        builder.AddPunctuation(",");

                        if (formatBaseList)
                        {
                            builder.AddLineBreak();
                            builder.AddIndentation();
                        }
                        else
                        {
                            builder.AddSpace();
                        }
                    }
                }

                if (interfaces.Any())
                {
                    ImmutableArray<(INamedTypeSymbol symbol, string displayString)> sortedInterfaces = SortInterfaces(interfaces.Select(f => ((f, GetDisplayParts(f, containingNamespace).ToDisplayString()))).ToImmutableArray());

                    builder.AddDisplayParts(sortedInterfaces[0].symbol, containingNamespace);

                    for (int i = 1; i < sortedInterfaces.Length; i++)
                    {
                        builder.AddPunctuation(",");

                        if (formatBaseList)
                        {
                            builder.AddLineBreak();
                            builder.AddIndentation();
                        }
                        else
                        {
                            builder.AddSpace();
                        }

                        builder.AddDisplayParts(sortedInterfaces[i].symbol, containingNamespace);
                    }
                }

                if (whereIndex != -1)
                {
                    if (!formatConstraints
                        || (baseListCount == 1 && constraintCount == 1))
                    {
                        builder.AddSpace();
                    }
                }
            }
            else if (whereIndex != -1)
            {
                builder.AddRange(parts, whereIndex);
            }
            else
            {
                builder.AddRange(parts);
            }

            if (whereIndex != -1)
            {
                for (int i = whereIndex; i < parts.Length; i++)
                {
                    if (parts[i].IsKeyword("where"))
                    {
                        if (formatConstraints
                            && (baseListCount > 1 || constraintCount > 1))
                        {
                            builder.AddLineBreak();
                            builder.AddIndentation();
                        }

                        builder.Add(parts[i]);
                    }
                    else if (parts[i].IsTypeName()
                        && parts[i].Symbol is INamedTypeSymbol namedTypeSymbol)
                    {
                        builder.AddDisplayParts(namedTypeSymbol, containingNamespace);
                    }
                    else
                    {
                        builder.Add(parts[i]);
                    }
                }
            }

            return builder.ToImmutableArray();
        }

        private static ImmutableArray<SymbolDisplayPart> GetDisplayParts(INamedTypeSymbol symbol, INamespaceSymbol containingNamespace)
        {
            ImmutableArray<SymbolDisplayPart>.Builder builder = ImmutableArray.CreateBuilder<SymbolDisplayPart>();

            builder.AddDisplayParts(symbol, containingNamespace);

            return builder.ToImmutableArray();
        }

        private static void AddDisplayParts(this ImmutableArray<SymbolDisplayPart>.Builder builder, INamedTypeSymbol symbol, INamespaceSymbol containingNamespace)
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
                        builder.AddDisplayParts((INamedTypeSymbol)en.Current, containingNamespace);
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

        private static void AddIndentation(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.Indentation());
        }

        private static void AddLineBreak(this ImmutableArray<SymbolDisplayPart>.Builder builder)
        {
            builder.Add(SymbolDisplayPartFactory.LineBreak());
        }

        private static void AddPunctuation(this ImmutableArray<SymbolDisplayPart>.Builder builder, string text)
        {
            builder.Add(SymbolDisplayPartFactory.Punctuation(text));
        }
    }
}
