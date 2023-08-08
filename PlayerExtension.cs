using System.Runtime.CompilerServices;

namespace CosmeticSlots
{
    [System.Serializable]
    public class PlayerAdditionalData
    {
        public ItemDrop.ItemData m_chestCosmeticItem, m_helmetCosmeticItem;

        public PlayerAdditionalData()
        {
            m_chestCosmeticItem = null;
            m_helmetCosmeticItem = null;
        }
    }

    public static class PlayerExtension
    {
        private static readonly ConditionalWeakTable<Player, PlayerAdditionalData> data = new();

        public static PlayerAdditionalData GetAdditionalData(this Player player) => data.GetOrCreateValue(player);

        public static void AddData(this Player player, PlayerAdditionalData value)
        {
            try  { data.Add(player, value); }
            catch { }
        }
    }
}