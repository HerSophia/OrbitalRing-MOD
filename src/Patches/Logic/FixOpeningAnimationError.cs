using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
