// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Roslynator.CSharp.Refactorings
{
    internal static class SortCaseLabelsRefactoring
    {
        public static void ComputeRefactoring(RefactoringContext context, CaseSwitchLabelSyntax caseLabel)
        {
            if (!caseLabel.Value.IsKind(SyntaxKind.StringLiteralExpression))
                return;

            if (!(caseLabel.Parent is SwitchSectionSyntax section))
                return;

            SyntaxList<SwitchLabelSyntax> labels = section.Labels;

            int count = labels.Count;

            if (count == 1)
                return;

            if (!(labels[0] is CaseSwitchLabelSyntax label))
                return;

            if (!label.Value.IsKind(SyntaxKind.StringLiteralExpression))
                return;

            string value = ((LiteralExpressionSyntax)label.Value).Token.ValueText;

            for (int i = 1; i < count; i++)
            {
                if (!(labels[i] is CaseSwitchLabelSyntax label2))
                    return;

                if (!label2.Value.IsKind(SyntaxKind.StringLiteralExpression))
                    return;

                string value2 = ((LiteralExpressionSyntax)label2.Value).Token.ValueText;

                if (StringComparer.CurrentCulture.Compare(value, value2) > 0)
                {
                    context.RegisterRefactoring(
                        "Sort labels",
                        ct => RefactorAsync(context.Document, section, ct),
                        RefactoringIdentifiers.SortCaseLabels);

                    return;
                }

                value = value2;
            }
        }

        private static Task<Document> RefactorAsync(
            Document document,
            SwitchSectionSyntax section,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            SyntaxList<SwitchLabelSyntax> labels = section.Labels;

            SyntaxList<SwitchLabelSyntax> newLabels = labels.OrderBy(f => f, SwitchLabelComparer.Instance).ToSyntaxList();

            SwitchSectionSyntax newSection = section.WithLabels(newLabels);

            return document.ReplaceNodeAsync(section, newSection, cancellationToken);
        }

        private sealed class SwitchLabelComparer : IComparer<SwitchLabelSyntax>
        {
            public static SwitchLabelComparer Instance { get; } = new SwitchLabelComparer();

            public int Compare(SwitchLabelSyntax x, SwitchLabelSyntax y)
            {
                if (object.ReferenceEquals(x, y))
                    return 0;

                if (x == null)
                    return -1;

                if (y == null)
                    return 1;

                var label1 = (CaseSwitchLabelSyntax)x;
                var label2 = ((CaseSwitchLabelSyntax)y);

                string value1 = ((LiteralExpressionSyntax)label1.Value).Token.ValueText;
                string value2 = ((LiteralExpressionSyntax)label2.Value).Token.ValueText;

                return StringComparer.CurrentCulture.Compare(value1, value2);
            }
        }
    }
}