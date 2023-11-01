using Extensions;
using HarmonyLib;
using UnityEngine;
using static Marketplace.Modules.NPC.Market_NPC;
using static CosmeticSlots.Plugin;
#pragma warning disable CS8509

namespace CosmeticSlots.Patch;

[HarmonyPatch]
internal class DisplayMarketPlace
{
    [HarmonyPatch(typeof(NPCcomponent), nameof(NPCcomponent.EquipItemsOnModel), typeof(SkinnedMeshRenderer),
        typeof(string), typeof(CapsuleCollider[]))] [HarmonyWrapSafe]
    public static class MarketplacePatch
    {
        private static ItemType? itemType;

        [HarmonyPrefix]
        public static void Prefix(NPCcomponent __instance, string prefab)
        {
            if (!prefab.IsGood()) return;
            var itemDrop = ObjectDB.instance.GetItemPrefab(prefab)?.GetComponent<ItemDrop>();
            if (itemDrop == null) return;

            itemType = itemDrop.m_itemData.m_shared.m_itemType;
            itemDrop.m_itemData.m_shared.m_itemType = itemDrop.m_itemData.m_shared.m_itemType switch
            {
                COSMETIC_CHEST => Chest,
                COSMETIC_HELMET => Helmet,
                COSMETIC_LEGS => Legs,
                COSMETIC_CAPE => Shoulder
            };
        }

        [HarmonyPostfix]
        public static void Postfix(NPCcomponent __instance, string prefab)
        {
            if (itemType == null || !prefab.IsGood()) return;
            var itemDrop = ObjectDB.instance.GetItemPrefab(prefab)?.GetComponent<ItemDrop>();
            if (itemDrop == null) return;
            itemDrop.m_itemData.m_shared.m_itemType = itemType.Value;
            itemType = null;
        }
    }
}