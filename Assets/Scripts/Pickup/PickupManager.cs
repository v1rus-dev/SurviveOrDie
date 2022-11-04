using Player;
using UnityEngine;

namespace Pickup
{
    public class PickupManager : MonoBehaviour
    {
        private Inventory Inventory;
        
        private void Start()
        {
            Inventory = GetComponent<Inventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            PickupItem item = other.GetComponent<PickupItem>();
            if (item != null)
            {
                Inventory.AddPickupItem(item);
                Destroy(other.gameObject);
            }
        }
    }
}