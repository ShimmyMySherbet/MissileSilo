using Newtonsoft.Json;
using SDG.Framework.Modules;
using SDG.Unturned;
using System.IO;
using System.Linq;
using Module = SDG.Framework.Modules.Module;

namespace MissileSilo.Unturned.Utils
{
    public static class ModuleInjector
    {
        public static bool MountFrom(string dir)
        {
            var moduleFile = Directory.GetFiles(dir, "*.module").FirstOrDefault();

            if (moduleFile == null)
            {
                return false;
            }

            var moduleConfigData = File.ReadAllText(moduleFile);

            var moduleConfig = JsonConvert.DeserializeObject<ModuleConfig>(moduleConfigData);
            moduleConfig.DirectoryPath = dir;
            moduleConfig.FilePath = moduleFile;
            moduleConfig.Version_Internal = Parser.getUInt32FromIP(moduleConfig.Version);

            foreach (var asm in moduleConfig.Assemblies)
            {
                var path = Path.Combine(dir, asm.Path.Trim('\\', '/'));
                ModuleHook.registerAssemblyPath(path, asm.Load_As_Byte_Array);
            }
            var module = new Module(moduleConfig);
            ModuleHook.modules.Add(module);
            return true;
        }
    }
}