﻿using Assets.Scripts.Effects.EffectHandlers;
using Assets.Scripts.Network;
using RebuildSharedData.Enum;
using UnityEngine;

namespace Assets.Scripts.SkillHandlers.Handlers
{
    [SkillHandler(CharacterSkill.GrandThunderstorm)]
    public class GrandThunderstormHandler : SkillHandlerBase
    {
        public override void StartSkillCasting(ServerControllable src, ServerControllable target, int lvl, float castTime)
        {
            src.AttachEffect(CastEffect.Create(castTime, "ring_yellow", src.gameObject));
        }

        public override void ExecuteSkillGroundTargeted(ServerControllable src, Vector2Int target, int lvl)
        {
            //DefaultSkillCastEffect.Create(src);
            src.PerformBasicAttackMotion();
        }
    }
}