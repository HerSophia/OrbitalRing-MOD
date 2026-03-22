using HarmonyLib;
using System.Linq;
using System;
using UnityEngine;

namespace ProjectOrbitalRing.Patches.Logic.FixMoonRadius
{
    // special thanks for https://github.com/Touhma/DSP_Galactic_Scale/Scripts/Patches/FactoryModel/InitCollectorMaterial.cs
    public class PatchOnFactoryModel
    {
        private static Vector3[][] originalMk2MinerEffectVertices;

        [HarmonyPrefix]
        [HarmonyPatch(typeof(FactoryModel), nameof(FactoryModel.InitMaterial))]
        public static void InitMaterial(ref FactoryModel __instance)
        {
            const float standardRadius = 200f;
            const float standardPlanetCurveOffset = 0.3f;

            var realRadius = __instance.planet.realRadius;
            var adjustVertexY = realRadius - standardRadius;

            // veins at the far end of the miner are lower due to the curve of the planet. Adjust this offset a bit so the effect appears closer to where it should.
            // Between 0.1 and 0.9. Anything above 0.9 clips above the the top glass.
            var planetCurveOffset = (uint)(90.0f / realRadius * 10.0f) / 10.0f;
            planetCurveOffset = planetCurveOffset > 0.9 ? 0.9f : planetCurveOffset;
            adjustVertexY += planetCurveOffset - standardPlanetCurveOffset;

            var prefabDesc = LDB.models.Select(59).prefabDesc;
            if (originalMk2MinerEffectVertices == null) {
                originalMk2MinerEffectVertices = new Vector3[prefabDesc.lodCount][];
                // Only one LOD mesh as of 0.9.27, but why not future proof.
                for (var i = 0; i < prefabDesc.lodCount; i++) {
                    var mesh = prefabDesc.lodMeshes[i];
                    originalMk2MinerEffectVertices[i] = mesh.vertices;
                }
            }

            // Only one LOD mesh as of 0.9.27, but why not future proof.
            for (var i = 0; i < prefabDesc.lodCount; i++) {
                var mesh = prefabDesc.lodMeshes[i];
                var adjustedVertices = new Vector3[originalMk2MinerEffectVertices[i].Length];
                Array.Copy(originalMk2MinerEffectVertices[i], adjustedVertices, originalMk2MinerEffectVertices[i].Length);
                // the submeshes of the mesh share a vertices array, but we only want to adjust three of the four submeshes, so iterate across each submesh.
                // Skip the first submesh: the spinning circle effect around the vein (also visible on Mk1 Miners) already works fine and does not need to be adjusted.
                // The remaining three submeshes: top-circle, laser, and collection should be adjusted.
                for (var j = 1; j < mesh.subMeshCount; j++) {
                    //GetIndices returns vertex indices of each triangle in the submesh, but triangles can share vertices, so iterate across distinct vertex indices.
                    foreach (var k in mesh.GetIndices(j).Distinct()) {
                        //GS2.Log($"Adjusting submodel {j}: vertex at index: {k} by {adjustVertexY} from {adjustedVertices[k].y} to {adjustedVertices[k].y + adjustVertexY}");
                        adjustedVertices[k].y += adjustVertexY;
                    }
                }

                mesh.vertices = adjustedVertices;
            }
        }
    }
}