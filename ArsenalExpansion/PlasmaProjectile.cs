using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class PlasmaProjectile : Bullet
{
    #region Overrides

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        if (Rand.Chance(CauseFireChance) && FireStrength >= MinFireStrength)
        {
            if (hitThing == null)
                FireUtility.TryStartFireIn(destination.ToIntVec3(), launcher.Map, FireStrength);
            else
                hitThing.TryAttachFire(FireStrength);
        }

        base.Impact(hitThing, blockedByShield);
    }

    #endregion

    #region Constants

    private const float MinFireStrength = 0.01f;
    private const float FireStrengthMult = 0.01f;
    private const float FireChanceMult = 0.015f;

    #endregion

    #region Properties

    // private PlasmaProjectileModExtension? ModExt => def.GetModExtension<PlasmaProjectileModExtension>();
    private PlasmaProjectileDef? Def => def as PlasmaProjectileDef;

    private float CauseFireChance =>
        Def?.BaseCauseFireChance >= 0 ? Def.BaseCauseFireChance : DamageAmount * FireChanceMult;

    private float FireStrength =>
        Def?.BaseFireStrength >= 0 ? Def.BaseFireStrength : DamageAmount * FireStrengthMult;

    #endregion
}