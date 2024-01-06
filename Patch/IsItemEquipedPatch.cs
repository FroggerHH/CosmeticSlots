namespace CosmeticSlots.Patch;

[HarmonyPatch] internal class IsItemEquipedPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.IsItemEquiped))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void HumanoidIsItemEquiped(Humanoid __instance, ItemData item, ref bool __result)
    {
        var data = __instance.GetAdditionalData();
        __result = __result ||
                   data.helmetCosmeticItem == item ||
                   data.chestCosmeticItem == item ||
                   data.legsCosmeticItem == item ||
                   data.capeCosmeticItem == item;
    }

    [HarmonyPatch(typeof(ItemData), nameof(ItemData.IsEquipable))] [HarmonyPostfix]
    public static void ItemDataIsEquipable(ItemData __instance, ref bool __result)
    {
        var itemType = __instance.m_shared.m_itemType;
        __result = __result ||
                   itemType == COSMETIC_CHEST ||
                   itemType == COSMETIC_HELMET ||
                   itemType == COSMETIC_LEGS ||
                   itemType == COSMETIC_CAPE;
    }
}