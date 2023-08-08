using HarmonyLib;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;

namespace CosmeticSlots;

[HarmonyPatch]
internal class EquipItemCosmeticPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.EquipItem)), HarmonyPostfix, HarmonyWrapSafe]
    public static void PatchEquip(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects,
        ref bool __result)
    {
        if (__instance is not Player { } player) return;

        if ((item.m_shared.m_itemType == Chest && player.GetAdditionalData().m_chestCosmeticItem == null) ||
            item.m_shared.m_itemType == (ItemType)(30))
            player.UnequipItem(player.GetAdditionalData().m_chestCosmeticItem, triggerEquipEffects);

        if ((item.m_shared.m_itemType == Helmet && player.GetAdditionalData().m_helmetCosmeticItem == null) ||
            item.m_shared.m_itemType == (ItemType)(31))
            player.UnequipItem(player.GetAdditionalData().m_helmetCosmeticItem, triggerEquipEffects);

        if (item.m_shared.m_itemType == (ItemType)(30))
        {
            player.GetAdditionalData().m_chestCosmeticItem = item;
        }
        else if (item.m_shared.m_itemType == (ItemType)(31))
        {
            player.GetAdditionalData().m_helmetCosmeticItem = item;
        }

        if (player.IsItemEquiped(item))
        {
            item.m_equipped = true;
            __result = true;
        }

        player.SetupEquipment();
        if (triggerEquipEffects)
            player.TriggerEquipEffect(item);
    }

    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.UnequipItem)), HarmonyPostfix]
    public static void PatchUnequipItem(Humanoid __instance, ItemDrop.ItemData item, bool triggerEquipEffects)
    {
        if (!__instance || item == null || __instance is not Player { } player) return;

        if (player.GetAdditionalData().m_chestCosmeticItem == item)
            player.GetAdditionalData().m_chestCosmeticItem = null;
        else if (player.GetAdditionalData().m_helmetCosmeticItem == item)
            player.GetAdditionalData().m_helmetCosmeticItem = null;

        __instance.SetupEquipment();
        if (triggerEquipEffects)
            __instance.TriggerEquipEffect(item);
    }
}