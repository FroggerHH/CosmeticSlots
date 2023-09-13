using HarmonyLib;
using static ItemDrop.ItemData.ItemType;
using static UnityEngine.Object;
using static CosmeticSlots.Plugin;

namespace CosmeticSlots;

[HarmonyPatch]
internal class SetCosmeticItemsTypePatch
{
    [HarmonyPatch(typeof(ZNetScene), nameof(ZNetScene.Awake))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void Postfix(ZNetScene __instance)
    {
        var hildirObj = __instance.GetPrefab("Hildir")?.GetComponent<Trader>();
        if (!hildirObj)
        {
            Destroy(_self);
            return;
        }

        foreach (var trade in hildirObj.m_items)
        {
            var sharedData = trade?.m_prefab?.m_itemData?.m_shared;
            if (sharedData == null) continue;

            if (sharedData.m_itemType == Chest) sharedData.m_itemType = COSMETIC_CHEST;
            else if (sharedData.m_itemType == Helmet) sharedData.m_itemType = COSMETIC_HELMET;
        }
    }
}