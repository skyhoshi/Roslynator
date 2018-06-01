// Copyright (c) Josef Pihrt. All rights reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Security;
using Roslynator.Configuration;

namespace Roslynator.VisualStudio
{
    internal static class PackageHelpers
    {
        public static void InitializeSettings(
            GeneralOptionsPage generalOptionsPage,
            RefactoringsOptionsPage refactoringsOptionsPage,
            CodeFixesOptionsPage codeFixesOptionsPage)
        {
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

        public static ConfigFileSettings LoadConfigFileSettings(string path)
        {
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

        public static void WatchConfigFile(string path, FileSystemWatcher watcher, Action onChangedCreatedDeleted)
        {
            if (string.IsNullOrEmpty(path))
                return;

            string directoryPath = Path.GetDirectoryName(path);

            if (string.IsNullOrEmpty(directoryPath))
                return;

            watcher = new FileSystemWatcher(directoryPath, ConfigFileSettings.FileName)
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = false,
            };

            watcher.Changed += (object sender, FileSystemEventArgs e) => onChangedCreatedDeleted();
            watcher.Created += (object sender, FileSystemEventArgs e) => onChangedCreatedDeleted();
            watcher.Deleted += (object sender, FileSystemEventArgs e) => onChangedCreatedDeleted();
        }
    }
}
