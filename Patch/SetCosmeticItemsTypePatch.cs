using HarmonyLib;
using UnityEngine;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;

namespace CosmeticSlots;

[HarmonyPatch]
internal class SetCosmeticItemsTypePatch
{

    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake)), HarmonyPostfix, HarmonyWrapSafe]
    public static void Postfix(ZNetScene __instance)
    {
        var hildirObj = __instance.GetPrefab("Hildir");
        if (!hildirObj)
        {
            Object.Destroy(CosmeticSlotsPlugin._self);
            return;
        }

        var tradeItems = hildirObj.GetComponent<Trader>().m_items;

        foreach (var trade in tradeItems)
        {
            var sharedData = trade?.m_prefab?.m_itemData?.m_shared;
            if (sharedData == null) continue;

            if (sharedData.m_itemType == Chest) sharedData.m_itemType = CosmeticSlotsPlugin.COSMETIC_CHEST;
            else if (sharedData.m_itemType == Helmet) sharedData.m_itemType = CosmeticSlotsPlugin.COSMETIC_HELMET;
        }
    }
}