using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static ProjectOrbitalRing.Patches.Logic.OrbitalRing.EquatorRing;
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic.OrbitalRing
{
    internal class OrbitalBeacon
    {
        [HarmonyPatch(typeof(BeaconComponent), nameof(BeaconComponent.GameTick))]
        [HarmonyPrefix]
        public static bool GameTickPatch(ref BeaconComponent __instance, PlanetFactory factory, PrefabDesc pdesc, EAggressiveLevel agglv, float power, long time)
        {
            if (pdesc.beaconSignalRadius == 0.0f) {
                int num = (int)(time % 60);
                if (num != 0) {
                    return false;
                }
                var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(factory.planetId);
                if (planetOrbitalRingData == null) return false;
                for (int i = 0; i < planetOrbitalRingData.Rings.Count; i++) {
                    EquatorRing ring = planetOrbitalRingData.Rings[i];
                    if (!ring.IsOneFull()) continue;
                    for (int j = 0; j < ring.Capacity; j++) {
                        var pair = ring.GetPair(j);
                        if (pair.stationType == StationType.GlobalIncCore && pair.OrbitalCorePoolId == __instance.id) {
                            if (power == 1) {
                                if (pdesc.workEnergyPerTick == 4200000) {
                                    ring.incCoreLevel[j] = 1;
                                } else if (pdesc.workEnergyPerTick == 8400000) {
                                    ring.incCoreLevel[j] = 2;
                                } else if (pdesc.workEnergyPerTick == 21000000) {
                                    ring.incCoreLevel[j] = 4;
                                }
                            } else {
                                ring.incCoreLevel[j] = 0;
                            }
                            return false;
                        }
                    }
                }
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(DefenseSystem), nameof(DefenseSystem.GameTick))]
        [HarmonyPostfix]
        public static void GameTickPatch(ref DefenseSystem __instance)
        {
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
            if (planetOrbitalRingData == null) return;
            int incLevel = 0;
            for (int i = 0; i < planetOrbitalRingData.Rings.Count; i++) {
                EquatorRing ring = planetOrbitalRingData.Rings[i];
                for (int j = 0; j < ring.Capacity; j++) {
                    incLevel = (incLevel < ring.incCoreLevel[j]) ? ring.incCoreLevel[j] : incLevel;
                }
            }
            planetOrbitalRingData.planetIncLevel = incLevel;
        }
    }
}
