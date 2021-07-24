using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OSVN
{
    /// <summary>
    /// Helper class to show user output either in terminal or as messagebox, depending on context.
    /// </summary>
    static class Output
    {
        public static void ShowMessage(string header, string text)
        {
            var caption = $"OfflineSVN v{FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion}";

            bool runsInTerminal = GetConsoleProcessList(new uint[64], 64) > 1;

            if (runsInTerminal)
            {
                Console.WriteLine(caption);
                Console.WriteLine();

                if (!string.IsNullOrEmpty(header))
                {
                    Console.WriteLine(header);
                    Console.WriteLine();
                }

                Console.WriteLine(text);
            }
            else
            {
                if (!string.IsNullOrEmpty(header))
                    text = header + Environment.NewLine + Environment.NewLine + text;
                MessageBox((IntPtr)0, text, caption, 0);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern uint GetConsoleProcessList(uint[] ProcessList, uint ProcessCount);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        static extern int MessageBox(IntPtr h, string m, string c, int type);
    }
}
