using System.Collections.Generic;
using HarmonyLib;
using ItemManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using WhoBuiltIt;

namespace TheFarmer;

[HarmonyPatch]
internal class FixTraderPatch
{
    [HarmonyPatch(typeof(Trader), nameof(Trader.Start)), HarmonyPrefix]
    public static bool PatchStart(Trader __instance)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.StartTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.Update)), HarmonyPrefix]
    public static bool PatchUpdate(Trader __instance)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.UpdateTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.RandomTalk)), HarmonyPrefix]
    public static bool PatchRandomTalk(Trader __instance)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.RandomTalkTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.GetHoverText)), HarmonyPrefix]
    public static bool PatchGetHoverText(Trader __instance, ref string __result)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            __result = farmerTrader.GetHoverTextTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.GetHoverName)), HarmonyPrefix]
    public static bool PatchGetHoverName(Trader __instance, ref string __result)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            __result = farmerTrader.GetHoverNameTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.Interact)), HarmonyPrefix]
    public static bool PatchInteract(Trader __instance, Humanoid character, bool hold, bool alt, ref bool __result)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            __result = farmerTrader.InteractTheFarmer(character, hold, alt);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.DiscoverItems)), HarmonyPrefix]
    public static bool PatchDiscoverItems(Trader __instance, Player player)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.DiscoverItemsTheFarmer(player);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.Say), typeof(List<string>), typeof(string)), HarmonyPrefix]
    public static bool PatchSay1(Trader __instance, List<string> texts, string trigger)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.SayTheFarmer(texts, trigger);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.Say), typeof(string), typeof(string)), HarmonyPrefix]
    public static bool PatchSay2(Trader __instance, string text, string trigger)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.SayTheFarmer(text, trigger);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.UseItem)), HarmonyPrefix]
    public static bool PatchUseItem(Trader __instance, Humanoid user, ItemDrop.ItemData item, ref bool __result)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            __result = farmerTrader.UseItemTheFarmer(user, item);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.OnBought)), HarmonyPrefix]
    public static bool PatchOnBought(Trader __instance, Trader.TradeItem item)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.OnBoughtTheFarmer(item);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.OnSold)), HarmonyPrefix]
    public static bool PatchOnSold(Trader __instance)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            farmerTrader.OnSoldTheFarmer();
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(Trader), nameof(Trader.GetAvailableItems)), HarmonyPrefix]
    public static bool Patch(Trader __instance, ref List<Trader.TradeItem> __result)
    {
        if (__instance is TheFarmerTrader farmerTrader)
        {
            __result = farmerTrader.GetAvailableItemsTheFarmer();
            return false;
        }

        return true;
    }
}