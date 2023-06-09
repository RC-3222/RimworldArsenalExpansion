﻿using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ArsenalExpansion;

public class StunProjectileDef : ThingDef
{
    public float BaseStunChance = -1f;
    public int BaseStunDuration = -1;
    public Dictionary<FleshTypeDef, float>? FleshTypeEffectivenessMultipliers;
}