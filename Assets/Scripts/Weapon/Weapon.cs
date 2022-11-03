using System;
using System.Collections;
using Enemy;
using Player;
using UnityEngine;

namespace Weapon
{
    [RequireComponent(typeof(AudioSource))]
    public class Weapon : MonoBehaviour
    {
        [Tooltip("The place from which the bullet is shot")] [SerializeField]
        private Transform ShootFrom;

        //Label
        [Header("Weapon Stats")] [SerializeField]
        private WeaponType weaponType;

        [SerializeField] private float damage;
        [SerializeField] private float maxDistance;
        [SerializeField] [Range(0, 2f)] private float fireRate;

        [Header("Weapon Utils")] [SerializeField]
        private ParticleSystem muzzleFlash;

        [SerializeField] private AudioClip shootSound;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private AmmoType ammoType;

        //TODO: Add a some impacts effects
        public ImpactInfo[] ImpactElemets = new ImpactInfo[0];

        private AudioSource audioSource;
        private PlayerHealth playerHealth;
        private Inventory inventory;
        private float nextFire;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = shootSound;

            playerHealth = GetComponentInParent<PlayerHealth>();
            inventory = GetComponentInParent<Inventory>();
        }

        // Update is called once per frame
        void Update()
        {
            switch (weaponType)
            {
                case WeaponType.Rifle:
                    HandleShootLikeAssaultRifle();
                    break;
                case WeaponType.Shotgun:
                    HandleShootLikePistolSniperShotgun();
                    break;
                case WeaponType.Sniper:
                    HandleShootLikePistolSniperShotgun();
                    break;
            }
        }

        void HandleShootLikeAssaultRifle()
        {
            if (Input.GetMouseButton(0) &&  playerHealth.IsLiving() && inventory.GetAmmoCount(ammoType) > 0 && Time.time >= nextFire)
            {
                //Some logic
                Shoot();
                nextFire = Time.time + fireRate;
            }
        }

        void HandleShootLikePistolSniperShotgun()
        {
            if (Input.GetMouseButtonDown(0) && playerHealth.IsLiving() && inventory.GetAmmoCount(ammoType) > 0 && Time.time >= nextFire)
            {
                //Some logic
                Shoot();
                nextFire = Time.time + fireRate;
            }
        }

        void Shoot()
        {
            PlayMuzzleFlash();
            PlayAudio();
            inventory.ReduceAmmo(ammoType);
            ProcessRaycast();
        }

        private void PlayMuzzleFlash()
        {
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }

        private void PlayAudio()
        {
            if (audioSource != null)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                audioSource.Play();
            }
        }

        private void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(ShootFrom.transform.position, ShootFrom.transform.forward, out hit, maxDistance))
            {
                CreateHitEffect(hit);
                // CreateImpactEffect(hit.transform.gameObject);
                EnemyHealth enemyHealth = hit.transform.GetComponentInParent<EnemyHealth>();

                if (enemyHealth != null)
                {
                    enemyHealth.GetDamage(damage);
                }
            }
            else
            {
                return;
            }
        }

        private void CreateHitEffect(RaycastHit hit)
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(effect, 0.5f);
            }
        }

        [System.Serializable]
        public class ImpactInfo
        {
            public MaterialTypes.MaterialTypeEnum MaterialType;
            public GameObject ImpactEffect;
        }

        GameObject GetImpactEffect(GameObject impactedGameObject)
        {
            var materialType = impactedGameObject.GetComponent<MaterialTypes>();
            if (materialType == null)
                return null;
            foreach (var impactInfo in ImpactElemets)
            {
                if (impactInfo.MaterialType == materialType.TypeOfMaterial)
                    return impactInfo.ImpactEffect;
            }

            return null;
        }
    }
}