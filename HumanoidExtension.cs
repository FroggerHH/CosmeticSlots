﻿using System.Runtime.CompilerServices;

namespace CosmeticSlots;

[Serializable]
public class HumanoidAdditionalData
{
    public ItemData chestCosmeticItem;
    public ItemData helmetCosmeticItem;
    public ItemData legsCosmeticItem;
    public ItemData capeCosmeticItem;

    public HumanoidAdditionalData()
    {
        chestCosmeticItem = null;
        helmetCosmeticItem = null;
        legsCosmeticItem = null;
        capeCosmeticItem = null;
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