﻿using System.Collections.ObjectModel;
using System.Reflection;
using RebuildSharedData.ClientTypes;
using RebuildSharedData.Enum;
using RoRebuildServer.Data.Player;
using RoRebuildServer.Logging;

namespace RoRebuildServer.Data.ServerConfigScript;

public class ServerConfigScriptAttribute(string name) : Attribute
{
    public readonly string ConfigName = name;
}

public class ServerConfigScriptManager
{
    private readonly List<ServerConfigScriptHandlerBase> handlers = new();

    public ServerConfigScriptManager(Assembly assembly)
    {
        var itemType = typeof(ServerConfigScriptHandlerBase);
        foreach (var type in assembly.GetTypes().Where(t => t.IsAssignableTo(itemType)))
        {
            if (type == itemType)
                continue;
            var handler = (ServerConfigScriptHandlerBase)Activator.CreateInstance(type)!;
            handlers.Add(handler);
        }
    }

    public int UpdateDropData(ItemClass type, string code, string subType, int rate)
    {
        foreach (var c in handlers)
            rate = c.OnLoadDropData(type, code, subType, rate);

        if (rate > 10000)
            rate = 10000;

        return rate;
    }

    public void UpdateItemValue(ReadOnlyDictionary<int, ItemInfo> itemList)
    {
        foreach (var (_, item) in itemList)
        {
            var price = item.Price;

            foreach (var c in handlers)
                price = c.OnSetItemPurchasePrice(item.ItemClass, item.Code, item.SubCategory, price);
            
            var sellValue = price;
            foreach (var c in handlers)
                sellValue = c.OnSetItemSaleValue(item.ItemClass, item.Code, item.SubCategory, sellValue);

            if (price == sellValue)
                sellValue /= 2;
            if (sellValue * 125 / 100 > price * 75 / 100)
            {
                ServerLogger.LogError($"Attempting to update the pricing for {item.Code} to have a price of {price} and npc sell value of {sellValue}, but it could be bought/sold at a profit. Forcing npc sell price to be 0.");
                sellValue = 0;
            }

            item.Price = price;
            item.SellToStoreValue = sellValue;
        }
    }
}


public abstract class ServerConfigScriptHandlerBase
{
    public virtual void Init() {}
    public virtual int OnLoadDropData(ItemClass type, string code, string subType, int rate) => rate;
    public virtual int OnSetItemPurchasePrice(ItemClass type, string code, string subType, int price) => price;
    public virtual int OnSetItemSaleValue(ItemClass type, string code, string subType, int price) => price;
}