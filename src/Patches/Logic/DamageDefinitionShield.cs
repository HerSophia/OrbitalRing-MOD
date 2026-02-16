using HarmonyLib;
using ProjectOrbitalRing.Patches.Logic.OrbitalRing;
using ProjectOrbitalRing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static GalacticScale.PatchOnUIGalaxySelect;

namespace ProjectOrbitalRing.Patches.Logic
{
    internal class DamageDefinitionShield
    {
        [HarmonyPatch(typeof(Mecha), nameof(Mecha.EnergyShieldResist), new[]
        {
            typeof(int)
        }, new[]
        {
            ArgumentType.Ref,
        })]
        [HarmonyPrefix]
        public static void EnergyShieldResist_PrePatch(Mecha __instance, ref int damage)
        {
            lock (__instance) {
                if (__instance.player.invincible) {
                    damage = 0;
                }
                if (__instance.player.package.GetItemCount(ProtoID.I损伤定义护盾) < 1) {
                    return;
                }
                int itemId = ProtoID.I损伤定义护盾;
                int itemCount = 1;
                __instance.player.TakeItemFromPlayer(ref itemId, ref itemCount, out _, true, null);

                if (itemId != ProtoID.I损伤定义护盾) return;

                if (itemCount != 1) return;

                //damage = (int)(1000 / __instance.energyShieldBurstDamageRate);
                damage = 1000;
            }
        }
    }
}
