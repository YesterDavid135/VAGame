using System;
using UnityEngine;
using Weapons;

namespace Weapons.Shotgun {
    public class Shotgun : MonoBehaviour, IWeapon {
        public GameObject bullet;
        public Transform firePoint;
        public float fireForce = 20;
        public int numberOfBullets = 7;
        public float spreadAngle = 10;
        public float fireRate = 0.5f;

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
            if (canFire) {
                for (int i = 0; i < numberOfBullets; i++) {
                    GameObject projectile = ObjectPooler.current.GetPooledObject();
                    if (projectile == null) continue;

                    // Calculate the spread angle for this bullet
                    float bulletAngle = spreadAngle * ((i + 0.5f) / numberOfBullets - 0.5f);

                    // Calculate the rotation based on the spread angle
                    Quaternion rotation = Quaternion.Euler(0, 0, bulletAngle) * firePoint.rotation;

                    // Set the layer, position, and rotation
                    projectile.layer = layer;
                    projectile.transform.position = firePoint.position;
                    projectile.transform.rotation = rotation;

                    // Activate the projectile and apply force
                    projectile.SetActive(true);
                    projectile.GetComponent<Rigidbody2D>()
                        .AddForce(rotation * Vector2.up * fireForce, ForceMode2D.Impulse);

                    // Set the cooldown and prevent further firing until it elapses
                    canFire = false;
                }
            }
        }

        public void Reload() {
            // Implement shotgun reloading logic here
        }

        public string GetWeaponName() {
            return "Shotgun";
        }
    }
}