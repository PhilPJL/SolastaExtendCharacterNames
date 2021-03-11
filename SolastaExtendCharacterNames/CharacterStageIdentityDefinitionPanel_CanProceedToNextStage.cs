using HarmonyLib;
using SolastaExtendCharacterNames;
using TMPro;

namespace SolastaCharacterNameExtended
{
    // Unfortunately called while entering name.
    // Need something that's called when leaving stage.

#if false
    [HarmonyPatch(typeof(CharacterStageIdentityDefinitionPanel), "CanProceedToNextStage")]
    static class CharacterStageIdentityDefinitionPanel_CanProceedToNextStage
    {
        public static void Prefix(TMP_InputField ___firstNameInputField, TMP_InputField ___lastNameInputField)
        {
            if (___firstNameInputField != null && ___lastNameInputField != null)
            {
                Main.Log("Trimming first+last names");

                ___lastNameInputField.text = ___lastNameInputField.text?.Trim();
                ___firstNameInputField.text = ___firstNameInputField.text?.Trim();
            }
        }
    }
#endif
}
