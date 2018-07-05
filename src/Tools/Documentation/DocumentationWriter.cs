// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class DocumentationWriter : IDisposable
    {
        private bool _disposed;

        public abstract SymbolDocumentationInfo DirectoryInfo { get; }

        public abstract ISymbol Symbol { get; }

        public abstract void WriteTitle(ISymbol symbol);

        public abstract void WriteNamespace(ISymbol symbol);

        public abstract void WriteAssembly(ISymbol symbol);

        public abstract void WriteObsolete(ISymbol symbol);

        public abstract void WriteSummary(ISymbol symbol);

        public abstract void WriteSignature(ISymbol symbol);

        public abstract void WriteTypeParameters(ISymbol symbol);

        public abstract void WriteParameters(ISymbol symbol);

        public abstract void WriteReturnValue(ISymbol symbol);

        public abstract void WriteInheritance(ITypeSymbol typeSymbol);

        public abstract void WriteAttributes(ISymbol symbol);

        public abstract void WriteDerived(ITypeSymbol typeSymbol);

        public abstract void WriteImplements(ITypeSymbol typeSymbol);

        public abstract void WriteExceptions(ISymbol symbol);

        public abstract void WriteExamples(ISymbol symbol);

        public abstract void WriteRemarks(ISymbol symbol);

        public abstract void WriteEnumFields(IEnumerable<IFieldSymbol> fields);

        public abstract void WriteConstructors(IEnumerable<IMethodSymbol> constructors);

        public abstract void WriteFields(IEnumerable<IFieldSymbol> fields);

        public abstract void WriteProperties(IEnumerable<IPropertySymbol> properties);

        public abstract void WriteMethods(IEnumerable<IMethodSymbol> methods);

        public abstract void WriteOperators(IEnumerable<IMethodSymbol> operators);

        public abstract void WriteEvents(IEnumerable<IEventSymbol> events);

        public abstract void WriteExplicitInterfaceImplementations(IEnumerable<ISymbol> explicitInterfaceImplementations);

        public abstract void WriteExtensionMethods(ITypeSymbol typeSymbol);

        public abstract void WriteSeeAlso(ISymbol symbol);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    Close();

                _disposed = true;
            }
        }

        public virtual void Close()
        {
        }
    }
}
