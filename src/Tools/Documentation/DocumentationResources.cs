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
        public virtual string Dot { get; } = ".";
        public virtual string OpenParenthesis { get; } = "(";

        public virtual string DllExtension { get; } = "dll";
        public virtual string Ellipsis { get; } = "...";
        public virtual string FalseValue { get; } = "false";
        public virtual string TrueValue { get; } = "true";

        public abstract string AssemblyTitle { get; }
        public abstract string AttributesTitle { get; }
        public abstract string ClassTitle { get; }
        public abstract string ClassesTitle { get; }
        public abstract string CombinationOfTitle { get; }
        public abstract string ConstructorTitle { get; }
        public abstract string ConstructorsTitle { get; }
        public abstract string DelegateTitle { get; }
        public abstract string DelegatesTitle { get; }
        public abstract string DerivedTitle { get; }
        public abstract string EnumTitle { get; }
        public abstract string EnumsTitle { get; }
        public abstract string EventTitle { get; }
        public abstract string EventsTitle { get; }
        public abstract string ExamplesTitle { get; }
        public abstract string ExceptionsTitle { get; }
        public abstract string ExplicitInterfaceImplementationTitle { get; }
        public abstract string ExplicitInterfaceImplementationsTitle { get; }
        public abstract string ExtendedExternalTypesTitle { get; }
        public abstract string ExtensionMethodTitle { get; }
        public abstract string ExtensionMethodsTitle { get; }
        public abstract string ExtensionsTitle { get; }
        public abstract string FieldTitle { get; }
        public abstract string FieldsTitle { get; }
        public abstract string FieldValueTitle { get; }
        public abstract string ImplementsTitle { get; }
        public abstract string InheritanceTitle { get; }
        public abstract string InheritedFrom { get; }
        public abstract string InterfaceTitle { get; }
        public abstract string InterfacesTitle { get; }
        public abstract string MemberTitle { get; }
        public abstract string MethodTitle { get; }
        public abstract string MethodsTitle { get; }
        public abstract string NameTitle { get; }
        public abstract string NamespaceTitle { get; }
        public abstract string NamespacesTitle { get; }
        public abstract string ObjectModelTitle { get; }
        public abstract string ObsoleteWarning { get; }
        public abstract string OperatorTitle { get; }
        public abstract string OperatorsTitle { get; }
        public abstract string OverloadsTitle { get; }
        public abstract string ParameterTitle { get; }
        public abstract string ParametersTitle { get; }
        public abstract string PropertiesTitle { get; }
        public abstract string PropertyTitle { get; }
        public abstract string PropertyValueTitle { get; }
        public abstract string RemarksTitle { get; }
        public abstract string ReturnsTitle { get; }
        public abstract string ReturnValueTitle { get; }
        public abstract string SeeAlsoTitle { get; }
        public abstract string StaticClassesTitle { get; }
        public abstract string StructTitle { get; }
        public abstract string StructsTitle { get; }
        public abstract string SummaryTitle { get; }
        public abstract string TypeParameterTitle { get; }
        public abstract string TypeParametersTitle { get; }
        public abstract string ValueTitle { get; }
        public abstract string ValuesTitle { get; }

        public string GetName(ISymbol symbol)
        {
            switch (symbol.Kind)
            {
                case SymbolKind.Event:
                    return EventTitle;
                case SymbolKind.Field:
                    return FieldTitle;
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                return ConstructorTitle;
                            case MethodKind.Conversion:
                            case MethodKind.UserDefinedOperator:
                                return OperatorTitle;
                            case MethodKind.ExplicitInterfaceImplementation:
                            case MethodKind.Ordinary:
                                return MethodTitle;
                        }

                        throw new InvalidOperationException();
                    }
                case SymbolKind.Namespace:
                    return NamespaceTitle;
                case SymbolKind.Property:
                    return PropertyTitle;
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
                    return EventsTitle;
                case SymbolKind.Field:
                    return FieldsTitle;
                case SymbolKind.Method:
                    {
                        var methodSymbol = (IMethodSymbol)symbol;

                        switch (methodSymbol.MethodKind)
                        {
                            case MethodKind.Constructor:
                                return ConstructorsTitle;
                            case MethodKind.Conversion:
                            case MethodKind.UserDefinedOperator:
                                return OperatorsTitle;
                            case MethodKind.ExplicitInterfaceImplementation:
                            case MethodKind.Ordinary:
                                return MethodsTitle;
                        }

                        throw new InvalidOperationException();
                    }
                case SymbolKind.Namespace:
                    return NamespacesTitle;
                case SymbolKind.Property:
                    return PropertiesTitle;
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
                    return ClassTitle;
                case TypeKind.Delegate:
                    return DelegateTitle;
                case TypeKind.Enum:
                    return EnumTitle;
                case TypeKind.Interface:
                    return InterfaceTitle;
                case TypeKind.Struct:
                    return StructTitle;
            }

            throw new InvalidOperationException();
        }

        public string GetPluralName(TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return ClassesTitle;
                case TypeKind.Delegate:
                    return DelegatesTitle;
                case TypeKind.Enum:
                    return EnumsTitle;
                case TypeKind.Interface:
                    return InterfacesTitle;
                case TypeKind.Struct:
                    return StructsTitle;
            }

            throw new InvalidOperationException();
        }

        private class DefaultDocumentationResources : DocumentationResources
        {
            public override string AssemblyTitle { get; } = "Assembly";
            public override string AttributesTitle { get; } = "Attributes";
            public override string ClassTitle { get; } = "Class";
            public override string ClassesTitle { get; } = "Classes";
            public override string CombinationOfTitle { get; } = "Combination of";
            public override string ConstructorTitle { get; } = "Constructor";
            public override string ConstructorsTitle { get; } = "Constructors";
            public override string DelegateTitle { get; } = "Delegate";
            public override string DelegatesTitle { get; } = "Delegates";
            public override string DerivedTitle { get; } = "Derived";
            public override string EnumTitle { get; } = "Enum";
            public override string EnumsTitle { get; } = "Enums";
            public override string EventTitle { get; } = "Event";
            public override string EventsTitle { get; } = "Events";
            public override string ExamplesTitle { get; } = "Examples";
            public override string ExceptionsTitle { get; } = "Exceptions";
            public override string ExplicitInterfaceImplementationTitle { get; } = "Explicit Interface Implementation";
            public override string ExplicitInterfaceImplementationsTitle { get; } = "Explicit Interface Implementations";
            public override string ExtendedExternalTypesTitle { get; } = "Extended External Types";
            public override string ExtensionMethodTitle { get; } = "Extension Method";
            public override string ExtensionMethodsTitle { get; } = "Extension Methods";
            public override string ExtensionsTitle { get; } = "Extensions";
            public override string FieldTitle { get; } = "Field";
            public override string FieldsTitle { get; } = "Fields";
            public override string FieldValueTitle { get; } = "Field Value";
            public override string ImplementsTitle { get; } = "Implements";
            public override string InheritanceTitle { get; } = "Inheritance";
            public override string InheritedFrom { get; } = "Inherited from";
            public override string InterfaceTitle { get; } = "Interface";
            public override string InterfacesTitle { get; } = "Interfaces";
            public override string MemberTitle { get; } = "Member";
            public override string MethodTitle { get; } = "Method";
            public override string MethodsTitle { get; } = "Methods";
            public override string NameTitle { get; } = "Name";
            public override string NamespaceTitle { get; } = "Namespace";
            public override string NamespacesTitle { get; } = "Namespaces";
            public override string ObjectModelTitle { get; } = "Object Model";
            public override string ObsoleteWarning { get; } = "WARNING: This API is now obsolete.";
            public override string OperatorTitle { get; } = "Operator";
            public override string OperatorsTitle { get; } = "Operators";
            public override string OverloadsTitle { get; } = "Overloads";
            public override string ParameterTitle { get; } = "Parameter";
            public override string ParametersTitle { get; } = "Parameters";
            public override string PropertiesTitle { get; } = "Properties";
            public override string PropertyTitle { get; } = "Property";
            public override string PropertyValueTitle { get; } = "Property Value";
            public override string RemarksTitle { get; } = "Remarks";
            public override string ReturnsTitle { get; } = "Returns";
            public override string ReturnValueTitle { get; } = "Return Value";
            public override string SeeAlsoTitle { get; } = "See Also";
            public override string StaticClassesTitle { get; } = "Static Classes";
            public override string StructTitle { get; } = "Struct";
            public override string StructsTitle { get; } = "Structs";
            public override string SummaryTitle { get; } = "Summary";
            public override string TypeParameterTitle { get; } = "Type Parameter";
            public override string TypeParametersTitle { get; } = "Type Parameters";
            public override string ValueTitle { get; } = "Value";
            public override string ValuesTitle { get; } = "Values";
        }
    }
}
