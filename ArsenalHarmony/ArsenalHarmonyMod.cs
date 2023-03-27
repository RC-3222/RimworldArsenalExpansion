using HarmonyLib;
using Verse;

namespace ArsenalHarmony;

[StaticConstructorOnStartup]
public static class ArsenalHarmonyMod
{
    static ArsenalHarmonyMod()
    {
        var harmony = new Harmony("AmP.ArsenalExpansion");
        harmony.PatchAll();
    }
}