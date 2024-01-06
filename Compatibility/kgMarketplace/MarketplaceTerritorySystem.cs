using BepInEx.Bootstrap;

namespace CosmeticSlots.Compatibility.kgMarketplace;

public class MarketplaceTerritorySystem
{
    private const string GUID = "MarketplaceAndServerNPCs";

    public static bool IsLoaded() { return Chainloader.PluginInfos.ContainsKey(GUID); }

    public static void Patch()
    {
        if (IsLoaded() == false) return;
        MarketplaceTerritorySystem_RAW.Patch();
    }
}