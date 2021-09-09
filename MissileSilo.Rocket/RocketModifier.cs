using MissileSilo.Rocket.Models;
using MissileSilo.Rocket.Utils;
using Rocket.Core.Plugins;

namespace MissileSilo.Rocket
{
    public static class RocketModifier
    {
        public static void Load()
        {
            // Override rocket's plugin load logic
            TypeOverrider.PatchRedirectType<RocketPluginManager, PluginsManager>();
        }
    }
}