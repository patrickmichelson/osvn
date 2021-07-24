using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OSVN.Config;

namespace OSVN
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                if (args.Length != 1
                    || args.Any(x => x.TrimStart('/', '-') == "?")
                    || args.Any(x => x.TrimStart('/', '-').Equals("help", StringComparison.InvariantCultureIgnoreCase)))
                {
                    ShowUsage();
                    return;
                }

                await Configuration.Update();
                OpenFileFromURL(args[0]);

            }
            catch (Exception ex)
            {
                Output.ShowMessage("Execution failed.", ex.Message);
            }
        }

        private static void OpenFileFromURL(string url)
        {
            var fileUrl = new Uri(url);

            foreach (var workingCopy in Configuration.Settings.Folders)
            {
                if (workingCopy.ContainsFile(fileUrl, out string filePath))
                {
                    Process.Start("explorer", "\"" + filePath + "\"");
                    return;
                }
            }

            throw new Exception("No matching working copy found.");
        }

        static void ShowUsage()
        {
            var sb = new StringBuilder();
            var appInfo = Assembly.GetExecutingAssembly().GetName();

            sb.AppendLine("Commandline tool to open SVN URLs from a local working copy.");
            sb.AppendLine("Edit workingcopies.txt next to " + appInfo.Name + ".exe to set list of local working copies.");
            sb.AppendLine();
            sb.AppendLine("USAGE:");
            sb.AppendLine("  " + appInfo.Name + ".exe <svn url>");
            sb.AppendLine();

            Output.ShowMessage(null, sb.ToString());
        }
    }
}
