using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXController
    {
        private VFXView vfxView;
        private VFXPool ownerPool;

        public VFXController(VFXView vfxPrefab)
        {
            vfxView = Object.Instantiate(vfxPrefab);
            vfxView.SetController(this);
        }

        public void Configure(Vector2 spawnPosition) => vfxView.ConfigureAndPlay(spawnPosition);

        public void SetOwnerPool(VFXPool pool) => ownerPool = pool;

        public void OnVFXFinished()
        {
            vfxView.gameObject.SetActive(false);
            ownerPool.ReturnVFX(this);
        }
    } 
}