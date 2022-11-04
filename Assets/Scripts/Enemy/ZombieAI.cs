using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Enemy
{
    [RequireComponent(typeof(TargetFinder))]
    public class ZombieAI : MonoBehaviour
    {
        [SerializeField] private float speedWalkToPoint = 0.5f;
        [SerializeField] private float speedWalkToTarget = 1.5f;
        [SerializeField] private float speedRunToTarget = 3f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private float minDistanceForCreateNewPoint = 2f;
        [SerializeField] private float distanceToForget = 20f;
        [SerializeField] private float radiusForRandomPoint = 10f;
        [SerializeField] [Range(0.1f, 2f)] private float maxTimeInIdle = 1f;
        [SerializeField] private ZombieState zombieState = ZombieState.Idle;

        private TargetFinder _targetFinder;
        private EnemyHealth _enemyHealth;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Vector3 _pointToMove;

        private bool isAttacking = false;

        private void Start()
        {
            _targetFinder = GetComponent<TargetFinder>();
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemyHealth = GetComponent<EnemyHealth>();
        }

        private void Update()
        {
            CheckTarget();
            CheckHealth();
            CheckState();
            CheckDistanceForForget();
            CheckDistanceToAttack();
        }

        private void OnDamageTaken(Transform target)
        {
            _targetFinder.SetTarget(target);
            zombieState = ZombieState.RunToTarget;
        }

        private void CheckState()
        {
            switch (zombieState)
            {
                case ZombieState.Idle:
                    EnableIdleAnimation();
                    StartCoroutine(WaitToWalkToPoint());
                    break;
                case ZombieState.WalkToPoint:
                    _navMeshAgent.speed = speedWalkToPoint;
                    EnableWalkToPoint();
                    WalkToPoint();
                    break;
                case ZombieState.WalkToTarget:
                    _navMeshAgent.speed = speedWalkToTarget;
                    WalkToTarget();
                    break;
                case ZombieState.RunToTarget:
                    _navMeshAgent.speed = speedRunToTarget;
                    RunToTarget();
                    break;
                case ZombieState.Attack:
                    if (!isAttacking)
                    {
                        isAttacking = true;
                        Attack();
                    }
                    break;
                case ZombieState.Dead:
                    _navMeshAgent.ResetPath();
                    EnableDeathAnim();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //Coroutine for wait time in idle state and then change state to walk to point
        private IEnumerator WaitToWalkToPoint()
        {
            yield return new WaitForSeconds(Random.Range(0.1f, maxTimeInIdle));
            GenerateRandomPoint();
            zombieState = ZombieState.WalkToPoint;
        }

        private void WalkToTarget()
        {
            _animator.SetBool("WalkToPoint", false);
            _animator.SetBool("WalkToTarget", true);
            _navMeshAgent.SetDestination(_targetFinder.Target.transform.position);
        }

        private void WalkToPoint()
        {
            _navMeshAgent.SetDestination(_pointToMove);
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        Debug.Log("I'm here");
                        _navMeshAgent.ResetPath();
                        zombieState = ZombieState.Idle;
                        // Done
                    }
                }
            }
        }

        private void RunToTarget()
        {
            _animator.SetBool("WalkToTarget", false);
            _animator.SetBool("WalkToPoint", false);
            _animator.SetBool("RunToTarget", true);
            zombieState = ZombieState.RunToTarget;
            _navMeshAgent.SetDestination(_targetFinder.Target.transform.position);
        }

        private void Attack()
        {
            _navMeshAgent.speed = speedWalkToTarget;
            _animator.SetBool("Attack", true);
        }

        public void AttackAnimIsEnd()
        {
            isAttacking = false;
            zombieState = ZombieState.WalkToTarget;
            _animator.SetBool("Attack", false);
        }

        private void CheckDistanceForForget()
        {
            if (_targetFinder.DistanceToTarget() >= distanceToForget && zombieState != ZombieState.RunToTarget)
            {
                _targetFinder.ForgetTarget();
                zombieState = ZombieState.Idle;
            }
        }

        private void CheckDistanceToAttack()
        {
            if (_targetFinder.Target == null) return;
            // if (zombieState == ZombieState.RunToTarget) return;
            
            if (_targetFinder.DistanceToTarget() <= attackRange)
            {
                zombieState = ZombieState.Attack;
            }
            else
            {
                zombieState = ZombieState.WalkToTarget;
            }
        }

        private void CheckTarget()
        {
            if (_targetFinder.Target == null) return;
            if (zombieState == ZombieState.RunToTarget) return;
            if (zombieState == ZombieState.Attack) return;
            zombieState = ZombieState.WalkToTarget;
        }

        private void CheckHealth()
        {
            if (_enemyHealth.isDead)
            {
                zombieState = ZombieState.Dead;
            }
        }

        private void EnableIdleAnimation()
        {
            _animator.SetBool("WalkToTarget", false);
            _animator.SetBool("WalkToPoint", false);
            _animator.SetBool("RunToTarget", false);
            _animator.SetBool("Attack", false);
        }

        private void EnableWalkToPoint()
        {
            _animator.SetBool("WalkToTarget", false);
            _animator.SetBool("WalkToPoint", true);
        }

        private void EnableDeathAnim()
        {
            _animator.SetBool("WalkToTarget", false);
            _animator.SetBool("WalkToPoint", false);
            _animator.SetBool("RunToTarget", false);
            _animator.SetBool("Attack", false);
            _animator.SetBool("Fall", true);
        }

        //Generate random point for walk to point state
        private void GenerateRandomPoint()
        {
            while (true)
            {
                Vector3 randomDirection = Random.insideUnitSphere * radiusForRandomPoint;
                randomDirection += transform.position;
                NavMeshHit hit;
                Vector3 finalPosition = Vector3.zero;
                if (NavMesh.SamplePosition(randomDirection, out hit, radiusForRandomPoint, 1))
                {
                    finalPosition = hit.position;
                }

                if (Vector3.Distance(transform.position, finalPosition) < minDistanceForCreateNewPoint)
                {
                    continue;
                }
                else
                {
                    _pointToMove = finalPosition;
                }

                break;
            }
        }

        // private void GenerateRandomPoint()
        // {
        //     Vector3 randomPoint = Random.insideUnitSphere * radiusForRandomPoint;
        //     randomPoint += transform.position;
        //     NavMeshHit hit;
        //     NavMesh.SamplePosition(randomPoint, out hit, radiusForRandomPoint, 1);
        //     _pointToMove = hit.position;
        // }
    }
}