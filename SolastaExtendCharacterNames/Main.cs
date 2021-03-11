using HarmonyLib;
using System;
using System.Diagnostics;
using UnityModManagerNet;

namespace SolastaExtendCharacterNames
{
    public class Main
    {
        [Conditional("DEBUG")]
        public static void Log(string msg) => logger.Log(msg);

        public static void Error(Exception ex) => logger?.Error(ex.ToString());
        public static void Error(string msg) => logger?.Error(msg);
        public static UnityModManager.ModEntry.ModLogger logger;
        public static bool enabled;

        public static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                logger = modEntry.Logger;
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