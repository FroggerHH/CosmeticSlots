using System;
using System.Runtime.CompilerServices;

namespace CosmeticSlots
{
    [Serializable]
    public class PlayerAdditionalData
    {
        public ItemDrop.ItemData m_chestCosmeticItem;
        public ItemDrop.ItemData m_helmetCosmeticItem;


        public PlayerAdditionalData()
        {
            m_chestCosmeticItem = null;
            m_helmetCosmeticItem = null;
        }
    }

    public static class PlayerExtension
    {
        private static readonly ConditionalWeakTable<Player, PlayerAdditionalData> data =
            new ConditionalWeakTable<Player, PlayerAdditionalData>();

        public static PlayerAdditionalData GetAdditionalData(this Player player)
        {
            return data.GetOrCreateValue(player);
        }

        public static void AddData(this Player player, PlayerAdditionalData value)
        {
            try
            {
                data.Add(player, value);
            }
            catch (Exception)
            {
            }
        }
    }
}