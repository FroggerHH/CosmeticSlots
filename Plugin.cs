using BepInEx;
using BepInEx.Bootstrap;
using CosmeticSlots.Patch;
using HarmonyLib;
using UnityEngine;

namespace CosmeticSlots;

[BepInPlugin(ModGUID, ModName, ModVersion)]
[BepInDependency("MarketplaceAndServerNPCs", BepInDependency.DependencyFlags.SoftDependency)]
internal class Plugin : BaseUnityPlugin
{
    public const string ModName = "Frogger.CosmeticSlots", ModVersion = "1.5.0", ModGUID = "com." + ModName;
    public const ItemType COSMETIC_CHEST = (ItemType)30;
    public const ItemType COSMETIC_HELMET = (ItemType)31;
    public const ItemType COSMETIC_LEGS = (ItemType)32;
    public const ItemType COSMETIC_CAPE = (ItemType)33;
    internal static Harmony harmony;
    public static Plugin _self;

    private void Awake()
    {
        _self = this;
        harmony = new Harmony(ModGUID);
        harmony.PatchAll(typeof(DisplayCosmeticsPatch));
        harmony.PatchAll(typeof(EquipItemCosmeticPatch));
        harmony.PatchAll(typeof(IsItemEquipedPatch));
        harmony.PatchAll(typeof(ItemStandCosmeticsPatch));
        harmony.PatchAll(typeof(SetCosmeticItemsTypePatch));

        if (Chainloader.PluginInfos.ContainsKey("MarketplaceAndServerNPCs"))
        {
            Logger.LogMessage("Pathing Marketplace");
            harmony.PatchAll(typeof(DisplayMarketPlace));
        }
    }

    private void OnDestroy()
    {
        Debug.LogWarning("You are not at Hildir update. Mod destroying ans unpatching itself...");
        harmony.UnpatchSelf();
    }
}