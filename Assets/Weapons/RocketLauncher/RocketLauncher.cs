using UnityEngine;
using Weapons;

namespace Weapons.RocketLauncher {
    public class RocketLauncher : MonoBehaviour, IWeapon {
        public Transform firePoint;
        public float fireForce = 5;

        public float fireRate = 4f;
        private bool canFire = true; // Add this variable to control the cooldown
        private float cooldownTimer; // Set the cooldown time

        // Update is called once per frame
        void Update() {
            // Check if the cooldown timer has elapsed
            if (!canFire) {
                cooldownTimer -= Time.deltaTime;
                Debug.Log(cooldownTimer);
                if (cooldownTimer <= 0) {
                    canFire = true;
                    cooldownTimer = fireRate; // Reset the timer
                }
            }
        }

        public void Fire(int layer) {
            if (!canFire) return;

            GameObject projectile = ObjectPooler.current.GetPooledObject();
            if (projectile == null) return;
            projectile.layer = layer;
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            Bullet bulletComponent = projectile.GetComponent<Bullet>();
            if (bulletComponent != null) {
                bulletComponent.damage = 5; // Set the desired damage value
                bulletComponent.isExplosive = true; // Set whether it's explosive or not
            }

            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            canFire = false;
        }

        public void Reload() {
            // Implement shotgun reloading logic here
        }

        public string GetWeaponName() {
            return "RocketLauncher";
        }
    }
}