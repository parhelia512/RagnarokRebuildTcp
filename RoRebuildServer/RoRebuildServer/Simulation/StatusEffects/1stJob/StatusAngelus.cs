﻿using RebuildSharedData.Enum;
using RebuildSharedData.Enum.EntityStats;
using RoRebuildServer.EntityComponents;
using RoRebuildServer.Simulation.StatusEffects.Setup;

namespace RoRebuildServer.Simulation.StatusEffects._1stJob;

[StatusEffectHandler(CharacterStatusEffect.Angelus, StatusClientVisibility.Owner)]
public class StatusAngelus : StatusEffectBase
{
    //value1 = skill level
    //value2 = level of divine protection learned by the caster
    //value3 = soft def added percent
    public override void OnApply(CombatEntity ch, ref StatusEffectState state)
    {
        var lvl = state.Value1;
        state.Value3 = (short)(lvl * 5);
        ch.AddStat(CharacterStat.AddSoftDefPercent, state.Value3);

        if (ch.Character.Type == CharacterType.Player)
        {
            //Angelus transfers the bonus of divine protection (the level stored in value2),
            //but we don't want it to stack if the player already has divine protection.
            var currentDp = ch.Player.MaxLearnedLevelOfSkill(CharacterSkill.DivineProtection);
            state.Value2 -= currentDp;
            if (state.Value2 <= 0)
            {
                state.Value2 = 0;
                return;
            }
            if (state.Value2 > lvl)
                state.Value2 = lvl;

            ch.AddStat(CharacterStat.AddResistRaceDemon, state.Value2);
            ch.AddStat(CharacterStat.AddResistRaceUndead, state.Value2);
        }
    }
    
    public override void OnExpiration(CombatEntity ch, ref StatusEffectState state)
    {
        ch.SubStat(CharacterStat.AddSoftDefPercent, state.Value3);
        ch.SubStat(CharacterStat.AddResistRaceDemon, state.Value2);
        ch.SubStat(CharacterStat.AddResistRaceUndead, state.Value2);
    }
}