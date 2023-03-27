using ArsenalHarmony.Utilities;
using HarmonyLib;
using Verse;

namespace ArsenalHarmony;

[HarmonyPatch(typeof(Projectile), nameof(Projectile.Impact))]
public static class ProjectilePatches
{
    [HarmonyPrefix]
    private static void Prefix(Projectile __instance, Thing hitThing, ref bool blockedByShield)
    {
        if (__instance.def.GetModExtension<StunProjectileModExtension>() is { } modExt && hitThing is Pawn hitPawn)
            StunnerUtility.StunPawn(hitPawn, modExt, __instance.DamageAmount);
    }
}