using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class HediffApplyingProjectileDef
{
    public Hediff? HediffToApply;
    public float BaseChance = -1f;
    public float BaseSeverity = -1f;

    public List<FleshTypeDef>? AffectedFleshTypes;
}