using HarmonyLib;

namespace SolastaExtendCharacterNames
{
    // My original idea was to create the save file name as "first+lastnames.chr" which would allow more than one character with the same first name.
    // Unfortunately characters are keyed on first name only throughout the codebase so not sensible to try to make this change.
    [HarmonyPatch(typeof(CharacterPoolManager), "SaveCharacter")]
    internal static class CharacterPoolManager_SaveCharacter
    {
        public static void Prefix(RulesetCharacterHero heroCharacter, [HarmonyArgument("addToPool")] bool _ = false)
        {
            Main.Log("SaveCharacter:enter");

            if (heroCharacter != null)
            {
                // Get rid of any trailing spaces
                heroCharacter.SurName = heroCharacter.SurName?.Trim();
                heroCharacter.Name = heroCharacter.Name?.Trim();
            }

            Main.Log("SaveCharacter:leave");
        }
    }
}