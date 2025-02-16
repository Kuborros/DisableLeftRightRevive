using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace DisableLeftRightRevive
{
    [BepInPlugin("com.kuborro.plugins.fp2.delayrevive", "DelayLRRevive", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(PatchFPPlayerKO));
        }
    }

    public class PatchFPPlayerKO
    {

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(FPPlayer),"State_KO",MethodType.Normal)]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> codes = new List<CodeInstruction>(instructions);
            for (var i = 1; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_R4)
                {
                    if ((float)codes[i].operand == 30f)
                    {
                        codes[i].operand = 3000f;
                    }
                    if ((float)codes[i].operand == 104f)
                    {
                        codes[i].operand = 3000f;
                    }
                }

            }
            return codes;
        }
    }
}
