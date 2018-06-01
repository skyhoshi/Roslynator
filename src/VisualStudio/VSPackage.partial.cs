// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using Roslynator.CodeFixes;
using Roslynator.Configuration;
using static Roslynator.VisualStudio.PackageHelpers;

#pragma warning disable RCS1090

namespace Roslynator.VisualStudio
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    public sealed partial class VSPackage : AsyncPackage
    {
        private FileSystemWatcher _watcher;

        public VSPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);

            InitializeSettings(
                (GeneralOptionsPage)GetDialogPage(typeof(GeneralOptionsPage)),
                (RefactoringsOptionsPage)GetDialogPage(typeof(RefactoringsOptionsPage)),
                (CodeFixesOptionsPage)GetDialogPage(typeof(CodeFixesOptionsPage)));

            if (await IsSolutionLoadedAsync(cancellationToken))
            {
                await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
                AfterOpenSolution();
            }

            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterOpenSolution += AfterOpenSolution;
            Microsoft.VisualStudio.Shell.Events.SolutionEvents.OnAfterCloseSolution += AfterCloseSolution;
        }

        private async Task<bool> IsSolutionLoadedAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            var solution = await GetServiceAsync(typeof(SVsSolution)) as IVsSolution;

            ErrorHandler.ThrowOnFailure(solution.GetProperty((int)__VSPROPID.VSPROPID_IsSolutionOpen, out object value));

            return value is bool isSolutionOpen && isSolutionOpen;
        }

        private void AfterOpenSolution(object sender = null, OpenSolutionEventArgs e = null)
        {
            UpdateSettings();

            if (GetService(typeof(DTE)) is DTE dte)
                WatchConfigFile(dte.Solution.FullName, _watcher, UpdateSettings);
        }

        private void AfterCloseSolution(object sender = null, EventArgs e = null)
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
                _watcher = null;
            }
        }

        private void UpdateSettings()
        {
            SettingsManager.Instance.ConfigFileSettings = (GetService(typeof(DTE)) is DTE dte)
                ? LoadConfigFileSettings(dte.Solution.FullName)
                : null;

            SettingsManager.Instance.ApplyTo(RefactoringSettings.Current);
            SettingsManager.Instance.ApplyTo(CodeFixSettings.Current);
        }
    }
}
