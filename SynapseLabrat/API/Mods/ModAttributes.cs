using System;

namespace SynapseLabrat.API.Mods
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple = false)]
    public class ModAttributes : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public int LoadPriority { get; set; } = 0;
    }
}
