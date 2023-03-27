using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class StunProjectile : Bullet
{
    #region Properties

    // private StunProjectileDef? Def => def as StunProjectileDef;

    private StunProjectileModExtension? ModExt => def.GetModExtension<StunProjectileModExtension>();

    #endregion


    #region Overwrites

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        if (hitThing is Pawn { Downed: false } hitPawn)
        {
            var resMult = GetResistanceMult(hitPawn);

            if (Rand.Chance(GetStunChance(hitPawn, resMult)))
            {
                var stunDuration = GetStunDuration(hitPawn, resMult);
                if (stunDuration > 30) // because there isn't much sense to apply stuns with such small durations
                    hitPawn.stances.stunner.StunFor(stunDuration, null, false);
            }
        }

        base.Impact(hitThing, blockedByShield);
    }

    #endregion

    #region Methods

    private float GetResistanceMult(Pawn hitPawn)
    {
        return ModExt?.ResistantFleshTypes is { } resistantDict
               && resistantDict.ContainsKey(hitPawn.RaceProps.FleshType)
               && resistantDict[hitPawn.RaceProps.FleshType] is float resMult and >= 0 and < 1
            ? resMult
            : -1f;
    }

    private int GetStunDuration(Pawn hitPawn, float resMult)
    {
        var stunDuration = (float)(ModExt?.BaseStunDuration >= 0 ? ModExt.BaseStunDuration : DamageAmount * 8);

        if (resMult is not -1f)
            stunDuration *= resMult;

        return (int)(stunDuration / hitPawn.BodySize);
    }

    private float GetStunChance(Pawn hitPawn, float resMult)
    {
        if (ModExt?.BaseStunChance >= 0)
            return ModExt.BaseStunChance;

        var chance = DamageAmount * 0.035f / hitPawn.BodySize;

        if (resMult is not -1f)
            chance *= resMult;

        return chance;
    }

    #endregion
}