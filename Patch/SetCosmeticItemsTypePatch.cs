#nullable enable
namespace CosmeticSlots.Patch;

[HarmonyPatch]
internal class SetCosmeticItemsTypePatch
{
    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void Postfix(ZNetScene __instance)
    {
        foreach (var trade in __instance.GetPrefab("Hildir").GetComponent<Trader>().m_items)
        {
            var sharedData = trade?.m_prefab?.m_itemData?.m_shared;
            if (sharedData == null) continue;

            if (sharedData.m_itemType == Chest) sharedData.m_itemType = COSMETIC_CHEST;
            else if (sharedData.m_itemType == Helmet) sharedData.m_itemType = COSMETIC_HELMET;
        }

        foreach (var itemToCosmetic in items) setType(itemToCosmetic.Key, itemToCosmetic.Value);
    }
}