using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemDrop;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;


namespace CosmeticSlots;

[HarmonyPatch]
internal class IsItemEquipedPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.IsItemEquiped)), HarmonyPostfix]
    public static void HumanoidIsItemEquiped(Humanoid __instance, ItemDrop.ItemData item, ref bool __result)
    {
        if (__instance is not Player { } player) return;

        __result = __result || player.GetAdditionalData().m_helmetCosmeticItem == item ||
                   player.GetAdditionalData().m_chestCosmeticItem == item;
    }

    [HarmonyPatch(typeof(ItemData), nameof(ItemData.IsEquipable)), HarmonyPostfix]
    public static void ItemDataIsEquipable(ItemDrop.ItemData __instance, ref bool __result)
    {
        __result = __result || __instance.m_shared.m_itemType == (ItemType)(30) ||
                   __instance.m_shared.m_itemType == (ItemType)(31);
    }
}