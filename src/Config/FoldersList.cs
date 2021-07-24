using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace OSVN.Config
{
    /// <summary>
    /// List with local paths to svn working copy folders
    /// </summary>
    class FoldersList
    {
        public IEnumerable<string> FolderPaths { get; protected set; }
        public string FileHash { get; protected set; }


        public static FoldersList FromFile(string path)
        {
            var content = File.ReadAllText(path);
            return new FoldersList
            {
                FileHash = GetFileHash(content),
                FolderPaths = GetFolders(content)
            };
        }

        public static string GetFileHash(string data)
        {
            using var sha256 = SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hash);
        }

        private static List<string> GetFolders(string content)
        {
            return content
                .Split(Environment.NewLine)
                .Select(x => x.Trim())
                .Where(x => x != string.Empty && !x.StartsWith('#'))
                .ToList();
        }
    }

}
