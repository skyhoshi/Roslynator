// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Roslynator.Documentation
{
    internal static class Program
    {
        [SuppressMessage("Redundancy", "RCS1163:Unused parameter.", Justification = "<Pending>")]
        private static void Main(string[] args)
        {
            const string assemblyName = "Roslynator.CSharp.dll";

            string path = RuntimeMetadataReference.TrustedPlatformAssemblies[assemblyName];

            string xmlDocPath = Path.ChangeExtension(path, "xml");

            if (!File.Exists(xmlDocPath))
                throw new InvalidOperationException();

            XmlDocumentation xmlDocumentation = XmlDocumentation.Load(xmlDocPath);

            IEnumerable<PortableExecutableReference> references = RuntimeMetadataReference
                .TrustedPlatformAssemblies
                .Select(f => MetadataReference.CreateFromFile(f.Value));

            CSharpCompilation compilation = CSharpCompilation.Create(
                "Temp",
                syntaxTrees: default(IEnumerable<SyntaxTree>),
                references: references,
                options: default(CSharpCompilationOptions));

            MetadataReference reference = compilation.References.First(f => ((PortableExecutableReference)f).FilePath == path);

            var assemblySymbol = (IAssemblySymbol)compilation.GetAssemblyOrModuleSymbol(reference);

            foreach (ITypeSymbol typeSymbol in assemblySymbol.GetPubliclyVisibleTypes())
            {
                using (var writer = new MarkdownDocumentationWriter(xmlDocumentation))
                {
                    writer.WriteTitle(typeSymbol);
                    writer.WriteNamespace(typeSymbol);
                    writer.WriteAssemblies(typeSymbol);
                    writer.WriteSummary(typeSymbol);
                    writer.WriteTypeParameters(typeSymbol);
                    writer.WriteInheritance(typeSymbol);
                    writer.WriteDerived(typeSymbol);
                    writer.WriteImplements(typeSymbol);
                    writer.WriteExamples(typeSymbol);
                    writer.WriteRemarks(typeSymbol);

                    ImmutableArray<ISymbol> members = typeSymbol.GetMembers();

                    IEnumerable<IMethodSymbol> constructors = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.Constructor);

                    writer.WriteConstructors(constructors);

                    IEnumerable<IPropertySymbol> properties = members
                        .Where(f => f.Kind == SymbolKind.Property)
                        .Cast<IPropertySymbol>();

                    writer.WriteProperties(properties);

                    IEnumerable<IMethodSymbol> methods = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.Ordinary);

                    writer.WriteMethods(methods);

                    IEnumerable<IMethodSymbol> explicitInterfaceImplementations = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.ExplicitInterfaceImplementation);

                    writer.WriteExplicitInterfaceImplementations(explicitInterfaceImplementations);

                    IEnumerable<IMethodSymbol> operators = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind.Is(MethodKind.UserDefinedOperator, MethodKind.Conversion));

                    writer.WriteOperators(operators);

                    writer.WriteExtensionMethods(typeSymbol);
                    writer.WriteSeeAlso(typeSymbol);

                    Console.WriteLine(writer.ToString());
                }
            }

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
