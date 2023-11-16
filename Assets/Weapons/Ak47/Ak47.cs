using System;
using Unity.Mathematics;
using UnityEngine;
using Weapons;
using Random = Unity.Mathematics.Random;

namespace Weapons.Ak47 {
    public class Ak47 : MonoBehaviour, IWeapon {
        public Transform firePoint;
        public float fireForce;
        public float damage = 4;
        public float doubleShot;
        private Random rand = new Random(34);
        public float fireRate = 0.1f;
        private bool canFire = true; // Add this variable to control the cooldown
        private float cooldownTimer; // Set the cooldown time

        // Update is called once per frame
        void Update() {
            // Check if the cooldown timer has elapsed
            if (!canFire) {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0) {
                    canFire = true;
                    cooldownTimer = fireRate; // Reset the timer
                }
            }
        }

        public void Fire(int layer)
        {
            if (!canFire) return;

            GameObject projectile = ObjectPooler.current.GetPooledObject();
            if (projectile == null) return;
            projectile.layer = layer;
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;

            Bullet bulletComponent = projectile.GetComponent<Bullet>();
            if (bulletComponent != null) {
                bulletComponent.damage = (int)damage; // Set the desired damage value
                bulletComponent.isExplosive = false; // Set whether it's explosive or not
            }

            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);

            float random = rand.NextFloat(0, 100);
            if (random < doubleShot)
            {
                GameObject projectiles = ObjectPooler.current.GetPooledObject();
                if (projectile == null) return;
                projectile.layer = layer;
                projectile.transform.position = firePoint.position;
                projectile.transform.rotation = firePoint.rotation;

                Bullet bulletComponent2 = projectile.GetComponent<Bullet>();
                if (bulletComponent2 != null) {
                    bulletComponent2.damage = (int)damage; // Set the desired damage value
                    bulletComponent2.isExplosive = false; // Set whether it's explosive or not
                }

                projectiles.SetActive(true);
                projectiles.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);  
            }
            
            canFire = false;
        }

        public void Reload() {
            // Implement shotgun reloading logic here
        }

        public string GetWeaponName() {
            return "Ak47";
        }

        public void Upgrade(String attribute) {
            switch (attribute) {
                case "Speed":

                    fireRate *= 0.99f; //1%
                    break;
                case "Damage":

                    damage += 0.5f;

                    break;
                case "Doubleshot":
                    doubleShot += 2;
                    break;
            }
        }
    }
}