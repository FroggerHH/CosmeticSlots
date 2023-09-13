using System;
using System.Runtime.CompilerServices;

namespace CosmeticSlots;

[Serializable]
public class HumanoidAdditionalData
{
    public ItemDrop.ItemData m_chestCosmeticItem, m_helmetCosmeticItem;

    public HumanoidAdditionalData()
    {
        m_chestCosmeticItem = null;
        m_helmetCosmeticItem = null;
    }
}

public static class HumanoidExtension
{
    private static readonly ConditionalWeakTable<Humanoid, HumanoidAdditionalData> data = new();

    public static HumanoidAdditionalData GetAdditionalData(this Humanoid humanoid)
    {
        return data.GetOrCreateValue(humanoid);
    }

    public static void AddData(this Humanoid humanoid, HumanoidAdditionalData value)
    {
        try { data.Add(humanoid, value); }
        catch { }
    }
}