using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class StunProjectile : Bullet
{
    #region Properties

    private StunProjectileDef? Def => def as StunProjectileDef;

    // private StunProjectileModExtension? ModExt => def.GetModExtension<StunProjectileModExtension>();

    #endregion

    #region Overwrites

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        if (hitThing is Pawn { Downed: false } hitPawn)
        {
            var effectivenessMult = GetEffectivenessMult(hitPawn);

            if (Rand.Chance(GetStunChance(hitPawn, effectivenessMult)))
            {
                var stunDuration = GetStunDuration(hitPawn, effectivenessMult);
                if (stunDuration > MinStunDuration)
                    hitPawn.stances.stunner.StunFor(stunDuration, null, false);
            }
        }

        base.Impact(hitThing, blockedByShield);
    }

    #endregion

    #region Constants

    private const int MinStunDuration = 30;
    private const int StunDurationMult = 8;
    private const float StunChanceMult = 0.035f;

    #endregion

    #region Methods

    private float GetEffectivenessMult(Pawn hitPawn)
    {
        return Def?.FleshTypeEffectivenessMultipliers is { } effectivenessMultDict
               && effectivenessMultDict.ContainsKey(hitPawn.RaceProps.FleshType)
               && effectivenessMultDict[hitPawn.RaceProps.FleshType] is float resMult and >= 0 and < 1
            ? resMult
            : -1f;
    }

    private int GetStunDuration(Pawn hitPawn, float effectivenessMult)
    {
        var stunDuration = (float)(Def?.BaseStunDuration >= 0 ? Def.BaseStunDuration : DamageAmount * StunDurationMult);

        if (effectivenessMult is not -1f)
            stunDuration *= effectivenessMult;

        return (int)(stunDuration / hitPawn.BodySize);
    }

    private float GetStunChance(Pawn hitPawn, float resMult)
    {
        if (Def?.BaseStunChance >= 0)
            return Def.BaseStunChance;

        var chance = DamageAmount * StunChanceMult / hitPawn.BodySize;

        if (resMult is not -1f)
            chance *= resMult;

        return chance;
    }

    #endregion
}