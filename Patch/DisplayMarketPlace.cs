using Extensions;
using HarmonyLib;
using UnityEngine;
using static Marketplace.Modules.NPC.Market_NPC;
using static CosmeticSlots.Plugin;

namespace CosmeticSlots;

[HarmonyPatch]
internal class DisplayMarketPlace
{
    private static readonly int ChestTex = Shader.PropertyToID("_ChestTex");
    private static readonly int ChestBumpMap = Shader.PropertyToID("_ChestBumpMap");
    private static readonly int ChestMetal = Shader.PropertyToID("_ChestMetal");

    [HarmonyPatch(typeof(NPCcomponent), nameof(NPCcomponent.EquipItemsOnModel), typeof(SkinnedMeshRenderer),
        typeof(string), typeof(CapsuleCollider[]))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void CosmeticSlots_DisplayCosmeticsPatch(NPCcomponent __instance, SkinnedMeshRenderer joint,
        string prefab, CapsuleCollider[] capsule)
    {
        if (!prefab.IsGood()) return;
        var itemDrop = ObjectDB.instance.GetItemPrefab(prefab)?.GetComponent<ItemDrop>();
        if (itemDrop == null) return;
        var armorMaterial = itemDrop.m_itemData.m_shared.m_armorMaterial;
        var itemType = itemDrop.m_itemData.m_shared.m_itemType;

        if (itemType == COSMETIC_CHEST && armorMaterial)
        {
            joint.material.SetTexture(ChestTex, armorMaterial.GetTexture(ChestTex));
            joint.material.SetTexture(ChestBumpMap, armorMaterial.GetTexture(ChestBumpMap));
            joint.material.SetTexture(ChestMetal, armorMaterial.GetTexture(ChestMetal));
        }
        __instance.AttachArmor(prefab.GetStableHashCode(), joint, capsule);
    }
}