using HarmonyLib;
using ProjectOrbitalRing.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrbitalRing.Patches.UI
{
    internal class U235RecipesResultCountFix
    {
        [HarmonyPatch(typeof(UIRecipeEntry), nameof(UIRecipeEntry.SetRecipe))]
        [HarmonyPostfix]
        public static void SetRecipePatch(UIRecipeEntry __instance, RecipeProto recipe)
        {
            if (recipe.ID == ProtoID.R铀235提纯) {
                __instance.countTexts[0].text = "1";
            }
        }

        [HarmonyPatch(typeof(UIReplicatorWindow), nameof(UIReplicatorWindow.OnSelectedRecipeChange))]
        [HarmonyPostfix]
        public static void OnSelectedRecipeChangePatch(UIReplicatorWindow __instance)
        {
            if (__instance.selectedRecipe == null) {
                return;
            }
            if (__instance.selectedRecipe.ID == ProtoID.R铀235提纯) {
                __instance.treeMainCountText0.text = "";
            }
        }
        [HarmonyPatch(typeof(UIReplicatorWindow), nameof(UIReplicatorWindow._OnUpdate))]
        [HarmonyPostfix]
        public static void _OnUpdatePatch(UIReplicatorWindow __instance)
        {
            if (__instance.selectedRecipe == null) {
                return;
            }
            if (__instance.selectedRecipe.ID == ProtoID.R铀235提纯) {
                __instance.treeMainCountText0.text = "";
            }
        }
    }
}
