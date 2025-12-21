using HarmonyLib;
using ProjectOrbitalRing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectOrbitalRing.Patches.Logic
{
    internal class PumpWaterDoubleSpeed
    {
        [HarmonyPatch(typeof(FactorySystem), nameof(FactorySystem.NewMinerComponent))]
        [HarmonyPostfix]
        public static void NewMinerComponent_Patch(FactorySystem __instance, int entityId, PrefabDesc desc, int __result)
        {
            __instance.minerPool[__result].type = desc.minerType;
            if (desc.minerType == EMinerType.Water) {
                if (__instance.planet.waterItemId == 1000) {
                    __instance.minerPool[__result].period = (int)(__instance.minerPool[__result].period * 0.5f);
                }
            }
        }
    }
}
