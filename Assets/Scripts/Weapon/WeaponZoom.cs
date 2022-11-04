using Player;
using UnityEngine;

namespace Weapon
{
    public class WeaponZoom : MonoBehaviour
    {
        [SerializeField]
        private Camera fpsCamera;
        [SerializeField]
        private float standardZoom = 60f;
        [SerializeField]
        private float zoomedInZoom = 20f;

        [SerializeField]
        private float mouseSensitivityIsZoomedIn = 1f;
    
        private FirstPersonController fpsController;
    
        private bool isZoomedIn = false;

        private void Start()
        {
            fpsController = GetComponentInParent<FirstPersonController>();
            fpsCamera.fieldOfView = standardZoom;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (isZoomedIn)
                {
                    ZoomOut();
                }
                else
                {
                    ZoomIn();
                }
            }
        }
    
        private void ZoomIn()
        {
            fpsCamera.fieldOfView = zoomedInZoom;
            isZoomedIn = true;
            fpsController.ChangeMouseSensitivity(mouseSensitivityIsZoomedIn);
        }
    
        private void ZoomOut()
        {
            fpsCamera.fieldOfView = standardZoom;
            isZoomedIn = false;
            if (fpsController != null)
            {
                fpsController.ResetMouseSensitivity();
            }
        }

        private void OnDisable()
        {
            ZoomOut();
        }
    }
}
