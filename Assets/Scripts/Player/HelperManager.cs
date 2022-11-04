using UnityEngine;

namespace Player
{
    public class HelperManager : MonoBehaviour
    {
        [SerializeField] private Transform lookFrom;
        [SerializeField] private float maxDistance = 2f;

        private void Update()
        {
            if (Physics.Raycast(lookFrom.transform.position, lookFrom.transform.forward, out var hit, maxDistance))
            {
                HelpItem item = hit.collider.GetComponent<HelpItem>();
                if (item != null)
                {
                    Debug.Log("Help item found with text: " + item.HelpText);
                }
            }
        }
    }
}