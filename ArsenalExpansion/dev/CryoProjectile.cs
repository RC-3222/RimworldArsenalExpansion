using RimWorld;
using Verse;

namespace ArsenalExpansion;

// TODO: Should think about chance depending on target's bodysize, and also should learn more about frostbite dmg
public class CryoProjectile : Bullet
{
    #region Overwrites

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        if (hitThing is Pawn hitPawn 
            && hitPawn.RaceProps.FleshType == FleshTypeDefOf.Normal
            && Rand.Chance(HypothermiaChance))
        {
            //hitPawn.TakeDamage(new DamageInfo(DamageDefOf.Frostbite, DamageAmount * 0.4f));
            HealthUtility.AdjustSeverity(hitPawn, HediffDefOf.Hypothermia, HypothermiaSeverity);
        }
        
        base.Impact(hitThing, blockedByShield);
    }

    #endregion

    #region Properties

    private CryoProjectileDef? Def => def as CryoProjectileDef;

    private float HypothermiaSeverity =>
        Def?.BaseHypothermiaSeverity >= 0 ? Def.BaseHypothermiaSeverity : DamageAmount * 0.005f;

    private float HypothermiaChance
    {
        get
        {
            if (Def?.BaseHypothermiaChance >= 0)
                return Def.BaseHypothermiaChance;

            var chance = DamageAmount * 0.02f;

            return chance < 1.0f ? chance : 1.0f;
        }
    }

    #endregion
}