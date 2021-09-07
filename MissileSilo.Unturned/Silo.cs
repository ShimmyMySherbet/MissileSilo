using MissileSilo.Unturned.Utils;
using SDG.Framework.Modules;

namespace MissileSilo.Unturned
{
    public class Silo : IModuleNexus
    {
        public void initialize()
        {
            PathTool.Init();

            ModuleInjector.MountFrom(PathTool.Extras_RocketmodFolder);
        }

        public void shutdown()
        {
        }
    }
}