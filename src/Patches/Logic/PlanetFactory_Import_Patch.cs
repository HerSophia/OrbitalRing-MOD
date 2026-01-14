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
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic
{
    internal class PlanetFactory_Import_Patch
    {

    }
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
    //        int j = r.ReadInt32();
    //        bool flag = j >= 2;
    //        bool flag2 = j >= 4;
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Planet);
    //        int i2 = r.ReadInt32();
    //        __instance.planet = __instance.gameData.galaxy.PlanetById(i2);
    //        __instance.planet.factory = __instance;
    //        __instance.planet.factoryIndex = _index;
    //        _ = __instance.gameData.gameDesc.savedThemeIds;
    //        if (j >= 5) {
    //            int i3 = 0;
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            i3 = r.ReadInt32();
    //            __instance.planet.style = i3;
    //        } else {
    //            __instance.planet.style = 0;
    //        }

    //        __instance.planet.ImportRuntime(r);
    //        if (j >= 3) {
    //            __instance.landed = r.ReadBoolean();
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Planet);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Factory);
    //        if (j >= 8) {
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
    //        int i4 = 0;
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Entity);
    //        if (flag2) {
    //            i4 = r.ReadInt32();
    //            __instance.SetEntityCapacity(i4);
    //            __instance.entityCursor = r.ReadInt32();
    //            __instance.entityRecycleCursor = r.ReadInt32();
    //            if (j <= 8) {
    //                for (int j = 1; j < __instance.entityCursor; j++) {
    //                    __instance.entityPool[j].Import(s, r);
    //                    if (__instance.entityPool[j].id == 0) {
    //                        continue;
    //                    }

    //                    bool flag3 = false;
    //                    __instance.entityAnimPool[j].time = r.ReadSingle();
    //                    __instance.entityAnimPool[j].prepare_length = r.ReadSingle();
    //                    __instance.entityAnimPool[j].working_length = r.ReadSingle();
    //                    __instance.entityAnimPool[j].state = r.ReadUInt32();
    //                    __instance.entityAnimPool[j].power = r.ReadSingle();
    //                    __instance.entitySignPool[j].signType = r.ReadByte();
    //                    __instance.entitySignPool[j].iconType = r.ReadByte();
    //                    if (__instance.entitySignPool[j].iconType >= 128) {
    //                        flag3 = true;
    //                        __instance.entitySignPool[j].iconType -= 128u;
    //                    }

    //                    __instance.entitySignPool[j].iconId0 = r.ReadUInt16();
    //                    if (flag3) {
    //                        __instance.entitySignPool[j].count0 = r.ReadSingle();
    //                    }

    //                    __instance.entitySignPool[j].x = r.ReadSingle();
    //                    __instance.entitySignPool[j].y = r.ReadSingle();
    //                    __instance.entitySignPool[j].z = r.ReadSingle();
    //                    __instance.entitySignPool[j].w = r.ReadSingle();
    //                    int i5 = j * 24;
    //                    int i6 = i5 + 16;
    //                    for (int j = i5; j < i6; j++) {
    //                        if (r.ReadByte() == 0) {
    //                            __instance.entityConnPool[j] = 0;
    //                        } else {
    //                            __instance.entityConnPool[j] = r.ReadInt32();
    //                        }
    //                    }
    //                    for (int j = i6; j < i6 + 8; j++) {
    //                        __instance.entityConnPool[j] = 0;
    //                    }
    //                    iCount++;

    //                    if (__instance.entityPool[j].beltId == 0 && __instance.entityPool[j].inserterId == 0 && __instance.entityPool[j].splitterId == 0 && __instance.entityPool[j].monitorId == 0 && __instance.entityPool[j].spraycoaterId == 0 && __instance.entityPool[j].pilerId == 0) {
    //                        __instance.entityMutexs[j] = new Mutex(j);
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

    //            i4 = r.ReadInt32();
    //            __instance.SetPrebuildCapacity(i4);
    //            __instance.prebuildCursor = r.ReadInt32();
    //            __instance.prebuildRecycleCursor = r.ReadInt32();
    //            for (int i7 = 1; i7 < __instance.prebuildCursor; i7++) {
    //                __instance.prebuildPool[i7].Import(r);
    //                if (__instance.prebuildPool[i7].id == 0) {
    //                    continue;
    //                }

    //                int i8 = i7 * 24;
    //                int i9 = i8 + 16;
    //                for (int i10 = i8; i10 < i9; i10++) {
    //                    if (r.ReadByte() == 0) {
    //                        __instance.prebuildConnPool[i10 + iCount2 * 8] = 0;
    //                    } else {
    //                        __instance.prebuildConnPool[i10 + iCount2 * 8] = r.ReadInt32();
    //                    }
    //                }
    //                for (int i10 = i9; i10 < i9 + 8; i10++) {
    //                    __instance.prebuildConnPool[i10] = 0;
    //                }
    //                iCount2++;
    //            }

    //            for (int i11 = 0; i11 < __instance.prebuildRecycleCursor; i11++) {
    //                __instance.prebuildRecycle[i11] = r.ReadInt32();
    //            }
    //        } else {
    //            i4 = r.ReadInt32();
    //            __instance.SetEntityCapacity(i4);
    //            __instance.entityCursor = r.ReadInt32();
    //            __instance.entityRecycleCursor = r.ReadInt32();
    //            for (int i12 = 1; i12 < __instance.entityCursor; i12++) {
    //                __instance.entityPool[i12].Import(s, r);
    //                if (__instance.entityPool[i12].id != 0 && __instance.entityPool[i12].beltId == 0 && __instance.entityPool[i12].inserterId == 0 && __instance.entityPool[i12].splitterId == 0 && __instance.entityPool[i12].monitorId == 0 && __instance.entityPool[i12].spraycoaterId == 0 && __instance.entityPool[i12].pilerId == 0) {
    //                    __instance.entityMutexs[i12] = new Mutex(i12);
    //                }
    //            }

    //            for (int i13 = 1; i13 < __instance.entityCursor; i13++) {
    //                __instance.entityAnimPool[i13].time = r.ReadSingle();
    //                __instance.entityAnimPool[i13].prepare_length = r.ReadSingle();
    //                __instance.entityAnimPool[i13].working_length = r.ReadSingle();
    //                __instance.entityAnimPool[i13].state = r.ReadUInt32();
    //                __instance.entityAnimPool[i13].power = r.ReadSingle();
    //            }

    //            if (flag) {
    //                for (int i14 = 1; i14 < __instance.entityCursor; i14++) {
    //                    __instance.entitySignPool[i14].signType = r.ReadByte();
    //                    __instance.entitySignPool[i14].iconType = r.ReadByte();
    //                    __instance.entitySignPool[i14].iconId0 = r.ReadUInt16();
    //                    __instance.entitySignPool[i14].x = r.ReadSingle();
    //                    __instance.entitySignPool[i14].y = r.ReadSingle();
    //                    __instance.entitySignPool[i14].z = r.ReadSingle();
    //                    __instance.entitySignPool[i14].w = r.ReadSingle();
    //                }
    //            } else {
    //                for (int i15 = 1; i15 < __instance.entityCursor; i15++) {
    //                    __instance.entitySignPool[i15].signType = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].iconType = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].iconId0 = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].iconId1 = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].iconId2 = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].iconId3 = r.ReadUInt32();
    //                    __instance.entitySignPool[i15].count0 = r.ReadSingle();
    //                    __instance.entitySignPool[i15].count1 = r.ReadSingle();
    //                    __instance.entitySignPool[i15].count2 = r.ReadSingle();
    //                    __instance.entitySignPool[i15].count3 = r.ReadSingle();
    //                    __instance.entitySignPool[i15].x = r.ReadSingle();
    //                    __instance.entitySignPool[i15].y = r.ReadSingle();
    //                    __instance.entitySignPool[i15].z = r.ReadSingle();
    //                    __instance.entitySignPool[i15].w = r.ReadSingle();
    //                }
    //            }

    //            int i16 = __instance.entityCursor * 16;
    //            int tempxxx = 8;
    //            for (int i17 = 16; i17 < i16; i17++) {
    //                __instance.entityConnPool[i17 + tempxxx] = r.ReadInt32();
    //                if (i17 % 16 == 0 && i17 != 16) {
    //                    for (int xxx = 1; xxx <= 8; xxx++) {
    //                        tempxxx++;
    //                        __instance.entityConnPool[i17 + tempxxx] = 0;
    //                    }
    //                }
    //            }

    //            for (int i18 = 0; i18 < __instance.entityRecycleCursor; i18++) {
    //                __instance.entityRecycle[i18] = r.ReadInt32();
    //            }

    //            i4 = r.ReadInt32();
    //            __instance.SetPrebuildCapacity(i4);
    //            __instance.prebuildCursor = r.ReadInt32();
    //            __instance.prebuildRecycleCursor = r.ReadInt32();
    //            for (int i19 = 1; i19 < __instance.prebuildCursor; i19++) {
    //                __instance.prebuildPool[i19].Import(r);
    //            }

    //            int i20 = __instance.prebuildCursor * 16;
    //            tempxxx = 8;
    //            for (int i21 = 16; i21 < i20; i21++) {
    //                __instance.prebuildConnPool[i21 + tempxxx] = r.ReadInt32();
    //                if (i21 % 16 == 0 && i21 != 16) {
    //                    for (int xxx = 1; xxx <= 8; xxx++) {
    //                        tempxxx++;
    //                        __instance.prebuildConnPool[i21 + tempxxx] = 0;
    //                    }
    //                }
    //            }

    //            for (int i22 = 0; i22 < __instance.prebuildRecycleCursor; i22++) {
    //                __instance.prebuildRecycle[i22] = r.ReadInt32();
    //            }
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Entity);
    //        if (j >= 8) {
    //            i4 = r.ReadInt32();
    //            __instance.SetCraftCapacity(i4);
    //            __instance.craftCursor = r.ReadInt32();
    //            __instance.craftRecycleCursor = r.ReadInt32();
    //            for (int i23 = 1; i23 < __instance.craftCursor; i23++) {
    //                __instance.craftPool[i23].Import(r);
    //            }

    //            for (int i24 = 0; i24 < __instance.craftRecycleCursor; i24++) {
    //                __instance.craftRecycle[i24] = r.ReadInt32();
    //            }

    //            for (int i25 = 1; i25 < __instance.craftCursor; i25++) {
    //                __instance.craftAnimPool[i25].time = r.ReadSingle();
    //                __instance.craftAnimPool[i25].prepare_length = r.ReadSingle();
    //                __instance.craftAnimPool[i25].working_length = r.ReadSingle();
    //                __instance.craftAnimPool[i25].state = r.ReadUInt32();
    //                __instance.craftAnimPool[i25].power = r.ReadSingle();
    //            }

    //            if (__instance.gameData.patch < 10) {
    //                for (int i26 = 1; i26 < __instance.craftCursor; i26++) {
    //                    ref CraftData reference = ref __instance.craftPool[i26];
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
    //        if (j >= 8) {
    //            i4 = r.ReadInt32();
    //            __instance.SetEnemyCapacity(i4);
    //            __instance.enemyCursor = r.ReadInt32();
    //            __instance.enemyRecycleCursor = r.ReadInt32();
    //            for (int i27 = 1; i27 < __instance.enemyCursor; i27++) {
    //                __instance.enemyPool[i27].Import(r);
    //            }

    //            for (int i28 = 0; i28 < __instance.enemyRecycleCursor; i28++) {
    //                __instance.enemyRecycle[i28] = r.ReadInt32();
    //            }

    //            for (int i29 = 1; i29 < __instance.enemyCursor; i29++) {
    //                __instance.enemyAnimPool[i29].time = r.ReadSingle();
    //                __instance.enemyAnimPool[i29].prepare_length = r.ReadSingle();
    //                __instance.enemyAnimPool[i29].working_length = r.ReadSingle();
    //                __instance.enemyAnimPool[i29].state = r.ReadUInt32();
    //                __instance.enemyAnimPool[i29].power = r.ReadSingle();
    //            }
    //        } else {
    //            __instance.SetEnemyCapacity(24);
    //            __instance.enemyCursor = 1;
    //            __instance.enemyRecycleCursor = 0;
    //        }

    //        PerformanceMonitor.EndData(ESaveDataEntry.Combat);
    //        PerformanceMonitor.EndData(ESaveDataEntry.Factory);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Planet);
    //        i4 = r.ReadInt32();
    //        __instance.SetVegeCapacity(i4);
    //        __instance.vegeCursor = r.ReadInt32();
    //        __instance.vegeRecycleCursor = r.ReadInt32();
    //        for (int i30 = 1; i30 < __instance.vegeCursor; i30++) {
    //            __instance.vegePool[i30].Import(r);
    //        }

    //        for (int i31 = 0; i31 < __instance.vegeRecycleCursor; i31++) {
    //            __instance.vegeRecycle[i31] = r.ReadInt32();
    //        }

    //        i4 = r.ReadInt32();
    //        __instance.SetVeinCapacity(i4);
    //        __instance.veinCursor = r.ReadInt32();
    //        __instance.veinRecycleCursor = r.ReadInt32();
    //        int i32 = 0;
    //        for (int i33 = 1; i33 < __instance.veinCursor; i33++) {
    //            __instance.veinPool[i33].Import(r);
    //            if (j < 7) {
    //                __instance.veinPool[i33].groupIndex++;
    //            }

    //            if (__instance.veinPool[i33].groupIndex < 0) {
    //                __instance.veinPool[i33].groupIndex = 0;
    //            }

    //            if (__instance.veinPool[i33].groupIndex > i32) {
    //                i32 = __instance.veinPool[i33].groupIndex;
    //            }
    //        }

    //        for (int i34 = 0; i34 < __instance.veinRecycleCursor; i34++) {
    //            __instance.veinRecycle[i34] = r.ReadInt32();
    //        }

    //        for (int i35 = 1; i35 < __instance.veinCursor; i35++) {
    //            __instance.veinAnimPool[i35].time = r.ReadSingle();
    //            __instance.veinAnimPool[i35].prepare_length = r.ReadSingle();
    //            __instance.veinAnimPool[i35].working_length = r.ReadSingle();
    //            __instance.veinAnimPool[i35].state = r.ReadUInt32();
    //            __instance.veinAnimPool[i35].power = r.ReadSingle();
    //        }

    //        __instance.InitVeinGroups(i32);
    //        __instance.RecalculateAllVeinGroups();
    //        PerformanceMonitor.EndData(ESaveDataEntry.Planet);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Factory);
    //        if (j < 8) {
    //            __instance.RefreshHashSystems();
    //        }

    //        PerformanceMonitor.BeginData(ESaveDataEntry.Ruin);
    //        if (j >= 8) {
    //            i4 = r.ReadInt32();
    //            __instance.SetRuinCapacity(i4);
    //            __instance.ruinCursor = r.ReadInt32();
    //            __instance.ruinRecycleCursor = r.ReadInt32();
    //            for (int i36 = 1; i36 < __instance.ruinCursor; i36++) {
    //                __instance.ruinPool[i36].Import(r);
    //            }

    //            for (int i37 = 0; i37 < __instance.ruinRecycleCursor; i37++) {
    //                __instance.ruinRecycle[i37] = r.ReadInt32();
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
    //        if (j >= 8) {
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
    //                for (int i38 = 1; i38 < cursor; i38++) {
    //                    if (buffer[i38] == null || buffer[i38].id != i38 || buffer[i38].ruinId <= 0) {
    //                        continue;
    //                    }

    //                    if (__instance.ruinPool[buffer[i38].ruinId].id != buffer[i38].ruinId) {
    //                        buffer[i38].ruinId = 0;
    //                        continue;
    //                    }

    //                    Vector3 normalized = __instance.ruinPool[buffer[i38].ruinId].pos.normalized;
    //                    Vector3 vector = __instance.enemyPool[buffer[i38].enemyId].pos.normalized;
    //                    if ((normalized * 200f - vector * 200f).magnitude > 10f) {
    //                        buffer[i38].ruinId = 0;
    //                    }
    //                }

    //                PowerGeneratorComponent[] genPool = __instance.powerSystem.genPool;
    //                int genCursor = __instance.powerSystem.genCursor;
    //                for (int i39 = 1; i39 < genCursor; i39++) {
    //                    ref PowerGeneratorComponent reference2 = ref genPool[i39];
    //                    if (reference2.id != i39 || !reference2.geothermal || genPool[i39].baseRuinId <= 0) {
    //                        continue;
    //                    }

    //                    if (__instance.ruinPool[genPool[i39].baseRuinId].id != genPool[i39].baseRuinId) {
    //                        genPool[i39].baseRuinId = 0;
    //                        continue;
    //                    }

    //                    Vector3 normalized2 = __instance.ruinPool[genPool[i39].baseRuinId].pos.normalized;
    //                    Vector3 normalized3 = __instance.entityPool[genPool[i39].entityId].pos.normalized;
    //                    if ((normalized2 * 200f - normalized3 * 200f).magnitude > 10f) {
    //                        genPool[i39].baseRuinId = 0;
    //                    }
    //                }
    //            }

    //            _ = __instance.gameData.patch;
    //            _ = 15;
    //            DataPool<UnitComponent> units = __instance.combatGroundSystem.units;
    //            for (int i40 = 1; i40 < units.cursor; i40++) {
    //                ref UnitComponent reference3 = ref units.buffer[i40];
    //                if (reference3.id != i40) {
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
    //            for (int i41 = 1; i41 < fleets.cursor; i41++) {
    //                ref FleetComponent reference5 = ref fleets.buffer[i41];
    //                if (reference5.id == i41) {
    //                    ref CraftData reference6 = ref __instance.craftPool[reference5.craftId];
    //                    if (reference6.id == reference5.craftId && !reference5.CheckOwnerExist(ref reference6, __instance, __instance.gameData.mainPlayer.mecha)) {
    //                        __instance.RemoveCraftWithComponents(reference5.craftId);
    //                    }
    //                }
    //            }

    //            ObjectPool<CombatModuleComponent> combatModules = __instance.combatGroundSystem.combatModules;
    //            for (int i42 = 1; i42 < combatModules.cursor; i42++) {
    //                ref CombatModuleComponent reference7 = ref combatModules.buffer[i42];
    //                if (reference7 == null || reference7.id != i42 || __instance.entityPool[reference7.entityId].id != reference7.entityId) {
    //                    continue;
    //                }

    //                ModuleFleet[] moduleFleets = reference7.moduleFleets;
    //                int fleetCount = reference7.fleetCount;
    //                for (int i43 = 0; i43 < fleetCount; i43++) {
    //                    int fleetId = moduleFleets[i43].fleetId;
    //                    if (fleetId == 0) {
    //                        continue;
    //                    }

    //                    fleets = __instance.combatGroundSystem.fleets;
    //                    ref FleetComponent reference8 = ref fleets.buffer[fleetId];
    //                    if (reference8.owner != reference7.entityId) {
    //                        moduleFleets[i43].ClearFleetForeignKey();
    //                        continue;
    //                    }

    //                    ref CraftData reference9 = ref __instance.craftPool[reference8.craftId];
    //                    if (reference9.id != reference8.craftId || reference9.owner != reference8.owner) {
    //                        moduleFleets[i43].ClearFleetForeignKey();
    //                        continue;
    //                    }

    //                    ModuleFighter[] fighters = moduleFleets[i43].fighters;
    //                    int i44 = fighters.Length;
    //                    for (int i45 = 0; i45 < i44; i45++) {
    //                        int craftId = fighters[i45].craftId;
    //                        if (craftId != 0) {
    //                            ref CraftData reference10 = ref __instance.craftPool[craftId];
    //                            if (reference10.id != craftId || reference10.unitId == 0) {
    //                                fighters[i45].ClearFighterForeignKey();
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
    //        if (j < 4) {
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            r.ReadInt32();
    //            r.ReadInt32();
    //        }

    //        PerformanceMonitor.BeginData(ESaveDataEntry.Platform);
    //        if (j >= 1) {
    //            __instance.platformSystem = new PlatformSystem(__instance.planet, import: true);
    //            __instance.platformSystem.Import(r);
    //        } else {
    //            __instance.platformSystem = new PlatformSystem(__instance.planet);
    //        }

    //        __instance.enemySystem.RefreshPlanetReformState();
    //        PerformanceMonitor.EndData(ESaveDataEntry.Platform);
    //        PerformanceMonitor.BeginData(ESaveDataEntry.Digital);
    //        if (j >= 6) {
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
