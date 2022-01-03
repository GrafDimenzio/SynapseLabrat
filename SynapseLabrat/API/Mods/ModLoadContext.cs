using System;
using System.Reflection;

namespace SynapseLabrat.API.Mods
{
    internal class ModLoadContext
    {
        public Assembly Assembly { get; set; }

        public Type Type { get; set; }

        public ModAttributes Attributes { get; set; }
    }
}
