using System;
using System.Collections.Generic;
using System.Linq;
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