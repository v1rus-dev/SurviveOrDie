using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private float damage = 30;
        [SerializeField] private float multiplierByAttack = 1.3f;

        private TargetFinder _targetFinder;
        private float currentDamage;
        private bool damageIsMultiplied = false;

        void Start()
        {
            _targetFinder = GetComponent<TargetFinder>();
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
            if (_targetFinder.Target == null) return;
            PlayerHealth playerHealth = _targetFinder.Target.GetComponent<PlayerHealth>();
            if (playerHealth == null) return;
            playerHealth.TakeDamage(currentDamage);
            currentDamage = damage;
            damageIsMultiplied = false;
        }
    }
}