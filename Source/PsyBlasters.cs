using System;
using System.Reflection;
using HarmonyLib;
using Verse;

namespace PsyBlasters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HotSwappableAttribute : Attribute
    {
    }
    
    public class PsyBlasters : Mod
    {
        public PsyBlasters(ModContentPack content) : base(content)
        {
            var harmony = new Harmony("com.arquebus.rimworld.mod.psyblasters");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}