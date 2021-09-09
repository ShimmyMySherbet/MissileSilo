using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace MissileSilo.API.Models
{
    public static class AssemblyCache
    {
        private static ConcurrentDictionary<string, Assembly> m_Assemblies = new ConcurrentDictionary<string, Assembly>();

        public static void Init()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            Console.WriteLine($"Requesting {args.Name}");
            if (m_Assemblies.TryGetValue(args.Name, out var asm))
            {
                return asm;
            }
            return null;
        }

        public static void RegisterAssembly(Assembly asm)
        {
            if (m_Assemblies.TryAdd(asm.FullName, asm))
            {
                Console.WriteLine($"Registered {asm.FullName}");
            } else
            {
                Console.WriteLine($"Failed to register {asm.FullName}");

            }
        }
    }
}