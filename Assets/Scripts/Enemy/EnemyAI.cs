using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float chaseRange = 5f;
        [SerializeField] private float atackRange = 2f;
        [SerializeField] private float turnSpeed = 5f;

        private Transform target;
        private bool isReachingTarget = false;

        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private float distanceToTarget = Mathf.Infinity;
        private bool isProvoked = false;
        private bool isProvokedByShooting = false;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Move = Animator.StringToHash("Move");

        // Start is called before the first frame update
        void Start()
        {
            Tags[] tags = FindObjectsOfType<Tags>();
            foreach (var tag in tags)
            {
                if (tag.HasTagByName("Player"))
                {
                    target = tag.gameObject.transform;
                    break;
                }
            }

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            distanceToTarget = Vector3.Distance(target.position, transform.position);

            if (isProvokedByShooting)
            {
                isProvoked = true;
            }

            if (distanceToTarget <= chaseRange)
            {
                isProvoked = true;
                isProvokedByShooting = false;
            }
            else if (distanceToTarget >= chaseRange && !isProvokedByShooting)
            {
                isProvoked = false;
            }

            if (isProvoked)
            {
                EngageTarget();
            }
            else
            {
                StopChasingTarget();
            }
        }

        public void OnDamageTaken()
        {
            isProvokedByShooting = true;
        }

        private void EngageTarget()
        {
            FaceTarget();

            if (distanceToTarget >= _navMeshAgent.stoppingDistance || isProvokedByShooting)
            {
                ChaseTarget();
            }
            else
            {
                StopChasingTarget();
            }

            if (distanceToTarget <= _navMeshAgent.stoppingDistance)
            {
                AttackTarget();
            }
            else
            {
                _animator.SetBool(Attack, false);
            }
        }

        private void ChaseTarget()
        {
            _navMeshAgent.isStopped = false;
            _animator.SetBool(Move, true);
            _navMeshAgent.SetDestination(target.position);
        }

        private void StopChasingTarget()
        {
            _animator.SetBool(Move, false);
            _navMeshAgent.isStopped = true;
        }

        private void AttackTarget()
        {
            _animator.SetBool(Attack, true);
        }

        private void FaceTarget()
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseRange);
        }
    }
}