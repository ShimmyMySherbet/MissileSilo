using MissileSilo.API.Models;
using MissileSilo.API.Utils;
using MissileSilo.Rocket;
using SDG.Framework.Modules;

namespace MissileSilo.Unturned
{
    public class Silo : IModuleNexus
    {
        public void initialize()
        {
            System.Console.WriteLine("Init ASM cache");
            AssemblyCache.Init();
            System.Console.WriteLine("Init PathTool");
            PathTool.Init();
            System.Console.WriteLine("Init Rocketmod");
            ModuleInjector.MountFrom(PathTool.Extras_RocketmodFolder);
            System.Console.WriteLine("Init Rocketmod Extensions");
            RocketModifier.Load();
        }

        public void shutdown()
        {
        }
    }
}