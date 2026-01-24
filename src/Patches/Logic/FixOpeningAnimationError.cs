using HarmonyLib;
using System;
using System.IO;
using static ProjectOrbitalRing.ProjectOrbitalRing;

namespace ProjectOrbitalRing.Patches.Logic
{
    internal class FixOpeningAnimationError
    {
        // 解决新档过场动画报错的问题
        [HarmonyPatch(typeof(LogisticDroneRenderer), nameof(LogisticDroneRenderer.Update))]
        [HarmonyPrefix]
        public static bool LogisticDroneRenderer_Update_Patch(LogisticDroneRenderer __instance)
        {
            if (DSPGame.IsMenuDemo || GameMain.mainPlayer == null) {
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(StationComponent), nameof(StationComponent.InternalTickLocal))]
        [HarmonyPrefix]
        public static bool StationComponent_InternalTickLocal_Patch(StationComponent __instance)
        {
            if (DSPGame.IsMenuDemo || GameMain.mainPlayer == null) {
                return false;
            }
            return true;
        }

        [HarmonyPatch(typeof(AssemblerComponent), nameof(AssemblerComponent.Import))]
        [HarmonyPrefix]
        public static bool AssemblerComponent_Import_Patch(AssemblerComponent __instance, BinaryReader r)
        {
            if (!DSPGame.IsMenuDemo && GameMain.mainPlayer != null) {
                return true;
            }
            int num = r.ReadInt32();
            bool flag = num >= 1;
            __instance.id = r.ReadInt32();
            __instance.entityId = r.ReadInt32();
            __instance.pcId = r.ReadInt32();
            __instance.replicating = r.ReadBoolean();
            if (num < 6) {
                r.ReadBoolean();
            }
            __instance.speed = r.ReadInt32();
            __instance.time = r.ReadInt32();
            if (num >= 2) {
                __instance.speedOverride = r.ReadInt32();
                __instance.extraTime = r.ReadInt32();
                __instance.extraSpeed = r.ReadInt32();
                __instance.extraPowerRatio = r.ReadInt32();
                if (num < 6) {
                    r.ReadBoolean();
                }
                if (num >= 3) {
                    __instance.forceAccMode = r.ReadBoolean();
                } else {
                    __instance.forceAccMode = false;
                }
                if (num >= 4) {
                    __instance.incUsed = r.ReadBoolean();
                } else {
                    __instance.incUsed = false;
                }
            } else {
                __instance.speedOverride = __instance.speed;
                __instance.extraTime = 0;
                __instance.extraSpeed = 0;
                __instance.extraPowerRatio = 0;
                __instance.forceAccMode = false;
                __instance.incUsed = false;
            }
            if (num < 4 && __instance.incServed != null) {
                for (int i = 0; i < __instance.incServed.Length; i++) {
                    if (__instance.incServed[i] > 0) {
                        __instance.incUsed = true;
                        break;
                    }
                }
            }
            if (num < 5) {
                __instance.cycleCount = 0;
                __instance.extraCycleCount = 0;
            } else {
                __instance.cycleCount = r.ReadInt32();
                __instance.extraCycleCount = r.ReadInt32();
            }
            if (flag) {
                __instance.recipeId = (int)r.ReadInt16();
            } else {
                __instance.recipeId = r.ReadInt32();
            }
            if (__instance.recipeId > 0) {
                RecipeProto recipeProto = LDB.recipes.Select(__instance.recipeId);
                if (flag) {
                    __instance.recipeType = (ERecipeType)r.ReadByte();
                    int num2;
                    int[] array;
                    if (num < 6) {
                        r.ReadInt32();
                        if (num >= 2) {
                            r.ReadInt32();
                        }
                        num2 = (int)r.ReadByte();
                        array = new int[num2];
                        for (int j = 0; j < num2; j++) {
                            array[j] = (int)r.ReadInt16();
                        }
                        num2 = (int)r.ReadByte();
                        int[] array2 = new int[num2];
                        for (int k = 0; k < num2; k++) {
                            array2[k] = (int)r.ReadInt16();
                        }
                    } else {
                        num2 = (int)r.ReadByte();
                        array = new int[num2];
                        for (int l = 0; l < num2; l++) {
                            array[l] = (int)r.ReadInt16();
                        }
                    }
                    num2 = (int)r.ReadByte();
                    __instance.served = new int[num2];
                    for (int m = 0; m < num2; m++) {
                        __instance.served[m] = r.ReadInt32();
                    }
                    if (num >= 2) {
                        num2 = (int)r.ReadByte();
                        __instance.incServed = new int[num2];
                        for (int n = 0; n < num2; n++) {
                            __instance.incServed[n] = r.ReadInt32();
                        }
                    } else {
                        __instance.incServed = new int[__instance.served.Length];
                        for (int num3 = 0; num3 < __instance.served.Length; num3++) {
                            __instance.incServed[num3] = 0;
                        }
                    }
                    num2 = (int)r.ReadByte();
                    __instance.needs = new int[num2];
                    for (int num4 = 0; num4 < num2; num4++) {
                        __instance.needs[num4] = (int)r.ReadInt16();
                    }
                    int[] array3;
                    if (num < 6) {
                        num2 = (int)r.ReadByte();
                        array3 = new int[num2];
                        for (int num5 = 0; num5 < num2; num5++) {
                            array3[num5] = (int)r.ReadInt16();
                        }
                        num2 = (int)r.ReadByte();
                        int[] array4 = new int[num2];
                        for (int num6 = 0; num6 < num2; num6++) {
                            array4[num6] = (int)r.ReadInt16();
                        }
                    } else {
                        num2 = (int)r.ReadByte();
                        array3 = new int[num2];
                        for (int num7 = 0; num7 < num2; num7++) {
                            array3[num7] = (int)r.ReadInt16();
                        }
                    }
                    num2 = (int)r.ReadByte();
                    __instance.produced = new int[num2];
                    for (int num8 = 0; num8 < num2; num8++) {
                        __instance.produced[num8] = r.ReadInt32();
                    }
                    if (recipeProto != null) {
                        __instance.recipeExecuteData = RecipeProto.recipeExecuteData[recipeProto.ID];
                        int num9 = __instance.recipeExecuteData.requires.Length;
                        int temp = array.Length;
                        if (array.Length != __instance.recipeExecuteData.requires.Length) {
                            LogError($"recipeProto.ID {recipeProto.ID} name {recipeProto.Name} array.Length {array.Length} requires.Length {__instance.recipeExecuteData.requires.Length} !!!!!!!!!!!!!!!!!!!!!!!!!");
                            temp = Math.Min(array.Length, __instance.recipeExecuteData.requires.Length);
                        }
                        for (int num10 = 0; num10 < temp; num10++) {
                            if (num10 > num9 || array[num10] != __instance.recipeExecuteData.requires[num10]) {
                                __instance.served[num10] = (__instance.incServed[num10] = 0);
                            }
                        }
                        int temp2 = array3.Length;
                        if (array3.Length != __instance.recipeExecuteData.products.Length) {
                            LogError($"recipeProto.ID {recipeProto.ID} name {recipeProto.Name} array3.Length {array3.Length} products.Length {__instance.recipeExecuteData.products.Length} !!!!!!!!!!!!!!!!!!!!!!!!!");
                            temp2 = Math.Min(array3.Length, __instance.recipeExecuteData.products.Length);
                        }
                        num9 = __instance.recipeExecuteData.products.Length;
                        for (int num11 = 0; num11 < temp2; num11++) {
                            if (num11 > num9 || array3[num11] != __instance.recipeExecuteData.products[num11]) {
                                __instance.produced[num11] = 0;
                            }
                        }
                        return false;
                    }
                } else {
                    __instance.recipeType = (ERecipeType)r.ReadInt32();
                    int num12;
                    if (num < 6) {
                        num12 = (int)r.ReadByte();
                        int[] array = new int[num12];
                        for (int num13 = 0; num13 < num12; num13++) {
                            array[num13] = (int)r.ReadInt16();
                        }
                        num12 = (int)r.ReadByte();
                        int[] array5 = new int[num12];
                        for (int num14 = 0; num14 < num12; num14++) {
                            array5[num14] = (int)r.ReadInt16();
                        }
                    }
                    num12 = r.ReadInt32();
                    __instance.served = new int[num12];
                    for (int num15 = 0; num15 < num12; num15++) {
                        __instance.served[num15] = r.ReadInt32();
                    }
                    if (num >= 2) {
                        num12 = (int)r.ReadByte();
                        __instance.incServed = new int[num12];
                        for (int num16 = 0; num16 < num12; num16++) {
                            __instance.incServed[num16] = r.ReadInt32();
                        }
                    } else {
                        __instance.incServed = new int[__instance.served.Length];
                        for (int num17 = 0; num17 < __instance.served.Length; num17++) {
                            __instance.incServed[num17] = 0;
                        }
                    }
                    num12 = r.ReadInt32();
                    __instance.needs = new int[num12];
                    for (int num18 = 0; num18 < num12; num18++) {
                        __instance.needs[num18] = r.ReadInt32();
                    }
                    if (num < 6) {
                        num12 = (int)r.ReadByte();
                        int[] array3 = new int[num12];
                        for (int num19 = 0; num19 < num12; num19++) {
                            array3[num19] = (int)r.ReadInt16();
                        }
                        num12 = (int)r.ReadByte();
                        int[] array6 = new int[num12];
                        for (int num20 = 0; num20 < num12; num20++) {
                            array6[num20] = (int)r.ReadInt16();
                        }
                        if (recipeProto != null) {
                            __instance.recipeExecuteData = RecipeProto.recipeExecuteData[recipeProto.ID];
                        }
                    }
                    num12 = r.ReadInt32();
                    __instance.produced = new int[num12];
                    for (int num21 = 0; num21 < num12; num21++) {
                        __instance.produced[num21] = r.ReadInt32();
                    }
                }
            }
            //}
            return false;
            return true;
        }
    }
}
