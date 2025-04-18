﻿using RebuildSharedData.Enum;
using RebuildSharedData.Enum.EntityStats;
using RoRebuildServer.EntityComponents;
using RoRebuildServer.EntityComponents.Character;
using RoRebuildServer.Simulation.StatusEffects.Setup;

namespace RoRebuildServer.Simulation.StatusEffects.ItemEffects;

[StatusEffectHandler(CharacterStatusEffect.IncreasedMoveSpeed, StatusClientVisibility.Owner, StatusEffectFlags.None, "SpeedBonus")]
public class StatusMoveSpeedIncrease : StatusEffectBase
{
    public override void OnApply(CombatEntity ch, ref StatusEffectState state)
    {
        ch.AddStat(CharacterStat.MoveSpeedBonus, state.Value1);
    }

    public override void OnExpiration(CombatEntity ch, ref StatusEffectState state)
    {
        ch.SubStat(CharacterStat.MoveSpeedBonus, state.Value1);
    }
}