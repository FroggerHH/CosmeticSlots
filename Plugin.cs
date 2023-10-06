using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using UnityEngine;

namespace CosmeticSlots;

[BepInPlugin(ModGUID, ModName, ModVersion)]
[BepInDependency("MarketplaceAndServerNPCs", BepInDependency.DependencyFlags.SoftDependency)]
internal class Plugin : BaseUnityPlugin
{
    public const string ModName = "Frogger.CosmeticSlots", ModVersion = "1.3.0", ModGUID = "com." + ModName;
    public const ItemDrop.ItemData.ItemType COSMETIC_CHEST = (ItemDrop.ItemData.ItemType)30;
    public const ItemDrop.ItemData.ItemType COSMETIC_HELMET = (ItemDrop.ItemData.ItemType)31;
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