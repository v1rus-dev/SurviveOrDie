using System;
using System.Collections.Generic;
using System.Linq;
using Pickup;
using UnityEngine;
using Weapon;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Ammo> ammoTypes;
        
        private Dictionary<AmmoType, Ammo> _ammoDictionary;

        private void Start()
        {
            InitAmmoDictionary();
        }
        
        #region Ammo
        
        public void ReduceAmmo(AmmoType ammoType)
        {
            _ammoDictionary[ammoType].ReduceAmmo();
        }
        
        public int GetAmmoCount(AmmoType ammoType)
        {
            return _ammoDictionary[ammoType].AmmoAmount;
        }

        public void AddPickupItem(PickupItem pickupItem)
        {
            switch (pickupItem.PickupType)
            {
                case PickupType.Ammo:
                    AddAmmo(pickupItem.AmmoType, pickupItem.CountElements);
                    break;
                case PickupType.Health:
                    break;
                case PickupType.Equipment:
                    break;
                case PickupType.Weapon:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void AddAmmo(AmmoType ammoType, int ammoAmount)
        {
            _ammoDictionary[ammoType].IncreaseAmmo(ammoAmount);
        }
        
        private void InitAmmoDictionary()
        {
            _ammoDictionary = new Dictionary<AmmoType, Ammo>();
            var ammoEnumTypes = Enum.GetValues(typeof(AmmoType)).Cast<AmmoType>().ToList();
            foreach (var ammoEnumType in ammoEnumTypes)
            {
                _ammoDictionary.Add(ammoEnumType, new Ammo(ammoEnumType, 0));
            }
            foreach (var ammo in ammoTypes)
            {
                _ammoDictionary[ammo.AmmoType].IncreaseAmmo(ammo.AmmoAmount);
            }
        }
        
        #endregion
    }
}