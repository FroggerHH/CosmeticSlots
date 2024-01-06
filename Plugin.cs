using BepInEx;
using BepInEx.Bootstrap;
using BepInEx.Configuration;
using CosmeticSlots.Compatibility.kgMarketplace;
using CosmeticSlots.Patch;

namespace CosmeticSlots;

[BepInPlugin(ModGUID, ModName, ModVersion)]
public class Plugin : BaseUnityPlugin
{
    private const string ModName = "CosmeticSlots",
        ModAuthor = "Frogger",
        ModVersion = "1.6.0",
        ModGUID = $"com.{ModAuthor}.{ModName}";

    public const ItemType COSMETIC_CHEST = (ItemType)30;
    public const ItemType COSMETIC_HELMET = (ItemType)31;
    public const ItemType COSMETIC_LEGS = (ItemType)32;
    public const ItemType COSMETIC_CAPE = (ItemType)33;

    private static ConfigEntry<string> itemsConfig;
    public static Dictionary<string, ItemType> items;

    private static readonly string availableTypesChestHelmetLegsCape_MSG = "Available types: Chest, Helmet, Legs, Cape";

    private void Awake()
    {
        CreateMod(this, ModName, ModAuthor, ModVersion, ModGUID);
        CreateDefaultItems();
        OnConfigurationChanged += UpdateConfiguration;
        itemsConfig = config("General", "Cosmetics items", ItemsToStr(items),
            "List of items in format: (itemPrefabName, ItemType)\n" + availableTypesChestHelmetLegsCape_MSG);

        MarketplaceTerritorySystem.Patch();
    }

    private static void CreateDefaultItems()
    {
        items = new Dictionary<string, ItemType>();
        items.Add("HelmetDverger", COSMETIC_HELMET);
        items.Add("HelmetPointyHat", COSMETIC_HELMET);
        items.Add("CapeLinen", COSMETIC_CAPE);

        if (Chainloader.PluginInfos.ContainsKey("MagicMike.Weedheim"))
        {
            items.Add("Rasta_Pants_01", COSMETIC_LEGS);
            items.Add("Rasta_Pants_02", COSMETIC_LEGS);
            items.Add("Rasta_Pants_03", COSMETIC_LEGS);
            items.Add("Rasta_Pants_04", COSMETIC_LEGS);

            items.Add("Rasta_Top_01", COSMETIC_CHEST);
            items.Add("Rasta_Top_02", COSMETIC_CHEST);
            items.Add("Rasta_Top_03", COSMETIC_CHEST);
            items.Add("Rasta_Top_04", COSMETIC_CHEST);

            items.Add("hat_01", COSMETIC_HELMET);
            items.Add("hat_02", COSMETIC_HELMET);
            items.Add("hat_03", COSMETIC_HELMET);
            items.Add("hat_04", COSMETIC_HELMET);
            items.Add("hat_05", COSMETIC_HELMET);
            items.Add("hat_06", COSMETIC_HELMET);

            items.Add("CapeHemp", COSMETIC_CAPE);
        }
    }

    private void UpdateConfiguration()
    {
        foreach (var itemToCosmetic in items)
            setType(itemToCosmetic.Key, CosmeticItemTypeToVanilla(itemToCosmetic.Value));

        items = LoadItemsFromStr(itemsConfig.Value);
        Debug($"Configuration updated: {items.GetString()}");

        foreach (var itemToCosmetic in items)
            setType(itemToCosmetic.Key, itemToCosmetic.Value);
    }

    private static Dictionary<string, ItemType> LoadItemsFromStr(string str)
    {
        var result = new Dictionary<string, ItemType>();
        var pairs = str.Split(new[] { "), (" }, StringSplitOptions.RemoveEmptyEntries);

        foreach (var pair in pairs)
        {
            var trimmedPair = pair.Trim('(', ')');
            var keyValue = trimmedPair.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (keyValue.Length != 2)
            {
                DebugError(
                    $"Could not parse item: '{keyValue.GetString()}', expected format: (itemPrefabName, CosmeticItemType)\n"
                    + availableTypesChestHelmetLegsCape_MSG);
                continue;
            }

            var itemPrefabName = keyValue[0];
            var typeStr = keyValue[1];
            var type = StringToItemType(typeStr);
            if (type == ItemType.None)
                continue;

            result.Add(itemPrefabName, type);
        }

        return result;
    }

    private static ItemType StringToItemType(string typeStr)
    {
        if (typeStr == "Chest") return COSMETIC_CHEST;
        if (typeStr == "Helmet") return COSMETIC_HELMET;
        if (typeStr == "Legs") return COSMETIC_LEGS;
        if (typeStr == "Cape") return COSMETIC_CAPE;

        DebugError($"Unknown type: {typeStr}\n" + availableTypesChestHelmetLegsCape_MSG);
        return ItemType.None;
    }

    private static string ItemsToStr(Dictionary<string, ItemType> items)
    {
        var itemsList = new List<string>();
        foreach (var item in items)
            itemsList.Add($"({item.Key}, {ItemTypeToString(item.Value)})");

        var itemsToStr = itemsList.GetString();
        Debug($"itemsToStr: {itemsToStr}");
        return itemsToStr;
    }

    private static string ItemTypeToString(ItemType itemType)
    {
        return itemType switch
        {
            COSMETIC_CHEST => "Chest",
            COSMETIC_HELMET => "Helmet",
            COSMETIC_LEGS => "Legs",
            COSMETIC_CAPE => "Cape",
            _ => "Unknown"
        };
    }

    private static ItemType CosmeticItemTypeToVanilla(ItemType itemType)
    {
        return itemType switch
        {
            COSMETIC_CHEST => Chest,
            COSMETIC_HELMET => Helmet,
            COSMETIC_LEGS => Legs,
            COSMETIC_CAPE => Shoulder,
            _ => itemType
        };
    }

    public static ItemDrop? getItem(string name)
    {
        return ObjectDB.instance?.GetItemPrefab(name)?.GetComponent<ItemDrop>();
    }

    public static void setType(string name, ItemType type)
    {
        var item = getItem(name);
        if (item == null) return;
        item.m_itemData.m_shared.m_itemType = type;
        if (Player.m_localPlayer)
            foreach (var itemData in Player.m_localPlayer.GetInventory().m_inventory)
            {
                if (!itemData.m_dropPrefab || itemData.m_dropPrefab.GetPrefabName() != name) continue;
                itemData.m_shared.m_itemType = type;
            }

        Debug($"Set type of '{name}'to '{type}'");
    }
}