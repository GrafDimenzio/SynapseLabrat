using System;
using System.IO;

namespace SynapseLabrat.API
{
    public static class Paths
    {
        public static class Directories
        {
            public static string Main { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SynapseLabrat");

            public static string Logs { get; } = Path.Combine(Main, "Logs");

            public static string Mods { get; } = Path.Combine(Main, "Mods");
        }

        public static class Files
        {
            public static string SynapseDLL { get; } = Path.Combine(Directories.Main, "SynapseLabrat.dll");

            public static string LogFile { get; } = Path.Combine(Directories.Logs, $"Log-{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}.txt");
        }
    }
}
