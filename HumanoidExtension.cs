using System;
using System.Runtime.CompilerServices;
using UnityEngine.Serialization;

namespace CosmeticSlots;

[Serializable]
public class HumanoidAdditionalData
{
    public ItemDrop.ItemData chestCosmeticItem;
    public ItemDrop.ItemData helmetCosmeticItem;
    public ItemDrop.ItemData legsCosmeticItem;

    public HumanoidAdditionalData()
    {
        chestCosmeticItem = null;
        helmetCosmeticItem = null;
        legsCosmeticItem = null;
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