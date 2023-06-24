using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CosmeticSlots;

[BepInPlugin(ModGUID, ModName, ModVersion)]
internal class Plugin : BaseUnityPlugin
{
    #region values

    internal const string ModName = "CosmeticSlots", ModVersion = "1.0.1", ModGUID = "com.Frogger." + ModName;
    internal static Harmony harmony = new(ModGUID);

    internal static Plugin _self;
    internal static AssetBundle assetBundle;

    #endregion

    #region tools

    public static void Debug(object msg)
    {
        _self.Logger.LogInfo(msg);
    }

    public void DebugError(object msg, bool showWriteToDev)
    {
        if (showWriteToDev)
        {
            msg += "Write to the developer and moderator if this happens often.";
        }

        Logger.LogError(msg);
    }

    public void DebugWarning(object msg, bool showWriteToDev)
    {
        if (showWriteToDev)
        {
            msg += "Write to the developer and moderator if this happens often.";
        }

        Logger.LogWarning(msg);
    }

    #endregion

    private void Awake()
    {
        _self = this;

        harmony.PatchAll();
    }
}