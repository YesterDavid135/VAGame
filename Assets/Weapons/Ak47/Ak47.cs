using UnityEngine;
using Weapons;

namespace Weapons.Ak47 {
    public class Ak47 : MonoBehaviour, IWeapon {
        public GameObject bullet;

        public Transform firePoint;

        public float fireForce;
        // Start is called before the first frame update

        public void Fire(int layer) {
            GameObject projectile = ObjectPooler.current.GetPooledObject();
            if (projectile == null) return;
            projectile.layer = layer;
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);
            projectile.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        }

        public void Reload() {
            // Implement shotgun reloading logic here
        }

        public string GetWeaponName() {
            return "Ak47";
        }
    }
}