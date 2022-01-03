using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SynapseLabrat.API
{
    public static class Log
    {
        static Log()
        {
            if(!Directory.Exists(Paths.Directories.Logs))
                Directory.CreateDirectory(Paths.Directories.Logs);

            if(!File.Exists(Paths.Files.LogFile))
                File.Create(Paths.Files.LogFile).Close();
        }

        public static void LogMessage(object message)
        {
            var lines = File.ReadAllLines(Paths.Files.LogFile).ToList();
            lines.Add($"{Assembly.GetCallingAssembly().GetName().Name} - {DateTime.Now.TimeOfDay.ToString().Split('.')[0]}: {message}");
            File.WriteAllLines(Paths.Files.LogFile, lines);
        }
    }
}
