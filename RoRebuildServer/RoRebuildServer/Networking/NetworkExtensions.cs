﻿using RebuildSharedData.Data;
using RebuildZoneServer.Networking;

namespace RoRebuildServer.Networking;

public static class NetworkExtensions
{
    public static void Write(this OutboundMessage buffer, Position position)
    {
        buffer.Write((short)position.X);
        buffer.Write((short)position.Y);
    }

    public static void Write(this OutboundMessage buffer, FloatPosition position)
    {
        buffer.Write(position.X);
        buffer.Write(position.Y);
    }

    public static void Write(this OutboundMessage buffer, Area area)
    {
        buffer.Write(area.Min);
        buffer.Write((byte)area.Width);
        buffer.Write((byte)area.Height);
    }
}