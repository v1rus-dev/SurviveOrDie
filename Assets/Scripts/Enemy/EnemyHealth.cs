using UnityEngine;

namespace Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float health = 100f;
        [HideInInspector]
        public bool isDead = false;
    
        // ReSharper disable Unity.PerformanceAnalysis
        public void GetDamage(float damage, Transform player)
        {
            BroadcastMessage("OnDamageTaken", player);
            health -= damage;
            if (health <= Mathf.Epsilon)
            {
                isDead = true;
            }
        }

        private void EnemyDie()
        {
            Destroy(gameObject);
        }
    }
}
