using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXController
    {
        private VFXView vfxView;

        public VFXController(VFXView vfxPrefab)
        {
            vfxView = Object.Instantiate(vfxPrefab);
            vfxView.SetController(this);
            // Start disabled so pool can Activate on demand
            vfxView.gameObject.SetActive(false);
        }

        public void Configure(Vector2 spawnPosition)
        {
            // Forward to view to position & play
            vfxView.ConfigureAndPlay(spawnPosition);
        }

     
        /// Called by the view when the particle playback has finished.
        /// Deactivates the view and returns this controller to the VFX pool via VFXService.
      
        public void OnPlaybackFinished()
        {
            vfxView.gameObject.SetActive(false);
            GameService.Instance.GetVFXService().ReturnVFXToPool(this);
        }
    } 
}