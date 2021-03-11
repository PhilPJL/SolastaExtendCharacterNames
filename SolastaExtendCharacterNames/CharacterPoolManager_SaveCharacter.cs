using HarmonyLib;
using System.Collections.Generic;
using System.IO;

namespace SolastaExtendCharacterNames
{
    // The idea was to create the save file name as "first+lastnames.chr".
    // Unfortunately characters are keyed on first name only throughout the codebase so not sensible
    // to try to make this change.
    [HarmonyPatch(typeof(CharacterPoolManager), "SaveCharacter")]
    static class CharacterPoolManager_SaveCharacter
    {
        public static void Prefix(RulesetCharacterHero heroCharacter, bool addToPool = false)
        {
            if (heroCharacter != null)
            {
                Main.Log("Trimming names on save");

                heroCharacter.SurName = heroCharacter.SurName?.Trim();
                heroCharacter.Name = heroCharacter.Name?.Trim();
            }
        }
#if false
        public static bool Prefix(CharacterPoolManager __instance, 
            Dictionary<string, RulesetCharacterHero.Snapshot> ___pool, ref string __result, RulesetCharacterHero heroCharacter, bool addToPool = false)
        {
            // NOTE: this is copy of the entire Solasta method
            // with a tiny change in order to make the filename = firstname + surname

            string path = TacticalAdventuresApplication.GameCharactersDirectory;

            // Create filename as First name + surname (if there is a surname)
            string filename = !string.IsNullOrWhiteSpace(heroCharacter.SurName)
                ? Path.Combine(path, heroCharacter.Name + " " + heroCharacter.SurName) + ".chr"
                : Path.Combine(path, heroCharacter.Name) + ".chr";

            RulesetCharacterHero.Snapshot snapshot = new RulesetCharacterHero.Snapshot();

            heroCharacter.FillSnapshot(snapshot, true, response: response);
            __result = filename;

            return true;

            void response()
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                if (File.Exists(filename))
                    File.Delete(filename);

                using (FileStream fileStream = File.Create(filename))
                using (BinarySerializer binarySerializer = new BinarySerializer(fileStream, Serializer.SerializationMode.Write, new BinarySerializer.Settings()))
                {
                    binarySerializer.SerializeElement("Snapshot", snapshot);
                    binarySerializer.SerializeElement("HeroCharacter", heroCharacter);
                }

                if (!addToPool)
                    return;

                if (!___pool.ContainsKey(filename))
                    ___pool.Add(filename, snapshot);
                else
                    ___pool[filename] = snapshot;

                var characterPoolRefreshed = __instance.CharacterPoolRefreshed;
                if (characterPoolRefreshed == null)
                    return;

                characterPoolRefreshed();
            }
        }
#endif
    }
}