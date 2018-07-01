// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    writer.WriteAssembly(typeSymbol);
                    writer.WriteSummary(typeSymbol);
                    writer.WriteTypeParameters(typeSymbol);
                    writer.WriteParameters(typeSymbol);
                    writer.WriteReturnValue(typeSymbol);
                    writer.WriteInheritance(typeSymbol);
                    writer.WriteAttributes(typeSymbol);
                    writer.WriteDerived(typeSymbol);
                    writer.WriteImplements(typeSymbol);
                    writer.WriteExamples(typeSymbol);
                    writer.WriteRemarks(typeSymbol);

                    IEnumerable<ISymbol> members = typeSymbol.GetMembers().Where(f => f.IsPubliclyVisible());

                    IEnumerable<IFieldSymbol> fields = members
                        .Where(f => f.Kind == SymbolKind.Field)
                        .Cast<IFieldSymbol>();

                    if (typeSymbol.TypeKind == TypeKind.Enum)
                        writer.WriteEnumFields(fields);

                    IEnumerable<IMethodSymbol> constructors = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.Constructor
                            && (f.ContainingType.TypeKind != TypeKind.Struct || f.Parameters.Any()));

                    writer.WriteConstructors(constructors);

                    writer.WriteFields(fields);

                    IEnumerable<IPropertySymbol> properties = members
                        .Where(f => f.Kind == SymbolKind.Property)
                        .Cast<IPropertySymbol>();

                    writer.WriteProperties(properties);

                    IEnumerable<IMethodSymbol> methods = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.Ordinary);

                    writer.WriteMethods(methods);

                    IEnumerable<IMethodSymbol> operators = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind.Is(MethodKind.UserDefinedOperator, MethodKind.Conversion));

                    writer.WriteOperators(operators);

                    IEnumerable<IEventSymbol> events = members
                        .Where(f => f.Kind == SymbolKind.Event)
                        .Cast<IEventSymbol>();

                    writer.WriteEvents(events);

                    IEnumerable<IMethodSymbol> explicitInterfaceImplementations = members
                        .Where(f => f.Kind == SymbolKind.Method)
                        .Cast<IMethodSymbol>()
                        .Where(f => f.MethodKind == MethodKind.ExplicitInterfaceImplementation);

                    writer.WriteExplicitInterfaceImplementations(explicitInterfaceImplementations);

                    writer.WriteExtensionMethods(typeSymbol);
                    writer.WriteSeeAlso(typeSymbol);

                    Console.WriteLine(writer.ToString());
                    Debug.WriteLine(writer.ToString());
                }
            }

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
