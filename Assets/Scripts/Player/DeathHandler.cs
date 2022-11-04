using UnityEngine;
using Weapon;

namespace Player
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
    
        private void Start()
        {
            gameOverCanvas = Instantiate(gameOverCanvas, GameObject.Find("UI").transform);
            SetCanvasEnabled(gameOverCanvas, false);
            DisableCursor();
            ChangeTime(false);
        }
    
        public void HandleDeath()
        {
            SetCanvasEnabled(gameOverCanvas, true);
            FindObjectOfType<WeaponSwitcher>().enabled = false;
            FindObjectOfType<FirstPersonController>().enabled = false;
            EnableCursor();
            ChangeTime(true);
        }
        
        private static void ChangeTime(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
        
        private static void SetCanvasEnabled(Canvas canvas, bool isEnabled)
        {
            canvas.gameObject.SetActive(isEnabled);
        }
        
        private void EnableCursor()
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
        private void DisableCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
