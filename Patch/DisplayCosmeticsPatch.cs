using HarmonyLib;

namespace CosmeticSlots;

[HarmonyPatch]
internal class DisplayCosmeticsPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.SetupVisEquipment))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void CosmeticSlots_DisplayCosmeticsPatch(Humanoid __instance, VisEquipment visEq, bool isRagdoll)
    {
        var chestCosmeticItem = __instance.GetAdditionalData().chestCosmeticItem;
        var helmetCosmeticItem = __instance.GetAdditionalData().helmetCosmeticItem;
        var legsCosmeticItem = __instance.GetAdditionalData().legsCosmeticItem;
        var chestName = string.Empty;
        var helmetName = string.Empty;
        var legsName = string.Empty;
        
        if (chestCosmeticItem != null) chestName = chestCosmeticItem.m_dropPrefab.name;
        else chestName = __instance.m_chestItem != null ? __instance.m_chestItem.m_dropPrefab.name : "";

        if (helmetCosmeticItem != null) helmetName = helmetCosmeticItem.m_dropPrefab.name;
        else helmetName = __instance.m_helmetItem != null ? __instance.m_helmetItem.m_dropPrefab.name : "";
        
        if (legsCosmeticItem != null) legsName = legsCosmeticItem.m_dropPrefab.name;
        else legsName = __instance.m_legItem != null ? __instance.m_legItem.m_dropPrefab.name : "";

        visEq.SetChestItem(chestName);
        visEq.SetHelmetItem(helmetName);
        visEq.SetLegItem(legsName);
    }
}