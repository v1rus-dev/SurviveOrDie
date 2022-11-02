using UnityEngine;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        private DeathHandler deathHandler;

        private void Start()
        {
            deathHandler = GetComponent<DeathHandler>();
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= Mathf.Epsilon)
            {
                Die();
            }
        }
        
        public bool IsDead()
        {
            return health <= Mathf.Epsilon;
        }
        
        public bool IsLiving()
        {
            return health > Mathf.Epsilon;
        }

        private void Die()
        {
            deathHandler.HandleDeath();
        }
    }
}