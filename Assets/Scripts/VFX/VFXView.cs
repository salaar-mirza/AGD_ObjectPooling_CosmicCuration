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
            gameObject.SetActive(true);
            vfx = GetComponent<ParticleSystem>();
            vfx.Play();
        }

        private void Update()
        {
            if (vfx != null && vfx.isStopped)
            {
                controller.OnVFXFinished();
            }
        }
    }
}