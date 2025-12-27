using HarmonyLib;
using ProjectOrbitalRing.Patches.Logic.OrbitalRing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrbitalRing.Patches.Logic
{
    internal class PowerExchangerOutputInputPatch
    {

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PowerExchangerComponent), nameof(PowerExchangerComponent.PickItemFromBelt))]
        public static bool PowerExchangerComponent_PickItemFromBelt_Patch(ref PowerExchangerComponent __instance, ref CargoTraffic cargoTraffic, int beltId, bool __result)
        {
            if (__instance.state == 1f) {
                byte b;
                byte b2;
                if (__instance.emptyCount < 5 && (beltId == __instance.belt1 || beltId == __instance.belt3) && __instance.emptyId == cargoTraffic.TryPickItemAtRear(beltId, __instance.emptyId, null, out b, out b2)) {
                    __instance.emptyCount += (short)b;
                    __instance.emptyInc += (short)b2;
                    __result = true;
                    return false;
                }
                if (__instance.fullCount < 5 && __instance.fullId == cargoTraffic.TryPickItemAtRear(beltId, __instance.fullId, null, out b, out b2)) {
                    __instance.fullCount += (short)b;
                    __instance.fullInc += (short)b2;
                    __result = true;
                    return false;
                }
                __result = false;
                return false;
            }
            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PowerExchangerComponent), nameof(PowerExchangerComponent.InsertItemToBelt), new[] { typeof(CargoTraffic), typeof(int), typeof(bool) })]
        public static bool PowerExchangerComponent_InsertItemToBelt_Patch(ref PowerExchangerComponent __instance, ref CargoTraffic cargoTraffic, int beltId, bool isEmptyAcc, bool __result)
        {
            if (isEmptyAcc == false) {
                if (beltId == __instance.belt1 || beltId == __instance.belt3) {
                    if (__instance.emptyCount > 4) {
                        int num = (int)__instance.emptyCount;
                        int num2 = (int)__instance.emptyInc;
                        byte inc = (byte)__instance.split_inc(ref num, ref num2, 1);
                        if (cargoTraffic.TryInsertItemAtHead(beltId, __instance.emptyId, 1, inc)) {
                            __instance.emptyCount = (short)num;
                            __instance.emptyInc = (short)num2;
                            __result = true;
                            return false;
                        }
                    }
                }
                if (beltId == __instance.belt0 || beltId == __instance.belt2) {
                    if (__instance.fullCount > 0) {
                        int num3 = (int)__instance.fullCount;
                        int num4 = (int)__instance.fullInc;
                        byte inc2 = (byte)__instance.split_inc(ref num3, ref num4, 1);
                        if (cargoTraffic.TryInsertItemAtHead(beltId, __instance.fullId, 1, inc2)) {
                            __instance.fullCount = (short)num3;
                            __instance.fullInc = (short)num4;
                            __result = true;
                            return false;
                        }
                    }
                }
            }
            return true;
        }
    }
}
