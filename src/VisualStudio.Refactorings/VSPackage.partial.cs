// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Events;
using Microsoft.VisualStudio.Shell.Interop;
using Roslynator.CodeFixes;
using Roslynator.Configuration;

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

            InitializeSettings();

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

            return value is bool isSolutionOpen
                && isSolutionOpen;
        }

        private void AfterOpenSolution(object sender = null, OpenSolutionEventArgs e = null)
        {
            UpdateSettingsAfterConfigFileChanged();

            WatchConfigFile();
        }

        private void AfterCloseSolution(object sender = null, EventArgs e = null)
        {
            if (_watcher != null)
            {
                _watcher.Dispose();
                _watcher = null;
            }
        }

        private void InitializeSettings()
        {
            var generalOptionsPage = (GeneralOptionsPage)GetDialogPage(typeof(GeneralOptionsPage));
            var refactoringsOptionsPage = (RefactoringsOptionsPage)GetDialogPage(typeof(RefactoringsOptionsPage));
            var codeFixesOptionsPage = (CodeFixesOptionsPage)GetDialogPage(typeof(CodeFixesOptionsPage));

            Version currentVersion = typeof(GeneralOptionsPage).Assembly.GetName().Version;

            if (!Version.TryParse(generalOptionsPage.ApplicationVersion, out Version version)
                || version < currentVersion)
            {
                generalOptionsPage.ApplicationVersion = currentVersion.ToString();
                generalOptionsPage.SaveSettingsToStorage();
            }

            codeFixesOptionsPage.CheckNewItemsDisabledByDefault();
            refactoringsOptionsPage.CheckNewItemsDisabledByDefault();

            SettingsManager.Instance.UpdateVisualStudioSettings(generalOptionsPage);
            SettingsManager.Instance.UpdateVisualStudioSettings(refactoringsOptionsPage);
            SettingsManager.Instance.UpdateVisualStudioSettings(codeFixesOptionsPage);
        }

        private void UpdateSettingsAfterConfigFileChanged()
        {
            SettingsManager.Instance.ConfigFileSettings = LoadConfigFileSettings();
            SettingsManager.Instance.ApplyTo(RefactoringSettings.Current);
            SettingsManager.Instance.ApplyTo(CodeFixSettings.Current);
        }

        private ConfigFileSettings LoadConfigFileSettings()
        {
            if (GetService(typeof(DTE)) is DTE dte)
            {
                string path = dte.Solution.FullName;

                if (!string.IsNullOrEmpty(path))
                {
                    string directoryPath = Path.GetDirectoryName(path);

                    if (!string.IsNullOrEmpty(directoryPath))
                    {
                        path = Path.Combine(directoryPath, ConfigFileSettings.FileName);

                        if (File.Exists(path))
                        {
                            try
                            {
                                return ConfigFileSettings.Load(path);
                            }
                            catch (IOException)
                            {
                            }
                            catch (UnauthorizedAccessException)
                            {
                            }
                            catch (SecurityException)
                            {
                            }
                        }
                    }
                }
            }

            return default(ConfigFileSettings);
        }

        private void WatchConfigFile()
        {
            if (!(GetService(typeof(DTE)) is DTE dte))
                return;

            string path = dte.Solution.FullName;

            if (string.IsNullOrEmpty(path))
                return;

            string directoryPath = Path.GetDirectoryName(path);

            if (string.IsNullOrEmpty(directoryPath))
                return;

            _watcher = new FileSystemWatcher(directoryPath, ConfigFileSettings.FileName)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = false,
            };

            _watcher.Changed += (object sender, FileSystemEventArgs e) => UpdateSettingsAfterConfigFileChanged();
            _watcher.Created += (object sender, FileSystemEventArgs e) => UpdateSettingsAfterConfigFileChanged();
            _watcher.Deleted += (object sender, FileSystemEventArgs e) => UpdateSettingsAfterConfigFileChanged();
        }
    }
}
