using HarmonyLib;
using ProjectOrbitalRing.Utils;
using UnityEngine;
using WinAPI;
using static ProjectOrbitalRing.Patches.Logic.OrbitalRing.PosTool;
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic.OrbitalRing
{
    internal class OrbitalConnect
    {
        static Vector3 startPos = new Vector3(0, 0, 0);
        [HarmonyPostfix]
        [HarmonyPatch(typeof(BuildTool_Path), "ConfirmOperation")]
        public static void ConfirmOperation_Patch(BuildTool_Path __instance)
        {
            if (__instance.handItem.ID == ProtoID.I轨道连接组件 || __instance.handItem.ID == ProtoID.I粒子加速轨道 || __instance.handItem.ID == ProtoID.I星环电网组件) {
                if (__instance.controller.cmd.stage == 1) {
                    for (int i = 0; i < __instance.buildPreviews.Count; i++) {
                        BuildPreview preview = __instance.buildPreviews[i];

                        if (preview.item.ID == ProtoID.I轨道连接组件 || preview.item.ID == ProtoID.I粒子加速轨道 || preview.item.ID == ProtoID.I星环电网组件) {
                            startPos = preview.lpos;
                        }
                    }
                } else {
                    startPos = new Vector3(0, 0, 0);
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(BuildTool_Path), "DeterminePreviews")]
        public static void DeterminePreviews_Path_Patch(BuildTool_Path __instance)
        {
            if (__instance.handItem.ID == ProtoID.I轨道连接组件) {
                __instance.altitude = 48;
            } else if (__instance.handItem.ID == ProtoID.I粒子加速轨道 || __instance.handItem.ID == ProtoID.I星环电网组件) {
                __instance.altitude = 43;
            }

            int count = __instance.buildPreviews.Count;
            for (int i = 0; i < count; i++) {
                BuildPreview preview = __instance.buildPreviews[i];

                if (preview.item.ID == ProtoID.I轨道连接组件 || __instance.handItem.ID == ProtoID.I粒子加速轨道 || __instance.handItem.ID == ProtoID.I星环电网组件) {
                    Vector3 pos = new Vector3(0,0,0);
                    if (preview.item.ID == ProtoID.I轨道连接组件) {
                        pos = BeltShouldBeAdsorb(preview.lpos, startPos, 0);
                    } else if (preview.item.ID == ProtoID.I粒子加速轨道) {
                        pos = BeltShouldBeAdsorb(preview.lpos, startPos, 1);
                    } else if (preview.item.ID == ProtoID.I星环电网组件) {
                        pos = BeltShouldBeAdsorb(preview.lpos, startPos, 2);
                    }

                    preview.lpos = pos;

                    // 计算原向量长度
                    float originalMagnitude = preview.lpos.magnitude;
                    if (originalMagnitude == 0 || originalMagnitude - __instance.planet.realRadius > 40) {
                        continue; // 避免除以零
                    }
                    // 获取单位向量（原方向）
                    Vector3 normalized = preview.lpos.normalized;
                    // 计算新长度并返回结果
                    preview.lpos = normalized * (__instance.planet.realRadius + 0.2f + __instance.altitude * 1.3333333f);
                    Debug.LogFormat("length {0}  altitude {1} ", __instance.planet.realRadius + 0.2f + __instance.altitude * 1.3333333f, __instance.altitude);
                    //preview.lrot2 *= Quaternion.AngleAxis(180, Vector3.right);

                }

            }
        }

        [HarmonyPatch(typeof(PlanetTransport), nameof(PlanetTransport.SetStationStorage))]
        [HarmonyPostfix]
        public static void SetStationStorage_Patch(ref PlanetTransport __instance)
        {
            OrbitalStationManager.Instance.AddPlanetId(__instance.planet.id);
            var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
            LogError($" 0 ring not complete inside {planetOrbitalRingData.Rings[0].isInsideRingComplete} outside {planetOrbitalRingData.Rings[0].isOutsideRingComplete}");
            LogError($" 0 ring not complete low inside {planetOrbitalRingData.Rings[0].isLowInsideRingComplete} outside {planetOrbitalRingData.Rings[0].isLowOutsideRingComplete}");
            LogError($" 1 ring not complete inside {planetOrbitalRingData.Rings[1].isInsideRingComplete} outside {planetOrbitalRingData.Rings[1].isOutsideRingComplete}");
            LogError($" 1 ring not complete low inside {planetOrbitalRingData.Rings[1].isLowInsideRingComplete} outside {planetOrbitalRingData.Rings[1].isLowOutsideRingComplete}");
            //planetOrbitalRingData.Rings[0].CheckRingComplete(true);
            //planetOrbitalRingData.Rings[1].CheckRingComplete(true);
        }



        [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.BuildFinally))]
        [HarmonyPrefix]
        public static void BuildFinallyPrePatch(ref PlanetFactory __instance, int prebuildId)
        {
            if (prebuildId != 0) {
                PrebuildData prebuildData = __instance.prebuildPool[prebuildId];
                if (prebuildData.id == prebuildId) {
                    if (prebuildData.protoId == ProtoID.I轨道连接组件 || prebuildData.protoId == ProtoID.I粒子加速轨道 || prebuildData.protoId == ProtoID.I星环电网组件) {
                        //LogError($"BuildFinallyPostPatch");
                        Vector3 pos = prebuildData.pos;
                        (int positionIndex, int ringBeltIndex, int ringIndex) = CalculateRingPosMark(pos);
                        OrbitalStationManager.Instance.AddPlanetId(__instance.planet.id);
                        var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
                        if (prebuildData.protoId == ProtoID.I轨道连接组件) {
                            planetOrbitalRingData.Rings[ringIndex].AddRing(positionIndex, ringBeltIndex, false);
                        } else if (prebuildData.protoId == ProtoID.I粒子加速轨道 || prebuildData.protoId == ProtoID.I星环电网组件) {
                            planetOrbitalRingData.Rings[ringIndex].AddRing(positionIndex, ringBeltIndex, true);
                        }
                    }
                }
            }
        }

        [HarmonyPatch(typeof(PlanetFactory), nameof(PlanetFactory.DismantleFinally))]
        [HarmonyPrefix]
        public static void DismantleFinallyPatch(PlanetFactory __instance, int objId, ref int protoId)
        {
            if (protoId == ProtoID.I轨道连接组件 || protoId == ProtoID.I粒子加速轨道) {
                Vector3 thisPos = __instance.entityPool[objId].pos;
                (int positionIndex, int ringBeltIndex, int ringIndex) = CalculateRingPosMark(thisPos);
                var planetOrbitalRingData = OrbitalStationManager.Instance.GetPlanetOrbitalRingData(__instance.planet.id);
                if (protoId == ProtoID.I轨道连接组件) {
                    planetOrbitalRingData.Rings[ringIndex].DelRing(positionIndex, ringBeltIndex, false);
                } else if (protoId == ProtoID.I粒子加速轨道) {
                    planetOrbitalRingData.Rings[ringIndex].DelRing(positionIndex, ringBeltIndex, true);
                }
            }
        }
    }
}
