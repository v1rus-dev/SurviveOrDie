using Enemy;
using UnityEngine;
using Weapon;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class Weapon : MonoBehaviour
    {

        [SerializeField] private Transform ShootFrom;
        [SerializeField] private float damage;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private GameObject hitEffect;
        [SerializeField] private AmmoType ammoType;
    
        //TODO: Add a some impacts effects
        public ImpactInfo[] ImpactElemets = new ImpactInfo[0];

        private AudioSource audioSource;
        private PlayerHealth playerHealth;
        private Inventory inventory;

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
            if (Input.GetButtonDown("Fire1") && playerHealth.IsLiving() && inventory.GetAmmoCount(ammoType) > 0)
            {
                Shoot();
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
            muzzleFlash.Play();
        }
    
        private void PlayAudio()
        {
            audioSource.Play();
        }

        private void ProcessRaycast()
        {
            RaycastHit hit;
            if (Physics.Raycast(ShootFrom.transform.position, ShootFrom.transform.forward, out hit))
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
            GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 0.5f);
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
            if (materialType==null)
                return null;
            foreach (var impactInfo in ImpactElemets)
            {
                if (impactInfo.MaterialType==materialType.TypeOfMaterial)
                    return impactInfo.ImpactEffect;
            }
            return null;
        }
    }
}
