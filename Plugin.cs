using System.Reflection;
using BepInEx;
using HarmonyLib;
using UnityEngine;

namespace CosmeticSlots;

[BepInPlugin(ModGUID, ModName, ModVersion)]
internal class Plugin : BaseUnityPlugin
{
    public const string ModName = "Frogger.CosmeticSlots", ModVersion = "1.1.0", ModGUID = "com." + ModName;
    public const ItemDrop.ItemData.ItemType COSMETIC_CHEST = (ItemDrop.ItemData.ItemType)30;
    public const ItemDrop.ItemData.ItemType COSMETIC_HELMET = (ItemDrop.ItemData.ItemType)31;
    internal static Harmony harmony;
    public static Plugin _self;

    private void Awake()
    {
        _self = this;
        harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), ModGUID);
    }

    private void OnDestroy()
    {
        Debug.LogWarning("You are not at Hildir update. Mod destroying ans unpatching itself...");
        harmony.UnpatchSelf();
    }
}