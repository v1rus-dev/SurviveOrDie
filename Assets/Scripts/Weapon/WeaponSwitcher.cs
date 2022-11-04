using UnityEngine;

namespace Weapon
{
    public class WeaponSwitcher : MonoBehaviour
    {
        [SerializeField] private int currentWeapon = 0;

        // Start is called before the first frame update
        void Start()
        {
            SetWeaponActive();
        }

        // Update is called once per frame
        void Update()
        {
            HandleChangeWeapon();
            HandleChangeWeaponViaNumbers();
        }

        private void HandleChangeWeapon()
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (currentWeapon >= transform.childCount - 1)
                {
                    currentWeapon = 0;
                }
                else
                {
                    currentWeapon++;
                }
                SetWeaponActive();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (currentWeapon <= 0)
                {
                    currentWeapon = transform.childCount - 1;
                }
                else
                {
                    currentWeapon--;
                }
                SetWeaponActive();
            }
        }

        private void HandleChangeWeaponViaNumbers()
        {
            var countWeapon = transform.childCount;
            for (int i = 0; i < countWeapon; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    currentWeapon = i;
                    SetWeaponActive();
                }
            }
        }

        private void SetWeaponActive()
        {
            int weaponIndex = 0;
            foreach (Transform weapon in transform)
            {
                if (weaponIndex == currentWeapon)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }

                weaponIndex++;
            }
        }
    }
}