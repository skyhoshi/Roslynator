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
    public class AbstractPackage : AsyncPackage
    {
        private FileSystemWatcher _watcher;

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

            return value is bool isSolutionOpen && isSolutionOpen;
        }

        public void InitializeSettings()
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

        private void AfterOpenSolution(object sender = null, OpenSolutionEventArgs e = null)
        {
            UpdateSettings();

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

        private void UpdateSettings()
        {
            SettingsManager.Instance.ConfigFileSettings = LoadConfigFileSettings();
            SettingsManager.Instance.ApplyTo(RefactoringSettings.Current);
            SettingsManager.Instance.ApplyTo(CodeFixSettings.Current);
        }

        public ConfigFileSettings LoadConfigFileSettings()
        {
            if (!(GetService(typeof(DTE)) is DTE dte))
                return null;

            string path = dte.Solution.FullName;

            if (string.IsNullOrEmpty(path))
                return null;

            string directoryPath = Path.GetDirectoryName(path);

            if (string.IsNullOrEmpty(directoryPath))
                return null;

            path = Path.Combine(directoryPath, ConfigFileSettings.FileName);

            if (!File.Exists(path))
                return null;

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

            return null;
        }

        public void WatchConfigFile()
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

            _watcher.Changed += (object sender, FileSystemEventArgs e) => UpdateSettings();
            _watcher.Created += (object sender, FileSystemEventArgs e) => UpdateSettings();
            _watcher.Deleted += (object sender, FileSystemEventArgs e) => UpdateSettings();
        }
    }
}
