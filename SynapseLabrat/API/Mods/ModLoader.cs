using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;

namespace SynapseLabrat.API.Mods
{
    public static class ModLoader
    {
        public static Dictionary<Assembly, Mod> Mods { get; } = new Dictionary<Assembly, Mod>();

        internal static void LoadMods()
        {
            try
            {
                if(!Directory.Exists(Paths.Directories.Mods))
                    Directory.CreateDirectory(Paths.Directories.Mods);

                var mods = new List<ModLoadContext>();
                foreach(var path in Directory.GetFiles(Paths.Directories.Mods))
                {
                    try
                    {
                        var assembly = Assembly.Load(File.ReadAllBytes(path));
                        foreach(var type in assembly.GetTypes())
                        {
                            if (!typeof(Mod).IsAssignableFrom(type)) continue;

                            var attribute = type.GetCustomAttribute<ModAttributes>();

                            if(attribute == null)
                            {
                                Log.LogMessage($"Tried to initialise a invalid Mod due to it's missing ModAttributes ... skipping Mod - Path: {path}");
                                break;
                            }

                            mods.Add(new ModLoadContext
                            {
                                Assembly = assembly,
                                Attributes = attribute,
                                Type = type,
                            });
                            break;
                        }
                    }
                    catch(Exception ex)
                    {
                        Log.LogMessage($"Error while trying to initialise a Mod - Path: {path} - Error: {ex}");
                    }
                }

                foreach(var mod in mods.OrderByDescending(x => x.Attributes.LoadPriority))
                {
                    try
                    {
                        Log.LogMessage($"Loading now {mod.Attributes?.Name}");

                        var modinstance = (Mod)Activator.CreateInstance(mod.Type);
                        modinstance.Attributes = mod.Attributes;
                        modinstance.ModDirectory = Path.Combine(Paths.Directories.Mods, mod.Attributes.Name);
                        modinstance.Load();
                        Mods.Add(mod.Assembly, modinstance);
                    }
                    catch (Exception ex)
                    {
                        Log.LogMessage($"Error while trying to load a Mod - Name: {mod.Attributes.Name} - Error: {ex}");
                    }
                }

                Log.LogMessage("Done with loading all Mods");
            }
            catch (Exception ex)
            {
                Log.LogMessage($"Error while loading Mods: {ex}");
            }
        }
    }
}
