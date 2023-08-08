using HarmonyLib;
using static ItemDrop.ItemData;

namespace CosmeticSlots;

[HarmonyPatch]
internal class ItemStandCosmeticsPatch
{
    [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.Awake)), HarmonyPostfix, HarmonyWrapSafe]
    public static void Patch(ItemStand __instance)
    {
        __instance.m_supportedTypes.Add((ItemType)(30));
        __instance.m_supportedTypes.Add((ItemType)(31));
    }
}