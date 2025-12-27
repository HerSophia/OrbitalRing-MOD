using HarmonyLib;
using ProjectOrbitalRing.Patches.Logic.AssemblerModule;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic.PlanetFocus
{
    internal class MoonPatch
    {
        private static ConcurrentDictionary<ValueTuple<int, int>, int> ColliderAccumulatorIncData = new ConcurrentDictionary<ValueTuple<int, int>, int>();

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.PickFrom))]
        public static void FactorySystem_PickFrom_Patch(PlanetFactory __instance, int entityId, int offset, int filter, int[] needs, ref byte stack, ref byte inc, int __result)
        {
            int beltId = __instance.entityPool[entityId].beltId;
            if (beltId <= 0) {
                int assemblerId = __instance.entityPool[entityId].assemblerId;
                if (assemblerId > 0) {
                    AssemblerComponent assembler = __instance.factorySystem.assemblerPool[assemblerId];
                    // 贫瘠荒漠特质，熔炉产物自带增产
                    if (__instance.planet.theme == 11) {
                        if ((assembler.recipeType == ERecipeType.Smelt || assembler.recipeType == (ERecipeType)11) && assembler.speed < 40000) {
                            if (__result != 0) {
                                inc = 4;
                            }
                        }
                    }

                    if (assembler.recipeId == 104) {
                        if (__result == 2206) {
                            ValueTuple<int, int> key = new ValueTuple<int, int>(__instance.planetId, assemblerId);
                            bool flag = ColliderAccumulatorIncData.ContainsKey(key) && ColliderAccumulatorIncData[key] >= 4;
                            if (flag) {
                                ColliderAccumulatorIncData[key] -= 4;
                                inc = 4;
                                if (ColliderAccumulatorIncData[key] < 0) {
                                    ColliderAccumulatorIncData[key] = 0;
                                }
                            }

                        }
                    }
                }
            }
        }


        public static void Export(BinaryWriter w)
        {
            w.Write(ColliderAccumulatorIncData.Count);
            foreach (KeyValuePair<ValueTuple<int, int>, int> keyValuePair in ColliderAccumulatorIncData) {
                w.Write(keyValuePair.Key.Item1);
                w.Write(keyValuePair.Key.Item2);
                w.Write(keyValuePair.Value);
            }
        }

        // Token: 0x0600015D RID: 349 RVA: 0x0000FF30 File Offset: 0x0000E130
        public static void Import(BinaryReader r)
        {
            ReInitAll();
            try {
                int num = r.ReadInt32();
                for (int i = 0; i < num; i++) {
                    int planetId = r.ReadInt32();
                    int assemblerId = r.ReadInt32();
                    int AccumulatorInc = r.ReadInt32();
                    ColliderAccumulatorIncData.TryAdd(new ValueTuple<int, int>(planetId, assemblerId), AccumulatorInc);
                }
            } catch (EndOfStreamException) {
            }
        }

        // Token: 0x0600015E RID: 350 RVA: 0x0000FFD4 File Offset: 0x0000E1D4
        public static void IntoOtherSave()
        {
            ReInitAll();
        }

        // Token: 0x0600015F RID: 351 RVA: 0x0000FFDC File Offset: 0x0000E1DC
        private static void ReInitAll()
        {
            ColliderAccumulatorIncData = new ConcurrentDictionary<ValueTuple<int, int>, int>();
        }


        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.InsertInto))]
        public static void FactorySystem_InsertInto_Patch(PlanetFactory __instance, int entityId, int offset, int itemId, byte itemCount, byte itemInc, ref byte remainInc, int __result)
        {
            int beltId = __instance.entityPool[entityId].beltId;
            if (beltId <= 0) {
                int[] array = __instance.entityNeeds[entityId];
                int assemblerId = __instance.entityPool[entityId].assemblerId;
                if (assemblerId > 0) {
                    if (array == null) {
                        return;
                    }
                    ref AssemblerComponent ptr = ref __instance.factorySystem.assemblerPool[assemblerId];
                    if (ptr.recipeId == 104 && itemId == 2207) {
                        ValueTuple<int, int> key = new ValueTuple<int, int>(__instance.planetId, assemblerId);
                        ColliderAccumulatorIncData.AddOrUpdate(key, itemInc, (k, v) => v + itemInc);
                    }

                }
            }
        }
    }
}
