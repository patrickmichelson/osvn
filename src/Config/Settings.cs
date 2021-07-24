using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace OSVN.Config
{
    /// <summary>
    /// Application settings
    /// Cache for information about working copy folders.
    /// </summary>
    class Settings
    {
        public string WorkingCopiesHash { get; set; }
        public List<WorkingCopy> Folders { get; set; }

        public static Settings FromFileOrNew(string path)
        {
            try
            {
                return JsonSerializer.Deserialize<Settings>(File.ReadAllText(path));
            }
            catch
            {
                return new Settings();
            }
        }

        public void WriteFile(string path)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using var writer = new Utf8JsonWriter(new FileStream(path, FileMode.OpenOrCreate), new JsonWriterOptions { Indented = true });
            JsonSerializer.Serialize(writer, this);
        }
    }

}
