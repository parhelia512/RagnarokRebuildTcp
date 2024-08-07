﻿using RebuildSharedData.Data;
using RebuildSharedData.Enum;
using RoRebuildServer.EntityComponents;
using RoRebuildServer.EntityComponents.Util;
using RoRebuildServer.Networking;

namespace RoRebuildServer.Simulation.Skills.SkillHandlers.Merchant;

[SkillHandler(CharacterSkill.Mammonite, SkillClass.Physical)]
public class MammoniteHandler : SkillHandlerBase
{
    public override void Process(CombatEntity source, CombatEntity? target, Position position, int lvl, bool isIndirect)
    {
        if (lvl < 0 || lvl > 10)
            lvl = 10;

        if (target == null || !target.IsValidTarget(source))
            return;

        //if (source.Entity.Type == EntityType.Monster && lvl == 10)
        //    lvl = 20;
            
        var res = source.CalculateCombatResult(target, 1f + lvl * 0.5f, 1, AttackFlags.Physical, CharacterSkill.Mammonite);
        source.ApplyCooldownForAttackAction(target);
        source.ExecuteCombatResult(res, false);

        var ch = source.Character;

        ch.Map?.AddVisiblePlayersAsPacketRecipients(ch);
        CommandBuilder.SkillExecuteTargetedSkill(source.Character, target.Character, CharacterSkill.Mammonite, lvl, res);
        CommandBuilder.ClearRecipients();
    }
}