using HarmonyLib;
using static ItemDrop;
using static ItemDrop.ItemData;

namespace CosmeticSlots;

[HarmonyPatch]
internal class IsItemEquipedPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.IsItemEquiped))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void HumanoidIsItemEquiped(Humanoid __instance, ItemData item, ref bool __result)
    {
        // if (__instance is not Player { } player) return;

        __result = __result || __instance.GetAdditionalData().m_helmetCosmeticItem == item ||
                   __instance.GetAdditionalData().m_chestCosmeticItem == item;
    }

    [HarmonyPatch(typeof(ItemData), nameof(ItemData.IsEquipable))] [HarmonyPostfix]
    public static void ItemDataIsEquipable(ItemData __instance, ref bool __result)
    {
        __result = __result || __instance.m_shared.m_itemType == (ItemType)30 ||
                   __instance.m_shared.m_itemType == (ItemType)31;
    }
}