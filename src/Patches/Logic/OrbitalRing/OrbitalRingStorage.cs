using HarmonyLib;
using Newtonsoft.Json;
using System.Collections.Generic;
using static ProjectOrbitalRing.Patches.Logic.OrbitalRing.EquatorRing;
using static ProjectOrbitalRing.Patches.Logic.OrbitalRing.PosTool;
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic.OrbitalRing
{
    public class OrbitalRingStorage
    {

        public Dictionary<int, int[]> storageItem = new Dictionary<int, int[]>();

        
    }

    internal class OrbitalRingStorageCalculate
    {
        [HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.GameTick))]
        [HarmonyPostfix]
        public static void PlanetTransport_GameTick_Patch(ref PlanetTransport __instance, long time)
        {
            int num = (int)(time % 60);
            if (num != 0) {
                return;
            }
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
            if (planetOrbitalRingData == null) return;
            //LogError($"planetOrbitalRingData.Rings.Count {planetOrbitalRingData.Rings.Count}");
            for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
                if (!planetOrbitalRingData.Rings[ringId].IsOneFull()) continue;
                for (int i = 0; i < planetOrbitalRingData.Rings[ringId].Capacity; i++) {
                    var pair = planetOrbitalRingData.Rings[ringId].GetPair(i);
                    if (pair.stationType == StationType.Station) {
                        if (__instance.stationPool == null) return;
                        StationComponent stationComponent = __instance.stationPool[pair.OrbitalStationPoolId];
                        if (stationComponent != null) {
                            //LogError($"ringId {ringId} i {i}");
                            ShareStorageForOrbitalStation(ref stationComponent, ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem);
                        }
                    } else if (pair.elevatorPoolId != -1) {
                        if (__instance.stationPool == null) return;
                        StationComponent stationComponent = __instance.stationPool[pair.elevatorPoolId];
                        if (stationComponent != null) {
                            ShareStorageForOrbitalStation(ref stationComponent, ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem);
                        }
                    }
                }
            }
        }

        public static void ShareStorageForOrbitalStation(ref StationComponent stationComponent, ref Dictionary<int, int[]> storageItem)
        {
            lock (stationComponent.storage) {
                for (int j = 0; j < stationComponent.storage.Length; j++) {
                    StationStore storage = stationComponent.storage[j];
                    //LogError($"count {storage.count} max {storage.max / 2}");
                    if (storage.count < storage.max / 2) {
                        if (storageItem.ContainsKey(storage.itemId)) {
                            int count = (storage.max / 2) - storage.count;
                            int[] countAndInc = storageItem.GetValueOrDefault(storage.itemId);
                            if (storageItem[storage.itemId][0] > count) {
                                //storageItem[storage.itemId][0] -= count;
                                int inc = split_inc(ref storageItem[storage.itemId][0], ref storageItem[storage.itemId][1], count);
                                stationComponent.storage[j].count += count;
                                stationComponent.storage[j].inc += (short)inc;
                            } else {
                                stationComponent.storage[j].count += storageItem[storage.itemId][0];
                                storageItem[storage.itemId][0] = 0;
                                stationComponent.storage[j].inc += (short)storageItem[storage.itemId][1];
                                storageItem[storage.itemId][1] = 0;
                            }
                        }
                    } else if (storage.count > storage.max / 2) {
                        if (!storageItem.ContainsKey(storage.itemId)) {
                            storageItem[storage.itemId] = new int[] { 0, 0 };
                        }
                        int count = storage.count - (storage.max / 2);
                        storageItem[storage.itemId][0] += count;
                        //storage.count -= count;
                        int inc = split_inc(ref stationComponent.storage[j].count, ref stationComponent.storage[j].inc, count);
                        storageItem[storage.itemId][1] += inc;
                        //LogError($"add count {stationComponent.storage[j].count} storageItem {storageItem[storage.itemId][0]}");
                    }
                }
            }
        }


        //[HarmonyPatch(typeof(FactorySystem), nameof(FactorySystem.GameTick))]
        //[HarmonyPostfix]
        //public static void FactorySystem_GameTick_Patch(ref FactorySystem __instance, long time)
        //{
        //    int num = (int)(time % 60);
        //    if (num != 0) {
        //        return;
        //    }
        //    var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
        //    if (planetOrbitalRingData == null) return;
        //    for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
        //        for (int i = 0; i < planetOrbitalRingData.Rings[ringId].Capacity; i++) {
        //            var pair = planetOrbitalRingData.Rings[ringId].GetPair(i);
        //            if (pair.stationType == StationType.Assembler) {
        //                if (__instance.assemblerPool == null) return;
        //                AssemblerComponent assemblerComponent = __instance.assemblerPool[pair.OrbitalStationPoolId];
        //                if (assemblerComponent.recipeId != 0) {
        //                    LogError($"ShareStorageForOrbitalAssembler");
        //                    ShareStorageForOrbitalAssembler(ref assemblerComponent, ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem);
        //                }
        //            }
        //        }
        //    }
        //}

        public static void ShareStorageForOrbitalAssembler(ref AssemblerComponent assemblerComponent, FactorySystem factory)
        {
            int itemId = 0;
            int count = 0;
            int inc = 0;
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(factory.planet.id);
            if (planetOrbitalRingData == null) return;
            for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
                if (!planetOrbitalRingData.Rings[ringId].IsOneFull()) continue;
                for (int j = 0; j < planetOrbitalRingData.Rings[ringId].Capacity; j++) {
                    var pair = planetOrbitalRingData.Rings[ringId].GetPair(j);
                    if (pair.stationType == StationType.Assembler) {
                        if (pair.OrbitalStationPoolId == assemblerComponent.id) {
                            ref Dictionary<int, int[]> storageItem = ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem;
                            int num = assemblerComponent.speedOverride * 180 / assemblerComponent.timeSpend + 1;
                            if (num < 2) {
                                num = 2;
                            }
                            for (int i = 0; i < assemblerComponent.needs.Length; i++) {
                                itemId = assemblerComponent.needs[i];
                                if (storageItem.ContainsKey(itemId)) {
                                    count = assemblerComponent.requireCounts[i] * num;
                                    if (storageItem[itemId][0] >= count) {
                                        assemblerComponent.served[i] += count;
                                        inc = split_inc(ref storageItem[itemId][0], ref storageItem[itemId][1], count);
                                        assemblerComponent.incServed[i] += inc;
                                    } else {
                                        assemblerComponent.served[i] += storageItem[itemId][0];
                                        assemblerComponent.incServed[i] += storageItem[itemId][1];
                                        storageItem[itemId][0] = 0;
                                        storageItem[itemId][1] = 0;
                                    }
                                }
                            }
                            for (int i = 0; i < assemblerComponent.products.Length; i++) {
                                itemId = assemblerComponent.products[i];
                                if (!storageItem.ContainsKey(itemId)) {
                                    storageItem[itemId] = new int[] { 0, 0 };
                                }
                                count = assemblerComponent.produced[i];
                                storageItem[itemId][0] += count;
                                assemblerComponent.produced[i] = 0;
                                if (assemblerComponent.recipeType == ERecipeType.Smelt) {
                                    storageItem[itemId][1] += 4 * count;
                                }
                            }
                        }
                    }
                }
            }
        }


        [HarmonyPatch(typeof(EjectorComponent), nameof(EjectorComponent.InternalUpdate))]
        [HarmonyPostfix]
        public static void EjectorComponent_InternalUpdate_Patch(ref EjectorComponent __instance, long tick)
        {
            int num = (int)(tick % 60);
            if (num != 0) {
                return;
            }
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planetId);
            if (planetOrbitalRingData == null) return;
            //LogError($"planetOrbitalRingData.Rings.Count {planetOrbitalRingData.Rings.Count}");
            for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
                if (!planetOrbitalRingData.Rings[ringId].IsOneFull()) continue;
                for (int i = 0; i < planetOrbitalRingData.Rings[ringId].Capacity; i++) {
                    var pair = planetOrbitalRingData.Rings[ringId].GetPair(i);
                    if (pair.stationType == StationType.EjectorCore) {
                        if (pair.OrbitalCorePoolId == __instance.id) {
                            if (__instance.bulletCount == 0) {
                                if (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem.ContainsKey(__instance.bulletId)) {
                                    int count = (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.bulletId][0] >= 40) ? 40 : planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.bulletId][0];
                                    __instance.bulletCount += count;
                                    int inc = split_inc(ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.bulletId][0], ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.bulletId][1], count);
                                    __instance.bulletInc += inc;
                                }
                            }
                            return;
                        }
                    }
                }
            }
        }


        [HarmonyPatch(typeof(TurretComponent), nameof(TurretComponent.InternalUpdate))]
        [HarmonyPostfix]
        public static void TurretComponent_InternalUpdate_Patch(ref TurretComponent __instance, long time, PlanetFactory factory)
        {
            int num = (int)(time % 60);
            if (num != 0) {
                return;
            }
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(factory.planetId);
            if (planetOrbitalRingData == null) return;
            //LogError($"planetOrbitalRingData.Rings.Count {planetOrbitalRingData.Rings.Count}");
            for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
                if (!planetOrbitalRingData.Rings[ringId].IsOneFull()) continue;
                for (int i = 0; i < planetOrbitalRingData.Rings[ringId].Capacity; i++) {
                    var pair = planetOrbitalRingData.Rings[ringId].GetPair(i);
                    if (pair.stationType == StationType.TurretCore) {
                        if (pair.OrbitalCorePoolId == __instance.id) {
                            if (__instance.itemCount <= 5) {
                                if (__instance.itemId != 0) {
                                    if (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem.ContainsKey(__instance.itemId)) {
                                        int count = (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.itemId][0] >= 5) ? 5 : planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.itemId][0];
                                        __instance.itemCount += (short)count;
                                        int inc = split_inc(ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.itemId][0], ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[__instance.itemId][1], count);
                                        __instance.itemInc += (short)inc;
                                    }
                                } else {
                                    int[] array = ItemProto.turretNeeds[(int)__instance.ammoType];
                                    if (array == null || array.Length == 0) {
                                        return;
                                    }
                                    for (int z = 0; z < array.Length; z++) {
                                        if (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem.ContainsKey(array[z]) && planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][0] >= 1) {
                                            int inc = split_inc(ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][0], ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][1], 1);
                                            __instance.SetNewItem(array[z], 1, (short)inc);
                                        }
                                    }
                                }
                            }
                            return;
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PowerSystem), nameof(PowerSystem.GameTick))]
        [HarmonyPostfix]
        public static void PowerSystem_GameTick_Patch(ref PowerSystem __instance, long time)
        {
            int num = (int)(time % 60);
            if (num != 0) {
                return;
            }
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
            if (planetOrbitalRingData == null) return;
            //LogError($"planetOrbitalRingData.Rings.Count {planetOrbitalRingData.Rings.Count}");
            for (int ringId = 0; ringId < planetOrbitalRingData.Rings.Count; ringId++) {
                if (!planetOrbitalRingData.Rings[ringId].IsOneFull()) continue;
                for (int i = 0; i < planetOrbitalRingData.Rings[ringId].Capacity; i++) {
                    var pair = planetOrbitalRingData.Rings[ringId].GetPair(i);
                    if (pair.stationType == StationType.PowerGenCore) {
                        if (pair.OrbitalCorePoolId < __instance.genPool.Length) {
                            int j = pair.OrbitalCorePoolId;
                            if (__instance.genPool[j].fuelCount <= 10) {
                                if (__instance.genPool[j].fuelId != 0) {
                                    int fuelId = __instance.genPool[j].fuelId;
                                    if (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem.ContainsKey(fuelId)) {
                                        int count = (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[fuelId][0] >= 10) ? 10 : planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[fuelId][0];
                                        __instance.genPool[j].fuelCount += (short)count;
                                        int inc = split_inc(ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[fuelId][0], ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[fuelId][1], count);
                                        __instance.genPool[j].fuelInc += (short)inc;
                                    }
                                } else {
                                    int[] array = ItemProto.fuelNeeds[(int)__instance.genPool[j].fuelMask];
                                    if (array == null || array.Length == 0) {
                                        return;
                                    }
                                    for (int z = 0; z < array.Length; z++) {
                                        if (planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem.ContainsKey(array[z]) && planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][0] >= 1) {
                                            int inc = split_inc(ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][0], ref planetOrbitalRingData.Rings[ringId].orbitalRingStorage.storageItem[array[z]][1], 1);
                                            __instance.genPool[j].SetNewFuel(array[z], 1, (short)inc);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
