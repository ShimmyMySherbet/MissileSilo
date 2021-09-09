using System.Reflection;

namespace MissileSilo.Unturned.Models
{
    public struct ExclusiveAssembly
    {
        public Assembly Owner { get; private set; }
        public Assembly Assembly { get; private set; }
        public string AssemblyName => Assembly.FullName;

        public ExclusiveAssembly(Assembly owner, Assembly assembly)
        {
            Owner = owner;
            Assembly = assembly;
        }
    }
}