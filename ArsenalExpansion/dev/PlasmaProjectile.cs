using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class PlasmaProjectile : Bullet
{
    #region Overrides

    protected override void Impact(Thing hitThing, bool blockedByShield = false)
    {
        if (Rand.Chance(CauseFireChance) && FireStrength >= 0.01f)
        {
            if (hitThing == null)
                FireUtility.TryStartFireIn(destination.ToIntVec3(), launcher.Map, FireStrength);
            else
                hitThing.TryAttachFire(FireStrength);
        }
        // float baseDamage = this.Def.projectile.GetDamageAmount(1f, null); // can be used to get base def damage

        base.Impact(hitThing, blockedByShield);
    }

    #endregion

    #region Properties

    private PlasmaProjectileModExtension? ModExt => def.GetModExtension<PlasmaProjectileModExtension>();

    private float CauseFireChance =>
        ModExt?.BaseCauseFireChance >= 0 ? ModExt.BaseCauseFireChance : DamageAmount * 0.015f;

    private float FireStrength =>
        ModExt?.BaseFireStrength >= 0 ? ModExt.BaseFireStrength : DamageAmount * 0.01f;

    #endregion
}