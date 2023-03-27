using Verse;

namespace ArsenalHarmony.Utilities;

public static class StunnerUtility
{
    #region Main
    public static void StunPawn(Pawn hitPawn, StunProjectileModExtension modExt, int dmgAmount)
    {
        var resMult = GetResistanceMult(hitPawn, modExt);

        if (Rand.Chance(GetStunChance(hitPawn, resMult, modExt, dmgAmount)))
        {
            var stunDuration = GetStunDuration(hitPawn, resMult, modExt, dmgAmount);
            if (stunDuration >
                MinStunDuration) // because there isn't much sense to apply stuns with such small durations
                hitPawn.stances.stunner.StunFor(stunDuration, null, false);
            
        }
    }

    #endregion

    #region Constants

    private const int MinStunDuration = 30;
    private const int StunDurationMult = 8;
    private const float StunChanceMult = 0.035f;

    #endregion

    #region Getters

    private static float GetResistanceMult(Pawn hitPawn, StunProjectileModExtension modExt)
    {
        return modExt?.ResistantFleshTypes is { } resistantDict
               && resistantDict.ContainsKey(hitPawn.RaceProps.FleshType)
               && resistantDict[hitPawn.RaceProps.FleshType] is float resMult and >= 0 and < 1
            ? resMult
            : -1f;
    }

    private static int GetStunDuration(Pawn hitPawn, float resMult, StunProjectileModExtension modExt, int dmgAmount)
    {
        var stunDuration =
            (float)(modExt?.BaseStunDuration >= 0 ? modExt.BaseStunDuration : dmgAmount * StunDurationMult);

        if (resMult is not -1f)
            stunDuration *= resMult;

        return (int)(stunDuration / hitPawn.BodySize);
    }

    private static float GetStunChance(Pawn hitPawn, float resMult, StunProjectileModExtension modExt, int dmgAmount)
    {
        if (modExt?.BaseStunChance >= 0)
            return modExt.BaseStunChance;

        var chance = dmgAmount * StunChanceMult / hitPawn.BodySize;

        if (resMult is not -1f)
        {
            chance *= resMult;
        }

        return chance;
    }

    #endregion
}