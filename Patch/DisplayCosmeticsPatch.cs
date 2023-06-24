using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ItemDrop;
using static ItemDrop.ItemData;
using static ItemDrop.ItemData.ItemType;


namespace CosmeticSlots;

[HarmonyPatch]
internal class DisplayCosmeticsPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.SetupVisEquipment)), HarmonyPostfix]
    public static void Patch(Humanoid __instance, VisEquipment visEq, bool isRagdoll)
    {
        if (__instance is not Player { } player) return;
        var chestCosmeticItem = player.GetAdditionalData().m_chestCosmeticItem;
        var helmetCosmeticItem = player.GetAdditionalData().m_helmetCosmeticItem;
        var chestName = string.Empty;
        var helmetName = string.Empty;
        if (chestCosmeticItem != null) chestName = chestCosmeticItem.m_dropPrefab.name;
        else chestName = __instance.m_chestItem != null ? __instance.m_chestItem.m_dropPrefab.name : "";

        if (helmetCosmeticItem != null) helmetName = helmetCosmeticItem.m_dropPrefab.name;
        else helmetName = __instance.m_helmetItem != null ? __instance.m_helmetItem.m_dropPrefab.name : "";

        visEq.SetChestItem(chestName);
        visEq.SetHelmetItem(helmetName);
        
        if(SceneManager.GetActiveScene().name == )
    }
}