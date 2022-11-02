using UnityEngine;

namespace Player
{
    public class DeathHandler : MonoBehaviour
    {
        [SerializeField] private Canvas gameOverCanvas;
    
        private void Start()
        {
            SetCanvasEnabled(gameOverCanvas, false);
            DisableCursor();
            ChangeTime(false);
        }
    
        public void HandleDeath()
        {
            SetCanvasEnabled(gameOverCanvas, true);
            EnableCursor();
            ChangeTime(true);
        }
        
        private static void ChangeTime(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
        }
        
        private static void SetCanvasEnabled(Canvas canvas, bool isEnabled)
        {
            canvas.enabled = isEnabled;
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
