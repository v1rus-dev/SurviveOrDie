using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        PlayerHealth playerHealth;
        [SerializeField] private float damage = 30;
        [SerializeField] private float multiplierByAttack = 1.3f;
        
        private float currentDamage;
        private bool damageIsMultiplied = false;

        void Start()
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
            currentDamage = damage;
        }
        
        public void OnDamageTaken()
        {
            if (!damageIsMultiplied)
            {
                currentDamage *= multiplierByAttack;
            }
        }

        // Called by Unity Animation Event
        public void TakeDamage()
        {
            if (playerHealth == null) return;
            playerHealth.TakeDamage(currentDamage);
            currentDamage = damage;
            damageIsMultiplied = false;
        }
    }
}