#nullable enable
using BepInEx.Bootstrap;
using HarmonyLib;
using static CosmeticSlots.Plugin;

// ReSharper disable Unity.NoNullPropagation

namespace CosmeticSlots;

[HarmonyPatch]
internal class SetCosmeticItemsTypePatch
{
    private static ItemDrop? getItem(string name)
    {
        return ObjectDB.instance?.GetItemPrefab(name)?.GetComponent<ItemDrop>();
    }

    public static void setType(string name, ItemType type)
    {
        var item = getItem(name);
        if (item == null) return;
        item.m_itemData.m_shared.m_itemType = type;
    }

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

        setType("HelmetDverger", COSMETIC_HELMET);
        setType("HelmetPointyHat", COSMETIC_HELMET);
        setType("CapeLinen", COSMETIC_CAPE);

        if (Chainloader.PluginInfos.ContainsKey("MagicMike.Weedheim"))
        {
            setType("Rasta_Pants_01", COSMETIC_LEGS);
            setType("Rasta_Pants_02", COSMETIC_LEGS);
            setType("Rasta_Pants_03", COSMETIC_LEGS);
            setType("Rasta_Pants_04", COSMETIC_LEGS);

            setType("Rasta_Top_01", COSMETIC_CHEST);
            setType("Rasta_Top_02", COSMETIC_CHEST);
            setType("Rasta_Top_03", COSMETIC_CHEST);
            setType("Rasta_Top_04", COSMETIC_CHEST);

            setType("hat_01", COSMETIC_HELMET);
            setType("hat_02", COSMETIC_HELMET);
            setType("hat_03", COSMETIC_HELMET);
            setType("hat_04", COSMETIC_HELMET);
            setType("hat_05", COSMETIC_HELMET);
            setType("hat_06", COSMETIC_HELMET);

            setType("CapeHemp", COSMETIC_CAPE);
        }
    }
}