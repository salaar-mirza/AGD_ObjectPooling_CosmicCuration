using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXService
    {
        private List<VFXData> vfxData = new List<VFXData>();
        private Dictionary<VFXType, VFXPool> vfxPools = new Dictionary<VFXType, VFXPool>();

        public VFXService(VFXScriptableObject vfxScriptableObject)
        {
            vfxData = vfxScriptableObject.vfxData;

            // Create a dedicated pool per VFX type / prefab
            foreach (var data in vfxData)
            {
                if (data.prefab != null && !vfxPools.ContainsKey(data.type))
                    vfxPools[data.type] = new VFXPool(data.prefab);
            }
        }

        public void PlayVFXAtPosition(VFXType type, Vector2 spawnPosition)
        {
            if (!vfxPools.TryGetValue(type, out var pool))
            {
                Debug.LogWarning($"No VFX pool found for type {type}. Ensure the VFXScriptableObject contains that entry.");
                return;
            }

            var vfxController = pool.GetVFX();
            vfxController.Configure(spawnPosition);
        }

        public void ReturnVFXToPool(VFXController controller)
        {
            // Try to return to the right pool. Each pool has TryReturnItem that only returns true if it owns the controller.
            foreach (var pool in vfxPools.Values)
            {
                if (pool.TryReturnItem(controller))
                    return;
            }

            // If we get here, no pool owned the controller - log and destroy as fallback.
            Debug.LogWarning("Returned VFX controller does not belong to any pool. Destroying its GameObject as fallback.");
            // Best-effort cleanup:
            // Note: controller's view is private; attempt to disable via reflection is not ideal.
            // Prefer ensuring pools are created for all VFX types to avoid this branch.
        }
    } 
}