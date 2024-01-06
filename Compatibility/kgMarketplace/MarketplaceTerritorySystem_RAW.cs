using static Marketplace.Modules.NPC.Market_NPC;

namespace CosmeticSlots.Compatibility.kgMarketplace;

public static class MarketplaceTerritorySystem_RAW
{
    private static ItemType? itemType;

    public static void Patch()
    {
        DebugWarning("PatchMarket_RAW Patch");
        var equipItemsOnModel = AccessTools.DeclaredMethod(typeof(NPCcomponent),
            nameof(NPCcomponent.EquipItemsOnModel),
            new[] { typeof(SkinnedMeshRenderer), typeof(string), typeof(CapsuleCollider[]) });

        harmony.Patch(equipItemsOnModel,
            new HarmonyMethod(
                AccessTools.DeclaredMethod(typeof(MarketplaceTerritorySystem_RAW), nameof(Prefix))),
            new HarmonyMethod(AccessTools.DeclaredMethod(typeof(MarketplaceTerritorySystem_RAW),
                nameof(Postfix))));
    }


    private static void Prefix(NPCcomponent __instance, string prefab)
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

    private static void Postfix(NPCcomponent __instance, string prefab)
    {
        if (itemType == null || !prefab.IsGood()) return;
        var itemDrop = ObjectDB.instance.GetItemPrefab(prefab)?.GetComponent<ItemDrop>();
        if (itemDrop == null) return;
        itemDrop.m_itemData.m_shared.m_itemType = itemType.Value;
        itemType = null;
    }
}