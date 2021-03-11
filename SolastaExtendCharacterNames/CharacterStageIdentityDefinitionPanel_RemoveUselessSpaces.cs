using HarmonyLib;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;

namespace SolastaCharacterNameExtended
{
    [HarmonyPatch(typeof(CharacterStageIdentityDefinitionPanel), "RemoveUselessSpaces")]
    static class CharacterStageIdentityDefinitionPanel_RemoveUselessSpaces
    {
        private static readonly HashSet<char> invalidFilenameChars = new HashSet<char>(Path.GetInvalidFileNameChars());

        public static bool Prefix(TMP_InputField textField)
        {
            if (textField != null)
            {
                // Solasta original code disallows invalid filename chars and an additional list of illegal chars.
                // We're disallowing invalid filename chars only.
                // We're trimming whitespace from start only as per original method.
                // This allows the users to create a name with spaces inside, but also allows trailing space.
                // Trailing spaces are removed on save.
                textField.text = new string(
                    textField.text
                        .Where(n => !invalidFilenameChars.Contains(n))
                        .ToArray()).TrimStart();

                return false;
            }

            return true;
        }
    }
}
