using HarmonyLib;
using System;
using System.Diagnostics;
using UnityModManagerNet;

namespace SolastaExtendCharacterNames
{
    public static class Main
    {
        [Conditional("DEBUG")]
        internal static void Log(string msg) => Logger.Log(msg);

        internal static void Error(Exception ex) => Logger?.Error(ex.ToString());
        internal static void Error(string msg) => Logger?.Error(msg);
        internal static UnityModManager.ModEntry.ModLogger Logger { get; private set; }

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                Logger = modEntry.Logger;
                new Harmony(modEntry.Info.Id).PatchAll();
            }
            catch (Exception ex)
            {
                Error(ex);
                throw;
            }

            return true;
        }
    }
}