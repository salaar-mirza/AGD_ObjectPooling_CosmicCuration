using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXService
    {
        private Dictionary<VFXType, VFXPool> vfxPools = new Dictionary<VFXType, VFXPool>();

        public VFXService(VFXScriptableObject vfxScriptableObject)
        {
            foreach (VFXData data in vfxScriptableObject.vfxData)
            {
                vfxPools.Add(data.type, new VFXPool(data.prefab));
            }
        }

        public void PlayVFXAtPosition(VFXType type, Vector2 spawnPosition)
        {
            VFXController vfxToPlay = vfxPools[type].GetVFX();
            vfxToPlay.Configure(spawnPosition);
        }
    } 
}