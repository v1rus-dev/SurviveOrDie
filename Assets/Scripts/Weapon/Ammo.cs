using System;
using UnityEngine;

namespace Weapon
{
    [Serializable]
    public class Ammo
    {
        [SerializeField]
        [Tooltip("The type of ammo this is")]
        private AmmoType ammoType;
        public AmmoType AmmoType => ammoType;
        [SerializeField]
        [Tooltip("Starting ammo")]
        private int ammoAmount;
        public int AmmoAmount => ammoAmount;
        
        //Constructor
        public Ammo(AmmoType ammoType, int ammoAmount)
        {
            this.ammoType = ammoType;
            this.ammoAmount = ammoAmount;
        }

        public void ReduceAmmo()
        {
            ammoAmount--;
        }
        
        public void IncreaseAmmo(int amount)
        {
            ammoAmount += amount;
        }
    }
}