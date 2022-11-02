using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void GetDamage(float damage)
        {
            BroadcastMessage("OnDamageTaken");
            health -= damage;
            if (health <= Mathf.Epsilon)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
