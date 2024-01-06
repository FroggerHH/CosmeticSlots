namespace CosmeticSlots.Patch;

[HarmonyPatch]
internal class DisplayCosmeticsPatch
{
    [HarmonyPatch(typeof(Humanoid), nameof(Humanoid.SetupVisEquipment))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void CosmeticSlots_DisplayCosmeticsPatch(Humanoid __instance, VisEquipment visEq, bool isRagdoll)
    {
        var chestCosmeticItem = __instance.GetAdditionalData().chestCosmeticItem;
        var helmetCosmeticItem = __instance.GetAdditionalData().helmetCosmeticItem;
        var legsCosmeticItem = __instance.GetAdditionalData().legsCosmeticItem;
        var capeCosmeticItem = __instance.GetAdditionalData().capeCosmeticItem;

        if (chestCosmeticItem != null)
            visEq.SetChestItem(chestCosmeticItem.m_dropPrefab.name);

        if (helmetCosmeticItem != null)
            visEq.SetHelmetItem(helmetCosmeticItem.m_dropPrefab.name);

        if (legsCosmeticItem != null)
            visEq.SetLegItem(legsCosmeticItem.m_dropPrefab.name);

        if (capeCosmeticItem != null)
            visEq.SetShoulderItem(capeCosmeticItem.m_dropPrefab.name, capeCosmeticItem.m_variant);
    }
}