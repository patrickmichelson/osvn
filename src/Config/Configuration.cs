using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using osvn;

namespace OSVN.Config
{
    /// <summary>
    /// Global static configuration
    /// </summary>
    static class Configuration
    {
        static string _settingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "OfflineSVN", "config.json");
        static string _foldersFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "workingcopies.txt");

        public static Settings Settings { get; private set; }

        public static async Task Update()
        {
            var folders = FoldersList.FromFile(_foldersFile);
            Settings = Settings.FromFileOrNew(_settingsFile);

            if (folders.FileHash == Settings.WorkingCopiesHash)
                return;

            Settings.WorkingCopiesHash = folders.FileHash;
            Settings.Folders = new List<WorkingCopy>();

            foreach (var path in folders.FolderPaths)
            {
                try
                {
                    var svn = new SvnWrapper(path);
                    var url = await svn.GetUrlAsync();
                    Settings.Folders.Add(new WorkingCopy { Path = path, URL = new Uri(url) });
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"Can't get repository url for working copy folder '{path}'. {ex.Message}");
                }
            }

            Settings.WriteFile(_settingsFile);
        }
    }
}
