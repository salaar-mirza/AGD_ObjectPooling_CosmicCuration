using System.Collections.Generic;

namespace CosmicCuration.VFX
{
    public class VFXPool
    {
        private VFXView vfxPrefab;
        private List<PooledVFX> pooledVFXs = new List<PooledVFX>();

        public VFXPool(VFXView vfxPrefab)
        {
            this.vfxPrefab = vfxPrefab;
        }

        public VFXController GetVFX()
        {
            PooledVFX vfx = pooledVFXs.Find(item => !item.isUsed);
            if (vfx != null)
            {
                vfx.isUsed = true;
                return vfx.VFX;
            }
            return CreateNewPooledVFX();
        }

        private VFXController CreateNewPooledVFX()
        {
            PooledVFX newVFX = new PooledVFX();
            newVFX.VFX = new VFXController(vfxPrefab);
            newVFX.VFX.SetOwnerPool(this);
            newVFX.isUsed = true;
            pooledVFXs.Add(newVFX);
            return newVFX.VFX;
        }

        public void ReturnVFX(VFXController vfx) => pooledVFXs.Find(item => item.VFX == vfx).isUsed = false;

        private class PooledVFX
        {
            public VFXController VFX;
            public bool isUsed;
        }
    }
}