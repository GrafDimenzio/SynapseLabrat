using SynapseLabrat.API;
using SynapseLabrat.API.Mods;
using System;
using System.IO;

namespace SynapseLabrat
{
    public static class SynapseController
    {
        public static void Load()
        {
            try
            {
                Log.LogMessage("Starting SynapseLabrat!");
                ModLoader.LoadMods();
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
