using System.Collections.Generic;
using RimWorld;
using Verse;

namespace ArsenalHarmony;

public class StunProjectileModExtension : DefModExtension
{
    public float BaseStunChance = -1f;
    public int BaseStunDuration = -1;
    public Dictionary<FleshTypeDef, float>? ResistantFleshTypes;
}