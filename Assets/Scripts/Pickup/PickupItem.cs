using UnityEngine;
using Weapon;

namespace Pickup
{
    public class PickupItem : MonoBehaviour
    {
        [SerializeField] private PickupType pickupType;
        [SerializeField] private AmmoType ammoType = AmmoType.None;
        [SerializeField] private int countElements = 0;
        
        public PickupType PickupType => pickupType;
        public AmmoType AmmoType => ammoType;
        public int CountElements => countElements;
    }
}