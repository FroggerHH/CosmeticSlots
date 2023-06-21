using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemDrop;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;


namespace CosmeticSlots;

[HarmonyPatch]
internal class ItemStandCosmeticsPatch
{
    [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.Awake)), HarmonyPostfix]
    public static void Patch(ItemStand __instance)
    {
        __instance.m_supportedTypes.Add((ItemType)(30));
        __instance.m_supportedTypes.Add((ItemType)(31));
    }
}