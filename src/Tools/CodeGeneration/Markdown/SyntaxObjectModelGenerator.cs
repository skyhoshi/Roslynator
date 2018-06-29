// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using DotMarkdown;
using DotMarkdown.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Roslynator.CodeGeneration.CSharp;
using static DotMarkdown.Linq.MFactory;

namespace Roslynator.CodeGeneration.Markdown
{
    public static class SyntaxObjectModelGenerator
    {
        public static string GenerateCSharpSyntaxObjectModel()
        {
            List<INamedTypeSymbol> symbols = Symbols.SyntaxSymbols.ToList();

            var stack = new Stack<(INamedTypeSymbol symbol, MBulletItem item)>();

            INamedTypeSymbol nodeSymbol = Symbols.Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.SyntaxNode");

            MBulletItem nodeItem = BulletItem(nodeSymbol.Name);

            INamedTypeSymbol csharpNodeSymbol = Symbols.Compilation.GetTypeByMetadataName("Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode");

            MBulletItem csharpNodeItem = BulletItem(csharpNodeSymbol.Name);

            nodeItem.Add(csharpNodeItem);

            stack.Push((csharpNodeSymbol, csharpNodeItem));

            while (stack.Count > 0)
            {
                (INamedTypeSymbol symbol, MBulletItem item) = stack.Peek();

                INamedTypeSymbol derivedSymbol = symbols.Find(f => f.BaseType == symbol);

                if (derivedSymbol != null)
                {
                    MBulletItem item2 = BulletItem(Link(derivedSymbol.Name, $"syntax/csharp/{derivedSymbol.Name}.md"));

                    item.Add(item2);

                    stack.Push((derivedSymbol, item2));

                    symbols.Remove(derivedSymbol);
                }
                else
                {
                    stack.Pop();
                }
            }

            var doc = new MDocument(Heading1("C# Syntax Object Model"), nodeItem);

            doc.AddFootnote();

            return doc.ToString();
        }

        public static string GenerateCSharpSyntaxTypeMetadata(INamedTypeSymbol typeSymbol)
        {
            var doc = new MDocument(CreateElements());

            doc.AddFootnote();

            var format = new MarkdownFormat(tableOptions: MarkdownFormat.Default.TableOptions | TableOptions.FormatContent);

            return doc.ToString(format);

            IEnumerable<MElement> CreateElements()
            {
                yield return Heading1(typeSymbol.Name);
                yield return Heading2("Inheritance");
                yield return CreateInheritance(typeSymbol);

                List<MTableRow> syntaxProperties = CreateSyntaxProperties(typeSymbol, f => Symbols.IsSyntaxTypeSymbol(f.Type)).ToList();

                if (syntaxProperties.Count > 0)
                {
                    yield return Heading2("Syntax Properties");
                    yield return Table(
                        TableRow("Name", "Type"),
                        syntaxProperties);
                }

                List<MTableRow> otherProperties = CreateSyntaxProperties(typeSymbol, f => !Symbols.IsSyntaxTypeSymbol(f.Type)).ToList();

                if (otherProperties.Count > 0)
                {
                    yield return Heading2((syntaxProperties.Count > 0) ? "Other Properties" : "Properties");
                    yield return Table(
                        TableRow("Name", "Type"),
                        otherProperties);
                }

                List<INamedTypeSymbol> derivedTypes = Symbols.GetDerivedTypes(typeSymbol, f => !f.IsAbstract).ToList();

                if (derivedTypes.Count > 0)
                {
                    yield return Heading2("Derived Types");

                    List<INamedTypeSymbol> directlyDerived = derivedTypes.Where(f => f.BaseType == typeSymbol).ToList();

                    if (directlyDerived.Count > 0)
                    {
                        yield return Heading3("Directly Derived Types");
                        yield return BulletList(directlyDerived.Select(f => NameOrLink(f)));
                    }

                    List<INamedTypeSymbol> indirectlyDerived = derivedTypes.Where(f => f.BaseType != typeSymbol).ToList();

                    if (indirectlyDerived.Count > 0)
                    {
                        yield return Heading3("Indirectly Derived Types");
                        yield return BulletList(indirectlyDerived.Select(f => NameOrLink(f)));
                    }
                }

                List<SyntaxKind> kinds = Symbols.GetKinds(typeSymbol).ToList();
                if (kinds.Count > 1)
                {
                    yield return Heading2("SyntaxKinds");
                    yield return BulletList(kinds.Select(f => f.ToString()).OrderBy(f => f));
                }

                yield return Heading2("See Also");
                yield return BulletList(Link("Official Documentation", $"https://docs.microsoft.com/en-us/dotnet/api/microsoft.codeanalysis.csharp.syntax.{typeSymbol.Name.ToLowerInvariant()}"));
            }
        }

        private static IEnumerable<MTableRow> CreateSyntaxProperties(INamedTypeSymbol typeSymbol, System.Func<IPropertySymbol, bool> predicate)
        {
            return Symbols.GetPropertySymbols(typeSymbol)
                .Where(predicate)
                .Select(propertySymbol => TableRow(propertySymbol.Name, NameOrLink(propertySymbol.Type)));
        }

        private static MBulletItem CreateInheritance(ITypeSymbol typeSymbol)
        {
            MBulletItem item = BulletItem(typeSymbol.ToDisplayString(SymbolDisplayFormats.Default));

            typeSymbol = typeSymbol.BaseType;

            while (typeSymbol != null)
            {
                item = BulletItem(NameOrLink(typeSymbol), item);

                typeSymbol = typeSymbol.BaseType;
            }

            return item;
        }

        private static MElement NameOrLink(ITypeSymbol typeSymbol)
        {
            INamedTypeSymbol typeSymbol2 = Symbols.SyntaxSymbols.FirstOrDefault(s => s == typeSymbol);

            if (typeSymbol2 != null)
            {
                return Link(typeSymbol.ToDisplayString(SymbolDisplayFormats.Default), $"{typeSymbol2.Name}.md");
            }
            else if (typeSymbol.OriginalDefinition.Equals(Symbols.SyntaxListSymbol)
                || typeSymbol.OriginalDefinition.Equals(Symbols.SeparatedSyntaxListSymbol))
            {
                ITypeSymbol typeArgument = ((INamedTypeSymbol)typeSymbol).TypeArguments[0];

                return Inline("SyntaxList<", Link(typeArgument.Name, $"{typeArgument.Name}.md"), ">");
            }
            else
            {
                return new MText(typeSymbol.ToDisplayString(SymbolDisplayFormats.Default));
            }
        }
    }
}
