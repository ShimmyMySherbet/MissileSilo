using HarmonyLib;
using MissileSilo.Rocket.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MissileSilo.Rocket.Utils
{
    public static class TypeOverrider
    {
        private static Harmony m_Harmony = new Harmony("MissileSilo.Unturned.TypeRedirects");

        public static void PatchRedirectType<A, B>() => PatchRedirectType(typeof(A), typeof(B));
        public static void PatchRedirectType(Type a, Type b)
        {
            var methods = new List<MatchPair<MethodInfo>>();

            foreach (var src in a.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance))
            {
                foreach (var trg in b.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                {
                    if (src.Name.Equals(trg.Name, StringComparison.InvariantCultureIgnoreCase)
                        && src.ReturnType == trg.ReturnType
                        && ParmetersMatch(src, trg))
                    {
                        methods.Add(new MatchPair<MethodInfo>(src, trg));
                    }
                }
            }

            foreach (var m in methods)
            {
                try
                {
                    m_Harmony.Patch(m.Source, new HarmonyMethod(m.Replacement));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        /// <param name="a">Source</param>
        /// <param name="b">Replacement</param>
        /// <returns></returns>
        private static bool ParmetersMatch(MethodInfo a, MethodInfo b)
        {
            var aParam = a.GetParameters();
            var bParams = b.GetParameters();

            foreach (var rep in bParams)
            {
                if (rep.Name.StartsWith("__"))
                {
                    continue;
                }

                foreach (var src in aParam)
                {
                    if (src.Name == rep.Name && src.ParameterType == rep.ParameterType)
                    {
                        continue;
                    }
                }
                return false;
            }

            return true;
        }
    }
}