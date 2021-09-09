using MissileSilo.API.Models;
using MissileSilo.API.Utils;
using Rocket.API;
using Rocket.Core.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using R = Rocket.Core.R;

namespace MissileSilo.Rocket.Models
{
    public class PluginsManager
    {
        private static FieldInfo m_PluginsFieldInfo = typeof(RocketPluginManager).GetField("plugins", BindingFlags.Static | BindingFlags.NonPublic);
        private static List<GameObject> plugins = (List<GameObject>)m_PluginsFieldInfo.GetValue(R.Plugins);

        private void loadPlugins()
        {
            LoadAssembliesFrom(PathTool.RocketLibraries);

            foreach (var pluginDir in Directory.GetDirectories(PathTool.RocketPlugins))
            {
                LoadAssembliesFrom(pluginDir);

                var innerLibPath = Path.Combine(pluginDir, "Libraries");
                if (Directory.Exists(innerLibPath))
                {
                    LoadAssembliesFrom(innerLibPath);
                }
            }

            foreach (var pluginAsm in Directory.GetFiles(PathTool.RocketPlugins, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(pluginAsm));
                    AssemblyCache.RegisterAssembly(assembly);

                    foreach (var pluginType in assembly.GetTypes().Where(x => typeof(IRocketPlugin).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract))
                    {
                        var gObject = new GameObject(pluginType.Name, pluginType);
                        GameObject.DontDestroyOnLoad(gObject);
                        plugins.Add(gObject);
                    }
                }
                catch (BadImageFormatException)
                {
                    Console.WriteLine($"Failed to load invalid assembly: {pluginAsm}");
                }
            }
        }

        private void LoadAssembliesFrom(string dir)
        {
            foreach (var asm in Directory.GetFiles(dir, "*.dll"))
            {
                try
                {
                    var assembly = Assembly.Load(File.ReadAllBytes(asm));
                    AssemblyCache.RegisterAssembly(assembly);
                }
                catch (BadImageFormatException)
                {
                    Console.WriteLine($"Failed to load invalid assembly: {asm}");
                }
            }
        }
    }
}