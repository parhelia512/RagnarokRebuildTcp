﻿using RebuildSharedData.Data;
using RebuildSharedData.Enum;
using RebuildSharedData.Networking;
using RoRebuildServer.EntityComponents.Character;
using RoRebuildServer.EntityComponents.Util;
using RoRebuildServer.Simulation;
using System.Diagnostics;
using RebuildSharedData.Enum.EntityStats;

namespace RoRebuildServer.Networking.PacketHandlers.Character;

[ClientPacketHandler(PacketType.Respawn)]
public class PacketRespawn : IClientPacketHandler
{
    public void Process(NetworkConnection connection, InboundMessage msg)
    {
        if (!connection.IsConnectedAndInGame)
            return;

        Debug.Assert(connection.Player != null);
        Debug.Assert(connection.Character != null);
        Debug.Assert(connection.Character.Map != null);

        var inPlace = msg.ReadByte() == 1;
        
        var player = connection.Player;
        var ce = player.CombatEntity;
        var ch = connection.Character;

        if (player.InActionCooldown())
            return;

        if (ch.State != CharacterState.Dead)
            return;

        if (!player.IsAdmin)
            inPlace = false;
        
        ch.ResetState(true);
        ch.SetSpawnImmunity();
        ce.ClearDamageQueue();

        if (inPlace)
        {
            var recoverHp = ce.GetStat(CharacterStat.MaxHp);
            ce.SetStat(CharacterStat.Hp, recoverHp);
        }
        else
        {
            if (ce.GetStat(CharacterStat.Hp) <= 0)
                ce.SetStat(CharacterStat.Hp, 1);
            player.ResetRegenTickTime();
        }

        if (!inPlace)
            player.ReturnToSavePoint();
        else
        {
            player.AddActionDelay(CooldownActionType.Click);

            ch.Map.AddVisiblePlayersAsPacketRecipients(ch);
            CommandBuilder.SendPlayerResurrection(ch);
            CommandBuilder.ClearRecipients();
        }
    }
}