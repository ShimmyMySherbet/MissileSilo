using SDG.Unturned;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace MissileSilo.API.Utils
{
    public static class PathTool
    {
        public static string UnturnedDir { get; private set; }
        public static string ServerDir { get; private set; }
        public static string SiloPath { get; private set; }
        public static string AssemblyCache => Path.Combine(SiloPath, "Cache");
        public static string RocketFolder => Path.Combine(ServerDir, "Rocket");
        public static string ExtrasFolder => Path.Combine(UnturnedDir, "Extras");
        public static string RocketPlugins => Path.Combine(RocketFolder, "Plugins");
        public static string RocketLibraries => Path.Combine(RocketFolder, "Libraries");
        public static string Extras_RocketmodFolder => Path.Combine(ExtrasFolder, "Rocket.Unturned");

        public static void Init()
        {
            UnturnedDir = new DirectoryInfo(Application.dataPath).Parent.FullName;

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            if (codeBase.StartsWith("file:///"))
            {
                codeBase = codeBase.Remove(0, 8);
            }
            var siloInfo = new FileInfo(codeBase);
            SiloPath = siloInfo.DirectoryName;

            ServerDir = Path.Combine(UnturnedDir, "Servers", Provider.serverID);

            if (!Directory.Exists(AssemblyCache))
            {
                Directory.CreateDirectory(AssemblyCache);
            }
        }
    }
}