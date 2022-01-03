using System.IO;

namespace SynapseLabrat.API.Mods
{
    public abstract class Mod
    {        
        public virtual void Load() { }

        public ModAttributes Attributes { get; internal set; }

        private string modDirectory;
        public string ModDirectory
        {
            get
            {
                if(!string.IsNullOrWhiteSpace(modDirectory) && !Directory.Exists(modDirectory))
                    Directory.CreateDirectory(modDirectory);

                return modDirectory;
            }
            set => modDirectory = value;
        }
    }
}
