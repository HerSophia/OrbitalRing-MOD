using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Playables;
using UnityEngine;
using System.Runtime.Remoting.Messaging;

namespace ProjectOrbitalRing.Patches.Logic
{
    //internal class PlanetFactory_Import_Patch
    //{
    //    static int iCount = 1;
    //    static int iCount2 = 1;

    //    [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.Import))]
    //    [HarmonyPrefix]
    //    public static bool PlanetFactory_Import_PrePatch(PlanetFactory __instance, int _index, GameData _gameData, Stream s, BinaryReader r)
    //    {
    //        if (DSPGame.IsMenuDemo || GameMain.mainPlayer == null) {
    //            return true;
    //        }
    //        __instance.index = _index;
    //        __instance.gameData = _gameData;
    //        __instance.sector = __instance.gameData.spaceSector;
    //        __instance.skillSystem = __instance.gameData.spaceSector.skillSystem;
    //        int num = r.ReadInt32();
    //        bool flag = num >= 2;
    //        bool flag2 = num >= 4;
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Planet);
    //        int num2 = r.ReadInt32();
    //        __instance.planet = __instance.gameData.galaxy.PlanetById(num2);
    //        __instance.planet.factory = __instance;
    //        __instance.planet.factoryIndex = _index;
    //        _ = __instance.gameData.gameDesc.savedThemeIds;
    //        if (num >= 5) {
    //            int num3 = 0;
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            num3 = r.ReadInt32();
    //            __instance.planet.style = num3;
    //        } else {
    //            __instance.planet.style = 0;
    //        }

    //        __instance.planet.ImportRuntime(r);
    //        if (num >= 3) {
    //            __instance.landed = r.ReadBoolean();
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Planet);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Factory);
    //        if (num >= 8) {
    //            __instance.hashSystemDynamic = new HashSystem(import: true);
    //            __instance.hashSystemDynamic.Import(r);
    //            __instance.hashSystemStatic = new HashSystem(import: true);
    //            __instance.hashSystemStatic.Import(r);
    //        } else {
    //            __instance.hashSystemDynamic = new HashSystem();
    //            __instance.hashSystemStatic = new HashSystem();
    //        }

    //        __instance.spaceHashSystemDynamic = new DFSDynamicHashSystem();
    //        __instance.spaceHashSystemDynamic.Init(__instance.planet);
    //        int num4 = 0;
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Entity);
    //        if (flag2) {
    //            num4 = r.ReadInt32();
    //            __instance.SetEntityCapacity(num4);
    //            __instance.entityCursor = r.ReadInt32();
    //            __instance.entityRecycleCursor = r.ReadInt32();
    //            if (num <= 8) {
    //                for (int i = 1; i < __instance.entityCursor; i++) {
    //                    __instance.entityPool[i].Import(s, r);
    //                    if (__instance.entityPool[i].id == 0) {
    //                        continue;
    //                    }

    //                    bool flag3 = false;
    //                    __instance.entityAnimPool[i].time = r.ReadSingle();
    //                    __instance.entityAnimPool[i].prepare_length = r.ReadSingle();
    //                    __instance.entityAnimPool[i].working_length = r.ReadSingle();
    //                    __instance.entityAnimPool[i].state = r.ReadUInt32();
    //                    __instance.entityAnimPool[i].power = r.ReadSingle();
    //                    __instance.entitySignPool[i].signType = r.ReadByte();
    //                    __instance.entitySignPool[i].iconType = r.ReadByte();
    //                    if (__instance.entitySignPool[i].iconType >= 128) {
    //                        flag3 = true;
    //                        __instance.entitySignPool[i].iconType -= 128u;
    //                    }

    //                    __instance.entitySignPool[i].iconId0 = r.ReadUInt16();
    //                    if (flag3) {
    //                        __instance.entitySignPool[i].count0 = r.ReadSingle();
    //                    }

    //                    __instance.entitySignPool[i].x = r.ReadSingle();
    //                    __instance.entitySignPool[i].y = r.ReadSingle();
    //                    __instance.entitySignPool[i].z = r.ReadSingle();
    //                    __instance.entitySignPool[i].w = r.ReadSingle();
    //                    int num5 = i * 24;
    //                    int num6 = num5 + 16;
    //                    for (int j = num5; j < num6; j++) {
    //                        if (r.ReadByte() == 0) {
    //                            __instance.entityConnPool[j] = 0;
    //                        } else {
    //                            __instance.entityConnPool[j] = r.ReadInt32();
    //                        }
    //                    }
    //                    for (int j = num6; j < num6 + 8; j++) {
    //                        __instance.entityConnPool[j] = 0;
    //                    }
    //                    iCount++;

    //                    if (__instance.entityPool[i].beltId == 0 && __instance.entityPool[i].inserterId == 0 && __instance.entityPool[i].splitterId == 0 && __instance.entityPool[i].monitorId == 0 && __instance.entityPool[i].spraycoaterId == 0 && __instance.entityPool[i].pilerId == 0) {
    //                        __instance.entityMutexs[i] = new Mutex(i);
    //                    }
    //                }
    //            } else {
    //                for (int k = 1; k < __instance.entityCursor; k++) {
    //                    __instance.entityPool[k].Import(s, r);
    //                    if (__instance.entityPool[k].id != 0 && __instance.entityPool[k].beltId == 0 && __instance.entityPool[k].inserterId == 0 && __instance.entityPool[k].splitterId == 0 && __instance.entityPool[k].monitorId == 0 && __instance.entityPool[k].spraycoaterId == 0 && __instance.entityPool[k].pilerId == 0) {
    //                        __instance.entityMutexs[k] = new Mutex(k);
    //                    }
    //                }

    //                UnsafeIO.ReadMassive(s, __instance.entityAnimPool, __instance.entityCursor);
    //                for (int l = 1; l < __instance.entityCursor; l++) {
    //                    if (__instance.entityPool[l].id != 0) {
    //                        bool flag4 = false;
    //                        __instance.entitySignPool[l].signType = r.ReadByte();
    //                        __instance.entitySignPool[l].iconType = r.ReadByte();
    //                        if (__instance.entitySignPool[l].iconType >= 128) {
    //                            flag4 = true;
    //                            __instance.entitySignPool[l].iconType -= 128u;
    //                        }

    //                        __instance.entitySignPool[l].iconId0 = r.ReadUInt16();
    //                        if (flag4) {
    //                            __instance.entitySignPool[l].count0 = r.ReadSingle();
    //                        }

    //                        UnsafeIO.Read(s, ref __instance.entitySignPool[l], 40, 16);
    //                    }
    //                }

    //                for (int m = 16; m < __instance.entityCursor * 16; m++) {
    //                    int tempxxx = 8;
    //                    if (__instance.entityPool[m / 16].id != 0) {
    //                        if (r.ReadByte() == 0) {
    //                            __instance.entityConnPool[m + tempxxx] = 0;
    //                        } else {
    //                            __instance.entityConnPool[m + tempxxx] = r.ReadInt32();
    //                        }

    //                        if (m % 16 == 0 && m != 16) {
    //                            for (int xxx = 1; xxx <= 8; xxx++) {
    //                                tempxxx++;
    //                                __instance.entityConnPool[m + tempxxx] = 0;
    //                            }
    //                        }
    //                    }
    //                }
    //            }

    //            for (int n = 0; n < __instance.entityRecycleCursor; n++) {
    //                __instance.entityRecycle[n] = r.ReadInt32();
    //            }

    //            num4 = r.ReadInt32();
    //            __instance.SetPrebuildCapacity(num4);
    //            __instance.prebuildCursor = r.ReadInt32();
    //            __instance.prebuildRecycleCursor = r.ReadInt32();
    //            for (int num7 = 1; num7 < __instance.prebuildCursor; num7++) {
    //                __instance.prebuildPool[num7].Import(r);
    //                if (__instance.prebuildPool[num7].id == 0) {
    //                    continue;
    //                }

    //                int num8 = num7 * 24;
    //                int num9 = num8 + 16;
    //                for (int num10 = num8; num10 < num9; num10++) {
    //                    if (r.ReadByte() == 0) {
    //                        __instance.prebuildConnPool[num10 + iCount2 * 8] = 0;
    //                    } else {
    //                        __instance.prebuildConnPool[num10 + iCount2 * 8] = r.ReadInt32();
    //                    }
    //                }
    //                for (int num10 = num9; num10 < num9 + 8; num10++) {
    //                    __instance.prebuildConnPool[num10] = 0;
    //                }
    //                iCount2++;
    //            }

    //            for (int num11 = 0; num11 < __instance.prebuildRecycleCursor; num11++) {
    //                __instance.prebuildRecycle[num11] = r.ReadInt32();
    //            }
    //        } else {
    //            num4 = r.ReadInt32();
    //            __instance.SetEntityCapacity(num4);
    //            __instance.entityCursor = r.ReadInt32();
    //            __instance.entityRecycleCursor = r.ReadInt32();
    //            for (int num12 = 1; num12 < __instance.entityCursor; num12++) {
    //                __instance.entityPool[num12].Import(s, r);
    //                if (__instance.entityPool[num12].id != 0 && __instance.entityPool[num12].beltId == 0 && __instance.entityPool[num12].inserterId == 0 && __instance.entityPool[num12].splitterId == 0 && __instance.entityPool[num12].monitorId == 0 && __instance.entityPool[num12].spraycoaterId == 0 && __instance.entityPool[num12].pilerId == 0) {
    //                    __instance.entityMutexs[num12] = new Mutex(num12);
    //                }
    //            }

    //            for (int num13 = 1; num13 < __instance.entityCursor; num13++) {
    //                __instance.entityAnimPool[num13].time = r.ReadSingle();
    //                __instance.entityAnimPool[num13].prepare_length = r.ReadSingle();
    //                __instance.entityAnimPool[num13].working_length = r.ReadSingle();
    //                __instance.entityAnimPool[num13].state = r.ReadUInt32();
    //                __instance.entityAnimPool[num13].power = r.ReadSingle();
    //            }

    //            if (flag) {
    //                for (int num14 = 1; num14 < __instance.entityCursor; num14++) {
    //                    __instance.entitySignPool[num14].signType = r.ReadByte();
    //                    __instance.entitySignPool[num14].iconType = r.ReadByte();
    //                    __instance.entitySignPool[num14].iconId0 = r.ReadUInt16();
    //                    __instance.entitySignPool[num14].x = r.ReadSingle();
    //                    __instance.entitySignPool[num14].y = r.ReadSingle();
    //                    __instance.entitySignPool[num14].z = r.ReadSingle();
    //                    __instance.entitySignPool[num14].w = r.ReadSingle();
    //                }
    //            } else {
    //                for (int num15 = 1; num15 < __instance.entityCursor; num15++) {
    //                    __instance.entitySignPool[num15].signType = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].iconType = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].iconId0 = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].iconId1 = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].iconId2 = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].iconId3 = r.ReadUInt32();
    //                    __instance.entitySignPool[num15].count0 = r.ReadSingle();
    //                    __instance.entitySignPool[num15].count1 = r.ReadSingle();
    //                    __instance.entitySignPool[num15].count2 = r.ReadSingle();
    //                    __instance.entitySignPool[num15].count3 = r.ReadSingle();
    //                    __instance.entitySignPool[num15].x = r.ReadSingle();
    //                    __instance.entitySignPool[num15].y = r.ReadSingle();
    //                    __instance.entitySignPool[num15].z = r.ReadSingle();
    //                    __instance.entitySignPool[num15].w = r.ReadSingle();
    //                }
    //            }

    //            int num16 = __instance.entityCursor * 16;
    //            int tempxxx = 8;
    //            for (int num17 = 16; num17 < num16; num17++) {
    //                __instance.entityConnPool[num17 + tempxxx] = r.ReadInt32();
    //                if (num17 % 16 == 0 && num17 != 16) {
    //                    for (int xxx = 1; xxx <= 8; xxx++) {
    //                        tempxxx++;
    //                        __instance.entityConnPool[num17 + tempxxx] = 0;
    //                    }
    //                }
    //            }

    //            for (int num18 = 0; num18 < __instance.entityRecycleCursor; num18++) {
    //                __instance.entityRecycle[num18] = r.ReadInt32();
    //            }

    //            num4 = r.ReadInt32();
    //            __instance.SetPrebuildCapacity(num4);
    //            __instance.prebuildCursor = r.ReadInt32();
    //            __instance.prebuildRecycleCursor = r.ReadInt32();
    //            for (int num19 = 1; num19 < __instance.prebuildCursor; num19++) {
    //                __instance.prebuildPool[num19].Import(r);
    //            }

    //            int num20 = __instance.prebuildCursor * 16;
    //            tempxxx = 8;
    //            for (int num21 = 16; num21 < num20; num21++) {
    //                __instance.prebuildConnPool[num21 + tempxxx] = r.ReadInt32();
    //                if (num21 % 16 == 0 && num21 != 16) {
    //                    for (int xxx = 1; xxx <= 8; xxx++) {
    //                        tempxxx++;
    //                        __instance.prebuildConnPool[num21 + tempxxx] = 0;
    //                    }
    //                }
    //            }

    //            for (int num22 = 0; num22 < __instance.prebuildRecycleCursor; num22++) {
    //                __instance.prebuildRecycle[num22] = r.ReadInt32();
    //            }
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Entity);
    //        if (num >= 8) {
    //            num4 = r.ReadInt32();
    //            __instance.SetCraftCapacity(num4);
    //            __instance.craftCursor = r.ReadInt32();
    //            __instance.craftRecycleCursor = r.ReadInt32();
    //            for (int num23 = 1; num23 < __instance.craftCursor; num23++) {
    //                __instance.craftPool[num23].Import(r);
    //            }

    //            for (int num24 = 0; num24 < __instance.craftRecycleCursor; num24++) {
    //                __instance.craftRecycle[num24] = r.ReadInt32();
    //            }

    //            for (int num25 = 1; num25 < __instance.craftCursor; num25++) {
    //                __instance.craftAnimPool[num25].time = r.ReadSingle();
    //                __instance.craftAnimPool[num25].prepare_length = r.ReadSingle();
    //                __instance.craftAnimPool[num25].working_length = r.ReadSingle();
    //                __instance.craftAnimPool[num25].state = r.ReadUInt32();
    //                __instance.craftAnimPool[num25].power = r.ReadSingle();
    //            }

    //            if (__instance.gameData.patch < 10) {
    //                for (int num26 = 1; num26 < __instance.craftCursor; num26++) {
    //                    ref CraftData reference = ref __instance.craftPool[num26];
    //                    if (reference.fleetId > 0 && reference.owner > 0 && reference.port == 1) {
    //                        reference.port = 0;
    //                    }
    //                }
    //            }
    //        } else {
    //            __instance.SetCraftCapacity(64);
    //            __instance.craftCursor = 1;
    //            __instance.craftRecycleCursor = 0;
    //        }

    //        PerformanceMonitor.BeginData(ESaveDataEntry.Combat);
    //        if (num >= 8) {
    //            num4 = r.ReadInt32();
    //            __instance.SetEnemyCapacity(num4);
    //            __instance.enemyCursor = r.ReadInt32();
    //            __instance.enemyRecycleCursor = r.ReadInt32();
    //            for (int num27 = 1; num27 < __instance.enemyCursor; num27++) {
    //                __instance.enemyPool[num27].Import(r);
    //            }

    //            for (int num28 = 0; num28 < __instance.enemyRecycleCursor; num28++) {
    //                __instance.enemyRecycle[num28] = r.ReadInt32();
    //            }

    //            for (int num29 = 1; num29 < __instance.enemyCursor; num29++) {
    //                __instance.enemyAnimPool[num29].time = r.ReadSingle();
    //                __instance.enemyAnimPool[num29].prepare_length = r.ReadSingle();
    //                __instance.enemyAnimPool[num29].working_length = r.ReadSingle();
    //                __instance.enemyAnimPool[num29].state = r.ReadUInt32();
    //                __instance.enemyAnimPool[num29].power = r.ReadSingle();
    //            }
    //        } else {
    //            __instance.SetEnemyCapacity(24);
    //            __instance.enemyCursor = 1;
    //            __instance.enemyRecycleCursor = 0;
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Combat);
    //        PerformanceMonitor.EndData(ESaveDataEntry.Factory);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Planet);
    //        num4 = r.ReadInt32();
    //        __instance.SetVegeCapacity(num4);
    //        __instance.vegeCursor = r.ReadInt32();
    //        __instance.vegeRecycleCursor = r.ReadInt32();
    //        for (int num30 = 1; num30 < __instance.vegeCursor; num30++) {
    //            __instance.vegePool[num30].Import(r);
    //        }

    //        for (int num31 = 0; num31 < __instance.vegeRecycleCursor; num31++) {
    //            __instance.vegeRecycle[num31] = r.ReadInt32();
    //        }

    //        num4 = r.ReadInt32();
    //        __instance.SetVeinCapacity(num4);
    //        __instance.veinCursor = r.ReadInt32();
    //        __instance.veinRecycleCursor = r.ReadInt32();
    //        int num32 = 0;
    //        for (int num33 = 1; num33 < __instance.veinCursor; num33++) {
    //            __instance.veinPool[num33].Import(r);
    //            if (num < 7) {
    //                __instance.veinPool[num33].groupIndex++;
    //            }

    //            if (__instance.veinPool[num33].groupIndex < 0) {
    //                __instance.veinPool[num33].groupIndex = 0;
    //            }

    //            if (__instance.veinPool[num33].groupIndex > num32) {
    //                num32 = __instance.veinPool[num33].groupIndex;
    //            }
    //        }

    //        for (int num34 = 0; num34 < __instance.veinRecycleCursor; num34++) {
    //            __instance.veinRecycle[num34] = r.ReadInt32();
    //        }

    //        for (int num35 = 1; num35 < __instance.veinCursor; num35++) {
    //            __instance.veinAnimPool[num35].time = r.ReadSingle();
    //            __instance.veinAnimPool[num35].prepare_length = r.ReadSingle();
    //            __instance.veinAnimPool[num35].working_length = r.ReadSingle();
    //            __instance.veinAnimPool[num35].state = r.ReadUInt32();
    //            __instance.veinAnimPool[num35].power = r.ReadSingle();
    //        }

    //        __instance.InitVeinGroups(num32);
    //        __instance.RecalculateAllVeinGroups();
    //        PerformanceMonitor.EndData(ESaveDataEntry.Planet);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Factory);
    //        if (num < 8) {
    //            __instance.RefreshHashSystems();
    //        }

    //        PerformanceMonitor.BeginData(ESaveDataEntry.Ruin);
    //        if (num >= 8) {
    //            num4 = r.ReadInt32();
    //            __instance.SetRuinCapacity(num4);
    //            __instance.ruinCursor = r.ReadInt32();
    //            __instance.ruinRecycleCursor = r.ReadInt32();
    //            for (int num36 = 1; num36 < __instance.ruinCursor; num36++) {
    //                __instance.ruinPool[num36].Import(r);
    //            }

    //            for (int num37 = 0; num37 < __instance.ruinRecycleCursor; num37++) {
    //                __instance.ruinRecycle[num37] = r.ReadInt32();
    //            }
    //        } else {
    //            __instance.SetRuinCapacity(24);
    //            __instance.ruinCursor = 1;
    //            __instance.ruinRecycleCursor = 0;
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Ruin);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.BeltAndCargo);
    //        __instance.cargoContainer = new CargoContainer(import: true);
    //        __instance.cargoContainer.Import(r);
    //        __instance.cargoTraffic = new CargoTraffic();
    //        __instance.cargoTraffic.planet = __instance.planet;
    //        __instance.cargoTraffic.factory = __instance;
    //        __instance.cargoTraffic.container = __instance.cargoContainer;
    //        __instance.cargoTraffic.Import(r);
    //        PerformanceMonitor.EndData(ESaveDataEntry.BeltAndCargo);
    //        __instance.blockContainer = new MiniBlockContainer();
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Storage);
    //        __instance.factoryStorage = new FactoryStorage(__instance.planet, import: true);
    //        __instance.factoryStorage.Import(r);
    //        PerformanceMonitor.EndData(ESaveDataEntry.Storage);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.PowerSystem);
    //        __instance.powerSystem = new PowerSystem(__instance.planet, import: true);
    //        __instance.powerSystem.Import(r);
    //        PerformanceMonitor.EndData(ESaveDataEntry.PowerSystem);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Facility);
    //        __instance.factorySystem = new FactorySystem(__instance.planet, import: true);
    //        __instance.factorySystem.Import(r);
    //        PerformanceMonitor.EndData(ESaveDataEntry.Facility);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Combat);
    //        if (num >= 8) {
    //            __instance.enemySystem = new EnemyDFGroundSystem(__instance.planet, import: true);
    //            __instance.enemySystem.Import(r);
    //            __instance.combatGroundSystem = new CombatGroundSystem(__instance.planet, import: true);
    //            __instance.combatGroundSystem.Import(r);
    //            PerformanceMonitor.BeginData(ESaveDataEntry.Defense);
    //            __instance.defenseSystem = new DefenseSystem(__instance.planet, import: true);
    //            __instance.defenseSystem.Import(r);
    //            __instance.planetATField = new PlanetATField(__instance.planet, import: true);
    //            __instance.planetATField.Import(r);
    //            __instance.planetATField.UpdatePhysicsShape(updateGeneratorMatrix: true);
    //            PerformanceMonitor.EndData(ESaveDataEntry.Defense);
    //            PerformanceMonitor.BeginData(ESaveDataEntry.Construction);
    //            __instance.constructionSystem = new ConstructionSystem(__instance.planet, import: true);
    //            __instance.constructionSystem.Import(r);
    //            PerformanceMonitor.EndData(ESaveDataEntry.Construction);
    //            if (__instance.gameData.patch < 12) {
    //                DFGBaseComponent[] buffer = __instance.enemySystem.bases.buffer;
    //                int cursor = __instance.enemySystem.bases.cursor;
    //                for (int num38 = 1; num38 < cursor; num38++) {
    //                    if (buffer[num38] == null || buffer[num38].id != num38 || buffer[num38].ruinId <= 0) {
    //                        continue;
    //                    }

    //                    if (__instance.ruinPool[buffer[num38].ruinId].id != buffer[num38].ruinId) {
    //                        buffer[num38].ruinId = 0;
    //                        continue;
    //                    }

    //                    Vector3 normalized = __instance.ruinPool[buffer[num38].ruinId].pos.normalized;
    //                    Vector3 vector = __instance.enemyPool[buffer[num38].enemyId].pos.normalized;
    //                    if ((normalized * 200f - vector * 200f).magnitude > 10f) {
    //                        buffer[num38].ruinId = 0;
    //                    }
    //                }

    //                PowerGeneratorComponent[] genPool = __instance.powerSystem.genPool;
    //                int genCursor = __instance.powerSystem.genCursor;
    //                for (int num39 = 1; num39 < genCursor; num39++) {
    //                    ref PowerGeneratorComponent reference2 = ref genPool[num39];
    //                    if (reference2.id != num39 || !reference2.geothermal || genPool[num39].baseRuinId <= 0) {
    //                        continue;
    //                    }

    //                    if (__instance.ruinPool[genPool[num39].baseRuinId].id != genPool[num39].baseRuinId) {
    //                        genPool[num39].baseRuinId = 0;
    //                        continue;
    //                    }

    //                    Vector3 normalized2 = __instance.ruinPool[genPool[num39].baseRuinId].pos.normalized;
    //                    Vector3 normalized3 = __instance.entityPool[genPool[num39].entityId].pos.normalized;
    //                    if ((normalized2 * 200f - normalized3 * 200f).magnitude > 10f) {
    //                        genPool[num39].baseRuinId = 0;
    //                    }
    //                }
    //            }

    //            _ = __instance.gameData.patch;
    //            _ = 15;
    //            DataPool<UnitComponent> units = __instance.combatGroundSystem.units;
    //            for (int num40 = 1; num40 < units.cursor; num40++) {
    //                ref UnitComponent reference3 = ref units.buffer[num40];
    //                if (reference3.id != num40) {
    //                    continue;
    //                }

    //                ref CraftData reference4 = ref __instance.craftPool[reference3.craftId];
    //                if (reference4.id == reference3.craftId) {
    //                    if (reference4.owner <= 0) {
    //                        __instance.RemoveCraftWithComponents(reference4.id);
    //                    } else if (__instance.craftPool[reference4.owner].id != reference4.owner) {
    //                        __instance.RemoveCraftWithComponents(reference4.id);
    //                    }
    //                }
    //            }

    //            DataPool<FleetComponent> fleets = __instance.combatGroundSystem.fleets;
    //            for (int num41 = 1; num41 < fleets.cursor; num41++) {
    //                ref FleetComponent reference5 = ref fleets.buffer[num41];
    //                if (reference5.id == num41) {
    //                    ref CraftData reference6 = ref __instance.craftPool[reference5.craftId];
    //                    if (reference6.id == reference5.craftId && !reference5.CheckOwnerExist(ref reference6, __instance, __instance.gameData.mainPlayer.mecha)) {
    //                        __instance.RemoveCraftWithComponents(reference5.craftId);
    //                    }
    //                }
    //            }

    //            ObjectPool<CombatModuleComponent> combatModules = __instance.combatGroundSystem.combatModules;
    //            for (int num42 = 1; num42 < combatModules.cursor; num42++) {
    //                ref CombatModuleComponent reference7 = ref combatModules.buffer[num42];
    //                if (reference7 == null || reference7.id != num42 || __instance.entityPool[reference7.entityId].id != reference7.entityId) {
    //                    continue;
    //                }

    //                ModuleFleet[] moduleFleets = reference7.moduleFleets;
    //                int fleetCount = reference7.fleetCount;
    //                for (int num43 = 0; num43 < fleetCount; num43++) {
    //                    int fleetId = moduleFleets[num43].fleetId;
    //                    if (fleetId == 0) {
    //                        continue;
    //                    }

    //                    fleets = __instance.combatGroundSystem.fleets;
    //                    ref FleetComponent reference8 = ref fleets.buffer[fleetId];
    //                    if (reference8.owner != reference7.entityId) {
    //                        moduleFleets[num43].ClearFleetForeignKey();
    //                        continue;
    //                    }

    //                    ref CraftData reference9 = ref __instance.craftPool[reference8.craftId];
    //                    if (reference9.id != reference8.craftId || reference9.owner != reference8.owner) {
    //                        moduleFleets[num43].ClearFleetForeignKey();
    //                        continue;
    //                    }

    //                    ModuleFighter[] fighters = moduleFleets[num43].fighters;
    //                    int num44 = fighters.Length;
    //                    for (int num45 = 0; num45 < num44; num45++) {
    //                        int craftId = fighters[num45].craftId;
    //                        if (craftId != 0) {
    //                            ref CraftData reference10 = ref __instance.craftPool[craftId];
    //                            if (reference10.id != craftId || reference10.unitId == 0) {
    //                                fighters[num45].ClearFighterForeignKey();
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        } else {
    //            __instance.enemySystem = new EnemyDFGroundSystem(__instance.planet);
    //            __instance.combatGroundSystem = new CombatGroundSystem(__instance.planet);
    //            __instance.defenseSystem = new DefenseSystem(__instance.planet);
    //            __instance.planetATField = new PlanetATField(__instance.planet);
    //            __instance.constructionSystem = new ConstructionSystem(__instance.planet);
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Combat);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Transport);
    //        __instance.transport = new PlanetTransport(__instance.gameData, __instance.planet, import: true);
    //        __instance.transport.Init();
    //        __instance.transport.Import(r);
    //        PerformanceMonitor.EndData(ESaveDataEntry.Transport);
    //        if (num < 4) {
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            r.ReadInt32();
    //        }

    //        PerformanceMonitor.BeginData(ESaveDataEntry.Platform);
    //        if (num >= 1) {
    //            __instance.platformSystem = new PlatformSystem(__instance.planet, import: true);
    //            __instance.platformSystem.Import(r);
    //        } else {
    //            __instance.platformSystem = new PlatformSystem(__instance.planet);
    //        }

    //        __instance.enemySystem.RefreshPlanetReformState();
    //        PerformanceMonitor.EndData(ESaveDataEntry.Platform);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Digital);
    //        if (num >= 6) {
    //            __instance.digitalSystem = new DigitalSystem(__instance.planet, import: true);
    //            __instance.digitalSystem.Import(r);
    //        } else {
    //            __instance.digitalSystem = new DigitalSystem(__instance.planet);
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Digital);
    //        ProductionStatistics production = __instance.gameData.statistics.production;
    //        if (production.factoryStatPool[__instance.index] == null) {
    //            production.CreateFactoryStat(__instance.index);
    //        }

    //        if (__instance.entityCount > 0 || __instance.prebuildCount > 0 || __instance.veinRecycleCursor > 0 || __instance.vegeRecycleCursor > 0 || __instance.planet.id == _gameData.galaxy.birthPlanetId) {
    //            __instance.landed = true;
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Factory);
    //        return false;
    //    }
    //}
}
