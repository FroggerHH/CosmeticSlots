using HarmonyLib;
using static CosmeticSlots.Plugin;

namespace CosmeticSlots;

[HarmonyPatch]
internal class ItemStandCosmeticsPatch
{
    [HarmonyPatch(typeof(ItemStand), nameof(ItemStand.Awake))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void PatchItemStand(ItemStand __instance)
    {
        __instance.m_supportedTypes.Add(COSMETIC_CHEST);
        __instance.m_supportedTypes.Add(COSMETIC_HELMET);
        __instance.m_supportedTypes.Add(COSMETIC_LEGS);
    }

    [HarmonyPatch(typeof(ArmorStand), nameof(ArmorStand.Awake))] [HarmonyPostfix] [HarmonyWrapSafe]
    public static void ArmorStandPatch(ArmorStand __instance)
    {
        __instance.m_slots.Find(x => x.m_slot == VisSlot.Chest)?.m_supportedTypes.Add(COSMETIC_CHEST);
        __instance.m_slots.Find(x => x.m_slot == VisSlot.Helmet)?.m_supportedTypes.Add(COSMETIC_HELMET);
        __instance.m_slots.Find(x => x.m_slot == VisSlot.Legs)?.m_supportedTypes.Add(COSMETIC_LEGS);
    }
}