namespace CosmeticSlots.Patch;

[HarmonyPatch]
internal class EquipItemCosmeticPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void PatchEquip(Humanoid __instance, ItemData item, bool triggerEquipEffects,
        ref bool __result)
    {
        var data = __instance.GetAdditionalData();
        if ((item.m_shared.m_itemType == Chest && data.chestCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_CHEST)
            __instance.UnequipItem(data.chestCosmeticItem, triggerEquipEffects);

        if ((item.m_shared.m_itemType == Helmet && data.helmetCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_HELMET)
            __instance.UnequipItem(data.helmetCosmeticItem, triggerEquipEffects);

        if ((item.m_shared.m_itemType == Legs && data.legsCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_LEGS)
            __instance.UnequipItem(data.legsCosmeticItem, triggerEquipEffects);

        if ((item.m_shared.m_itemType == Shoulder && data.capeCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_CAPE)
            __instance.UnequipItem(data.capeCosmeticItem, triggerEquipEffects);

        if (item.m_shared.m_itemType == COSMETIC_CHEST)
            data.chestCosmeticItem = item;
        else if (item.m_shared.m_itemType == COSMETIC_HELMET)
            data.helmetCosmeticItem = item;
        else if (item.m_shared.m_itemType == COSMETIC_LEGS)
            data.legsCosmeticItem = item;
        else if (item.m_shared.m_itemType == COSMETIC_CAPE)
            data.capeCosmeticItem = item;

        if (__instance.IsItemEquiped(item))
        {
            item.m_equipped = true;
            __result = true;
        }

        __instance.SetupEquipment();
        if (triggerEquipEffects) __instance.TriggerEquipEffect(item);
    }

    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.UnequipItem))] [HarmonyPostfix]
    public static void PatchUnequipItem(Humanoid __instance, ItemData item, bool triggerEquipEffects)
    {
        if (!__instance || item == null) return;

        if (__instance.GetAdditionalData().chestCosmeticItem == item)
            __instance.GetAdditionalData().chestCosmeticItem = null;
        else if (__instance.GetAdditionalData().helmetCosmeticItem == item)
            __instance.GetAdditionalData().helmetCosmeticItem = null;
        else if (__instance.GetAdditionalData().legsCosmeticItem == item)
            __instance.GetAdditionalData().legsCosmeticItem = null;
        else if (__instance.GetAdditionalData().capeCosmeticItem == item)
            __instance.GetAdditionalData().capeCosmeticItem = null;

        __instance.SetupEquipment();
        if (triggerEquipEffects) __instance.TriggerEquipEffect(item);
    }
}