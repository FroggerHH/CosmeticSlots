using HarmonyLib;
using static CosmeticSlots.Plugin;
using static ItemDrop.ItemData.ItemType;

namespace CosmeticSlots;

[HarmonyPatch]
internal class EquipItemCosmeticPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void PatchEquip(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects,
        ref bool __result)
    {
        // if (__instance is not Player { } player) return;

        if ((item.m_shared.m_itemType == Chest && __instance.GetAdditionalData().m_chestCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_CHEST)
            __instance.UnequipItem(__instance.GetAdditionalData().m_chestCosmeticItem, triggerEquipEffects);

        if ((item.m_shared.m_itemType == Helmet && __instance.GetAdditionalData().m_helmetCosmeticItem == null) ||
            item.m_shared.m_itemType == COSMETIC_HELMET)
            __instance.UnequipItem(__instance.GetAdditionalData().m_helmetCosmeticItem, triggerEquipEffects);

        if (item.m_shared.m_itemType == COSMETIC_CHEST)
            __instance.GetAdditionalData().m_chestCosmeticItem = item;
        else if (item.m_shared.m_itemType == COSMETIC_HELMET)
            __instance.GetAdditionalData().m_helmetCosmeticItem = item;

        if (__instance.IsItemEquiped(item))
        {
            item.m_equipped = true;
            __result = true;
        }

        __instance.SetupEquipment();
        if (triggerEquipEffects)
            __instance.TriggerEquipEffect(item);
    }

    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.UnequipItem))] [HarmonyPostfix]
    public static void PatchUnequipItem(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects)
    {
        if (!__instance || item == null) return;

        if (__instance.GetAdditionalData().m_chestCosmeticItem == item)
            __instance.GetAdditionalData().m_chestCosmeticItem = null;
        else if (__instance.GetAdditionalData().m_helmetCosmeticItem == item)
            __instance.GetAdditionalData().m_helmetCosmeticItem = null;

        __instance.SetupEquipment();
        if (triggerEquipEffects)
            __instance.TriggerEquipEffect(item);
    }
}