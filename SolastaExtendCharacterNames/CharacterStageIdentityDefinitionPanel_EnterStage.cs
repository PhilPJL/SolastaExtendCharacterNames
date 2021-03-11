using HarmonyLib;
using TMPro;

namespace SolastaExtendCharacterNames
{
    [HarmonyPatch(typeof(CharacterStageIdentityDefinitionPanel), "EnterStage")]
    internal static class CharacterStageIdentityDefinitionPanel_EnterStage
    {
        public static void Postfix(TMP_InputField ___firstNameInputField, TMP_InputField ___lastNameInputField)
        {
            Main.Log("EnterStage:enter");

            // Increase default from 14 to 20 - could make it configurable
            ___firstNameInputField.characterLimit = 20;
            ___lastNameInputField.characterLimit = 20;

            Main.Log("EnterStage:leave");
        }
    }
}