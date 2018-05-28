// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Immutable;

namespace Roslynator
{
    internal static class FullyQualifiedMetadataNames
    {
        public static FullyQualifiedMetadataName System_ArgumentException { get; } = new FullyQualifiedMetadataName(Namespaces.System, "ArgumentException");
        public static FullyQualifiedMetadataName System_ArgumentNullException { get; } = new FullyQualifiedMetadataName(Namespaces.System, "ArgumentNullException");
        public static FullyQualifiedMetadataName System_Collections_Generic { get; } = new FullyQualifiedMetadataName(Namespaces.System_Collections, "Generic");
        public static FullyQualifiedMetadataName System_Collections_Generic_List_T { get; } = new FullyQualifiedMetadataName(Namespaces.System_Collections_Generic, "List`1");
        public static FullyQualifiedMetadataName System_Collections_IDictionary { get; } = new FullyQualifiedMetadataName(Namespaces.System_Collections, "IDictionary");
        public static FullyQualifiedMetadataName System_Collections_Immutable_ImmutableArray_T { get; } = new FullyQualifiedMetadataName(Namespaces.System_Collections_Immutable, "ImmutableArray`1");
        public static FullyQualifiedMetadataName System_ComponentModel_INotifyPropertyChanged { get; } = new FullyQualifiedMetadataName(ImmutableArray.Create("System", "ComponentModel"), "INotifyPropertyChanged");
        public static FullyQualifiedMetadataName System_Diagnostics_Debug { get; } = new FullyQualifiedMetadataName(Namespaces.System_Diagnostics, "Debug");
        public static FullyQualifiedMetadataName System_Diagnostics_DebuggerDisplayAttribute { get; } = new FullyQualifiedMetadataName(Namespaces.System_Diagnostics, "DebuggerDisplayAttribute");
        public static FullyQualifiedMetadataName System_EventArgs { get; } = new FullyQualifiedMetadataName(Namespaces.System, "EventArgs");
        public static FullyQualifiedMetadataName System_EventHandler { get; } = new FullyQualifiedMetadataName(Namespaces.System, "EventHandler");
        public static FullyQualifiedMetadataName System_FlagsAttribute { get; } = new FullyQualifiedMetadataName(Namespaces.System, "FlagsAttribute");
        public static FullyQualifiedMetadataName System_Func_T2 { get; } = new FullyQualifiedMetadataName(Namespaces.System, "Func`2");
        public static FullyQualifiedMetadataName System_Func_T3 { get; } = new FullyQualifiedMetadataName(Namespaces.System, "Func`3");
        public static FullyQualifiedMetadataName System_IEquatable_T { get; } = new FullyQualifiedMetadataName(Namespaces.System, "IEquatable`1");
        public static FullyQualifiedMetadataName System_Linq_Enumerable { get; } = new FullyQualifiedMetadataName(Namespaces.System_Linq, "Enumerable");
        public static FullyQualifiedMetadataName System_Linq_Expressions_Expression_1 { get; } = new FullyQualifiedMetadataName(ImmutableArray.Create("System", "Linq", "Expressions"), "Expression`1");
        public static FullyQualifiedMetadataName System_Linq_ImmutableArrayExtensions { get; } = new FullyQualifiedMetadataName(Namespaces.System_Linq, "ImmutableArrayExtensions");
        public static FullyQualifiedMetadataName System_Linq_IOrderedEnumerable_T { get; } = new FullyQualifiedMetadataName(Namespaces.System_Linq, "IOrderedEnumerable`1");
        public static FullyQualifiedMetadataName System_NonSerializedAttribute { get; } = new FullyQualifiedMetadataName(Namespaces.System, "NonSerializedAttribute");
        public static FullyQualifiedMetadataName System_Runtime_CompilerServices_ConfiguredTaskAwaitable { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_CompilerServices, "ConfiguredTaskAwaitable");
        public static FullyQualifiedMetadataName System_Runtime_CompilerServices_ConfiguredTaskAwaitable_T { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_CompilerServices, "ConfiguredTaskAwaitable`1");
        public static FullyQualifiedMetadataName System_Runtime_InteropServices_LayoutKind { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_InteropServices, "LayoutKind");
        public static FullyQualifiedMetadataName System_Runtime_InteropServices_StructLayoutAttribute { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_InteropServices, "StructLayoutAttribute");
        public static FullyQualifiedMetadataName System_Runtime_Serialization_DataMemberAttribute { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_Serialization, "DataMemberAttribute");
        public static FullyQualifiedMetadataName System_Runtime_Serialization_SerializationInfo { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_Serialization, "SerializationInfo");
        public static FullyQualifiedMetadataName System_Runtime_Serialization_StreamingContext { get; } = new FullyQualifiedMetadataName(Namespaces.System_Runtime_Serialization, "StreamingContext");
        public static FullyQualifiedMetadataName System_Text_RegularExpressions_Regex { get; } = new FullyQualifiedMetadataName(Namespaces.System_Text_RegularExpressions, "Regex");
        public static FullyQualifiedMetadataName System_Text_RegularExpressions_RegexOptions { get; } = new FullyQualifiedMetadataName(Namespaces.System_Text_RegularExpressions, "RegexOptions");
        public static FullyQualifiedMetadataName System_Text_StringBuilder { get; } = new FullyQualifiedMetadataName(Namespaces.System_Text, "StringBuilder");
        public static FullyQualifiedMetadataName System_Threading_Tasks_Task { get; } = new FullyQualifiedMetadataName(Namespaces.System_Threading_Tasks, "Task");
        public static FullyQualifiedMetadataName System_Threading_Tasks_Task_T { get; } = new FullyQualifiedMetadataName(Namespaces.System_Threading_Tasks, "Task`1");
        public static FullyQualifiedMetadataName System_TimeSpan { get; } = new FullyQualifiedMetadataName(Namespaces.System, "TimeSpan");

        private static class Namespaces
        {
            public static readonly ImmutableArray<string> System = ImmutableArray.Create("System");
            public static readonly ImmutableArray<string> System_Collections = ImmutableArray.Create("System", "Collections");
            public static readonly ImmutableArray<string> System_Collections_Generic = ImmutableArray.Create("System", "Collections", "Generic");
            public static readonly ImmutableArray<string> System_Collections_Immutable = ImmutableArray.Create("System", "Collections", "Immutable");
            public static readonly ImmutableArray<string> System_Diagnostics = ImmutableArray.Create("System", "Diagnostics");
            public static readonly ImmutableArray<string> System_Linq = ImmutableArray.Create("System", "Linq");
            public static readonly ImmutableArray<string> System_Runtime_CompilerServices = ImmutableArray.Create("System", "Runtime", "CompilerServices");
            public static readonly ImmutableArray<string> System_Runtime_InteropServices = ImmutableArray.Create("System", "Runtime", "InteropServices");
            public static readonly ImmutableArray<string> System_Runtime_Serialization = ImmutableArray.Create("System", "Runtime", "Serialization");
            public static readonly ImmutableArray<string> System_Text = ImmutableArray.Create("System", "Text");
            public static readonly ImmutableArray<string> System_Text_RegularExpressions = ImmutableArray.Create("System", "Text", "RegularExpressions");
            public static readonly ImmutableArray<string> System_Threading_Tasks = ImmutableArray.Create("System", "Threading", "Tasks");
        }
    }
}
