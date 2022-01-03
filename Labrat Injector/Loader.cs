using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Labrat_Injector
{
    internal class Loader
    {
        public static Assembly Synapse { get; private set; }

        public static List<Assembly> Dependencies { get; private set; } = new List<Assembly>();

        public static void LoadSystem()
        {
            try
            {
                var localpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Synapse");
                var synapsepath = Directory.Exists(localpath) ? localpath : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SynapseLabrat");

                if (!Directory.Exists(synapsepath)) Directory.CreateDirectory(synapsepath);

                foreach (var depend in Directory.GetFiles(Path.Combine(synapsepath, "dependencies"), "*.dll"))
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(depend));
                    Dependencies.Add(assembly);
                };

                Synapse = Assembly.Load(File.ReadAllBytes(Path.Combine(synapsepath, "SynapseLabrat.dll")));
                InvokeAssembly(Synapse);
            }
            catch (Exception ex)
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SynapseLabrat");
                var file = Path.Combine(path, "Error.txt");
                File.Create(file).Close();
                File.WriteAllText(file, $"Couldn't load SynapseLabrat.dll or one of its dependencies \n{ex}");
            }
            
        }

        private static void InvokeAssembly(Assembly assembly)
        {
            try
            {
                assembly.GetTypes()
                    .First((Type t) => t.Name == "SynapseController").GetMethods()
                    .First((MethodInfo m) => m.Name == "Load")
                    .Invoke(null, null);
            }
            catch (Exception ex)
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SynapseLabrat");
                var file = Path.Combine(path, "Error.txt");
                File.Create(file).Close();
                File.WriteAllText(file, $"Couldn't load SynapseLabrat\n{ex}");
            }
        }
    }
}
