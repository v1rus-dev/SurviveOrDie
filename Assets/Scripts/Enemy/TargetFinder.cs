using UnityEngine;

namespace Enemy
{
    public class TargetFinder : MonoBehaviour
    {
        [SerializeField] private Transform raycastFrom;
        [SerializeField] private int countRay = 5;
        [SerializeField] private float rayDistance = 10f;
        [SerializeField] private float angle = 60f;

        private Transform _target;
        public Transform Target => _target;

        private void Update()
        {
            if (_target == null)
            {
                FindTarget();
            }
        }

        private void FindTarget()
        {
            var angleStep = angle * 2 / countRay;
            var startDirection = -angle;
            var endDirection = angle;

            for (var i = startDirection; i < endDirection; i += angleStep)
            {
                var direction = Quaternion.AngleAxis(i, Vector3.up) * transform.forward;
                var ray = new Ray(raycastFrom.position, direction);
                Debug.DrawRay(raycastFrom.position, direction * rayDistance, Color.red, 0.1f);
                if (Physics.Raycast(ray, out var hit, rayDistance))
                {
                    Tags tags = hit.transform.GetComponent<Tags>();
                    if (tags == null) return;
                    if (tags.HasTagByName("Player"))
                    {
                        _target = hit.transform;
                        Debug.Log("Target found");
                        break;
                    }
                }
            }
        }

        public void SetTarget(Transform player)
        {
            _target = player;
        }

        public void ForgetTarget()
        {
            _target = null;
        }

        public float DistanceToTarget()
        {
            if (_target == null) return -1;
            return Vector3.Distance(transform.position, _target.position);
        }
    }
}