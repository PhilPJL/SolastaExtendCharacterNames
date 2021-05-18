using HarmonyLib;
using TMPro;

namespace SolastaExtendCharacterNames
{
    [HarmonyPatch(typeof(UserLocationSettingsModal), "RemoveUselessSpaces")]
    internal static class UserLocationSettingsModal_RemoveUselessSpaces
    {
        public static bool Prefix(TMP_InputField textField) => RemoveInvalidFilenameChars.Invoke(textField);
    }
}
