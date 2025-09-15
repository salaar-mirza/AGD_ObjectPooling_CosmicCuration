using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXView : MonoBehaviour
    {
        private VFXController controller;
        private ParticleSystem vfx;

        public void SetController(VFXController controllerToSet) => controller = controllerToSet;

        public void ConfigureAndPlay(Vector2 positionToSet)
        {
            transform.position = positionToSet;
            vfx = GetComponent<ParticleSystem>();
            gameObject.SetActive(true);

            if (vfx != null)
            {
                vfx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                vfx.Play();
            }
        }

        private void Update()
        {
            if (vfx != null && vfx.isStopped)
            {
                // Inform controller so the controller can return itself to the pool
                controller?.OnPlaybackFinished();
            }
        }
    }
}