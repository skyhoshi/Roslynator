// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis;

namespace Roslynator.Documentation
{
    public abstract class DocumentationResources
    {
        public static DocumentationResources Default { get; } = new DefaultDocumentationResources();

        public virtual char InheritanceChar { get; } = '\u2192';

        public virtual string CloseParenthesis { get; } = ")";
        public virtual string Colon { get; } = ":";
        public virtual string Comma { get; } = ",";
        public virtual string DllExtension { get; } = "dll";
        public virtual string Dot { get; } = ".";
        public virtual string Ellipsis { get; } = "...";
        public virtual string False { get; } = "false";
        public virtual string OpenParenthesis { get; } = "(";
        public virtual string True { get; } = "true";

        //TODO: Class atd.
        public abstract string Assembly { get; }
        public abstract string Attributes { get; }
        public abstract string Class { get; }
        public abstract string Classes { get; }
        public abstract string Constructor { get; }
        public abstract string Constructors { get; }
        public abstract string Delegate { get; }
        public abstract string Delegates { get; }
        public abstract string Derived { get; }
        public abstract string Enum { get; }
        public abstract string Enums { get; }
        public abstract string Event { get; }
        public abstract string Events { get; }
        public abstract string Examples { get; }
        public abstract string Exceptions { get; }
        public abstract string ExplicitInterfaceImplementation { get; }
        public abstract string ExplicitInterfaceImplementations { get; }
        public abstract string ExtendedTypes { get; }
        public abstract string ExtensionMethod { get; }
        public abstract string ExtensionMethods { get; }
        public abstract string Extensions { get; }
        public abstract string Field { get; }
        public abstract string Fields { get; }
        public abstract string FieldValue { get; }
        public abstract string Implements { get; }
        public abstract string Inheritance { get; }
        public abstract string InheritedFrom { get; }
        public abstract string Interface { get; }
        public abstract string Interfaces { get; }
        public abstract string Member { get; }
        public abstract string Method { get; }
        public abstract string Methods { get; }
        public abstract string Name { get; }
        public abstract string Namespace { get; }
        public abstract string Namespaces { get; }
        public abstract string ObjectModel { get; }
        public abstract string ObsoleteWarning { get; }
        public abstract string Operator { get; }
        public abstract string Operators { get; }
        public abstract string Overloads { get; }
        public abstract string Parameter { get; }
        public abstract string Parameters { get; }
        public abstract string Properties { get; }
        public abstract string Property { get; }
        public abstract string PropertyValue { get; }
        public abstract string Remarks { get; }
        public abstract string Returns { get; }
        public abstract string ReturnValue { get; }
        public abstract string SeeAlso { get; }
        public abstract string Struct { get; }
        public abstract string Structs { get; }
        public abstract string Summary { get; }
        public abstract string TypeParameter { get; }
        public abstract string TypeParameters { get; }
        public abstract string Value { get; }

        public string GetName(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return Event;
                case SymbolKind.Field:
                    return Field;
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                return Constructor;
                            case MethodKind.Conversion:
                            case MethodKind.UserDefinedOperator:
                                return Operator;
                            case MethodKind.ExplicitInterfaceImplementation:
                            case MethodKind.Ordinary:
                                return Method;
                        }

                        throw new InvalidOperationException();
                    }
                case SymbolKind.Namespace:
                    return Namespace;
                case SymbolKind.Property:
                    return Property;
                case SymbolKind.NamedType:
                    return GetName(((ITypeSymbol)symbol).TypeKind);
            }

            throw new InvalidOperationException();
        }

        internal string GetPluralName(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return Events;
                case SymbolKind.Field:
                    return Fields;
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                return Constructors;
                            case MethodKind.Conversion:
                            case MethodKind.UserDefinedOperator:
                                return Operators;
                            case MethodKind.ExplicitInterfaceImplementation:
                            case MethodKind.Ordinary:
                                return Methods;
                        }

                        throw new InvalidOperationException();
                    }
                case SymbolKind.Namespace:
                    return Namespaces;
                case SymbolKind.Property:
                    return Properties;
                case SymbolKind.NamedType:
                    return GetPluralName(((ITypeSymbol)symbol).TypeKind);
            }

            throw new InvalidOperationException();
        }

        public string GetName(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return Class;
                case TypeKind.Delegate:
                    return Delegate;
                case TypeKind.Enum:
                    return Enum;
                case TypeKind.Interface:
                    return Interface;
                case TypeKind.Struct:
                    return Struct;
            }

            throw new InvalidOperationException();
        }

        public string GetPluralName(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return Classes;
                case TypeKind.Delegate:
                    return Delegates;
                case TypeKind.Enum:
                    return Enums;
                case TypeKind.Interface:
                    return Interfaces;
                case TypeKind.Struct:
                    return Structs;
            }

            throw new InvalidOperationException();
        }

        private class DefaultDocumentationResources : DocumentationResources
        {
            public override string Assembly { get; } = "Assembly";
            public override string Attributes { get; } = "Attributes";
            public override string Class { get; } = "Class";
            public override string Classes { get; } = "Classes";
            public override string Constructor { get; } = "Constructor";
            public override string Constructors { get; } = "Constructors";
            public override string Delegate { get; } = "Delegate";
            public override string Delegates { get; } = "Delegates";
            public override string Derived { get; } = "Derived";
            public override string Enum { get; } = "Enum";
            public override string Enums { get; } = "Enums";
            public override string Event { get; } = "Event";
            public override string Events { get; } = "Events";
            public override string Examples { get; } = "Examples";
            public override string Exceptions { get; } = "Exceptions";
            public override string ExplicitInterfaceImplementation { get; } = "Explicit Interface Implementation";
            public override string ExplicitInterfaceImplementations { get; } = "Explicit Interface Implementations";
            public override string ExtendedTypes { get; } = "Extended Types";
            public override string ExtensionMethod { get; } = "Extension Method";
            public override string ExtensionMethods { get; } = "Extension Methods";
            public override string Extensions { get; } = "Extensions";
            public override string Field { get; } = "Field";
            public override string Fields { get; } = "Fields";
            public override string FieldValue { get; } = "Field Value";
            public override string Implements { get; } = "Implements";
            public override string Inheritance { get; } = "Inheritance";
            public override string InheritedFrom { get; } = "Inherited from";
            public override string Interface { get; } = "Interface";
            public override string Interfaces { get; } = "Interfaces";
            public override string Member { get; } = "Member";
            public override string Method { get; } = "Method";
            public override string Methods { get; } = "Methods";
            public override string Name { get; } = "Name";
            public override string Namespace { get; } = "Namespace";
            public override string Namespaces { get; } = "Namespaces";
            public override string ObjectModel { get; } = "Object Model";
            public override string ObsoleteWarning { get; } = "WARNING: This API is now obsolete.";
            public override string Operator { get; } = "Operator";
            public override string Operators { get; } = "Operators";
            public override string Overloads { get; } = "Overloads";
            public override string Parameter { get; } = "Parameter";
            public override string Parameters { get; } = "Parameters";
            public override string Properties { get; } = "Properties";
            public override string Property { get; } = "Property";
            public override string PropertyValue { get; } = "Property Value";
            public override string Remarks { get; } = "Remarks";
            public override string Returns { get; } = "Returns";
            public override string ReturnValue { get; } = "Return Value";
            public override string SeeAlso { get; } = "See Also";
            public override string Struct { get; } = "Struct";
            public override string Structs { get; } = "Structs";
            public override string Summary { get; } = "Summary";
            public override string TypeParameter { get; } = "Type Parameter";
            public override string TypeParameters { get; } = "Type Parameters";
            public override string Value { get; } = "Value";
        }
    }
}
