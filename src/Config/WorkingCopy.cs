using System;

namespace OSVN.Config
{
    /// <summary>
    /// Represents a local SVN working copy folder
    /// </summary>
    class WorkingCopy
    {
        public string Path { get; set; }
        public Uri URL { get; set; }

        public bool ContainsFile(Uri fileUrl, out string filePath)
        {
            filePath = null;

            if (URL.Host != fileUrl.Host)
                return false;

            if (!fileUrl.LocalPath.StartsWith(URL.LocalPath))
                return false;

            var relativePath = System.IO.Path.GetRelativePath(URL.LocalPath, fileUrl.LocalPath);

            filePath = System.IO.Path.Join(Path, relativePath);

            if (!System.IO.File.Exists(filePath) && !System.IO.Directory.Exists(filePath))
                return false;

            return true;
        }
    }

}
