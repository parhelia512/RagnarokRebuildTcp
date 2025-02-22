﻿using System;
using Assets.Scripts.Network.HandlerBase;
using Assets.Scripts.Sprites;
using Assets.Scripts.UI.ConfigWindow;
using Assets.Scripts.Utility;
using RebuildSharedData.Networking;
using UnityEngine;

namespace Assets.Scripts.Network.IncomingPacketHandlers.Network
{
    [ClientPacketHandler(PacketType.EnterServer)]
    public class PacketOnEnterServer : ClientPacketHandlerBase
    {
        public override void ReceivePacket(ClientInboundMessage msg)
        {
            var id = msg.ReadInt32();
            var mapName = msg.ReadString();
            var bytes = new byte[16];
            msg.ReadBytes(bytes, 16);
            Network.CharacterGuid = new Guid(bytes);
            PlayerPrefs.SetString("characterid", Network.CharacterGuid.ToString());

            Debug.Log($"We're id {id} on map {mapName} with guid {Network.CharacterGuid}");

            Network.CurrentMap = mapName;
            Network.PlayerId = id;
            
            UiManager.OnLogIn();

            if(ClientDataLoader.Instance.LatestPatchNotes != GameConfig.Data.LastViewedPatchNotes)
                UiManager.Instance.HelpWindow.ShowWindow();
            else
                UiManager.Instance.HelpWindow.HideWindow();
            GameConfig.Data.LastViewedPatchNotes = ClientDataLoader.Instance.LatestPatchNotes;
            
            SceneTransitioner.Instance.LoadScene(Network.CurrentMap, Network.OnMapLoad);

        }
    }
}