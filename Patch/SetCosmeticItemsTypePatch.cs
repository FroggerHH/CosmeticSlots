using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemDrop;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;

namespace CosmeticSlots;

[HarmonyPatch]
internal class SetCosmeticItemsTypePatch
{
    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake)), HarmonyPostfix]
    public static void Postfix(ZNetScene __instance)
    {
        var hildir = __instance.GetPrefab("Hildir").GetComponent<Trader>();
        var itemDrops = hildir.m_items;

        foreach (var drop in itemDrops)
        {
            var sharedData = drop.m_prefab.m_itemData.m_shared;

            if (sharedData.m_itemType == Chest)
            {
                sharedData.m_itemType = (ItemType)(30);
            }
            else if (sharedData.m_itemType == Helmet)
            {
                sharedData.m_itemType = (ItemType)(31);
            }
        }
    }
}